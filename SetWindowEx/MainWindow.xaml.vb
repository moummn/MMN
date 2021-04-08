Class MainWindow

    Private Declare Function GetDesktopWindow Lib "user32" Alias "GetDesktopWindow" () As Integer

    'Private Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
    'Private Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal hWnd1 As Integer, ByVal hWnd2 As Integer, ByVal lpsz1 As String, ByVal lpsz2 As String) As Integer
    Private Declare Function GetWindowText Lib "user32" Alias "GetWindowTextA" (ByVal hwnd As Integer, ByVal lpString As String, ByVal cch As Integer) As Integer
    Private Declare Function GetWindow1 Lib "user32" Alias "GetWindow" (ByVal hwnd As Integer, ByVal wCmd As Integer) As Integer

    Private Declare Function IsWindow Lib "user32" Alias "IsWindow" (ByVal hwnd As Integer) As Integer
    Private Declare Function IsWindowVisible Lib "user32" Alias "IsWindowVisible" (ByVal hwnd As Integer) As Integer

    Private Declare Function GetNextWindow Lib "user32" Alias "GetWindow" (ByVal hwnd As Integer, ByVal wFlag As Integer) As Integer
    Private Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As Integer, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer
    Private Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal hwnd As Integer, ByVal nIndex As Integer) As Integer

    Private Const GW_CHILD = 5
    Private Const GW_HWNDFIRST = 0
    Private Const GW_HWNDLAST = 1
    Private Const GW_HWNDNEXT = 2
    Private Const GW_HWNDPREV = 3
    Private Const GW_MAX = 5
    Private Const GW_OWNER = 4

    Private Const GWL_EXSTYLE = (-20)
    Private Const GWL_HINSTANCE = (-6)
    Private Const GWL_HWNDPARENT = (-8)
    Private Const GWL_ID = (-12)
    Private Const GWL_STYLE = (-16)
    Private Const GWL_USERDATA = (-21)
    Private Const GWL_WNDPROC = (-4)

    Private Const SW_HIDE = 0

    Private Const WS_EX_APPWINDOW = &H40000
    Private Const WS_EX_TOOLWINDOW = &H80


    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        listView1.Items.Clear()
        Dim dt As New Data.DataTable
        dt.Clear()
        dt.Columns.Add("HWND")
        dt.Columns.Add("Name")
        Dim DesktopHwmd As Integer = GetDesktopWindow

        'dt.Rows.Add(CStr(DesktopHwmd), "Desktop")
        Dim hwnd As Integer = GetWindow1(DesktopHwmd, GW_CHILD)
        Dim strName As String
        Dim DisplayName As String
        Do
            hwnd = GetNextWindow(hwnd, GW_HWNDNEXT)
            If hwnd = 0 Then Exit Do
            If IsWindowVisible(hwnd) Then
                strName = Space(255)
                GetWindowText(hwnd, strName, 255)
                DisplayName = Trim(strName)
                If DisplayName <> vbNullChar Then
                    'Debug.Print(Len(DisplayName))
                    dt.Rows.Add(CStr(hwnd), DisplayName)
                End If
            End If
        Loop
        listView1.DataContext = dt
        listView1.SetBinding(ListView.ItemsSourceProperty, New Binding())
    End Sub

    Private Sub button_Click(sender As Object, e As RoutedEventArgs) Handles button.Click
        If listView1.SelectedIndex = -1 Then Exit Sub
        Dim dt As New Data.DataTable
        dt = listView1.DataContext
        Dim hwnd As Integer = CInt(dt.Rows(listView1.SelectedIndex).Item(0))
        SetWindowLong(hwnd, GWL_EXSTYLE, GetWindowLong(hwnd, GWL_EXSTYLE) Or WS_EX_TOOLWINDOW And Not WS_EX_APPWINDOW)
    End Sub
End Class
