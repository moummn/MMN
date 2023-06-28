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
        Me.btnClose = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.btnCleanAll = New System.Windows.Forms.Button()
        Me.btnReverse = New System.Windows.Forms.Button()
        Me.lvLN = New System.Windows.Forms.ListView()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(-1, -1)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(1, 1)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "X"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lvLN, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(226, 525)
        Me.TableLayoutPanel1.TabIndex = 7
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.Controls.Add(Me.btnSelectAll, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnCleanAll, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.btnReverse, 1, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(226, 24)
        Me.TableLayoutPanel2.TabIndex = 9
        '
        'btnSelectAll
        '
        Me.btnSelectAll.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnSelectAll.Location = New System.Drawing.Point(0, 0)
        Me.btnSelectAll.Margin = New System.Windows.Forms.Padding(0)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Padding = New System.Windows.Forms.Padding(1)
        Me.btnSelectAll.Size = New System.Drawing.Size(75, 24)
        Me.btnSelectAll.TabIndex = 10
        Me.btnSelectAll.Text = "全选(&S)"
        Me.btnSelectAll.UseVisualStyleBackColor = True
        '
        'btnCleanAll
        '
        Me.btnCleanAll.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCleanAll.Location = New System.Drawing.Point(150, 0)
        Me.btnCleanAll.Margin = New System.Windows.Forms.Padding(0)
        Me.btnCleanAll.Name = "btnCleanAll"
        Me.btnCleanAll.Padding = New System.Windows.Forms.Padding(1)
        Me.btnCleanAll.Size = New System.Drawing.Size(76, 24)
        Me.btnCleanAll.TabIndex = 9
        Me.btnCleanAll.Text = "清除(&C)"
        Me.btnCleanAll.UseVisualStyleBackColor = True
        '
        'btnReverse
        '
        Me.btnReverse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnReverse.Location = New System.Drawing.Point(75, 0)
        Me.btnReverse.Margin = New System.Windows.Forms.Padding(0)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Padding = New System.Windows.Forms.Padding(1)
        Me.btnReverse.Size = New System.Drawing.Size(75, 24)
        Me.btnReverse.TabIndex = 8
        Me.btnReverse.Text = "反选(&R)"
        Me.btnReverse.UseVisualStyleBackColor = True
        '
        'lvLN
        '
        Me.lvLN.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvLN.AutoArrange = False
        Me.lvLN.CheckBoxes = True
        Me.lvLN.HideSelection = False
        Me.lvLN.Location = New System.Drawing.Point(1, 25)
        Me.lvLN.Margin = New System.Windows.Forms.Padding(1)
        Me.lvLN.MultiSelect = False
        Me.lvLN.Name = "lvLN"
        Me.lvLN.Size = New System.Drawing.Size(224, 499)
        Me.lvLN.TabIndex = 11
        Me.lvLN.UseCompatibleStateImageBehavior = False
        Me.lvLN.View = System.Windows.Forms.View.SmallIcon
        '
        'frmOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(226, 525)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.btnClose)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmOptions"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "选择内容"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As Button
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lvLN As ListView
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents btnSelectAll As Button
    Friend WithEvents btnCleanAll As Button
    Friend WithEvents btnReverse As Button
End Class
