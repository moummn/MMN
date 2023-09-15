<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class mVolTune
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
        Me.btnVolUp = New System.Windows.Forms.Button()
        Me.btnVolDown = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnVolUp
        '
        Me.btnVolUp.Location = New System.Drawing.Point(22, 40)
        Me.btnVolUp.Name = "btnVolUp"
        Me.btnVolUp.Size = New System.Drawing.Size(75, 23)
        Me.btnVolUp.TabIndex = 0
        Me.btnVolUp.Text = "Vol&Up"
        Me.btnVolUp.UseVisualStyleBackColor = True
        '
        'btnVolDown
        '
        Me.btnVolDown.Location = New System.Drawing.Point(125, 40)
        Me.btnVolDown.Name = "btnVolDown"
        Me.btnVolDown.Size = New System.Drawing.Size(75, 23)
        Me.btnVolDown.TabIndex = 1
        Me.btnVolDown.Text = "Vol&Down"
        Me.btnVolDown.UseVisualStyleBackColor = True
        '
        'mVolTune
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(238, 124)
        Me.Controls.Add(Me.btnVolDown)
        Me.Controls.Add(Me.btnVolUp)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "mVolTune"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmMain"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnVolUp As Button
    Friend WithEvents btnVolDown As Button
End Class
