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


    Public NumList As New ArrayList

    Private Sub sbGetRandomABCD(ByRef Ans())
        Dim S1(4), S2(4) As String
        For I As Integer = 1 To 4
            S1(I) = Ans(I)
        Next

        For I As Integer = 1 To Len(RightAns)
            S2(Val(Mid(RightAns, I, 1))) = "1"
        Next
        RightAns = ""
        For I As Integer = 1 To 4
            Dim R As Integer = Int((5 - I) * Rnd(TimeOfDay.ToBinary) + 1)
            Ans(I) = S1(R)
            If S2(R) = "1" Then RightAns &= CStr(I)
            For A As Integer = R To 3
                S1(A) = S1(A + 1)
                S2(A) = S2(A + 1)
            Next
        Next
        cb1.Text = Ans(1)
        cb2.Text = Ans(2)
        cb3.Text = Ans(3)
        cb4.Text = Ans(4)
    End Sub
    Private Sub fnGetNewQuestList()
        NumList.Clear()
        Dim tList As New ArrayList
        For I As Long = 0 To AllSubjects
            If Cache(1, I) = "1" AndAlso AllowPanDuan = True Then
                tList.Add(I)
            End If
            If Cache(1, I) = "2" AndAlso AllowDanXuan = True Then
                tList.Add(I)
            End If
            If Cache(1, I) = "3" AndAlso AllowDuoXuan = True Then
                tList.Add(I)
            End If
        Next
        Do Until tList.Count <= 0
            Dim I As Long = Int(Rnd(TimeOfDay.ToBinary) * tList.Count)
            NumList.Add(tList.Item(I))
            tList.RemoveAt(I)
        Loop
    End Sub
    Private Sub fnGetNewQuest()
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
                sbGetRandomABCD(Ans)
                btnOK.Visible = True
                Exit Do
            End If
        Loop
        S = S & Cache(2, CurSub) & "：" & vbCrLf & Cache(3, CurSub)
        tbQuest.Text = S

        cb1.Checked = False
        cb1.Font = New System.Drawing.Font(cb1.Font, FontStyle.Regular)
        cb1.ForeColor = SystemColors.ControlText
        cb2.Checked = False
        cb2.Font = New System.Drawing.Font(cb1.Font, FontStyle.Regular)
        cb2.ForeColor = SystemColors.ControlText
        cb3.Checked = False
        cb3.Font = New System.Drawing.Font(cb1.Font, FontStyle.Regular)
        cb3.ForeColor = SystemColors.ControlText
        cb4.Checked = False
        cb4.Font = New System.Drawing.Font(cb1.Font, FontStyle.Regular)
        cb4.ForeColor = SystemColors.ControlText
        btnNext.Visible = False
        Label1.Visible = False
        AcceptButton = btnOK
        Application.DoEvents()

        cb1.Focus()
        GettingNewQuest = False
    End Sub
    Private Sub sbGetSubjects()
        If System.IO.File.Exists("mTestEx.dbt") = False Then
            MsgBox("mTestEx.dbt文件不存在", vbCritical)
            End
        End If
        Dim S As String = System.IO.File.ReadAllText("mTestEx.dbt", System.Text.Encoding.Default)
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
        Dim MyAns As String = ""
        If cb1.Checked = True Then MyAns &= "1"
        If cb2.Checked = True Then MyAns &= "2"
        If cb3.Checked = True Then MyAns &= "3"
        If cb4.Checked = True Then MyAns &= "4"
        If MyAns = RightAns Then
            Label1.Text = "正确"
            Label1.ForeColor = SystemColors.ControlText
            Label1.Visible = True
            btnNext.Visible = True
            btnOK.Visible = False
        Else
            Label1.Text = "错误"
            Label1.ForeColor = Color.Red
            Label1.Visible = True
            btnNext.Visible = True
            If InStr(RightAns, "1") > 0 Then
                cb1.Font = New Font(cb1.Font, FontStyle.Bold)
                cb1.ForeColor = Color.Red
            End If
            If InStr(RightAns, "2") > 0 Then
                cb2.Font = New Font(cb1.Font, FontStyle.Bold)
                cb2.ForeColor = Color.Red
            End If
            If InStr(RightAns, "3") > 0 Then
                cb3.Font = New Font(cb1.Font, FontStyle.Bold)
                cb3.ForeColor = Color.Red
            End If
            If InStr(RightAns, "4") > 0 Then
                cb4.Font = New Font(cb1.Font, FontStyle.Bold)
                cb4.ForeColor = Color.Red
            End If
        End If

        AcceptButton = btnNext
    End Sub

    Private Sub cb_CheckedChanged(sender As Object, e As EventArgs) Handles cb1.CheckedChanged, cb2.CheckedChanged, cb3.CheckedChanged, cb4.CheckedChanged
        If GettingNewQuest = True Then Exit Sub
        If Cache(1, CurSub) = "3" Then Exit Sub
        If btnNext.Visible = True Then Exit Sub
        Dim MyAns As String = ""
        If cb1.Checked = True Then MyAns = "1"
        If cb2.Checked = True Then MyAns = "2"
        If cb3.Checked = True Then MyAns = "3"
        If cb4.Checked = True Then MyAns = "4"
        If MyAns = RightAns Then
            Label1.Text = "正确"
            Label1.ForeColor = SystemColors.ControlText
            Label1.Visible = True
            btnNext.Visible = True
        Else
            Label1.Text = "错误"
            Label1.ForeColor = Color.Red
            Label1.Visible = True
            btnNext.Visible = True
            If RightAns = "1" Then
                cb1.Font = New Font(cb1.Font, FontStyle.Bold)
                cb1.ForeColor = Color.Red
            End If
            If RightAns = "2" Then
                cb2.Font = New Font(cb1.Font, FontStyle.Bold)
                cb2.ForeColor = Color.Red
            End If
            If RightAns = "3" Then
                cb3.Font = New Font(cb1.Font, FontStyle.Bold)
                cb3.ForeColor = Color.Red
            End If
            If RightAns = "4" Then
                cb4.Font = New Font(cb1.Font, FontStyle.Bold)
                cb4.ForeColor = Color.Red
            End If
        End If

        AcceptButton = btnNext
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        sbGetNewQuest()
    End Sub
End Class
