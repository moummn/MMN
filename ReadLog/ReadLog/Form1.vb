Public Class frmMain
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim log As New EventLog("Application")
        For I As Long = 0 To 100
            Dim entry As EventLogEntry = Nothing
            Try
                entry = log.Entries(log.Entries.Count - I - 1)
                TextBox1.Text = TextBox1.Text & entry.EntryType.ToString &
                                ":" & entry.Message & vbCrLf & "===================" & vbCrLf
            Catch ex As Exception

            End Try


        Next
    End Sub
End Class
