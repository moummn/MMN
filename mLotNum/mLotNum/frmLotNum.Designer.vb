﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btnMenu = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblLotName
        '
        Me.lblLotName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLotName.Font = New System.Drawing.Font("宋体", 144.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblLotName.ForeColor = System.Drawing.Color.White
        Me.lblLotName.Location = New System.Drawing.Point(0, 0)
        Me.lblLotName.Name = "lblLotName"
        Me.lblLotName.Size = New System.Drawing.Size(624, 442)
        Me.lblLotName.TabIndex = 2
        Me.lblLotName.Text = " "
        Me.lblLotName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnStartStop
        '
        Me.btnStartStop.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnStartStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStartStop.ForeColor = System.Drawing.Color.White
        Me.btnStartStop.Location = New System.Drawing.Point(269, 356)
        Me.btnStartStop.Name = "btnStartStop"
        Me.btnStartStop.Size = New System.Drawing.Size(89, 31)
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
        Me.btnClose.Location = New System.Drawing.Point(584, 12)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(28, 28)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "X"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Interval = 1
        '
        'btnMenu
        '
        Me.btnMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMenu.ForeColor = System.Drawing.Color.White
        Me.btnMenu.Location = New System.Drawing.Point(12, 12)
        Me.btnMenu.Name = "btnMenu"
        Me.btnMenu.Size = New System.Drawing.Size(23, 23)
        Me.btnMenu.TabIndex = 5
        Me.btnMenu.Text = "≡"
        Me.btnMenu.UseVisualStyleBackColor = True
        '
        'frmLotNum
        '
        Me.AcceptButton = Me.btnStartStop
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(624, 442)
        Me.Controls.Add(Me.btnMenu)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnStartStop)
        Me.Controls.Add(Me.lblLotName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmLotNum"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "随机摇号"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lblLotName As Label
    Friend WithEvents btnStartStop As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents Timer1 As Timer
    Friend WithEvents btnMenu As Button
End Class
