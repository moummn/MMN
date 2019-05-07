Public Class frmOptions
    Private Sub FrmOptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub frmOptions_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Dim S As String = ""
        Dim CO As Integer = lvLN.Items.Count
        For I As Integer = 0 To CO - 1
            S &= lvLN.Items(I).Text & "<!>" & CInt(lvLN.Items(I).Checked)
            If I < CO - 1 Then S &= "<@>"
        Next
        Debug.Print(S)
        My.Settings.NameChecked = S
        My.Settings.Save()
    End Sub

    Private Sub BtnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        For I As Integer = 0 To lvLN.Items.Count - 1
            lvLN.Items(I).Checked = True
        Next
    End Sub

    Private Sub BtnClearAll_Click(sender As Object, e As EventArgs) Handles btnClearAll.Click
        For I As Integer = 0 To lvLN.Items.Count - 1
            lvLN.Items(I).Checked = False
        Next
    End Sub
End Class