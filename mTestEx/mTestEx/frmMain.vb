Public Class frmMain

    Private Const MAX_SUBJECTS As Long = 9999

    Public TestMode As Integer = 0
    Public AllowPanDuan As Boolean = True
    Public AllowDanXuan As Boolean = True
    Public AllowDuoXuan As Boolean = True

    Dim AllSubjects As Integer = 0
    Dim Cache(8, MAX_SUBJECTS) As String
    Dim CurSub As Long
    Private Sub GetNewQuest()
        Dim I As Long
        Randomize()
        Do
            I = Int(Rnd(TimeOfDay.ToBinary) * AllSubjects)
            If Cache(1, I) = "1" AndAlso AllowPanDuan = True Then Exit Do
            If Cache(1, I) = "2" AndAlso AllowDanXuan = True Then Exit Do
            If Cache(1, I) = "3" AndAlso AllowDuoXuan = True Then Exit Do
        Loop
        MsgBox(I)
    End Sub
    Private Sub sbGetSubjects()
        Dim S As String = System.IO.File.ReadAllText("mTestEx.txt", System.Text.Encoding.Default)
        Dim C() As String = Split(S, vbCrLf)
        AllSubjects = UBound(C)
        Try
            For I As Integer = 0 To AllSubjects
                Dim CA() As String = Split(C(I), Chr(9))
                For A As Integer = 1 To 8
                    Cache(A, I) = CA(A - 1)
                Next
            Next
        Catch ex As System.IndexOutOfRangeException
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call sbGetSubjects()

        frmOptions.btnCancel.Enabled = False
        frmOptions.ShowDialog()

    End Sub

    Private Sub 选项OToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 选项OToolStripMenuItem.Click
        frmOptions.ShowDialog()

    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        sbGetSubjects()
    End Sub
End Class
