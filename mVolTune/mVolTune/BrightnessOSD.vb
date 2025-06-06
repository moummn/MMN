Public Class BrightnessOSD

    Private Const BottomMargin As Integer = 40

    Private hideTimer As New Timer()
    Private lblBrightness As Label
    Protected Overrides ReadOnly Property ShowWithoutActivation() As Boolean
        Get
            Return True
        End Get
    End Property

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H8 ' WS_EX_TOPMOST
            cp.ExStyle = cp.ExStyle Or &H80 ' WS_EX_TOOLWINDOW
            cp.ExStyle = cp.ExStyle Or &H8000000 ' WS_EX_NOACTIVATE
            Return cp
        End Get
    End Property
    Public Sub New()
        InitializeComponent()
        Me.FormBorderStyle = FormBorderStyle.None
        Me.TopMost = True
        Me.BackColor = Color.Black
        Me.Opacity = 0.7
        Me.ShowInTaskbar = False
        Me.StartPosition = FormStartPosition.Manual ' 关键：改为Manual
        Me.Width = 200
        Me.Height = 80

        lblBrightness = New Label With {
    .ForeColor = Color.White,
    .Font = New Font("微软雅黑", 24, FontStyle.Bold),
    .AutoSize = False,
    .TextAlign = ContentAlignment.MiddleCenter,
    .Dock = DockStyle.Fill
}
        Me.Controls.Add(lblBrightness)
        hideTimer.Interval = 1000 ' 1秒后自动隐藏
        AddHandler hideTimer.Tick, AddressOf HideTimer_Tick
    End Sub

    Public Sub ShowBrightness(value As Integer)
        lblBrightness.Text = $"亮度: {value}%"
        ' 计算底部中间位置
        Dim screen = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea
        Me.Left = screen.Left + (screen.Width - Me.Width) \ 2
        Me.Top = screen.Top + screen.Height - Me.Height - BottomMargin

        If Not Me.Visible Then
            Me.Show()
        End If
        Me.BringToFront() ' 确保OSD在最上层

        hideTimer.Stop()
        hideTimer.Start()
    End Sub

    Private Sub HideTimer_Tick(sender As Object, e As EventArgs)
        hideTimer.Stop()
        Me.Hide()
    End Sub
End Class