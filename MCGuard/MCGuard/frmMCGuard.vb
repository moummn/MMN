
Public Class frmMCGuard


    Private Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
    Private Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hwndParent As Integer, ByVal hwndChildAfter As Integer, ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
    Private Declare Function GetDesktopWindow Lib "user32" Alias "GetDesktopWindow" () As Integer
    Public Declare Function GetWindow Lib "user32" (ByVal hwnd As Integer, ByVal wCmd As Integer) As Integer
    Private Declare Function GetNextWindow Lib "user32" Alias "GetWindow" (ByVal hwnd As Integer, ByVal wFlag As Integer) As Integer
    Private Declare Function GetWindowText Lib "user32" Alias "GetWindowTextA" (ByVal hwnd As Integer, ByVal lpString As String, ByVal cch As Long) As Long
    Private Declare Function GetWindowTextLength Lib "user32" Alias "GetWindowTextLengthA" (ByVal hwnd As IntPtr) As Integer

    Private Declare Function GetParent Lib "user32" Alias "GetParent" (ByVal hwnd As Long) As Integer
    Private Declare Function PostMessage Lib "user32" Alias "PostMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    Private Declare Function MapVirtualKey Lib "user32" Alias "MapVirtualKeyA" (ByVal wCode As Integer, ByVal wMapType As Integer) As Integer
    Private Const WM_KEYDOWN = &H100
    Private Const WM_KEYUP = &H101
    Private Const WM_CHAR = &H102
    Private Const VK_SPACE = &H20
    Private Const GW_HWNDFIRST = 0
    Private Const GW_HWNDLAST = 1
    Private Const GW_HWNDNEXT = 2
    Private Const GW_HWNDPREV = 3
    Private Const GW_OWNER = 4
    Private Const GW_CHILD = 5

    Private Function fnMakeKeyLparam(ByVal VirtualKey As Integer, ByVal Flag As Integer) As Integer
        Dim s As String
        Dim Firstbyte As String     'lparam参数的24-31位
        If Flag = WM_KEYDOWN Then   '如果是按下键
            Firstbyte = "00"
        Else
            Firstbyte = "C0"        '如果是释放键
        End If
        Dim Scancode As Long
        '获得键的扫描码
        Scancode = MapVirtualKey(VirtualKey, 0)
        Dim Secondbyte As String    'lparam参数的16-23位，即虚拟键扫描码
        Secondbyte = Microsoft.VisualBasic.Right("00" & Hex(Scancode), 2)
        s = Firstbyte & Secondbyte & "0001"   '0001为lparam参数的0-15位，即发送次数和其它扩展信息
        fnMakeKeyLparam = Val("&H" & s)
    End Function
    Private Function fnKeyTypeMessage(ByVal hWnd As Integer, ByVal KeyCode As Integer) As Integer
        Dim I As Integer

        fnKeyTypeMessage = 0

        'I = PostMessage(hWnd, WM_KEYDOWN, KeyCode, fnMakeKeyLparam(KeyCode, WM_KEYDOWN))
        'fnKeyPressMessage = I
        'Debug.Print("WM_KEYDOWN," & Hex(KeyCode) & ":" & I)

        I = PostMessage(hWnd, WM_CHAR, KeyCode, fnMakeKeyLparam(KeyCode, WM_KEYDOWN))
        fnKeyTypeMessage += I
        Debug.Print("WM_CHAR," & Hex(KeyCode) & ":" & I)

        'I = PostMessage(hWnd, WM_KEYUP, KeyCode, fnMakeKeyLparam(KeyCode, WM_KEYUP))
        'fnKeyPressMessage += I
        'Debug.Print("WM_KEYUP," & Hex(KeyCode) & ":" & I)
    End Function
    Private Function fnKeyPressMessage(ByVal hWnd As Integer, ByVal KeyCode As Integer) As Integer
        Dim I As Integer

        fnKeyPressMessage = 0

        I = PostMessage(hWnd, WM_KEYDOWN, KeyCode, fnMakeKeyLparam(KeyCode, WM_KEYDOWN))
        fnKeyPressMessage = I
        Debug.Print("WM_KEYDOWN," & Hex(KeyCode) & ":" & I)

        I = PostMessage(hWnd, WM_CHAR, KeyCode, fnMakeKeyLparam(KeyCode, WM_KEYDOWN))
        fnKeyPressMessage += I
        Debug.Print("WM_CHAR," & Hex(KeyCode) & ":" & I)

        I = PostMessage(hWnd, WM_KEYUP, KeyCode, fnMakeKeyLparam(KeyCode, WM_KEYUP))
        fnKeyPressMessage += I
        Debug.Print("WM_KEYUP," & Hex(KeyCode) & ":" & I)
    End Function
    Private Function fnFindWindow(ByVal hwndParent As Integer, ByVal lpWindowName As String) As Integer
        Dim I As Integer = hwndParent
        Dim S As String
        I = GetWindow(I, GW_CHILD)
        Do Until I = 0
            I = GetNextWindow(I, GW_HWNDNEXT)
            S = Space(GetWindowTextLength(I) + 1)
            GetWindowText(I, S, S.Length)
            If Mid(S, 1, lpWindowName.Length) = lpWindowName Then Exit Do
            'Do Until S.Length = 0 OrElse Asc(Mid(S, S.Length, 1)) <> 0
            '    S = Mid(S, 1, S.Length - 1)
            'Loop

            'Debug.Print(I.ToString & "-" & Trim(S))

        Loop
        Return I
    End Function
    Private Sub sbParameter()
        Dim myParameters() As String = System.Environment.GetCommandLineArgs
        For Each myPara As String In myParameters
            If myPara = "/min" Then tmrMiniStart.Enabled = True

        Next
    End Sub
    Private Sub frmMCGuard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Left = My.Computer.Screen.Bounds.Width - Me.Width
        Me.Top = My.Computer.Screen.Bounds.Height - Me.Height - 45
        NotifyIcon1.Icon = Me.Icon
        Call sbParameter()
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Dim S As String
        Dim N() As String
        Dim mHwnd As Integer = fnFindWindow(GetDesktopWindow, "将在")
        Dim I As Integer

        'I = FindWindow("CalcFrame", "计算")
        'I = FindWindow("Static", "做一个简单算术题吧")

        'I = FindWindowEx(I, 0, "", "将在")
        If mHwnd <> 0 Then
            S = Space(GetWindowTextLength(mHwnd) + 1)
            GetWindowText(mHwnd, S, S.Length)
            If InStr(S, "分钟后自动继续") = 0 Then Exit Sub
            I = fnFindWindow(mHwnd, "做一个简单算术题吧")
            S = Space(GetWindowTextLength(I) + 1)
            GetWindowText(I, S, S.Length)
            N = Split(S, " ")
            N(0) = Mid(N(0), 11, 2)
            Debug.Print(N(0) & N(2))
            Dim Ans As Integer
            If N(1) = "+" Then Ans = Val(N(0)) + Val(N(2))
            If N(1) = "-" Then Ans = Val(N(0)) - Val(N(2))
            I = GetNextWindow(I, GW_HWNDNEXT)
            For B As Integer = 1 To Ans.ToString.Length
                fnKeyTypeMessage(I, Asc(Mid(Ans.ToString, B, 1)))
                Threading.Thread.Sleep(50)
                'Application.DoEvents()
            Next
            I = fnFindWindow(mHwnd, "继续")
            fnKeyPressMessage(I, 32)
        End If
        'End
    End Sub

    Private Sub frmMCGuard_SizeChanged(sender As System.Object, e As System.EventArgs) Handles MyBase.SizeChanged
        If Me.WindowState = FormWindowState.Minimized Then Me.Hide()
    End Sub

    Private Sub NotifyIcon1_MouseClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseClick
        If e.Button = MouseButtons.Left Then
            Me.Show()
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub tsmi还原_Click(sender As System.Object, e As System.EventArgs) Handles tsmi还原.Click
        Me.Show()
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub tsmi退出_Click(sender As System.Object, e As System.EventArgs) Handles tsmi退出.Click
        Me.Close()
    End Sub

    Private Sub tmrMiniStart_Tick(sender As System.Object, e As System.EventArgs) Handles tmrMiniStart.Tick
        tmrMiniStart.Enabled = False
        Me.WindowState = FormWindowState.Minimized
        Me.Hide()
    End Sub
End Class
