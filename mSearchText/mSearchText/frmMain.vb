Public Class frmMain
    Private OutputText As String
    Private Function fnGetFolder(ByVal FilePath As String) As String
        Dim I As Integer = 0
        Dim S As String = Replace(FilePath, "/", "\")
        Dim TS As String = ""
        Dim LastP As Integer = 0
        For I = 1 To Len(S)
            TS = Mid(S, I, 1)
            If TS = "\" Then LastP = I
        Next
        If LastP > 0 Then
            I = InStr(LastP, S, ".", vbBinaryCompare)
        Else
            I = 0
        End If
        If I > 0 Then
            S = Mid(S, 1, LastP)
        End If
        If Strings.Right(S, 1) <> "\" Then
            S &= "\"
        End If
        Return S
    End Function
    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        TextBox3.Text = fnGetFolder(sender.Text) & "Output.txt"
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        If TextBox3.Text = "" Then TextBox3.Text = "Output.txt"
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        btnStart.Enabled = False

        Dim SearchKeyWord As String = TextBox1.Text
        Dim SearchFolder As String = TextBox2.Text
        Dim WorkFolder As String = ""
        Dim OutputFile As String = TextBox3.Text
        Dim AllFiles As Integer = 0
        Dim FileBuffer As String = ""
        OutputText = ""

        Dim I As Integer = 0

        WorkFolder = fnGetFolder(SearchFolder)
        If Mid(WorkFolder, 1, Len(WorkFolder) - 1) = SearchFolder Then
            SearchFolder = WorkFolder
        End If

        Dim FileName As String = FileSystem.Dir(SearchFolder)
        Do
            If FileName = "" Then Exit Do
            AllFiles += 1
            FileName = FileSystem.Dir()
        Loop

        If AllFiles >= 1 Then

            ProgressBar1.Maximum = AllFiles
            ProgressBar1.Minimum = 0



            FileName = FileSystem.Dir(SearchFolder)
            I = 0
            Do
                If FileName = "" Then Exit Do

                FileBuffer = My.Computer.FileSystem.ReadAllText(WorkFolder & FileName, System.Text.Encoding.Default)
                Debug.Print(FileBuffer)
                If InStr(FileBuffer, SearchKeyWord, vbBinaryCompare) > 0 Then
                    OutputText &= WorkFolder & FileName & vbCrLf
                End If
                I += 1
                FileName = FileSystem.Dir()
                ProgressBar1.Value = I
                My.Application.DoEvents()
            Loop
            My.Computer.FileSystem.WriteAllText(OutputFile, OutputText, False)


        End If

        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        btnStart.Enabled = True
        ProgressBar1.Value = 0
    End Sub
End Class
