Public Class frmLotNum
    Dim LNList() As String
    Private Sub frmLotNum_Load(sender As Object, e As EventArgs) Handles Me.Load
        '初始化
        Randomize()
        Me.Icon = My.Resources.mLotNum
        '加载文件
        Dim S As String
        frmOptions.lvLN.Clear()
        FileOpen(1, Application.StartupPath & "\LotName.txt", OpenMode.Binary)
        Do
            Try
                S = LineInput(1)
                frmOptions.lvLN.Items.Add(S)
                frmOptions.lvLN.Items(frmOptions.lvLN.Items.Count - 1).Checked = True
            Catch ex As System.IO.EndOfStreamException
                Exit Do
            Catch ex As Exception
                MsgBox(ex.ToString)
                Exit Do
            End Try
        Loop

        '加载设置
        Dim SN(), SC() As String
        SN = Split(My.Settings.NameChecked, "<@>",, CompareMethod.Binary)
        For I As Integer = 0 To UBound(SN)
            SC = Split(SN(I), "<!>",, CompareMethod.Binary)
            Try
                frmOptions.lvLN.FindItemWithText(SC(0)).Checked = CBool(SC(1))
            Catch ex As Exception

            End Try

        Next
    End Sub
    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
        End
    End Sub

    Private Sub BtnMenu_Click(sender As Object, e As EventArgs) Handles btnMenu.Click
        frmOptions.Left = Me.Left
        frmOptions.Top = Me.Top
        frmOptions.Height = Screen.FromHandle(Me.Handle).Bounds.Height
        Timer1.Enabled = False
        frmOptions.ShowDialog()
    End Sub

    Private Sub BtnStartStop_Click(sender As Object, e As EventArgs) Handles btnStartStop.Click
        If Timer1.Enabled = False Then
            Dim CO As Integer = frmOptions.lvLN.Items.Count - 1
            ReDim LNList(0)
            For I As Integer = 0 To CO
                If frmOptions.lvLN.Items(I).Checked = False Then GoTo NX
                If Trim(LNList(UBound(LNList))) <> vbNullString Then ReDim Preserve LNList(UBound(LNList) + 1)
                LNList(UBound(LNList)) = frmOptions.lvLN.Items(I).Text
NX:
            Next
            Timer1.Enabled = True
        Else
            Timer1.Enabled = False
        End If


    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim I As Integer = Int(UBound(LNList) * Rnd(Now.Millisecond))
        lblLotName.Text = LNList(I)
    End Sub
End Class
