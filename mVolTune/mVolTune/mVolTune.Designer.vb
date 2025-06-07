<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class mVolTune
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.btnBackRun = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnBackRun
        '
        Me.btnBackRun.Location = New System.Drawing.Point(12, 12)
        Me.btnBackRun.Name = "btnBackRun"
        Me.btnBackRun.Size = New System.Drawing.Size(259, 47)
        Me.btnBackRun.TabIndex = 0
        Me.btnBackRun.Text = "&BackRun"
        Me.btnBackRun.UseVisualStyleBackColor = True
        '
        'mVolTune
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(283, 71)
        Me.Controls.Add(Me.btnBackRun)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.MaximizeBox = False
        Me.Name = "mVolTune"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "mVolTune"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnBackRun As Button
End Class
