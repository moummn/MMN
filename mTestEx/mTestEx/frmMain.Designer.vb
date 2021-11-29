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
        Me.StatusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.tbQuest = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.panButton = New System.Windows.Forms.Panel()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.panAns = New System.Windows.Forms.Panel()
        Me.tbAns = New System.Windows.Forms.TextBox()
        Me.tpCheckBox = New System.Windows.Forms.TableLayoutPanel()
        Me.cb4 = New System.Windows.Forms.CheckBox()
        Me.cb3 = New System.Windows.Forms.CheckBox()
        Me.cb2 = New System.Windows.Forms.CheckBox()
        Me.cb1 = New System.Windows.Forms.CheckBox()
        Me.panButtomLeft = New System.Windows.Forms.Panel()
        Me.MenuStrip1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.panButton.SuspendLayout()
        Me.panAns.SuspendLayout()
        Me.tpCheckBox.SuspendLayout()
        Me.panButtomLeft.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(19, 19)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.选项OToolStripMenuItem, Me.StatusToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(747, 28)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        '选项OToolStripMenuItem
        '
        Me.选项OToolStripMenuItem.Name = "选项OToolStripMenuItem"
        Me.选项OToolStripMenuItem.Size = New System.Drawing.Size(72, 24)
        Me.选项OToolStripMenuItem.Text = "选项(&O)"
        '
        'StatusToolStripMenuItem
        '
        Me.StatusToolStripMenuItem.Name = "StatusToolStripMenuItem"
        Me.StatusToolStripMenuItem.Size = New System.Drawing.Size(64, 24)
        Me.StatusToolStripMenuItem.Text = "Status"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.tbQuest, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 28)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(747, 427)
        Me.TableLayoutPanel1.TabIndex = 1000
        '
        'tbQuest
        '
        Me.tbQuest.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbQuest.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.tbQuest.Location = New System.Drawing.Point(4, 4)
        Me.tbQuest.Margin = New System.Windows.Forms.Padding(4)
        Me.tbQuest.Multiline = True
        Me.tbQuest.Name = "tbQuest"
        Me.tbQuest.ReadOnly = True
        Me.tbQuest.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.tbQuest.Size = New System.Drawing.Size(739, 205)
        Me.tbQuest.TabIndex = 1000
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel3, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.panButtomLeft, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 216)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(741, 208)
        Me.TableLayoutPanel2.TabIndex = 1001
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.panButton, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(558, 3)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(180, 202)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'panButton
        '
        Me.panButton.Controls.Add(Me.btnOK)
        Me.panButton.Controls.Add(Me.btnNext)
        Me.panButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panButton.Location = New System.Drawing.Point(3, 104)
        Me.panButton.Name = "panButton"
        Me.panButton.Size = New System.Drawing.Size(174, 95)
        Me.panButton.TabIndex = 1003
        '
        'btnOK
        '
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnOK.Location = New System.Drawing.Point(0, 0)
        Me.btnOK.Margin = New System.Windows.Forms.Padding(4)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(174, 95)
        Me.btnOK.TabIndex = 9
        Me.btnOK.Text = "确定(&O)"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnNext.Location = New System.Drawing.Point(0, 0)
        Me.btnNext.Margin = New System.Windows.Forms.Padding(4)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(174, 95)
        Me.btnNext.TabIndex = 8
        Me.btnNext.Text = "下一题(&N)"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("宋体", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.Location = New System.Drawing.Point(4, 0)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(172, 101)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "结果"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'panAns
        '
        Me.panAns.Controls.Add(Me.tbAns)
        Me.panAns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panAns.Location = New System.Drawing.Point(0, 0)
        Me.panAns.Name = "panAns"
        Me.panAns.Size = New System.Drawing.Size(549, 202)
        Me.panAns.TabIndex = 1005
        '
        'tbAns
        '
        Me.tbAns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbAns.Font = New System.Drawing.Font("微软雅黑", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.tbAns.Location = New System.Drawing.Point(0, 0)
        Me.tbAns.Margin = New System.Windows.Forms.Padding(4)
        Me.tbAns.Multiline = True
        Me.tbAns.Name = "tbAns"
        Me.tbAns.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.tbAns.Size = New System.Drawing.Size(549, 202)
        Me.tbAns.TabIndex = 7
        '
        'tpCheckBox
        '
        Me.tpCheckBox.ColumnCount = 1
        Me.tpCheckBox.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tpCheckBox.Controls.Add(Me.cb1, 0, 0)
        Me.tpCheckBox.Controls.Add(Me.cb2, 0, 1)
        Me.tpCheckBox.Controls.Add(Me.cb3, 0, 2)
        Me.tpCheckBox.Controls.Add(Me.cb4, 0, 3)
        Me.tpCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tpCheckBox.Location = New System.Drawing.Point(0, 0)
        Me.tpCheckBox.Name = "tpCheckBox"
        Me.tpCheckBox.RowCount = 4
        Me.tpCheckBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tpCheckBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tpCheckBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tpCheckBox.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tpCheckBox.Size = New System.Drawing.Size(549, 202)
        Me.tpCheckBox.TabIndex = 1004
        '
        'cb4
        '
        Me.cb4.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cb4.AutoSize = True
        Me.cb4.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.cb4.Location = New System.Drawing.Point(4, 165)
        Me.cb4.Margin = New System.Windows.Forms.Padding(4)
        Me.cb4.Name = "cb4"
        Me.cb4.Size = New System.Drawing.Size(541, 21)
        Me.cb4.TabIndex = 4
        Me.cb4.Text = "CheckBox4"
        Me.cb4.UseVisualStyleBackColor = True
        '
        'cb3
        '
        Me.cb3.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cb3.AutoSize = True
        Me.cb3.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.cb3.Location = New System.Drawing.Point(4, 114)
        Me.cb3.Margin = New System.Windows.Forms.Padding(4)
        Me.cb3.Name = "cb3"
        Me.cb3.Size = New System.Drawing.Size(541, 21)
        Me.cb3.TabIndex = 3
        Me.cb3.Text = "CheckBox3"
        Me.cb3.UseVisualStyleBackColor = True
        '
        'cb2
        '
        Me.cb2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cb2.AutoSize = True
        Me.cb2.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.cb2.Location = New System.Drawing.Point(4, 64)
        Me.cb2.Margin = New System.Windows.Forms.Padding(4)
        Me.cb2.Name = "cb2"
        Me.cb2.Size = New System.Drawing.Size(541, 21)
        Me.cb2.TabIndex = 2
        Me.cb2.Text = "CheckBox2"
        Me.cb2.UseVisualStyleBackColor = True
        '
        'cb1
        '
        Me.cb1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cb1.AutoSize = True
        Me.cb1.Font = New System.Drawing.Font("宋体", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.cb1.Location = New System.Drawing.Point(4, 14)
        Me.cb1.Margin = New System.Windows.Forms.Padding(4)
        Me.cb1.Name = "cb1"
        Me.cb1.Size = New System.Drawing.Size(541, 21)
        Me.cb1.TabIndex = 1
        Me.cb1.Text = "CheckBox1"
        Me.cb1.UseVisualStyleBackColor = True
        '
        'panButtomLeft
        '
        Me.panButtomLeft.Controls.Add(Me.tpCheckBox)
        Me.panButtomLeft.Controls.Add(Me.panAns)
        Me.panButtomLeft.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panButtomLeft.Location = New System.Drawing.Point(3, 3)
        Me.panButtomLeft.Name = "panButtomLeft"
        Me.panButtomLeft.Size = New System.Drawing.Size(549, 202)
        Me.panButtomLeft.TabIndex = 1
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(747, 455)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "mTestEx"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.panButton.ResumeLayout(False)
        Me.panAns.ResumeLayout(False)
        Me.panAns.PerformLayout()
        Me.tpCheckBox.ResumeLayout(False)
        Me.tpCheckBox.PerformLayout()
        Me.panButtomLeft.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents 选项OToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StatusToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents tbQuest As TextBox
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents panButton As Panel
    Friend WithEvents btnOK As Button
    Friend WithEvents btnNext As Button
    Friend WithEvents panButtomLeft As Panel
    Friend WithEvents tpCheckBox As TableLayoutPanel
    Friend WithEvents cb1 As CheckBox
    Friend WithEvents cb2 As CheckBox
    Friend WithEvents cb3 As CheckBox
    Friend WithEvents cb4 As CheckBox
    Friend WithEvents panAns As Panel
    Friend WithEvents tbAns As TextBox
End Class
