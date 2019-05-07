<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOptions
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
        Me.lvLN = New System.Windows.Forms.ListView()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.btnClearAll = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lvLN
        '
        Me.lvLN.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvLN.AutoArrange = False
        Me.lvLN.CheckBoxes = True
        Me.lvLN.Location = New System.Drawing.Point(0, 22)
        Me.lvLN.MultiSelect = False
        Me.lvLN.Name = "lvLN"
        Me.lvLN.Size = New System.Drawing.Size(122, 428)
        Me.lvLN.TabIndex = 0
        Me.lvLN.UseCompatibleStateImageBehavior = False
        Me.lvLN.View = System.Windows.Forms.View.SmallIcon
        '
        'btnSelectAll
        '
        Me.btnSelectAll.Location = New System.Drawing.Point(0, 0)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(61, 23)
        Me.btnSelectAll.TabIndex = 1
        Me.btnSelectAll.Text = "全选(&S)"
        Me.btnSelectAll.UseVisualStyleBackColor = True
        '
        'btnClearAll
        '
        Me.btnClearAll.Location = New System.Drawing.Point(61, 0)
        Me.btnClearAll.Name = "btnClearAll"
        Me.btnClearAll.Size = New System.Drawing.Size(61, 23)
        Me.btnClearAll.TabIndex = 2
        Me.btnClearAll.Text = "清除(&C)"
        Me.btnClearAll.UseVisualStyleBackColor = True
        '
        'frmOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(122, 450)
        Me.Controls.Add(Me.btnClearAll)
        Me.Controls.Add(Me.btnSelectAll)
        Me.Controls.Add(Me.lvLN)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmOptions"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "选择人员"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lvLN As ListView
    Friend WithEvents btnSelectAll As Button
    Friend WithEvents btnClearAll As Button
End Class
