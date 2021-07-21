Module mdMain
    Public FormWindowListInit As Boolean = True

    '获得窗体句柄
    Public Declare Function GetDesktopWindow Lib "user32.dll" () As Int32
    Public Declare Function GetWindow Lib "user32.dll" (ByVal hwnd As Int32, ByVal wCmd As Int32) As Int32
    Public Declare Function GetWindowText Lib "user32.dll" Alias "GetWindowTextA" (ByVal hwnd As Int32, ByVal lpString As String, ByVal cch As Int32) As Int32
    Public Declare Function IsWindowVisible Lib "user32" (ByVal hwnd As Int32) As Int32

    Public Const GW_CHILD = 5
    Public Const GW_HWNDNEXT = 2
End Module
