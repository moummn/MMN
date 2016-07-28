Public Class Form1
    Private Function fnGETPID(ByVal startPoint As Integer, ByVal StrString As String, ByRef PIDString As String) As Integer
        Dim I As Integer = InStr(startPoint, StrString, "-")
        If I = 0 OrElse I < 6 OrElse I > Len(StrString) - 23 Then
            Return 0
            Exit Function
        End If
        If Mid(StrString, I + 6, 1) = "-" AndAlso Mid(StrString, I + 12, 1) = "-" AndAlso Mid(StrString, I + 18, 1) = "-" Then
            PIDString = Mid(StrString, I - 5, 29)
            Return I + 24
            Exit Function
        End If
        Return 0
    End Function
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call Form1_Resize(sender, e)
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        TextBox1.Height = (Me.ClientSize.Height - Button1.Height) / 2
        TextBox2.Height = TextBox1.Height
        TextBox2.Top = (Me.ClientSize.Height + Button1.Height) / 2
        Button1.Top = TextBox1.Height

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim S As String = TextBox1.Text
        Dim PID As String = ""
        Dim TEXT As String = ""
        Dim I As Integer = 1
        Do
            I = fnGETPID(I, S, PID)
            If I = 0 Then Exit Do
            TEXT = TEXT & PID & vbCrLf
        Loop
        TextBox2.Text = TEXT
    End Sub
End Class
