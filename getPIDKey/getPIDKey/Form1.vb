﻿Public Class Form1
    Private Function fnGETPID(ByVal startPoint As Integer, ByVal StrString As String, ByRef PIDString As String) As Integer
        PIDString = ""
        Dim I As Integer = InStr(startPoint, StrString, "-")
        If I = 0 OrElse I > Len(StrString) - 23 Then
            Return 0
        ElseIf I < 6 Then
            If I + 1 > Len(StrString) Then
                Return 0
            Else
                Return I + 1
            End If
        End If
        If Mid(StrString, I + 6, 1) = "-" AndAlso Mid(StrString, I + 12, 1) = "-" AndAlso Mid(StrString, I + 18, 1) = "-" Then
            PIDString = Mid(StrString, I - 5, 29)
            Return I + 24
        End If
        Return I + 1
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
            If PID <> "" Then TEXT = TEXT & PID & vbCrLf
        Loop
        TextBox2.Text = TEXT
    End Sub

    Private Sub TextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown, TextBox2.KeyDown
        If e.KeyCode = Keys.A AndAlso e.Control = True Then sender.SelectAll
    End Sub
End Class
