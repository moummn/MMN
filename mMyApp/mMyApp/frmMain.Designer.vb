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
        Me.btnViewWorkFolder.Location = New System.Drawing.Point(987, 15)
        Me.btnViewWorkFolder.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.btnViewWorkFolder.Name = "btnViewWorkFolder"
        Me.btnViewWorkFolder.Size = New System.Drawing.Size(163, 50)
        Me.btnViewWorkFolder.TabIndex = 0
        Me.btnViewWorkFolder.Text = "浏览(&V)..."
        Me.btnViewWorkFolder.UseVisualStyleBackColor = True
        '
        'lblWorkFolder
        '
        Me.lblWorkFolder.AutoSize = True
        Me.lblWorkFolder.Location = New System.Drawing.Point(21, 27)
        Me.lblWorkFolder.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblWorkFolder.Name = "lblWorkFolder"
        Me.lblWorkFolder.Size = New System.Drawing.Size(154, 24)
        Me.lblWorkFolder.TabIndex = 2
        Me.lblWorkFolder.Text = "工作文件夹："
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Location = New System.Drawing.Point(987, 75)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(163, 50)
        Me.btnRefresh.TabIndex = 3
        Me.btnRefresh.Text = "刷新(&R)"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'muRightClick
        '
        Me.muRightClick.Name = "muRightClick"
        Me.muRightClick.Size = New System.Drawing.Size(301, 48)
        '
        'cbWorkFolder
        '
        Me.cbWorkFolder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbWorkFolder.FormattingEnabled = True
        Me.cbWorkFolder.Location = New System.Drawing.Point(21, 82)
        Me.cbWorkFolder.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cbWorkFolder.Name = "cbWorkFolder"
        Me.cbWorkFolder.Size = New System.Drawing.Size(954, 32)
        Me.cbWorkFolder.TabIndex = 5
        '
        'twAppList
        '
        Me.twAppList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.twAppList.Location = New System.Drawing.Point(21, 130)
        Me.twAppList.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.twAppList.Name = "twAppList"
        Me.twAppList.ShowPlusMinus = False
        Me.twAppList.Size = New System.Drawing.Size(954, 618)
        Me.twAppList.TabIndex = 7
        '
        'cbRunAdmin
        '
        Me.cbRunAdmin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbRunAdmin.AutoSize = True
        Me.cbRunAdmin.Enabled = False
        Me.cbRunAdmin.Location = New System.Drawing.Point(990, 135)
        Me.cbRunAdmin.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cbRunAdmin.Name = "cbRunAdmin"
        Me.cbRunAdmin.Size = New System.Drawing.Size(174, 28)
        Me.cbRunAdmin.TabIndex = 8
        Me.cbRunAdmin.Text = "尝试提权(&A)"
        Me.cbRunAdmin.UseVisualStyleBackColor = True
        '
        'cbRunUser
        '
        Me.cbRunUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbRunUser.AutoSize = True
        Me.cbRunUser.Enabled = False
        Me.cbRunUser.Location = New System.Drawing.Point(990, 178)
        Me.cbRunUser.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.cbRunUser.Name = "cbRunUser"
        Me.cbRunUser.Size = New System.Drawing.Size(174, 28)
        Me.cbRunUser.TabIndex = 9
        Me.cbRunUser.Text = "尝试降权(&U)"
        Me.cbRunUser.UseVisualStyleBackColor = True
        Me.cbRunUser.Visible = False
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1171, 771)
        Me.Controls.Add(Me.cbRunUser)
        Me.Controls.Add(Me.cbRunAdmin)
        Me.Controls.Add(Me.twAppList)
        Me.Controls.Add(Me.cbWorkFolder)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.lblWorkFolder)
        Me.Controls.Add(Me.btnViewWorkFolder)
        Me.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
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
