Public Class frmMain
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim log As New EventLog("system")
        Dim ErrLogs As Long = 0
        Dim entry As EventLogEntry = Nothing
        Dim I As Long = 1
        TextBox1.Text = ""
        Do
            Try
                entry = log.Entries(log.Entries.Count - I)
                If entry.EntryType = EventLogEntryType.Error Then
                    TextBox1.Text = TextBox1.Text & Now.ToBinary & ":[" & entry.TimeGenerated.ToString & "]" &
                                CStr(entry.InstanceId) &
                                ":" & entry.Message & vbCrLf & "===================" & vbCrLf
                    ErrLogs = ErrLogs + 1
                End If
            Catch ex As Exception

            End Try

            I = I + 1
        Loop Until I > log.Entries.Count OrElse ErrLogs >= 100
    End Sub
End Class
