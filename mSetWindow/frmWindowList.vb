Public Class frmWindowList

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Me.Left - 300 > 0 Then
            Me.Left = Me.Left - 300
        Else
            Me.Left = 0
        End If
        FormWindowListInit = False
        frmSettings.Show()
        frmSettings.sbFormRefresh()
        Call btnRefresh_Click(sender, e)
    End Sub

    Private Sub frmWindowList_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        frmSettings.sbFormRefresh()
    End Sub

    Private Sub frmWindowList_Move(sender As Object, e As EventArgs) Handles MyBase.Move
        frmSettings.sbFormRefresh()
    End Sub
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
                ElseIf ScanHandle = frmSettings.Handle Then
                Else
                    tItem = ListView1.Items.Add(strName)
                    'tItem = ListView1.ListItems.Add(ListView1.ListItems.Count + 1, "", strName, LoadPicture(), LoadPicture())
                    tItem.SubItems.Add(CStr(ScanHandle))
                End If
            End If
            ScanHandle = GetWindow(ScanHandle, GW_HWNDNEXT)
        Loop

    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.SelectedItems.Count = 0 Then Exit Sub
        frmSettings.lblHandle.Text = ListView1.SelectedItems(0).SubItems(1).Text
    End Sub
End Class
