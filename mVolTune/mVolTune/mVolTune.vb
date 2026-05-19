Imports System.Management
Imports System.Runtime.InteropServices
Public Class mVolTune

    Private osdForm As New BrightnessOSD()
    '音量调节相关API
    Private Declare Function SendMessage Lib "user32" Alias "SendMessageA" _
        (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, lParam As Integer) As Long
    Private Const APPCOMMAND_VOLUME_MUTE As Integer = &H80000
    Private Const APPCOMMAND_VOLUME_UP As Integer = &HA0000
    Private Const APPCOMMAND_VOLUME_DOWN As Integer = &H90000
    Private Const WM_APPCOMMAND As Integer = &H319
    '全局热键相关API
    Private Const WM_HOTKEY = &H312
    Private Const MOD_ALT = &H1
    Private Const MOD_CONTROL = &H2
    Private Const MOD_SHIFT = &H4
    Private Const GWL_WNDPROC = (-4)
    Private Declare Auto Function RegisterHotKey Lib "user32.dll" Alias "RegisterHotKey" _
        (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer,
         ByVal vk As Integer) As Boolean
    Private Declare Auto Function UnRegisterHotKey Lib "user32.dll" Alias "UnregisterHotKey" _
        (ByVal hwnd As IntPtr, ByVal id As Integer) As Boolean

    ' GDI gamma 调整（软件亮度）
    Private Declare Function SetDeviceGammaRamp Lib "gdi32.dll" (ByVal hdc As IntPtr, ByVal lpRamp As IntPtr) As Boolean
    Private Declare Function GetDC Lib "user32.dll" (ByVal hwnd As IntPtr) As IntPtr
    Private Declare Function ReleaseDC Lib "user32.dll" (ByVal hwnd As IntPtr, ByVal hdc As IntPtr) As Integer

    Private softwareMode As Boolean = False
    Private softwareBrightness As Integer = -1

    '热键常量
    Private Const HOTKEY_ID_VOL_UP As Integer = 0
    Private Const HOTKEY_ID_VOL_DOWN As Integer = 1
    Private Const HOTKEY_ID_BRIGHTNESS_UP As Integer = 2
    Private Const HOTKEY_ID_BRIGHTNESS_DOWN As Integer = 3

    '亮度调节相关API
    Private Sub AdjustBrightness(delta As Integer)
        Try
            Dim brightnessClass As New ManagementClass("WmiMonitorBrightness")
            brightnessClass.Scope = New ManagementScope("root\wmi")
            Dim brightnessInstances = brightnessClass.GetInstances()

            Dim methodsClass As New ManagementClass("WmiMonitorBrightnessMethods")
            methodsClass.Scope = New ManagementScope("root\wmi")
            Dim methodsInstances = methodsClass.GetInstances()

            For Each instance As ManagementObject In brightnessInstances
                Dim currentBrightness As Integer = Convert.ToInt32(instance("CurrentBrightness"))
                Dim newBrightness As Integer = currentBrightness

                ' 如果 WMI 提供支持级别（Level 数组），则选择下一个/上一个支持的级别
                Dim supportedLevels As Integer() = Nothing
                If instance("Level") IsNot Nothing Then
                    Try
                        Dim levelObj As Object = instance("Level")
                        Dim tmpLevels As Integer() = Nothing

                        Dim levelArr As Array = TryCast(levelObj, Array)
                        If levelArr Is Nothing Then
                            ' 单个值的情况
                            ReDim tmpLevels(0)
                            tmpLevels(0) = Convert.ToInt32(levelObj)
                        Else
                            tmpLevels = New Integer(levelArr.Length - 1) {}
                            For i As Integer = 0 To levelArr.Length - 1
                                tmpLevels(i) = Convert.ToInt32(levelArr.GetValue(i))
                            Next
                        End If

                        Array.Sort(tmpLevels)
                        supportedLevels = tmpLevels
                    Catch
                        supportedLevels = Nothing
                    End Try
                End If

                If supportedLevels IsNot Nothing AndAlso supportedLevels.Length > 0 Then
                    ' 检查硬件是否支持 1% 步进
                    If HardwareSupportsOnePercent(supportedLevels) Then
                        ' 使用硬件级别变化（硬件支持 1%）
                        softwareMode = False
                        If delta > 0 Then
                            Dim foundUp As Boolean = False
                            For Each level As Integer In supportedLevels
                                If level > currentBrightness Then
                                    newBrightness = level
                                    foundUp = True
                                    Exit For
                                End If
                            Next
                            If Not foundUp Then
                                newBrightness = supportedLevels(supportedLevels.Length - 1)
                            End If
                        ElseIf delta < 0 Then
                            Dim foundDown As Boolean = False
                            For i As Integer = supportedLevels.Length - 1 To 0 Step -1
                                If supportedLevels(i) < currentBrightness Then
                                    newBrightness = supportedLevels(i)
                                    foundDown = True
                                    Exit For
                                End If
                            Next
                            If Not foundDown Then
                                newBrightness = supportedLevels(0)
                            End If
                        End If
                    Else
                        ' 硬件不支持 1% 步进，使用软件伽马来实现每次 1% 调节
                        If Not softwareMode Then
                            softwareMode = True
                            softwareBrightness = currentBrightness
                        End If
                        softwareBrightness = Math.Max(0, Math.Min(100, softwareBrightness + delta))
                        ApplySoftwareBrightness(softwareBrightness)
                        newBrightness = softwareBrightness
                    End If
                Else
                    newBrightness = Math.Max(0, Math.Min(100, currentBrightness + delta))
                End If

                ' 优先找匹配的 InstanceName 并调用对应方法
                Dim targetName As String = Nothing
                If instance("InstanceName") IsNot Nothing Then targetName = instance("InstanceName").ToString()
                Dim invoked As Boolean = False
                If Not String.IsNullOrEmpty(targetName) Then
                    For Each methodInstance As ManagementObject In methodsInstances
                        If methodInstance("InstanceName") IsNot Nothing AndAlso methodInstance("InstanceName").ToString() = targetName Then
                            methodInstance.InvokeMethod("WmiSetBrightness", New Object() {CUInt(1), CUInt(newBrightness)})
                            invoked = True
                            Exit For
                        End If
                    Next
                End If

                ' 如果没有找到匹配的 InstanceName，回退到对所有方法实例调用（兼容某些驱动）
                If Not invoked Then
                    For Each methodInstance As ManagementObject In methodsInstances
                        Try
                            methodInstance.InvokeMethod("WmiSetBrightness", New Object() {CUInt(1), CUInt(newBrightness)})
                        Catch
                            ' 忽略单个调用失败，继续尝试其他实例
                        End Try
                    Next
                End If

                ' 显示OSD
                If osdForm Is Nothing OrElse osdForm.IsDisposed Then
                    osdForm = New BrightnessOSD()
                End If
                osdForm.ShowBrightness(newBrightness)
            Next
        Catch ex As Exception
            Debug.Print("调节亮度失败: " & ex.Message)
        End Try
    End Sub
    ' 检测是否支持亮度调节
    Private brightnessSupported As Boolean = False
    Private Function IsBrightnessSupported() As Boolean
        Try
            Dim mclass As New ManagementClass("WmiMonitorBrightness")
            mclass.Scope = New ManagementScope("root\wmi")
            Dim instances = mclass.GetInstances()
            For Each instance As ManagementObject In instances
                Debug.Print("支持亮度调节")
                Return True ' 有实例即支持
            Next
        Catch ex As Exception
            Debug.Print("不支持亮度调节:")
            ' 忽略异常，返回不支持
        End Try
        Return False
    End Function
    ''调节显示器亮度API（测试，弃用）
    'Private Declare Auto Function SetMonitorBrightness Lib "dxva2.dll" Alias "SetMonitorBrightness" _
    '    (ByVal hMonitor As Integer, ByVal dwNewBrightness As UInteger)

    Private Sub mVolTune_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '注册全局热键
        RegisterHotKey(Handle, HOTKEY_ID_VOL_UP, MOD_CONTROL + MOD_ALT, Keys.Right)
        RegisterHotKey(Handle, HOTKEY_ID_VOL_DOWN, MOD_CONTROL + MOD_ALT, Keys.Left)

        ' 仅在支持时注册亮度热键
        brightnessSupported = IsBrightnessSupported()
        If brightnessSupported Then
            RegisterHotKey(Handle, HOTKEY_ID_BRIGHTNESS_UP, MOD_CONTROL + MOD_ALT, Keys.Up)
            RegisterHotKey(Handle, HOTKEY_ID_BRIGHTNESS_DOWN, MOD_CONTROL + MOD_ALT, Keys.Down)
        End If
    End Sub
    Private Sub mVolTune_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        '注销全局热键
        UnRegisterHotKey(Handle, HOTKEY_ID_VOL_UP)
        UnRegisterHotKey(Handle, HOTKEY_ID_VOL_DOWN)
        ' 注销亮度热键（只在支持时注销）
        If brightnessSupported Then
            UnRegisterHotKey(Handle, HOTKEY_ID_BRIGHTNESS_UP)
            UnRegisterHotKey(Handle, HOTKEY_ID_BRIGHTNESS_DOWN)
        End If
        If osdForm IsNot Nothing Then
            osdForm.Close()
            osdForm.Dispose()
        End If
    End Sub

    ' 判断支持级别数组是否包含所有 0..100 的连续步进（即支持 1% 步进）
    Private Function HardwareSupportsOnePercent(levels As Integer()) As Boolean
        If levels Is Nothing OrElse levels.Length = 0 Then Return False
        ' 如果 levels 包含 0 到 100 中的每个值则认为支持 1% 步进
        If levels.Length < 101 Then Return False
        For i As Integer = 0 To 100
            Dim found As Boolean = False
            For Each lv As Integer In levels
                If lv = i Then
                    found = True
                    Exit For
                End If
            Next
            If Not found Then Return False
        Next
        Return True
    End Function

    ' 使用 GDI SetDeviceGammaRamp 模拟亮度（软件亮度）
    Private Sub ApplySoftwareBrightness(brightnessPercent As Integer)
        If brightnessPercent < 0 Then brightnessPercent = 0
        If brightnessPercent > 100 Then brightnessPercent = 100

        ' 构造 gamma ramp：256 级，每项为 WORD（0..65535）
        ' 使用字节缓冲区以避免将超出 Int16 范围的值强制转换为 Short 导致溢出
        Dim channels As Integer = 3
        Dim entries As Integer = 256
        Dim sizeInBytes As Integer = entries * channels * 2 ' 每项 2 字节
        Dim buffer(sizeInBytes - 1) As Byte

        Dim scale As Double = brightnessPercent / 100.0
        For i As Integer = 0 To entries - 1
            Dim value As Integer = CInt(Math.Min(65535, Math.Max(0, (i * 256) * scale)))
            Dim v As UShort = CUShort(value)
            Dim b() As Byte = BitConverter.GetBytes(v)
            For ch As Integer = 0 To channels - 1
                Dim pos As Integer = ((ch * entries) + i) * 2
                buffer(pos) = b(0)
                buffer(pos + 1) = b(1)
            Next
        Next

        Dim pRamp As IntPtr = Marshal.AllocHGlobal(sizeInBytes)
        Try
            Marshal.Copy(buffer, 0, pRamp, sizeInBytes)
            Dim hdc As IntPtr = GetDC(IntPtr.Zero)
            If hdc <> IntPtr.Zero Then
                SetDeviceGammaRamp(hdc, pRamp)
                ReleaseDC(IntPtr.Zero, hdc)
            End If
        Finally
            Marshal.FreeHGlobal(pRamp)
        End Try
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = WM_HOTKEY Then
            'MsgBox("在这里添加你要执行的代码", MsgBoxStyle.Information, "全局热键")
            Debug.Print(m.ToString)
            'Select Case m.LParam
            '    Case &H270003
            '        SendMessage(Me.Handle, WM_APPCOMMAND, Me.Handle, APPCOMMAND_VOLUME_UP)
            '    Case &H250003
            '        SendMessage(Me.Handle, WM_APPCOMMAND, Me.Handle, APPCOMMAND_VOLUME_DOWN)
            '    Case &H260003
            '        Dim TS As New Management.Instrumentation.ManagementKeyAttribute
            '        Debug.Print(TS.ToString)
            'End Select
            Select Case m.WParam.ToInt32()
                Case HOTKEY_ID_VOL_UP
                    SendMessage(Me.Handle, WM_APPCOMMAND, Me.Handle, APPCOMMAND_VOLUME_UP)
                Case HOTKEY_ID_VOL_DOWN
                    SendMessage(Me.Handle, WM_APPCOMMAND, Me.Handle, APPCOMMAND_VOLUME_DOWN)
                Case HOTKEY_ID_BRIGHTNESS_UP
                    AdjustBrightness(1)
                Case HOTKEY_ID_BRIGHTNESS_DOWN
                    AdjustBrightness(-1)
            End Select
        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub mVolTune_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Dim S As String = vbNullString
        For I As Integer = 0 To My.Application.CommandLineArgs.Count - 1
            S = My.Application.CommandLineArgs.Item(I)
            S = UCase(S)
            Debug.Print("启动参数:" & S)
            If S = "/S" Then
                Debug.Print("隐藏主窗口")
                Me.Visible = False
            End If
        Next
    End Sub

    Private Sub btnBackRun_Click(sender As Object, e As EventArgs) Handles btnBackRun.Click
        Debug.Print("隐藏主窗口")
        Me.Visible = False
    End Sub
End Class
