Public Class frmLotNum
    Dim LotState As Boolean = False
    Public Sub EnableDoubleBuffering()
        ' Set the value of the double-buffering style bits to true.
        Me.SetStyle(ControlStyles.DoubleBuffer _
     Or ControlStyles.UserPaint _
     Or ControlStyles.AllPaintingInWmPaint,
     True)
        Me.UpdateStyles()
    End Sub
    Private Sub sbLotName()
        LotState = True
        Dim I As Integer
        Dim LNList() As String
        Dim CO As Integer = frmOptions.lvLN.Items.Count - 1

        ReDim LNList(0)
        For I = 0 To CO
            If frmOptions.lvLN.Items(I).Checked = False Then GoTo NX
            If Trim(LNList(UBound(LNList))) <> vbNullString Then ReDim Preserve LNList(UBound(LNList) + 1)
            LNList(UBound(LNList)) = frmOptions.lvLN.Items(I).Text
NX:
        Next
        I = 0
        CO = UBound(LNList)
        Do
            I += Int(Rnd(Now.Millisecond + 1) * CO / 10 + 1)
            If I > CO Then I = I - CO - 1
            lblLotName.Text = LNList(I)
            Application.DoEvents()
        Loop Until LotState = False

    End Sub
    Private Sub frmLotNum_Load(sender As Object, e As EventArgs) Handles Me.Load
        '初始化
        Randomize()
        Me.Icon = My.Resources.mLotNum
        EnableDoubleBuffering()
        '加载文件
        Dim S As String
        frmOptions.lvLN.Clear()
        FileOpen(1, Application.StartupPath & "\LotName.txt", OpenMode.Binary)
        If LOF(1) < 1 Then
            S = "名称1" & vbCrLf & "名称2" & vbCrLf & "名称3" & vbCrLf
            FileSystem.FilePut(1, S)
            FileSystem.Seek(1, 1)
            S = ""
        End If
        Do
            Try
                S = LineInput(1)
                frmOptions.lvLN.Items.Add(S)
                frmOptions.lvLN.Items(frmOptions.lvLN.Items.Count - 1).Checked = True
            Catch ex As System.IO.EndOfStreamException
                Exit Do
            Catch ex As Exception
                MsgBox(ex.ToString)
                Exit Do
            End Try
        Loop

        '加载设置
        Dim SN(), SC() As String
        SN = Split(My.Settings.NameChecked, "<@>",, CompareMethod.Binary)
        For I As Integer = 0 To UBound(SN)
            SC = Split(SN(I), "<!>",, CompareMethod.Binary)
            Try
                frmOptions.lvLN.FindItemWithText(SC(0)).Checked = CBool(SC(1))
            Catch ex As Exception

            End Try

        Next
    End Sub
    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
        End
    End Sub

    Private Sub BtnMenu_Click(sender As Object, e As EventArgs) Handles btnMenu.Click
        frmOptions.Left = Me.Left
        frmOptions.Top = Me.Top
        frmOptions.Height = Screen.FromHandle(Me.Handle).Bounds.Height
        frmOptions.ShowDialog()
        btnStartStop.Focus()
    End Sub

    Private Sub BtnStartStop_Click(sender As Object, e As EventArgs) Handles btnStartStop.Click
        If LotState = False Then
            sbLotName()
        Else
            LotState = False
        End If


    End Sub

End Class
