<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLotNum
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.lblLotName = New System.Windows.Forms.Label()
        Me.btnStartStop = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnMenu = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'lblLotName
        '
        Me.lblLotName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLotName.Font = New System.Drawing.Font("宋体", 120.2143!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblLotName.ForeColor = System.Drawing.Color.White
        Me.lblLotName.Location = New System.Drawing.Point(0, 0)
        Me.lblLotName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLotName.Name = "lblLotName"
        Me.lblLotName.Size = New System.Drawing.Size(728, 516)
        Me.lblLotName.TabIndex = 2
        Me.lblLotName.Text = " "
        Me.lblLotName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnStartStop
        '
        Me.btnStartStop.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnStartStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStartStop.ForeColor = System.Drawing.Color.White
        Me.btnStartStop.Location = New System.Drawing.Point(314, 415)
        Me.btnStartStop.Margin = New System.Windows.Forms.Padding(4)
        Me.btnStartStop.Name = "btnStartStop"
        Me.btnStartStop.Size = New System.Drawing.Size(104, 36)
        Me.btnStartStop.TabIndex = 0
        Me.btnStartStop.Text = "开始/停止(&S)"
        Me.btnStartStop.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(681, 14)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(33, 33)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "X"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnMenu
        '
        Me.btnMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMenu.ForeColor = System.Drawing.Color.White
        Me.btnMenu.Location = New System.Drawing.Point(14, 14)
        Me.btnMenu.Margin = New System.Windows.Forms.Padding(4)
        Me.btnMenu.Name = "btnMenu"
        Me.btnMenu.Size = New System.Drawing.Size(27, 27)
        Me.btnMenu.TabIndex = 5
        Me.btnMenu.Text = "≡"
        Me.btnMenu.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("宋体", 7.714285!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.Location = New System.Drawing.Point(-1, 504)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(11, 12)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "0"
        Me.Label1.Visible = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'frmLotNum
        '
        Me.AcceptButton = Me.btnStartStop
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(728, 516)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnMenu)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnStartStop)
        Me.Controls.Add(Me.lblLotName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmLotNum"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "随机摇号"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblLotName As Label
    Friend WithEvents btnStartStop As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents btnMenu As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Timer1 As Timer
End Class
