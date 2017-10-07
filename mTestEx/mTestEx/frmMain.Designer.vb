<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.选项OToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tbQuest = New System.Windows.Forms.TextBox()
        Me.cb1 = New System.Windows.Forms.CheckBox()
        Me.cb2 = New System.Windows.Forms.CheckBox()
        Me.cb3 = New System.Windows.Forms.CheckBox()
        Me.cb4 = New System.Windows.Forms.CheckBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.选项OToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(533, 25)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        '选项OToolStripMenuItem
        '
        Me.选项OToolStripMenuItem.Name = "选项OToolStripMenuItem"
        Me.选项OToolStripMenuItem.Size = New System.Drawing.Size(62, 21)
        Me.选项OToolStripMenuItem.Text = "选项(&O)"
        '
        'tbQuest
        '
        Me.tbQuest.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbQuest.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.tbQuest.Location = New System.Drawing.Point(12, 28)
        Me.tbQuest.Multiline = True
        Me.tbQuest.Name = "tbQuest"
        Me.tbQuest.ReadOnly = True
        Me.tbQuest.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.tbQuest.Size = New System.Drawing.Size(509, 183)
        Me.tbQuest.TabIndex = 1
        '
        'cb1
        '
        Me.cb1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cb1.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.cb1.Location = New System.Drawing.Point(12, 217)
        Me.cb1.Name = "cb1"
        Me.cb1.Size = New System.Drawing.Size(413, 32)
        Me.cb1.TabIndex = 2
        Me.cb1.Text = "CheckBox1"
        Me.cb1.UseVisualStyleBackColor = True
        '
        'cb2
        '
        Me.cb2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cb2.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.cb2.Location = New System.Drawing.Point(12, 255)
        Me.cb2.Name = "cb2"
        Me.cb2.Size = New System.Drawing.Size(413, 32)
        Me.cb2.TabIndex = 3
        Me.cb2.Text = "CheckBox2"
        Me.cb2.UseVisualStyleBackColor = True
        '
        'cb3
        '
        Me.cb3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cb3.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.cb3.Location = New System.Drawing.Point(12, 293)
        Me.cb3.Name = "cb3"
        Me.cb3.Size = New System.Drawing.Size(413, 32)
        Me.cb3.TabIndex = 4
        Me.cb3.Text = "CheckBox3"
        Me.cb3.UseVisualStyleBackColor = True
        '
        'cb4
        '
        Me.cb4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cb4.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.cb4.Location = New System.Drawing.Point(12, 331)
        Me.cb4.Name = "cb4"
        Me.cb4.Size = New System.Drawing.Size(413, 32)
        Me.cb4.TabIndex = 5
        Me.cb4.Text = "CheckBox4"
        Me.cb4.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(431, 289)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(90, 74)
        Me.btnOK.TabIndex = 6
        Me.btnOK.Text = "确定(&O)"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(533, 374)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.cb4)
        Me.Controls.Add(Me.cb3)
        Me.Controls.Add(Me.cb2)
        Me.Controls.Add(Me.cb1)
        Me.Controls.Add(Me.tbQuest)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "mTestEx"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents 选项OToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents tbQuest As TextBox
    Friend WithEvents cb1 As CheckBox
    Friend WithEvents cb2 As CheckBox
    Friend WithEvents cb3 As CheckBox
    Friend WithEvents cb4 As CheckBox
    Friend WithEvents btnOK As Button
End Class
