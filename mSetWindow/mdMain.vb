Module mdMain
    'Public FormWindowListInit As Boolean = True
    Public CurrenthWnd As Int32 = 0
    Public CheckBoxRefreshStatus As Boolean = False
    '获得窗体句柄
    Public Declare Function GetDesktopWindow Lib "user32.dll" () As Int32
    Public Declare Function GetWindow Lib "user32.dll" (ByVal hwnd As Int32, ByVal wCmd As Int32) As Int32
    Public Declare Function GetWindowText Lib "user32.dll" Alias "GetWindowTextA" (ByVal hwnd As Int32, ByVal lpString As String, ByVal cch As Int32) As Int32
    Public Declare Function IsWindowVisible Lib "user32" (ByVal hwnd As Int32) As Int32

    Public Const GW_CHILD = 5
    Public Const GW_HWNDNEXT = 2

    '窗体属性
    Public Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As Int32, ByVal nIndex As Int32, ByVal dwNewLong As Int32) As Int32
    Public Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal hwnd As Int32, ByVal nIndex As Int32) As Int32

    Public Const GWL_EXSTYLE = (-20)
    Public Const GWL_HINSTANCE = (-6)
    Public Const GWL_HWNDPARENT = (-8)
    Public Const GWL_ID = (-12)
    Public Const GWL_STYLE = (-16)
    Public Const GWL_USERDATA = (-21)
    Public Const GWL_WNDPROC = (-4)

    Public Const WS_BORDER = &H800000
    Public Const WS_CAPTION = &HC00000                  '  WS_BORDER Or WS_DLGFRAME
    Public Const WS_CHILD = &H40000000
    Public Const WS_CHILDWINDOW = (WS_CHILD)
    Public Const WS_CLIPCHILDREN = &H2000000
    Public Const WS_CLIPSIBLINGS = &H4000000
    Public Const WS_DISABLED = &H8000000
    Public Const WS_DLGFRAME = &H400000
    Public Const WS_GROUP = &H20000
    Public Const WS_HSCROLL = &H100000
    Public Const WS_ICONIC = WS_MINIMIZE
    Public Const WS_MAXIMIZE = &H1000000
    Public Const WS_MAXIMIZEBOX = &H10000
    Public Const WS_MINIMIZE = &H20000000
    Public Const WS_MINIMIZEBOX = &H20000
    Public Const WS_OVERLAPPED = &H0&
    Public Const WS_OVERLAPPEDWINDOW = (WS_OVERLAPPED Or WS_CAPTION Or WS_SYSMENU Or WS_THICKFRAME Or WS_MINIMIZEBOX Or WS_MAXIMIZEBOX)
    Public Const WS_POPUP = &H80000000
    Public Const WS_POPUPWINDOW = (WS_POPUP Or WS_BORDER Or WS_SYSMENU)
    Public Const WS_SIZEBOX = WS_THICKFRAME
    Public Const WS_SYSMENU = &H80000
    Public Const WS_TABSTOP = &H10000
    Public Const WS_THICKFRAME = &H40000
    Public Const WS_TILED = WS_OVERLAPPED
    Public Const WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW
    Public Const WS_VISIBLE = &H10000000
    Public Const WS_VSCROLL = &H200000

    Public Const WS_EX_ACCEPTFILES = &H10&
    Public Const WS_EX_APPWINDOW = &H40000&
    Public Const WS_EX_CLIENTEDGE = &H200&
    Public Const WS_EX_COMPOSITED = &H2000000&
    Public Const WS_EX_CONTEXTHELP = &H400&
    Public Const WS_EX_CONTROLPARENT = &H10000&
    Public Const WS_EX_DLGMODALFRAME = &H1&
    Public Const WS_EX_LAYERED = &H80000&
    Public Const WS_EX_LAYOUTRTL = &H400000&
    Public Const WS_EX_LEFT = &H0&
    Public Const WS_EX_LEFTSCROLLBAR = &H4000&
    Public Const WS_EX_LTRREADING = &H0&
    Public Const WS_EX_MDICHILD = &H40&
    Public Const WS_EX_NOACTIVATE = &H8000000&
    Public Const WS_EX_NOINHERITLAYOUT = &H100000&
    Public Const WS_EX_NOPARENTNOTIFY = &H4&
    Public Const WS_EX_NOREDIRECTIONBITMAP = &H200000&
    Public Const WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE Or WS_EX_CLIENTEDGE)
    Public Const WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE Or WS_EX_TOOLWINDOW Or WS_EX_TOPMOST)
    Public Const WS_EX_RIGHT = &H1000&
    Public Const WS_EX_RIGHTSCROLLBAR = &H0&
    Public Const WS_EX_RTLREADING = &H2000&
    Public Const WS_EX_STATICEDGE = &H20000&
    Public Const WS_EX_TOOLWINDOW = &H80&
    Public Const WS_EX_TOPMOST = &H8&
    Public Const WS_EX_TRANSPARENT = &H20&
    Public Const WS_EX_WINDOWEDGE = &H100&
End Module
