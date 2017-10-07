Public Class frmMain

    Private Const MAX_SUBJECTS As Long = 9999

    Public TestMode As Integer = 0
    Public AllowPanDuan As Boolean = True
    Public AllowDanXuan As Boolean = True
    Public AllowDuoXuan As Boolean = True

    Dim AllSubjects As Integer = 0
    Dim Cache(8, MAX_SUBJECTS) As String
    Dim CurSub As Long
    Dim RightAns As String = ""
    Dim GettingNewQuest As Boolean = False
    Private Sub sbGetRandomABCD(ByRef Ans())
        Dim S1(), S2() As String
        ReDim S1(4), S2(4)
        S1 = Ans
        For I As Integer = 1 To Len(RightAns)
            S2(Val(Mid(RightAns, I, 1))) = "1"
        Next
        For I As Integer = 1 To 4
            Dim R As Integer = Int((5 - I) * Rnd(TimeOfDay.ToBinary) + 1)

        Next
    End Sub
    Private Sub sbGetNewQuest()
        GettingNewQuest = True
        Dim S As String = ""

        Do
            CurSub = Int(Rnd(TimeOfDay.ToBinary) * AllSubjects)
            If Cache(1, CurSub) = "1" AndAlso AllowPanDuan = True Then
                S = "判断题"
                cb3.Visible = False
                cb4.Visible = False
                cb1.Text = Cache(4, CurSub)
                cb2.Text = Cache(5, CurSub)
                btnOK.Visible = False
                RightAns = Cache(8, CurSub)
                Exit Do
            End If
            If Cache(1, CurSub) = "2" AndAlso AllowDanXuan = True Then
                S = "单选题"
                cb3.Visible = True
                cb4.Visible = True
                RightAns = Cache(8, CurSub)
                Dim Ans(4) As String
                Ans(1) = Cache(4, CurSub)
                Ans(2) = Cache(5, CurSub)
                Ans(3) = Cache(6, CurSub)
                Ans(4) = Cache(7, CurSub)
                sbGetRandomABCD(Ans)
                btnOK.Visible = False
                Exit Do
            End If
            If Cache(1, CurSub) = "3" AndAlso AllowDuoXuan = True Then
                S = "多选题"
                cb3.Visible = True
                cb4.Visible = True
                RightAns = Cache(8, CurSub)
                Dim Ans(4)
                Ans(1) = Cache(4, CurSub)
                Ans(2) = Cache(5, CurSub)
                Ans(3) = Cache(6, CurSub)
                Ans(4) = Cache(7, CurSub)
                sbGetRandomABCD(Ans(4))
                btnOK.Visible = True
                Exit Do
            End If
        Loop
        S = S & Cache(2, CurSub) & "：" & vbCrLf & Cache(3, CurSub)
        tbQuest.Text = S

        cb1.Checked = False
        cb2.Checked = False
        cb3.Checked = False
        cb4.Checked = False
        Application.DoEvents()

        GettingNewQuest = False
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
        Randomize()

        Call sbGetSubjects()

        frmOptions.btnCancel.Enabled = False
        frmOptions.ShowDialog()

        sbGetNewQuest()

    End Sub

    Private Sub 选项OToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 选项OToolStripMenuItem.Click
        frmOptions.ShowDialog()

    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        sbGetNewQuest()
    End Sub

    Private Sub cb_CheckedChanged(sender As Object, e As EventArgs) Handles cb1.CheckedChanged, cb2.CheckedChanged, cb3.CheckedChanged, cb4.CheckedChanged
        If GettingNewQuest = True Then Exit Sub
        If Cache(1, CurSub) = "3" Then Exit Sub
        'If cb1.Checked = True Then

    End Sub
End Class
