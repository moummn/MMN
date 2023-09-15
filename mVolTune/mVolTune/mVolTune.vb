Public Class mVolTune
    Private Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, lParam As Integer) As Long
    Private Const APPCOMMAND_VOLUME_MUTE As Integer = &H80000
    Private Const APPCOMMAND_VOLUME_UP As Integer = &HA0000
    Private Const APPCOMMAND_VOLUME_DOWN As Integer = &H90000
    Private Const WM_APPCOMMAND As Integer = &H319
    Private Sub btnVolUp_Click(sender As Object, e As EventArgs) Handles btnVolUp.Click
        SendMessage(Me.Handle, WM_APPCOMMAND, Me.Handle, APPCOMMAND_VOLUME_UP)
    End Sub

    Private Sub btnVolDown_Click(sender As Object, e As EventArgs) Handles btnVolDown.Click
        SendMessage(Me.Handle, WM_APPCOMMAND, Me.Handle, APPCOMMAND_VOLUME_DOWN)
    End Sub
End Class
