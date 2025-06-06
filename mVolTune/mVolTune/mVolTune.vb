'Imports System.ComponentModel.Design.Serialization
Imports System.Management
Public Class mVolTune
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

    '热键常量
    Private Const HOTKEY_ID_VOL_UP As Integer = 0
    Private Const HOTKEY_ID_VOL_DOWN As Integer = 1
    Private Const HOTKEY_ID_BRIGHTNESS_UP As Integer = 2
    Private Const HOTKEY_ID_BRIGHTNESS_DOWN As Integer = 3

    '亮度调节相关API
    Private Sub AdjustBrightness(delta As Integer)
        Try
            Dim mclass As New ManagementClass("WmiMonitorBrightness")
            mclass.Scope = New ManagementScope("root\wmi")
            Dim instances = mclass.GetInstances()
            For Each instance As ManagementObject In instances
                Dim currentBrightness As Byte = CByte(instance("CurrentBrightness"))
                Dim newBrightness As Integer = Math.Max(0, Math.Min(100, currentBrightness + delta))
                Dim mclassMethods As New ManagementClass("WmiMonitorBrightnessMethods")
                mclassMethods.Scope = New ManagementScope("root\wmi")
                For Each methodInstance As ManagementObject In mclassMethods.GetInstances()
                    methodInstance.InvokeMethod("WmiSetBrightness", New Object() {1, newBrightness})
                Next
            Next
        Catch ex As Exception
            Debug.Print("调节亮度失败: " & ex.Message)
        End Try
    End Sub

    ''调节显示器亮度API（测试）
    'Private Declare Auto Function SetMonitorBrightness Lib "dxva2.dll" Alias "SetMonitorBrightness" _
    '    (ByVal hMonitor As Integer, ByVal dwNewBrightness As UInteger)

    Private Sub mVolTune_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '注册全局热键
        RegisterHotKey(Handle, HOTKEY_ID_VOL_UP, MOD_CONTROL + MOD_ALT, Keys.Right)
        RegisterHotKey(Handle, HOTKEY_ID_VOL_DOWN, MOD_CONTROL + MOD_ALT, Keys.Left)
        RegisterHotKey(Handle, HOTKEY_ID_BRIGHTNESS_UP, MOD_CONTROL + MOD_ALT, Keys.Up)
        RegisterHotKey(Handle, HOTKEY_ID_BRIGHTNESS_DOWN, MOD_CONTROL + MOD_ALT, Keys.Down)
    End Sub
    Private Sub mVolTune_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        '注销全局热键
        UnRegisterHotKey(Handle, HOTKEY_ID_VOL_UP)
        UnRegisterHotKey(Handle, HOTKEY_ID_VOL_DOWN)
        UnRegisterHotKey(Handle, HOTKEY_ID_BRIGHTNESS_UP)
        UnRegisterHotKey(Handle, HOTKEY_ID_BRIGHTNESS_DOWN)
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
                    AdjustBrightness(10)
                Case HOTKEY_ID_BRIGHTNESS_DOWN
                    AdjustBrightness(-10)
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
End Class
