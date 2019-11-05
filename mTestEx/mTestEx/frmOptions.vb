Public Class frmOptions
    Private Sub frmOptions_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        btnCancel.Enabled = True
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        frmMain.AllowPanDuan = CheckBox1.Checked
        frmMain.AllowDanXuan = CheckBox2.Checked
        frmMain.AllowDuoXuan = CheckBox3.Checked
        frmMain.AllowTianKong = CheckBox4.Checked
        Me.Close()

    End Sub

    Private Sub frmOptions_Load(sender As Object, e As EventArgs) Handles Me.Load
        CheckBox1.Checked = frmMain.AllowPanDuan
        CheckBox2.Checked = frmMain.AllowDanXuan
        CheckBox3.Checked = frmMain.AllowDuoXuan
        CheckBox4.Checked = frmMain.AllowTianKong
    End Sub

    Private Sub CheckBox_CheckedChanged1(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged, CheckBox2.CheckedChanged, CheckBox3.CheckedChanged
        If CheckBox1.Checked = False AndAlso CheckBox2.Checked = False AndAlso CheckBox3.Checked = False Then
            sender.Checked = True
        End If
    End Sub
End Class