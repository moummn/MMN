Public Class mVolTune
    '音量调节相关API
    Private Declare Function SendMessage Lib "user32" Alias "SendMessageA" _
        (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, lParam As Integer) As Long
    Private Const APPCOMMAND_VOLUME_MUTE As Integer = &H80000
    Private Const APPCOMMAND_VOLUME_UP As Integer = &HA0000
    Private Const APPCOMMAND_VOLUME_DOWN As Integer = &H90000
    Private Const WM_APPCOMMAND As Integer = &H319
    '全局热键相关API
    Public Const WM_HOTKEY = &H312
    Public Const MOD_ALT = &H1
    Public Const MOD_CONTROL = &H2
    Public Const MOD_SHIFT = &H4
    Public Const GWL_WNDPROC = (-4)
    Public Declare Auto Function RegisterHotKey Lib "user32.dll" Alias "RegisterHotKey" _
        (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer,
         ByVal vk As Integer) As Boolean
    Public Declare Auto Function UnRegisterHotKey Lib "user32.dll" Alias "UnregisterHotKey" _
        (ByVal hwnd As IntPtr, ByVal id As Integer) As Boolean

    Private Sub mVolTune_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '注册全局热键
        RegisterHotKey(Handle, 0, MOD_CONTROL + MOD_ALT, Keys.Right) '第一个热键
        'RegisterHotKey(Handle, 1, Nothing, Keys.F4) '第二个热键
        RegisterHotKey(Handle, 1, MOD_CONTROL + MOD_ALT, Keys.Left) '第二个热键
    End Sub
    Private Sub mVolTune_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        '注销全局热键
        UnRegisterHotKey(Handle, 0)
        UnRegisterHotKey(Handle, 1)
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = WM_HOTKEY Then
            'MsgBox("在这里添加你要执行的代码", MsgBoxStyle.Information, "全局热键")
            Debug.Print(m.ToString)
            Select Case m.LParam
                Case &H270003
                    SendMessage(Me.Handle, WM_APPCOMMAND, Me.Handle, APPCOMMAND_VOLUME_UP)
                Case &H250003
                    SendMessage(Me.Handle, WM_APPCOMMAND, Me.Handle, APPCOMMAND_VOLUME_DOWN)
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
