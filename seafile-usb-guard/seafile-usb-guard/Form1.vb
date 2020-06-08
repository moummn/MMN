Public Class frmSFUG
    Dim SaveLog As Boolean = False
    Dim TestMode As Boolean = False '测试模式，不判断是否USB断开，直接执行脚本
    Dim Syslog As New EventLog("system")
    Dim entry As EventLogEntry = Nothing
    Dim LastEnrayGeneratedDate As Date = Nothing '上一次的日志时间
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
    ''' <summary>
    ''' 获得当前可执行文件的文件名，不含.EXE
    ''' </summary>
    ''' <returns></returns>
    Private Function fnGetExeName() As String
        Dim ExeName As String = Mid(Application.ExecutablePath, Len(Application.StartupPath) + 2, Len(Application.ExecutablePath) - Len(Application.StartupPath) - 1)
        Dim ST As Long = 0
        Dim intTemp As Long = 1
        Do
            intTemp = InStr(ST + 1, ExeName, ".", vbBinaryCompare)
            If intTemp = 0 Then Exit Do
            ST = intTemp
        Loop
        If ST = 0 Then
            fnGetExeName = ExeName
        Else
            fnGetExeName = Mid(ExeName, 1, ST - 1)
        End If

    End Function
    Private Sub sbSaveLog(Optional ByVal logString As String = "")
        Dim logFileName As String = fnGetExeName() & ".log"
        Dim FileNum As Long = FreeFile()
        logString = Now.ToString & " : " & logString & vbCrLf
        Dim LogBinary() As Byte = System.Text.Encoding.Unicode.GetBytes(logString)
        Try
            My.Computer.FileSystem.WriteAllBytes(logFileName, LogBinary, True)
        Catch ex As Exception
            Debug.Print(ex.ToString)
        End Try

    End Sub
    Private Sub frmSFUG_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim myParameters() As String = System.Environment.GetCommandLineArgs
        'Me.Width = 1
        Me.Height = 1
        Me.Left = -2
        Me.Top = -2
        For Each myPara As String In myParameters
            If LCase(myPara) = "-savelog" Then SaveLog = True
            If LCase(myPara) = "-test" Then TestMode = True
        Next


        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        L = 1
        Do
            Try
                If TestMode = True Then
                    If SaveLog = True Then
                        sbSaveLog("TestMode - SaveLog : CreateProcess")
                    End If
                    sbCreateProcess(fnGetExeName() & "-bat.BAT")
                End If
                    entry = Syslog.Entries(Syslog.Entries.Count - L)
                If entry.TimeGenerated.AddSeconds(20).ToBinary < Now.ToBinary Then Exit Do
                If entry.InstanceId = 3221225484 AndAlso LastEnrayGeneratedDate <> entry.TimeGenerated Then
                    'sbCreateProcess("shutdown /f /r /t 0 /c ""因USB断开，重启系统。""")
                    sbCreateProcess(fnGetExeName() & "-bat.BAT")
                    LastEnrayGeneratedDate = entry.TimeGenerated
                End If
            Catch ex As Exception
                If SaveLog = True Then
                    sbSaveLog(ex.ToString)
                End If
            End Try
            L = L + 1
        Loop Until L > Syslog.Entries.Count
    End Sub

    Private Sub frmSFUG_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If Me.Visible = True Then Me.Visible = False
    End Sub
End Class
