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
        Me.components = New System.ComponentModel.Container()
        Me.btnViewWorkFolder = New System.Windows.Forms.Button()
        Me.lblWorkFolder = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.muRightClick = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cbWorkFolder = New System.Windows.Forms.ComboBox()
        Me.twAppList = New System.Windows.Forms.TreeView()
        Me.cbRunAdmin = New System.Windows.Forms.CheckBox()
        Me.cbRunUser = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'btnViewWorkFolder
        '
        Me.btnViewWorkFolder.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnViewWorkFolder.Location = New System.Drawing.Point(576, 9)
        Me.btnViewWorkFolder.Name = "btnViewWorkFolder"
        Me.btnViewWorkFolder.Size = New System.Drawing.Size(95, 29)
        Me.btnViewWorkFolder.TabIndex = 0
        Me.btnViewWorkFolder.Text = "浏览(&V)..."
        Me.btnViewWorkFolder.UseVisualStyleBackColor = True
        '
        'lblWorkFolder
        '
        Me.lblWorkFolder.AutoSize = True
        Me.lblWorkFolder.Location = New System.Drawing.Point(12, 16)
        Me.lblWorkFolder.Name = "lblWorkFolder"
        Me.lblWorkFolder.Size = New System.Drawing.Size(91, 14)
        Me.lblWorkFolder.TabIndex = 2
        Me.lblWorkFolder.Text = "工作文件夹："
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Location = New System.Drawing.Point(576, 44)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(95, 29)
        Me.btnRefresh.TabIndex = 3
        Me.btnRefresh.Text = "刷新(&R)"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'muRightClick
        '
        Me.muRightClick.Name = "muRightClick"
        Me.muRightClick.Size = New System.Drawing.Size(61, 4)
        '
        'cbWorkFolder
        '
        Me.cbWorkFolder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbWorkFolder.FormattingEnabled = True
        Me.cbWorkFolder.Location = New System.Drawing.Point(12, 48)
        Me.cbWorkFolder.Name = "cbWorkFolder"
        Me.cbWorkFolder.Size = New System.Drawing.Size(558, 22)
        Me.cbWorkFolder.TabIndex = 5
        '
        'twAppList
        '
        Me.twAppList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.twAppList.Location = New System.Drawing.Point(12, 76)
        Me.twAppList.Name = "twAppList"
        Me.twAppList.ShowPlusMinus = False
        Me.twAppList.Size = New System.Drawing.Size(558, 362)
        Me.twAppList.TabIndex = 7
        '
        'cbRunAdmin
        '
        Me.cbRunAdmin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbRunAdmin.AutoSize = True
        Me.cbRunAdmin.Location = New System.Drawing.Point(576, 79)
        Me.cbRunAdmin.Name = "cbRunAdmin"
        Me.cbRunAdmin.Size = New System.Drawing.Size(103, 18)
        Me.cbRunAdmin.TabIndex = 8
        Me.cbRunAdmin.Text = "尝试提权(&A)"
        Me.cbRunAdmin.UseVisualStyleBackColor = True
        '
        'cbRunUser
        '
        Me.cbRunUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbRunUser.AutoSize = True
        Me.cbRunUser.Enabled = False
        Me.cbRunUser.Location = New System.Drawing.Point(576, 104)
        Me.cbRunUser.Name = "cbRunUser"
        Me.cbRunUser.Size = New System.Drawing.Size(103, 18)
        Me.cbRunUser.TabIndex = 9
        Me.cbRunUser.Text = "尝试降权(&U)"
        Me.cbRunUser.UseVisualStyleBackColor = True
        Me.cbRunUser.Visible = False
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(683, 450)
        Me.Controls.Add(Me.cbRunUser)
        Me.Controls.Add(Me.cbRunAdmin)
        Me.Controls.Add(Me.twAppList)
        Me.Controls.Add(Me.cbWorkFolder)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.lblWorkFolder)
        Me.Controls.Add(Me.btnViewWorkFolder)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "mMyApp"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnViewWorkFolder As Button
    Friend WithEvents lblWorkFolder As Label
    Friend WithEvents btnRefresh As Button
    Friend WithEvents muRightClick As ContextMenuStrip
    Friend WithEvents cbWorkFolder As ComboBox
    Friend WithEvents twAppList As TreeView
    Friend WithEvents cbRunAdmin As CheckBox
    Friend WithEvents cbRunUser As CheckBox
End Class
