Public Class frmSFUG
    Dim SaveLog As Boolean = False
    Dim Syslog As New EventLog("system")
    Dim entry As EventLogEntry = Nothing
    Dim L As Long = 1
    Private Sub sbCreateProcess(ByVal cmdString As String)
        Dim PATH As String = Application.StartupPath

        Dim proc As New ProcessStartInfo
        With proc
            .UseShellExecute = True
            .WorkingDirectory = Environment.CurrentDirectory
            .FileName = "CMD.EXE"
            'If (Environment.OSVersion.Version.Major >= 6) Then
            '    .Verb = "runas"
            'Else
            .Verb = ""
            'End If
            .Arguments = "/C " & cmdString
        End With
        Try
            Process.Start(proc)
        Catch
            ' The user refused the elevation.
            ' Do nothing and return directly ...
            Return
        End Try
    End Sub
    Private Sub frmSFUG_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim myParameters() As String = System.Environment.GetCommandLineArgs
        Me.Width = 1
        Me.Height = 1
        Me.Left = -2
        Me.Top = -2
        For Each myPara As String In myParameters
            If LCase(myPara) = "/savelog" Then SaveLog = True
        Next


        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        L = 1
        Do
            Try
                entry = Syslog.Entries(Syslog.Entries.Count - L)
                If entry.TimeGenerated.AddMinutes(1).ToBinary < Now.ToBinary Then Exit Do
                If entry.InstanceId = 3221225484 Then
                    sbCreateProcess("shutdown /f /r /t 0 /c ""因USB断开，重启系统。""")
                End If
            Catch
            End Try
            L = L + 1
        Loop Until L > Syslog.Entries.Count
    End Sub

    Private Sub frmSFUG_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If Me.Visible = True Then Me.Visible = False
    End Sub
End Class
