Public Class frmmSetWindow

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If Me.Left - 300 > 0 Then
        '    Me.Left = Me.Left - 300
        'Else
        '    Me.Left = 0
        'End If
        'FormWindowListInit = False
        'frmSettings.Show()
        'frmSettings.sbFormRefresh()
        Call btnRefresh_Click(sender, e)

    End Sub

    'Private Sub frmWindowList_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
    '    frmSettings.sbFormRefresh()
    'End Sub

    'Private Sub frmWindowList_Move(sender As Object, e As EventArgs) Handles MyBase.Move
    '    frmSettings.sbFormRefresh()
    'End Sub
    ''' <summary>
    ''' 刷新按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim tItem As ListViewItem
        Dim DesktopHandle As Int32 = GetDesktopWindow()
        Dim ScanHandle As Int32 = GetWindow(DesktopHandle, GW_CHILD)
        Dim StartHandle As Int32 = StartHandle
        Dim strName As String

        ListView1.Items.Clear()

        Do While ScanHandle <> 0
            strName = Space(255)
            GetWindowText(ScanHandle, strName, Len(strName))
            If IsWindowVisible(ScanHandle) Then
                If Mid(strName, 1, 1) = Chr(0) Then
                ElseIf Mid(strName, 1, 15) = "Program Manager" Then
                ElseIf ScanHandle = Me.Handle Then
                Else
                    tItem = ListView1.Items.Add(strName)
                    'tItem = ListView1.ListItems.Add(ListView1.ListItems.Count + 1, "", strName, LoadPicture(), LoadPicture())
                    tItem.SubItems.Add(CStr(ScanHandle))
                End If
            End If
            ScanHandle = GetWindow(ScanHandle, GW_HWNDNEXT)
        Loop
        'sbAllDisable()

    End Sub
    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.SelectedItems.Count = 0 Then
            'sbAllDisable()
            Exit Sub
        Else
            'sbAllEnable()
        End If
        CheckBoxRefreshStatus = True
        CurrenthWnd = CInt(ListView1.SelectedItems(0).SubItems(1).Text)
        'frmSettings.sbWLRefresh(hWnd)
        lblHandle.Text = CStr(CurrenthWnd) & "    0x" & Hex(CurrenthWnd)
        Dim intWS As Integer = GetWindowLong(CurrenthWnd, GWL_STYLE)
        lblWS.Text = CStr(intWS) & "    0x" & Hex(intWS)
        Dim intWSEX As Integer = GetWindowLong(CurrenthWnd, GWL_EXSTYLE)
        lblWSEX.Text = CStr(intWSEX) & "    0x" & Hex(intWSEX)

        cbWS_BORDER.Checked = intWS And WS_BORDER
        cbWS_CAPTION.Checked = intWS And WS_CAPTION
        cbWS_CHILD.Checked = intWS And WS_CHILD
        cbWS_CHILDWINDOW.Checked = intWS And WS_CHILDWINDOW
        cbWS_CLIPCHILDREN.Checked = intWS And WS_CLIPCHILDREN
        cbWS_CLIPSIBLINGS.Checked = intWS And WS_CLIPSIBLINGS
        cbWS_DISABLED.Checked = intWS And WS_DISABLED
        cbWS_DLGFRAME.Checked = intWS And WS_DLGFRAME
        cbWS_GROUP.Checked = intWS And WS_GROUP
        cbWS_HSCROLL.Checked = intWS And WS_HSCROLL
        cbWS_ICONIC.Checked = intWS And WS_ICONIC
        cbWS_MAXIMIZE.Checked = intWS And WS_MAXIMIZE
        cbWS_MAXIMIZEBOX.Checked = intWS And WS_MAXIMIZEBOX
        cbWS_MINIMIZE.Checked = intWS And WS_MINIMIZE
        cbWS_MINIMIZEBOX.Checked = intWS And WS_MINIMIZEBOX
        cbWS_OVERLAPPED.Checked = intWS And WS_OVERLAPPED
        cbWS_OVERLAPPEDWINDOW.Checked = intWS And WS_OVERLAPPEDWINDOW
        cbWS_POPUP.Checked = intWS And WS_POPUP
        cbWS_POPUPWINDOW.Checked = intWS And WS_POPUPWINDOW
        cbWS_SIZEBOX.Checked = intWS And WS_SIZEBOX
        cbWS_SYSMENU.Checked = intWS And WS_SYSMENU
        cbWS_TABSTOP.Checked = intWS And WS_TABSTOP
        cbWS_THICKFRAME.Checked = intWS And WS_THICKFRAME
        cbWS_TILED.Checked = intWS And WS_TILED
        cbWS_TILEDWINDOW.Checked = intWS And WS_TILEDWINDOW
        cbWS_VISIBLE.Checked = intWS And WS_VISIBLE
        cbWS_VSCROLL.Checked = intWS And WS_VSCROLL

        cbWS_EX_ACCEPTFILES.Checked = intWSEX And WS_EX_ACCEPTFILES
        cbWS_EX_APPWINDOW.Checked = intWSEX And WS_EX_APPWINDOW
        cbWS_EX_CLIENTEDGE.Checked = intWSEX And WS_EX_CLIENTEDGE
        cbWS_EX_COMPOSITED.Checked = intWSEX And WS_EX_COMPOSITED
        cbWS_EX_CONTEXTHELP.Checked = intWSEX And WS_EX_CONTEXTHELP
        cbWS_EX_CONTROLPARENT.Checked = intWSEX And WS_EX_CONTROLPARENT
        cbWS_EX_DLGMODALFRAME.Checked = intWSEX And WS_EX_DLGMODALFRAME
        cbWS_EX_LAYERED.Checked = intWSEX And WS_EX_LAYERED
        cbWS_EX_LAYOUTRTL.Checked = intWSEX And WS_EX_LAYOUTRTL
        cbWS_EX_LEFT.Checked = intWSEX And WS_EX_LEFT
        cbWS_EX_LEFTSCROLLBAR.Checked = intWSEX And WS_EX_LEFTSCROLLBAR
        cbWS_EX_LTRREADING.Checked = intWSEX And WS_EX_LTRREADING
        cbWS_EX_MDICHILD.Checked = intWSEX And WS_EX_MDICHILD
        cbWS_EX_NOACTIVATE.Checked = intWSEX And WS_EX_NOACTIVATE
        cbWS_EX_NOINHERITLAYOUT.Checked = intWSEX And WS_EX_NOINHERITLAYOUT
        cbWS_EX_NOPARENTNOTIFY.Checked = intWSEX And WS_EX_NOPARENTNOTIFY
        cbWS_EX_NOREDIRECTIONBITMAP.Checked = intWSEX And WS_EX_NOREDIRECTIONBITMAP
        cbWS_EX_OVERLAPPEDWINDOW.Checked = intWSEX And WS_EX_OVERLAPPEDWINDOW
        cbWS_EX_PALETTEWINDOW.Checked = intWSEX And WS_EX_PALETTEWINDOW
        cbWS_EX_RIGHT.Checked = intWSEX And WS_EX_RIGHT
        cbWS_EX_RIGHTSCROLLBAR.Checked = intWSEX And WS_EX_RIGHTSCROLLBAR
        cbWS_EX_RTLREADING.Checked = intWSEX And WS_EX_RTLREADING
        cbWS_EX_STATICEDGE.Checked = intWSEX And WS_EX_STATICEDGE
        cbWS_EX_TOOLWINDOW.Checked = intWSEX And WS_EX_TOOLWINDOW
        cbWS_EX_TOPMOST.Checked = intWSEX And WS_EX_TOPMOST
        cbWS_EX_TRANSPARENT.Checked = intWSEX And WS_EX_TRANSPARENT
        cbWS_EX_WINDOWEDGE.Checked = intWSEX And WS_EX_WINDOWEDGE

        CheckBoxRefreshStatus = False
    End Sub

    Private Sub cbWS_CheckedChanged(sender As Object, e As EventArgs) Handles cbWS_BORDER.CheckedChanged,
                                                                            cbWS_CAPTION.CheckedChanged,
                                                                            cbWS_CHILD.CheckedChanged,
                                                                            cbWS_CHILDWINDOW.CheckedChanged,
                                                                            cbWS_CLIPCHILDREN.CheckedChanged,
                                                                            cbWS_CLIPSIBLINGS.CheckedChanged,
                                                                            cbWS_DISABLED.CheckedChanged,
                                                                            cbWS_DLGFRAME.CheckedChanged,
                                                                            cbWS_GROUP.CheckedChanged,
                                                                            cbWS_HSCROLL.CheckedChanged,
                                                                            cbWS_ICONIC.CheckedChanged,
                                                                            cbWS_MAXIMIZE.CheckedChanged,
                                                                            cbWS_MAXIMIZEBOX.CheckedChanged,
                                                                            cbWS_MINIMIZE.CheckedChanged,
                                                                            cbWS_MINIMIZEBOX.CheckedChanged,
                                                                            cbWS_OVERLAPPED.CheckedChanged,
                                                                            cbWS_OVERLAPPEDWINDOW.CheckedChanged,
                                                                            cbWS_POPUP.CheckedChanged,
                                                                            cbWS_POPUPWINDOW.CheckedChanged,
                                                                            cbWS_SIZEBOX.CheckedChanged,
                                                                            cbWS_SYSMENU.CheckedChanged,
                                                                            cbWS_TABSTOP.CheckedChanged,
                                                                            cbWS_THICKFRAME.CheckedChanged,
                                                                            cbWS_TILED.CheckedChanged,
                                                                            cbWS_TILEDWINDOW.CheckedChanged,
                                                                            cbWS_VISIBLE.CheckedChanged,
                                                                            cbWS_VSCROLL.CheckedChanged
        'Here Start
        If CheckBoxRefreshStatus = True Then Exit Sub
        Dim intWS As Integer = GetWindowLong(CurrenthWnd, GWL_STYLE)
        Select Case sender.name
            Case "cbWS_BORDER"
                If sender.Checked = True Then
                    intWS = intWS + WS_BORDER
                Else
                    intWS = intWS - WS_BORDER
                End If
                SetWindowLong(CurrenthWnd, GWL_STYLE, intWS)
            Case "cbWS_CAPTION"
                If sender.Checked = True Then
                    intWS = intWS + WS_CAPTION
                Else
                    intWS = intWS - WS_CAPTION
                End If
                SetWindowLong(CurrenthWnd, GWL_STYLE, intWS)
            Case "cbWS_CHILD"
                If sender.Checked = True Then
                    intWS = intWS + WS_CHILD
                Else
                    intWS = intWS - WS_CHILD
                End If
                SetWindowLong(CurrenthWnd, GWL_STYLE, intWS)
            Case "cbWS_CHILDWINDOW"
                If sender.Checked = True Then
                    intWS = intWS + WS_CHILDWINDOW
                Else
                    intWS = intWS - WS_CHILDWINDOW
                End If
                SetWindowLong(CurrenthWnd, GWL_STYLE, intWS)
            Case "cbWS_CLIPCHILDREN"
                If sender.Checked = True Then
                    intWS = intWS + WS_CLIPCHILDREN
                Else
                    intWS = intWS - WS_CLIPCHILDREN
                End If
                SetWindowLong(CurrenthWnd, GWL_STYLE, intWS)
            Case "cbWS_CLIPSIBLINGS"
                If sender.Checked = True Then
                    intWS = intWS + WS_CLIPSIBLINGS
                Else
                    intWS = intWS - WS_CLIPSIBLINGS
                End If
                SetWindowLong(CurrenthWnd, GWL_STYLE, intWS)
            Case "cbWS_DISABLED"
                If sender.Checked = True Then
                    intWS = intWS + WS_DISABLED
                Else
                    intWS = intWS - WS_DISABLED
                End If
                SetWindowLong(CurrenthWnd, GWL_STYLE, intWS)
            Case "cbWS_DLGFRAME"
                If sender.Checked = True Then
                    intWS = intWS + WS_DLGFRAME
                Else
                    intWS = intWS - WS_DLGFRAME
                End If
                SetWindowLong(CurrenthWnd, GWL_STYLE, intWS)
            Case "cbWS_GROUP"
                If sender.Checked = True Then
                    intWS = intWS + WS_GROUP
                Else
                    intWS = intWS - WS_GROUP
                End If
                SetWindowLong(CurrenthWnd, GWL_STYLE, intWS)
            Case "cbWS_HSCROLL"
                If sender.Checked = True Then
                    intWS = intWS + WS_HSCROLL
                Else
                    intWS = intWS - WS_HSCROLL
                End If
                SetWindowLong(CurrenthWnd, GWL_STYLE, intWS)
            Case "cbWS_ICONIC"
                If sender.Checked = True Then
                    intWS = intWS + WS_ICONIC
                Else
                    intWS = intWS - WS_ICONIC
                End If
                SetWindowLong(CurrenthWnd, GWL_STYLE, intWS)
            Case "cbWS_MAXIMIZE"
                If sender.Checked = True Then
                    intWS = intWS + WS_MAXIMIZE
                Else
                    intWS = intWS - WS_MAXIMIZE
                End If
                SetWindowLong(CurrenthWnd, GWL_STYLE, intWS)
            Case "cbWS_MAXIMIZEBOX"
                If sender.Checked = True Then
                    intWS = intWS + WS_MAXIMIZEBOX
                Else
                    intWS = intWS - WS_MAXIMIZEBOX
                End If
                SetWindowLong(CurrenthWnd, GWL_STYLE, intWS)
            Case "cbWS_MINIMIZE"
                If sender.Checked = True Then
                    intWS = intWS + WS_MINIMIZE
                Else
                    intWS = intWS - WS_MINIMIZE
                End If
                SetWindowLong(CurrenthWnd, GWL_STYLE, intWS)
            Case "cbWS_MINIMIZEBOX"
                If sender.Checked = True Then
                    intWS = intWS + WS_MINIMIZEBOX
                Else
                    intWS = intWS - WS_MINIMIZEBOX
                End If
                SetWindowLong(CurrenthWnd, GWL_STYLE, intWS)
            Case "cbWS_OVERLAPPED"
                If sender.Checked = True Then
                    intWS = intWS + WS_OVERLAPPED
                Else
                    intWS = intWS - WS_OVERLAPPED
                End If
                SetWindowLong(CurrenthWnd, GWL_STYLE, intWS)
            Case "cbWS_OVERLAPPEDWINDOW"
                If sender.Checked = True Then
                    intWS = intWS + WS_OVERLAPPEDWINDOW
                Else
                    intWS = intWS - WS_OVERLAPPEDWINDOW
                End If
                SetWindowLong(CurrenthWnd, GWL_STYLE, intWS)
            Case "cbWS_POPUP"
                If sender.Checked = True Then
                    intWS = intWS + WS_POPUP
                Else
                    intWS = intWS - WS_POPUP
                End If
                SetWindowLong(CurrenthWnd, GWL_STYLE, intWS)
            Case "cbWS_POPUPWINDOW"
                If sender.Checked = True Then
                    intWS = intWS + WS_POPUPWINDOW
                Else
                    intWS = intWS - WS_POPUPWINDOW
                End If
                SetWindowLong(CurrenthWnd, GWL_STYLE, intWS)
            Case "cbWS_SIZEBOX"
                If sender.Checked = True Then
                    intWS = intWS + WS_SIZEBOX
                Else
                    intWS = intWS - WS_SIZEBOX
                End If
                SetWindowLong(CurrenthWnd, GWL_STYLE, intWS)
            Case "cbWS_SYSMENU"
                If sender.Checked = True Then
                    intWS = intWS + WS_SYSMENU
                Else
                    intWS = intWS - WS_SYSMENU
                End If
                SetWindowLong(CurrenthWnd, GWL_STYLE, intWS)
            Case "cbWS_TABSTOP"
                If sender.Checked = True Then
                    intWS = intWS + WS_TABSTOP
                Else
                    intWS = intWS - WS_TABSTOP
                End If
                SetWindowLong(CurrenthWnd, GWL_STYLE, intWS)
            Case "cbWS_THICKFRAME"
                If sender.Checked = True Then
                    intWS = intWS + WS_THICKFRAME
                Else
                    intWS = intWS - WS_THICKFRAME
                End If
                SetWindowLong(CurrenthWnd, GWL_STYLE, intWS)
            Case "cbWS_TILED"
                If sender.Checked = True Then
                    intWS = intWS + WS_TILED
                Else
                    intWS = intWS - WS_TILED
                End If
                SetWindowLong(CurrenthWnd, GWL_STYLE, intWS)
            Case "cbWS_TILEDWINDOW"
                If sender.Checked = True Then
                    intWS = intWS + WS_TILEDWINDOW
                Else
                    intWS = intWS - WS_TILEDWINDOW
                End If
                SetWindowLong(CurrenthWnd, GWL_STYLE, intWS)
            Case "cbWS_VISIBLE"
                If sender.Checked = True Then
                    intWS = intWS + WS_VISIBLE
                Else
                    intWS = intWS - WS_VISIBLE
                End If
                SetWindowLong(CurrenthWnd, GWL_STYLE, intWS)
            Case "cbWS_VSCROLL"
                If sender.Checked = True Then
                    intWS = intWS + WS_VSCROLL
                Else
                    intWS = intWS - WS_VSCROLL
                End If
                SetWindowLong(CurrenthWnd, GWL_STYLE, intWS)
            Case Else
                MsgBox(sender.ToString)
        End Select
        ListView1_SelectedIndexChanged(sender, e)
    End Sub

    Private Sub cbWS_EX_CheckedChanged(sender As Object, e As EventArgs) Handles cbWS_EX_ACCEPTFILES.CheckedChanged,
                                                                                cbWS_EX_APPWINDOW.CheckedChanged,
                                                                                cbWS_EX_CLIENTEDGE.CheckedChanged,
                                                                                cbWS_EX_COMPOSITED.CheckedChanged,
                                                                                cbWS_EX_CONTEXTHELP.CheckedChanged,
                                                                                cbWS_EX_CONTROLPARENT.CheckedChanged,
                                                                                cbWS_EX_DLGMODALFRAME.CheckedChanged,
                                                                                cbWS_EX_LAYERED.CheckedChanged,
                                                                                cbWS_EX_LAYOUTRTL.CheckedChanged,
                                                                                cbWS_EX_LEFT.CheckedChanged,
                                                                                cbWS_EX_LEFTSCROLLBAR.CheckedChanged,
                                                                                cbWS_EX_LTRREADING.CheckedChanged,
                                                                                cbWS_EX_MDICHILD.CheckedChanged,
                                                                                cbWS_EX_NOACTIVATE.CheckedChanged,
                                                                                cbWS_EX_NOINHERITLAYOUT.CheckedChanged,
                                                                                cbWS_EX_NOPARENTNOTIFY.CheckedChanged,
                                                                                cbWS_EX_NOREDIRECTIONBITMAP.CheckedChanged,
                                                                                cbWS_EX_OVERLAPPEDWINDOW.CheckedChanged,
                                                                                cbWS_EX_PALETTEWINDOW.CheckedChanged,
                                                                                cbWS_EX_RIGHT.CheckedChanged,
                                                                                cbWS_EX_RIGHTSCROLLBAR.CheckedChanged,
                                                                                cbWS_EX_RTLREADING.CheckedChanged,
                                                                                cbWS_EX_STATICEDGE.CheckedChanged,
                                                                                cbWS_EX_TOOLWINDOW.CheckedChanged,
                                                                                cbWS_EX_TOPMOST.CheckedChanged,
                                                                                cbWS_EX_TRANSPARENT.CheckedChanged,
                                                                                cbWS_EX_WINDOWEDGE.CheckedChanged
        'HereStart
        If CheckBoxRefreshStatus = True Then Exit Sub
        Dim intWSEX As Integer = GetWindowLong(CurrenthWnd, GWL_EXSTYLE)
        Select Case sender.name
            Case "cbWS_EX_ACCEPTFILES"
                If sender.Checked = True Then
                    intWSEX = intWSEX + WS_EX_ACCEPTFILES
                Else
                    intWSEX = intWSEX - WS_EX_ACCEPTFILES
                End If
                SetWindowLong(CurrenthWnd, GWL_EXSTYLE, intWSEX)
            Case "cbWS_EX_APPWINDOW"
                If sender.Checked = True Then
                    intWSEX = intWSEX + WS_EX_APPWINDOW
                Else
                    intWSEX = intWSEX - WS_EX_APPWINDOW
                End If
                SetWindowLong(CurrenthWnd, GWL_EXSTYLE, intWSEX)
            Case "cbWS_EX_CLIENTEDGE"
                If sender.Checked = True Then
                    intWSEX = intWSEX + WS_EX_CLIENTEDGE
                Else
                    intWSEX = intWSEX - WS_EX_CLIENTEDGE
                End If
                SetWindowLong(CurrenthWnd, GWL_EXSTYLE, intWSEX)
            Case "cbWS_EX_COMPOSITED"
                If sender.Checked = True Then
                    intWSEX = intWSEX + WS_EX_COMPOSITED
                Else
                    intWSEX = intWSEX - WS_EX_COMPOSITED
                End If
                SetWindowLong(CurrenthWnd, GWL_EXSTYLE, intWSEX)
            Case "cbWS_EX_CONTEXTHELP"
                If sender.Checked = True Then
                    intWSEX = intWSEX + WS_EX_CONTEXTHELP
                Else
                    intWSEX = intWSEX - WS_EX_CONTEXTHELP
                End If
                SetWindowLong(CurrenthWnd, GWL_EXSTYLE, intWSEX)
            Case "cbWS_EX_CONTROLPARENT"
                If sender.Checked = True Then
                    intWSEX = intWSEX + WS_EX_CONTROLPARENT
                Else
                    intWSEX = intWSEX - WS_EX_CONTROLPARENT
                End If
                SetWindowLong(CurrenthWnd, GWL_EXSTYLE, intWSEX)
            Case "cbWS_EX_DLGMODALFRAME"
                If sender.Checked = True Then
                    intWSEX = intWSEX + WS_EX_DLGMODALFRAME
                Else
                    intWSEX = intWSEX - WS_EX_DLGMODALFRAME
                End If
                SetWindowLong(CurrenthWnd, GWL_EXSTYLE, intWSEX)
            Case "cbWS_EX_LAYERED"
                If sender.Checked = True Then
                    intWSEX = intWSEX + WS_EX_LAYERED
                Else
                    intWSEX = intWSEX - WS_EX_LAYERED
                End If
                SetWindowLong(CurrenthWnd, GWL_EXSTYLE, intWSEX)
            Case "cbWS_EX_LAYOUTRTL"
                If sender.Checked = True Then
                    intWSEX = intWSEX + WS_EX_LAYOUTRTL
                Else
                    intWSEX = intWSEX - WS_EX_LAYOUTRTL
                End If
                SetWindowLong(CurrenthWnd, GWL_EXSTYLE, intWSEX)
            Case "cbWS_EX_LEFT"
                If sender.Checked = True Then
                    intWSEX = intWSEX + WS_EX_LEFT
                Else
                    intWSEX = intWSEX - WS_EX_LEFT
                End If
                SetWindowLong(CurrenthWnd, GWL_EXSTYLE, intWSEX)
            Case "cbWS_EX_LEFTSCROLLBAR"
                If sender.Checked = True Then
                    intWSEX = intWSEX + WS_EX_LEFTSCROLLBAR
                Else
                    intWSEX = intWSEX - WS_EX_LEFTSCROLLBAR
                End If
                SetWindowLong(CurrenthWnd, GWL_EXSTYLE, intWSEX)
            Case "cbWS_EX_LTRREADING"
                If sender.Checked = True Then
                    intWSEX = intWSEX + WS_EX_LTRREADING
                Else
                    intWSEX = intWSEX - WS_EX_LTRREADING
                End If
                SetWindowLong(CurrenthWnd, GWL_EXSTYLE, intWSEX)
            Case "cbWS_EX_MDICHILD"
                If sender.Checked = True Then
                    intWSEX = intWSEX + WS_EX_MDICHILD
                Else
                    intWSEX = intWSEX - WS_EX_MDICHILD
                End If
                SetWindowLong(CurrenthWnd, GWL_EXSTYLE, intWSEX)
            Case "cbWS_EX_NOACTIVATE"
                If sender.Checked = True Then
                    intWSEX = intWSEX + WS_EX_NOACTIVATE
                Else
                    intWSEX = intWSEX - WS_EX_NOACTIVATE
                End If
                SetWindowLong(CurrenthWnd, GWL_EXSTYLE, intWSEX)
            Case "cbWS_EX_NOINHERITLAYOUT"
                If sender.Checked = True Then
                    intWSEX = intWSEX + WS_EX_NOINHERITLAYOUT
                Else
                    intWSEX = intWSEX - WS_EX_NOINHERITLAYOUT
                End If
                SetWindowLong(CurrenthWnd, GWL_EXSTYLE, intWSEX)
            Case "cbWS_EX_NOPARENTNOTIFY"
                If sender.Checked = True Then
                    intWSEX = intWSEX + WS_EX_NOPARENTNOTIFY
                Else
                    intWSEX = intWSEX - WS_EX_NOPARENTNOTIFY
                End If
                SetWindowLong(CurrenthWnd, GWL_EXSTYLE, intWSEX)
            Case "cbWS_EX_NOREDIRECTIONBITMAP"
                If sender.Checked = True Then
                    intWSEX = intWSEX + WS_EX_NOREDIRECTIONBITMAP
                Else
                    intWSEX = intWSEX - WS_EX_NOREDIRECTIONBITMAP
                End If
                SetWindowLong(CurrenthWnd, GWL_EXSTYLE, intWSEX)
            Case "cbWS_EX_OVERLAPPEDWINDOW"
                If sender.Checked = True Then
                    intWSEX = intWSEX + WS_EX_OVERLAPPEDWINDOW
                Else
                    intWSEX = intWSEX - WS_EX_OVERLAPPEDWINDOW
                End If
                SetWindowLong(CurrenthWnd, GWL_EXSTYLE, intWSEX)
            Case "cbWS_EX_PALETTEWINDOW"
                If sender.Checked = True Then
                    intWSEX = intWSEX + WS_EX_PALETTEWINDOW
                Else
                    intWSEX = intWSEX - WS_EX_PALETTEWINDOW
                End If
                SetWindowLong(CurrenthWnd, GWL_EXSTYLE, intWSEX)
            Case "cbWS_EX_RIGHT"
                If sender.Checked = True Then
                    intWSEX = intWSEX + WS_EX_RIGHT
                Else
                    intWSEX = intWSEX - WS_EX_RIGHT
                End If
                SetWindowLong(CurrenthWnd, GWL_EXSTYLE, intWSEX)
            Case "cbWS_EX_RIGHTSCROLLBAR"
                If sender.Checked = True Then
                    intWSEX = intWSEX + WS_EX_RIGHTSCROLLBAR
                Else
                    intWSEX = intWSEX - WS_EX_RIGHTSCROLLBAR
                End If
                SetWindowLong(CurrenthWnd, GWL_EXSTYLE, intWSEX)
            Case "cbWS_EX_RTLREADING"
                If sender.Checked = True Then
                    intWSEX = intWSEX + WS_EX_RTLREADING
                Else
                    intWSEX = intWSEX - WS_EX_RTLREADING
                End If
                SetWindowLong(CurrenthWnd, GWL_EXSTYLE, intWSEX)
            Case "cbWS_EX_STATICEDGE"
                If sender.Checked = True Then
                    intWSEX = intWSEX + WS_EX_STATICEDGE
                Else
                    intWSEX = intWSEX - WS_EX_STATICEDGE
                End If
                SetWindowLong(CurrenthWnd, GWL_EXSTYLE, intWSEX)
            Case "cbWS_EX_TOOLWINDOW"
                If sender.Checked = True Then
                    intWSEX = intWSEX + WS_EX_TOOLWINDOW
                Else
                    intWSEX = intWSEX - WS_EX_TOOLWINDOW
                End If
                SetWindowLong(CurrenthWnd, GWL_EXSTYLE, intWSEX)
            Case "cbWS_EX_TOPMOST"
                If sender.Checked = True Then
                    intWSEX = intWSEX + WS_EX_TOPMOST
                Else
                    intWSEX = intWSEX - WS_EX_TOPMOST
                End If
                SetWindowLong(CurrenthWnd, GWL_EXSTYLE, intWSEX)
            Case "cbWS_EX_TRANSPARENT"
                If sender.Checked = True Then
                    intWSEX = intWSEX + WS_EX_TRANSPARENT
                Else
                    intWSEX = intWSEX - WS_EX_TRANSPARENT
                End If
                SetWindowLong(CurrenthWnd, GWL_EXSTYLE, intWSEX)
            Case "cbWS_EX_WINDOWEDGE"
                If sender.Checked = True Then
                    intWSEX = intWSEX + WS_EX_WINDOWEDGE
                Else
                    intWSEX = intWSEX - WS_EX_WINDOWEDGE
                End If
                SetWindowLong(CurrenthWnd, GWL_EXSTYLE, intWSEX)
            Case Else
                MsgBox(sender.ToString)
        End Select
        ListView1_SelectedIndexChanged(sender, e)
    End Sub
    Private Sub sbAllDisable()
        cbWS_BORDER.Enabled = False
        cbWS_CAPTION.Enabled = False
        cbWS_CHILD.Enabled = False
        cbWS_CHILDWINDOW.Enabled = False
        cbWS_CLIPCHILDREN.Enabled = False
        cbWS_CLIPSIBLINGS.Enabled = False
        cbWS_DISABLED.Enabled = False
        cbWS_DLGFRAME.Enabled = False
        cbWS_GROUP.Enabled = False
        cbWS_HSCROLL.Enabled = False
        cbWS_ICONIC.Enabled = False
        cbWS_MAXIMIZE.Enabled = False
        cbWS_MAXIMIZEBOX.Enabled = False
        cbWS_MINIMIZE.Enabled = False
        cbWS_MINIMIZEBOX.Enabled = False
        'cbWS_OVERLAPPED.Enabled = False
        cbWS_OVERLAPPEDWINDOW.Enabled = False
        cbWS_POPUP.Enabled = False
        cbWS_POPUPWINDOW.Enabled = False
        cbWS_SIZEBOX.Enabled = False
        cbWS_SYSMENU.Enabled = False
        cbWS_TABSTOP.Enabled = False
        cbWS_THICKFRAME.Enabled = False
        cbWS_TILED.Enabled = False
        cbWS_TILEDWINDOW.Enabled = False
        cbWS_VISIBLE.Enabled = False
        cbWS_VSCROLL.Enabled = False

        cbWS_EX_ACCEPTFILES.Enabled = False
        cbWS_EX_APPWINDOW.Enabled = False
        cbWS_EX_CLIENTEDGE.Enabled = False
        cbWS_EX_COMPOSITED.Enabled = False
        cbWS_EX_CONTEXTHELP.Enabled = False
        cbWS_EX_CONTROLPARENT.Enabled = False
        cbWS_EX_DLGMODALFRAME.Enabled = False
        cbWS_EX_LAYERED.Enabled = False
        cbWS_EX_LAYOUTRTL.Enabled = False
        'cbWS_EX_LEFT.Enabled = False
        cbWS_EX_LEFTSCROLLBAR.Enabled = False
        'cbWS_EX_LTRREADING.Enabled = False
        cbWS_EX_MDICHILD.Enabled = False
        cbWS_EX_NOACTIVATE.Enabled = False
        cbWS_EX_NOINHERITLAYOUT.Enabled = False
        cbWS_EX_NOPARENTNOTIFY.Enabled = False
        cbWS_EX_NOREDIRECTIONBITMAP.Enabled = False
        cbWS_EX_OVERLAPPEDWINDOW.Enabled = False
        cbWS_EX_PALETTEWINDOW.Enabled = False
        cbWS_EX_RIGHT.Enabled = False
        'cbWS_EX_RIGHTSCROLLBAR.Enabled = False
        cbWS_EX_RTLREADING.Enabled = False
        cbWS_EX_STATICEDGE.Enabled = False
        cbWS_EX_TOOLWINDOW.Enabled = False
        cbWS_EX_TOPMOST.Enabled = False
        cbWS_EX_TRANSPARENT.Enabled = False
        cbWS_EX_WINDOWEDGE.Enabled = False

        lblHandle.Text = "0"
        lblWS.Text = "0"
        lblWSEX.Text = "0"
    End Sub
    Private Sub sbAllEnable()
        cbWS_BORDER.Enabled = True
        cbWS_CAPTION.Enabled = True
        cbWS_CHILD.Enabled = True
        cbWS_CHILDWINDOW.Enabled = True
        cbWS_CLIPCHILDREN.Enabled = True
        cbWS_CLIPSIBLINGS.Enabled = True
        cbWS_DISABLED.Enabled = True
        cbWS_DLGFRAME.Enabled = True
        cbWS_GROUP.Enabled = True
        cbWS_HSCROLL.Enabled = True
        cbWS_ICONIC.Enabled = True
        cbWS_MAXIMIZE.Enabled = True
        cbWS_MAXIMIZEBOX.Enabled = True
        cbWS_MINIMIZE.Enabled = True
        cbWS_MINIMIZEBOX.Enabled = True
        'cbWS_OVERLAPPED.Enabled =True
        cbWS_OVERLAPPEDWINDOW.Enabled = True
        cbWS_POPUP.Enabled = True
        cbWS_POPUPWINDOW.Enabled = True
        cbWS_SIZEBOX.Enabled = True
        cbWS_SYSMENU.Enabled = True
        cbWS_TABSTOP.Enabled = True
        cbWS_THICKFRAME.Enabled = True
        cbWS_TILED.Enabled = True
        cbWS_TILEDWINDOW.Enabled = True
        cbWS_VISIBLE.Enabled = True
        cbWS_VSCROLL.Enabled = True

        cbWS_EX_ACCEPTFILES.Enabled = True
        cbWS_EX_APPWINDOW.Enabled = True
        cbWS_EX_CLIENTEDGE.Enabled = True
        cbWS_EX_COMPOSITED.Enabled = True
        cbWS_EX_CONTEXTHELP.Enabled = True
        cbWS_EX_CONTROLPARENT.Enabled = True
        cbWS_EX_DLGMODALFRAME.Enabled = True
        cbWS_EX_LAYERED.Enabled = True
        cbWS_EX_LAYOUTRTL.Enabled = True
        'cbWS_EX_LEFT.Enabled =True
        cbWS_EX_LEFTSCROLLBAR.Enabled = True
        'cbWS_EX_LTRREADING.Enabled = True
        cbWS_EX_MDICHILD.Enabled = True
        cbWS_EX_NOACTIVATE.Enabled = True
        cbWS_EX_NOINHERITLAYOUT.Enabled = True
        cbWS_EX_NOPARENTNOTIFY.Enabled = True
        cbWS_EX_NOREDIRECTIONBITMAP.Enabled = True
        cbWS_EX_OVERLAPPEDWINDOW.Enabled = True
        cbWS_EX_PALETTEWINDOW.Enabled = True
        cbWS_EX_RIGHT.Enabled = True
        'cbWS_EX_RIGHTSCROLLBAR.Enabled = True
        cbWS_EX_RTLREADING.Enabled = True
        cbWS_EX_STATICEDGE.Enabled = True
        cbWS_EX_TOOLWINDOW.Enabled = True
        cbWS_EX_TOPMOST.Enabled = True
        cbWS_EX_TRANSPARENT.Enabled = True
        cbWS_EX_WINDOWEDGE.Enabled = True
    End Sub
End Class
