
Imports System.Xml

Public Class frmMain
    <Runtime.InteropServices.StructLayout(Runtime.InteropServices.LayoutKind.Sequential)>
    Private Structure SHFILEINFO
        Public hIcon As IntPtr
        Public iIcon As Integer
        Public dwAttributes As UInteger
        <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=260)>
        Public szDisplayName As String
        <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=80)>
        Public szTypeName As String
    End Structure

    <Runtime.InteropServices.DllImport("shell32.dll", CharSet:=Runtime.InteropServices.CharSet.Auto)>
    Private Shared Function SHGetFileInfo(pszPath As String, dwFileAttributes As UInteger, ByRef psfi As SHFILEINFO, cbFileInfo As UInteger, uFlags As UInteger) As IntPtr
    End Function

    <Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function DestroyIcon(hIcon As IntPtr) As Boolean
    End Function

    <Runtime.InteropServices.DllImport("shell32.dll", CharSet:=Runtime.InteropServices.CharSet.Auto)>
    Private Shared Function ExtractIconEx(pszFile As String, nIconIndex As Integer, ByRef phiconLarge As IntPtr, ByRef phiconSmall As IntPtr, nIcons As UInteger) As UInteger
    End Function

    Private Const SHGFI_ICON As UInteger = &H100
    Private Const SHGFI_SMALLICON As UInteger = &H1
    Private Const SHGFI_USEFILEATTRIBUTES As UInteger = &H10
    Private Const FILE_ATTRIBUTE_DIRECTORY As UInteger = &H10
    Private Const FILE_ATTRIBUTE_NORMAL As UInteger = &H80

    Private Function GetSystemIconForPath(path As String, isFolder As Boolean) As Drawing.Icon
        Try
            Dim shfi As SHFILEINFO = New SHFILEINFO()
            Dim flags As UInteger = SHGFI_ICON Or SHGFI_SMALLICON
            Dim attr As UInteger = FILE_ATTRIBUTE_NORMAL
            If isFolder Then
                attr = FILE_ATTRIBUTE_DIRECTORY
                flags = flags Or SHGFI_USEFILEATTRIBUTES
            End If
            Dim ret As IntPtr = SHGetFileInfo(path, attr, shfi, CUInt(Runtime.InteropServices.Marshal.SizeOf(GetType(SHFILEINFO))), flags)
            If shfi.hIcon <> IntPtr.Zero Then
                Dim ico As Drawing.Icon = Drawing.Icon.FromHandle(shfi.hIcon).Clone()
                DestroyIcon(shfi.hIcon)
                Return ico
            End If
        Catch
        End Try
        Return Nothing
    End Function

    Private Function GetIconFromFileWithIndex(filePath As String, index As Integer) As Drawing.Icon
        Try
            Dim hLarge As IntPtr = IntPtr.Zero
            Dim hSmall As IntPtr = IntPtr.Zero
            Dim got = ExtractIconEx(filePath, index, hLarge, hSmall, 1)
            If hSmall <> IntPtr.Zero Then
                Dim ico As Drawing.Icon = Drawing.Icon.FromHandle(hSmall).Clone()
                DestroyIcon(hSmall)
                If hLarge <> IntPtr.Zero Then DestroyIcon(hLarge)
                Return ico
            ElseIf hLarge <> IntPtr.Zero Then
                Dim ico2 As Drawing.Icon = Drawing.Icon.FromHandle(hLarge).Clone()
                DestroyIcon(hLarge)
                Return ico2
            End If
        Catch
        End Try
        Return Nothing
    End Function

    Private Function GetCustomFolderIcon(folderPath As String) As Drawing.Icon
        Try
            Dim iniPath = System.IO.Path.Combine(folderPath, "desktop.ini")
            If Not System.IO.File.Exists(iniPath) Then Return Nothing

            Dim lines() As String = System.IO.File.ReadAllLines(iniPath, System.Text.Encoding.Default)
            Dim inShellClass As Boolean = False
            Dim iconFile As String = Nothing
            Dim iconIndex As Integer = 0
            For Each raw In lines
                Dim line = raw.Trim()
                If line.StartsWith("[") Then
                    inShellClass = line.Equals("[.ShellClassInfo]", StringComparison.OrdinalIgnoreCase)
                    Continue For
                End If
                If Not inShellClass Then Continue For
                If line.StartsWith("IconResource", StringComparison.OrdinalIgnoreCase) OrElse line.StartsWith("IconFile", StringComparison.OrdinalIgnoreCase) Then
                    Dim parts() As String = line.Split(New Char() {"="c}, 2)
                    If parts.Length >= 2 Then
                        Dim val = parts(1).Trim().Trim(""""c)
                        ' IconResource may be "file,index"
                        If val.Contains(",") Then
                            Dim p = val.Split(New Char() {","c}, 2)
                            iconFile = System.Environment.ExpandEnvironmentVariables(p(0).Trim())
                            Integer.TryParse(p(1).Trim(), iconIndex)
                        Else
                            iconFile = System.Environment.ExpandEnvironmentVariables(val)
                        End If
                    End If
                ElseIf line.StartsWith("IconIndex", StringComparison.OrdinalIgnoreCase) Then
                    Dim parts() As String = line.Split(New Char() {"="c}, 2)
                    If parts.Length >= 2 Then Integer.TryParse(parts(1).Trim(), iconIndex)
                End If
            Next

            If String.IsNullOrEmpty(iconFile) Then Return Nothing
            ' If relative path, combine with folder
            If Not System.IO.Path.IsPathRooted(iconFile) Then
                iconFile = System.IO.Path.Combine(folderPath, iconFile)
            End If
            iconFile = System.IO.Path.GetFullPath(iconFile)
            If Not System.IO.File.Exists(iconFile) Then Return Nothing

            Dim ext = System.IO.Path.GetExtension(iconFile)
            If String.Equals(ext, ".ico", StringComparison.OrdinalIgnoreCase) Then
                Try
                    Return New Drawing.Icon(iconFile)
                Catch
                End Try
            End If

            ' Try extract by index
            Dim icoExtract = GetIconFromFileWithIndex(iconFile, iconIndex)
            If icoExtract IsNot Nothing Then Return icoExtract

            ' Fallback: Extract associated icon
            Try
                Dim assoc = Drawing.Icon.ExtractAssociatedIcon(iconFile)
                If assoc IsNot Nothing Then Return assoc
            Catch
            End Try
        Catch
        End Try
        Return Nothing
    End Function
    Private configPath As String = System.IO.Path.Combine(Application.StartupPath, "Config.xml")
    Private lastSavedWorkFolder As String = String.Empty
    Private notifyIcon As NotifyIcon
    Private sharedImgList As ImageList = Nothing
    Private sharedIconKeyMap As New System.Collections.Generic.Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
    Private allowExit As Boolean = False
    ' 应用运行时的配置：是否尝试以管理员运行
    Private appAdminMap As New System.Collections.Generic.Dictionary(Of String, Boolean)(StringComparer.OrdinalIgnoreCase)
    Private suppressCbRunAdminEvents As Boolean = False
    ' 记录用户上一次明确设置的勾选状态（用于在文件夹显示为 Indeterminate 时，点击切换到上一次的相反状态）

    Private Sub btnViewWorkFolder_Click(sender As Object, e As EventArgs) Handles btnViewWorkFolder.Click
        Using dlg As New FolderBrowserDialog()
            dlg.Description = "请选择工作文件夹"
            dlg.ShowNewFolderButton = True
            ' 如果 cbWorkFolder 已有值并且目录存在，则默认打开到该目录
            If Not cbWorkFolder.Text = "" AndAlso System.IO.Directory.Exists(cbWorkFolder.Text) Then

                dlg.SelectedPath = cbWorkFolder.Text
            End If
            If dlg.ShowDialog() = DialogResult.OK Then
                Dim path As String = dlg.SelectedPath
                If Not cbWorkFolder.Items.Contains(path) Then
                    cbWorkFolder.Items.Insert(0, path)
                End If
                cbWorkFolder.Text = path
                ' 浏览操作（用户已确认）后，检测并保存配置变化
                If Not String.Equals(cbWorkFolder.Text, lastSavedWorkFolder, StringComparison.OrdinalIgnoreCase) Then
                    SaveConfig(cbWorkFolder.Text)
                    ' 浏览确认后自动刷新
                    btnRefresh.PerformClick()
                End If
            End If
        End Using

    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim root As String = cbWorkFolder.Text
        If root = "" OrElse Not System.IO.Directory.Exists(root) Then
            MessageBox.Show("请选择有效的工作文件夹后再刷新。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        twAppList.BeginUpdate()
        Try
            twAppList.Nodes.Clear()

            ' 创建或重用图片列表用于图标
            If sharedImgList Is Nothing Then
                sharedImgList = New ImageList()
                sharedImgList.ImageSize = New Drawing.Size(16, 16)
                sharedImgList.ColorDepth = ColorDepth.Depth32Bit
            Else
                sharedImgList.Images.Clear()
                ' 清除与 ImageList 对应的键缓存，避免使用已失效的键导致图标丢失
                sharedIconKeyMap.Clear()
            End If
            twAppList.ImageList = sharedImgList

            ' 添加系统文件夹图标作为默认文件夹图标
            Dim folderIconKey As String = "__sys_folder__"
            Dim folderIco = GetSystemIconForPath(root, True)
            If folderIco IsNot Nothing Then
                If Not sharedImgList.Images.ContainsKey(folderIconKey) Then
                    sharedImgList.Images.Add(folderIconKey, folderIco)
                End If
            End If
            ' 添加空白图标用于目标不存在或无图标的情况
            Dim blankIconKey As String = "__blank__"
            If Not sharedImgList.Images.ContainsKey(blankIconKey) Then
                Dim bmpBlank As New Drawing.Bitmap(sharedImgList.ImageSize.Width, sharedImgList.ImageSize.Height)
                Using g As Drawing.Graphics = Drawing.Graphics.FromImage(bmpBlank)
                    g.Clear(Drawing.Color.Transparent)
                End Using
                sharedImgList.Images.Add(blankIconKey, bmpBlank)
            End If

            ' 用来去重同一图标
            Dim iconKeyMap As System.Collections.Generic.Dictionary(Of String, String) = sharedIconKeyMap

            ' 获取所有 .lnk 文件
            Dim files() As String = System.IO.Directory.GetFiles(root, "*.lnk", System.IO.SearchOption.AllDirectories)

            ' 缓存已创建的文件夹节点（键为相对路径）
            Dim folderNodeMap As New System.Collections.Generic.Dictionary(Of String, TreeNode)(StringComparer.OrdinalIgnoreCase)

            For Each linkPath In files
                Dim folder As String = System.IO.Path.GetDirectoryName(linkPath)
                Dim relFolder As String = If(folder.StartsWith(root, StringComparison.OrdinalIgnoreCase), folder.Substring(root.Length).TrimStart("\"c), folder)
                If relFolder = String.Empty Then relFolder = "."

                ' 确保对应的文件夹节点存在，按层级创建
                Dim segments() As String = relFolder.Split(New Char() {System.IO.Path.DirectorySeparatorChar}, StringSplitOptions.RemoveEmptyEntries)
                Dim currentPath As String = String.Empty
                Dim parentNode As TreeNode = Nothing
                For Each seg In segments
                    currentPath = If(currentPath = String.Empty, seg, currentPath & System.IO.Path.DirectorySeparatorChar & seg)
                    Dim node As TreeNode = Nothing
                    If folderNodeMap.ContainsKey(currentPath) Then
                        node = folderNodeMap(currentPath)
                    Else
                        node = New TreeNode(seg)
                        Dim nodeFolderPath = System.IO.Path.Combine(root, currentPath)
                        node.Tag = nodeFolderPath
                        ' 尝试读取 desktop.ini 指定的自定义图标
                        Try
                            Dim lower = nodeFolderPath.ToLowerInvariant()
                            If Not sharedIconKeyMap.ContainsKey(lower) Then
                                Dim customIco = GetCustomFolderIcon(nodeFolderPath)
                                If customIco IsNot Nothing Then
                                    Dim folderKey = "f_" & Math.Abs(nodeFolderPath.GetHashCode()).ToString()
                                    If Not sharedImgList.Images.ContainsKey(folderKey) Then
                                        sharedImgList.Images.Add(folderKey, customIco)
                                    End If
                                    sharedIconKeyMap(lower) = folderKey
                                    node.ImageKey = folderKey
                                    node.SelectedImageKey = folderKey
                                End If
                            Else
                                Dim k = sharedIconKeyMap(lower)
                                If sharedImgList.Images.ContainsKey(k) Then
                                    node.ImageKey = k
                                    node.SelectedImageKey = k
                                End If
                            End If
                        Catch
                        End Try
                        If parentNode Is Nothing Then
                            twAppList.Nodes.Add(node)
                        Else
                            parentNode.Nodes.Add(node)
                        End If
                        folderNodeMap(currentPath) = node
                    End If
                    parentNode = node
                Next

                ' 如果 relFolder 为 "."（根），parentNode 可能为 Nothing
                Dim containerNode As TreeNode = If(parentNode, Nothing)
                If relFolder = "." Then
                    ' 使用根节点容器，创建或取名为 "."
                    If Not folderNodeMap.ContainsKey(".") Then
                        Dim rootNode As New TreeNode(".")
                        rootNode.Tag = root
                        twAppList.Nodes.Add(rootNode)
                        folderNodeMap(".") = rootNode
                    End If
                    containerNode = folderNodeMap(".")
                End If

                Dim displayName = System.IO.Path.GetFileNameWithoutExtension(linkPath)
                Dim key As String = ""
                Dim imageKey As String = ""
                Try
                    ' 使用 WScript.Shell 读取快捷方式目标
                    Dim wsh = CreateObject("WScript.Shell")
                    Dim sc = wsh.CreateShortcut(linkPath)
                    Dim target As String = If(sc.TargetPath, String.Empty)
                    If Not String.IsNullOrEmpty(target) Then
                        If System.IO.Directory.Exists(target) Then
                            imageKey = folderIconKey
                        ElseIf System.IO.File.Exists(target) Then
                            key = target.ToLowerInvariant()
                            If Not iconKeyMap.ContainsKey(key) Then
                                Try
                                    Dim ico As Drawing.Icon = GetSystemIconForPath(target, False)
                                    If ico IsNot Nothing Then
                                        iconKeyMap(key) = key
                                        sharedImgList.Images.Add(key, ico)
                                        imageKey = key
                                    Else
                                        ' 目标文件存在但无法取得图标，使用空白图标
                                        imageKey = blankIconKey
                                    End If
                                Catch
                                    imageKey = blankIconKey
                                End Try
                            Else
                                imageKey = iconKeyMap(key)
                            End If
                        Else
                            ' 目标不存在，使用空白图标
                            imageKey = blankIconKey
                        End If
                    Else
                        ' 无目标，使用空白图标
                        imageKey = blankIconKey
                    End If
                Catch
                    ' 无法解析快捷方式，继续
                End Try

                Dim fileNode As New TreeNode(displayName)
                fileNode.Tag = linkPath
                If imageKey <> String.Empty AndAlso sharedImgList.Images.ContainsKey(imageKey) Then
                    fileNode.ImageKey = imageKey
                    fileNode.SelectedImageKey = imageKey
                End If

                If containerNode Is Nothing Then
                    twAppList.Nodes.Add(fileNode)
                Else
                    containerNode.Nodes.Add(fileNode)
                End If
            Next

            ' 为所有未设置图标的文件夹节点设置默认文件夹图标（如果存在）
            For Each kvp In folderNodeMap
                Dim fn As TreeNode = kvp.Value
                If (String.IsNullOrEmpty(fn.ImageKey) OrElse Not sharedImgList.Images.ContainsKey(fn.ImageKey)) AndAlso sharedImgList.Images.ContainsKey(folderIconKey) Then
                    fn.ImageKey = folderIconKey
                    fn.SelectedImageKey = folderIconKey
                End If
            Next

            ' 刷新完成
            ' 对 TreeView 节点按名称排序（对子节点递归处理），然后展开并同时填充右键菜单
            Try
                SortTreeNodes(twAppList.Nodes)
            Catch
            End Try
            twAppList.ExpandAll()
            PopulateContextMenuFromTree()
            ' 刷新完成后，检测配置变化并保存
            If Not String.Equals(cbWorkFolder.Text, lastSavedWorkFolder, StringComparison.OrdinalIgnoreCase) Then
                SaveConfig(cbWorkFolder.Text)
            End If
        Finally
            twAppList.EndUpdate()
        End Try
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadConfig()
        ' 默认禁用 cbRunAdmin，避免对初始状态误操作；启用三态以支持文件夹混合状态显示
        Try
            cbRunAdmin.Enabled = False
            cbRunAdmin.ThreeState = True
        Catch
        End Try
        ' 初始化托盘图标
        Try
            notifyIcon = New NotifyIcon()
            notifyIcon.Icon = Me.Icon
            notifyIcon.Visible = True
            notifyIcon.ContextMenuStrip = muRightClick
            AddHandler notifyIcon.MouseClick, AddressOf NotifyIcon_MouseClick
            ' 确保即使在应用列表为空时也有退出菜单
            Try
                PopulateContextMenuFromTree()
            Catch
            End Try
        Catch
        End Try

        If Not String.IsNullOrEmpty(cbWorkFolder.Text) AndAlso System.IO.Directory.Exists(cbWorkFolder.Text) Then
            ' 自动刷新一次
            btnRefresh.PerformClick()
        End If

        ' 启动参数：若包含 -s 则启动后最小化到托盘
        Try
            Dim args = Environment.GetCommandLineArgs()
            For i = 0 To args.Length - 1
                If String.Equals(args(i), "-s", StringComparison.OrdinalIgnoreCase) Then
                    ' 延迟执行最小化到托盘，确保窗体加载完成
                    Me.BeginInvoke(New MethodInvoker(Sub()
                                                         Try
                                                             MinimizeToTray()
                                                         Catch
                                                         End Try
                                                     End Sub))
                    Exit For
                End If
            Next
        Catch
        End Try

    End Sub

    Private Sub NotifyIcon_MouseClick(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            ' 显示主窗体（使用 Invoke 确保在 UI 线程执行）
            Try
                Me.BeginInvoke(New MethodInvoker(Sub()
                                                     Try
                                                         RestoreFromTray()
                                                     Catch
                                                     End Try
                                                 End Sub))
            Catch
            End Try
        End If
        ' 右键菜单由 ContextMenuStrip 处理
    End Sub

    Private Sub twAppList_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles twAppList.AfterSelect
        ' 当用户选择节点时，尝试加载该项目的提权设置到 cbRunAdmin
        Try
            Dim tn As TreeNode = e.Node
            If tn Is Nothing Then Return
            suppressCbRunAdminEvents = True
            Try
                ' 默认：当有节点被选中时启用 cbRunAdmin
                cbRunAdmin.Enabled = True

                ' 判断是否为文件：优先认为叶子节点为文件，或使用 IsNodeAFile 进一步确认
                Dim isFile As Boolean = False
                Try
                    If tn.Nodes IsNot Nothing AndAlso tn.Nodes.Count = 0 Then isFile = True
                    If Not isFile AndAlso IsNodeAFile(tn) Then isFile = True
                Catch
                End Try

                If isFile Then
                    ' 文件：解析目标并显示其状态（使用 CheckState 明确设置，避免遗留的 Indeterminate）
                    Dim resolvedTarget As String = ResolveTargetFromNode(tn)
                    If String.IsNullOrEmpty(resolvedTarget) Then
                        Dim tagStr = TryCast(tn.Tag, String)
                        If Not String.IsNullOrEmpty(tagStr) AndAlso System.IO.File.Exists(tagStr) Then
                            resolvedTarget = tagStr
                        End If
                    End If
                    Dim val As Boolean = False
                    If Not String.IsNullOrEmpty(resolvedTarget) AndAlso appAdminMap.TryGetValue(resolvedTarget, val) Then
                        cbRunAdmin.CheckState = If(val, CheckState.Checked, CheckState.Unchecked)
                        cbRunAdmin.CheckState = If(val, CheckState.Checked, CheckState.Unchecked)
                    Else
                        cbRunAdmin.CheckState = CheckState.Unchecked
                    End If
                Else
                    ' 文件夹：检查子节点状态，设置三态显示
                    Dim targets = GetTargetsUnderNode(tn)
                    Dim anyChecked As Boolean = False
                    Dim anyUnchecked As Boolean = False
                    For Each t In targets
                        Dim v As Boolean = False
                        If appAdminMap.TryGetValue(t, v) Then
                            If v Then anyChecked = True Else anyUnchecked = True
                        Else
                            anyUnchecked = True
                        End If
                        If anyChecked AndAlso anyUnchecked Then Exit For
                    Next
                    If anyChecked AndAlso anyUnchecked Then
                        cbRunAdmin.CheckState = CheckState.Indeterminate
                    ElseIf anyChecked Then
                        cbRunAdmin.CheckState = CheckState.Checked
                    Else
                        cbRunAdmin.CheckState = CheckState.Unchecked
                    End If
                End If
            Finally
                suppressCbRunAdminEvents = False
            End Try
        Catch
        End Try
    End Sub

    Private Function ResolveTargetFromNode(tn As TreeNode) As String
        Try
            Dim tag = TryCast(tn.Tag, String)
            If String.IsNullOrEmpty(tag) Then Return String.Empty
            If String.Equals(System.IO.Path.GetExtension(tag), ".lnk", StringComparison.OrdinalIgnoreCase) Then
                Try
                    Dim wsh = CreateObject("WScript.Shell")
                    Dim sc = wsh.CreateShortcut(tag)
                    If sc IsNot Nothing Then
                        Return If(sc.TargetPath, String.Empty)
                    End If
                Catch
                End Try
            End If
            ' 如果 tag 本身是可执行或文件则返回
            If System.IO.File.Exists(tag) Then Return tag
        Catch
        End Try
        Return String.Empty
    End Function

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' 如果不是通过退出菜单触发的关闭，则取消关闭并隐藏到托盘
        If Not allowExit Then
            e.Cancel = True
            Try
                MinimizeToTray()
            Catch
            End Try
            Return
        End If

        Try
            If notifyIcon IsNot Nothing Then
                notifyIcon.Visible = False
                notifyIcon.Dispose()
                notifyIcon = Nothing
            End If
        Catch
        End Try
    End Sub

    Private Sub frmMain_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Try
            If Me.WindowState = FormWindowState.Minimized Then
                MinimizeToTray()
            Else
                Me.ShowInTaskbar = True
                ' 恢复显示时展开所有节点
                Try
                    If twAppList IsNot Nothing Then twAppList.ExpandAll()
                Catch
                End Try
            End If
        Catch
        End Try
    End Sub

    Private Sub SortTreeNodes(nodes As TreeNodeCollection)
        Try
            ' 将子节点按其 Text 进行排序，使用不区分大小写的比较
            Dim list As New System.Collections.Generic.List(Of TreeNode)()
            For Each tn As TreeNode In nodes
                list.Add(tn)
            Next
            list.Sort(Function(a, b) String.Compare(a.Text, b.Text, StringComparison.OrdinalIgnoreCase))
            nodes.Clear()
            For Each tn2 In list
                nodes.Add(tn2)
            Next
            ' 对每个节点的子节点递归排序
            For Each tn As TreeNode In nodes
                If tn.Nodes IsNot Nothing AndAlso tn.Nodes.Count > 0 Then
                    SortTreeNodes(tn.Nodes)
                End If
            Next
        Catch
        End Try
    End Sub

    Private Sub PopulateContextMenuFromTree()
        muRightClick.Items.Clear()
        For Each tn As TreeNode In twAppList.Nodes
            AddNodeToMenu(tn, muRightClick.Items)
        Next
        ' 添加分隔符和退出菜单
        If muRightClick.Items.Count > 0 Then
            muRightClick.Items.Add(New ToolStripSeparator())
        End If
        Dim miExit As New ToolStripMenuItem("退出应用(&X)")
        AddHandler miExit.Click, AddressOf ExitMenu_Click
        muRightClick.Items.Add(miExit)

    End Sub

    ' 将主窗口最小化到托盘（隐藏任务栏图标并隐藏窗口）
    Private Sub MinimizeToTray()
        Try
            Me.ShowInTaskbar = False
            Me.Hide()
        Catch
        End Try
    End Sub

    ' 从托盘恢复主窗口
    Private Sub RestoreFromTray()
        Try
            Me.ShowInTaskbar = True
            Me.Show()
            Me.WindowState = FormWindowState.Normal
            Me.Visible = True
            Me.BringToFront()
            Me.Activate()
            ' 恢复时展开全部节点
            Try
                If twAppList IsNot Nothing Then twAppList.ExpandAll()
            Catch
            End Try
        Catch
        End Try
    End Sub

    Private Sub ExitMenu_Click()
        ' 标记允许退出，然后关闭窗体（将触发 FormClosing）
        allowExit = True
        Me.Close()
    End Sub


    Private Sub AddNodeToMenu(tn As TreeNode, items As ToolStripItemCollection)
        Dim menu As ToolStripMenuItem = New ToolStripMenuItem(tn.Text)
        ' 设定图标（如果有）
        If Not String.IsNullOrEmpty(tn.ImageKey) AndAlso sharedImgList IsNot Nothing AndAlso sharedImgList.Images.ContainsKey(tn.ImageKey) Then
            menu.Image = sharedImgList.Images(tn.ImageKey)
        End If

        If tn.Nodes.Count > 0 Then
            ' 文件夹
            For Each child As TreeNode In tn.Nodes
                AddNodeToMenu(child, menu.DropDownItems)
            Next
            items.Add(menu)
        Else
            ' 叶子节点，快速方式项
            menu.Tag = tn.Tag
            AddHandler menu.Click, AddressOf ContextMenuItem_Click
            items.Add(menu)
        End If
    End Sub

    Private Sub ContextMenuItem_Click(sender As Object, e As EventArgs)
        Dim mi = TryCast(sender, ToolStripMenuItem)
        If mi Is Nothing Then Return
        Dim tag = TryCast(mi.Tag, String)
        If String.IsNullOrEmpty(tag) Then Return
        ' 优先尝试通过 mdUACRun 强制降权启动（解析 .lnk 获取目标、参数和工作目录）
        Try
            Dim launchPath As String = tag
            Dim launchArgs As String = String.Empty
            Dim launchWorkDir As String = String.Empty

            If String.Equals(System.IO.Path.GetExtension(tag), ".lnk", StringComparison.OrdinalIgnoreCase) Then
                Try
                    Dim wsh = CreateObject("WScript.Shell")
                    Dim sc = wsh.CreateShortcut(tag)
                    If sc IsNot Nothing Then
                        launchPath = If(sc.TargetPath, String.Empty)
                        launchArgs = If(sc.Arguments, String.Empty)
                        launchWorkDir = If(sc.WorkingDirectory, If(String.IsNullOrEmpty(sc.WorkingDirectory), System.IO.Path.GetDirectoryName(If(sc.TargetPath, String.Empty)), sc.WorkingDirectory))
                    End If
                Catch
                End Try
            End If

            ' 尝试使用 mdUACRun 启动（根据配置决定是否尝试提权或降权）
            If Not String.IsNullOrEmpty(launchPath) Then
                Try
                    Dim runMode As mdUACRun.RunMode = mdUACRun.RunMode.ForceDemote强制降权
                    Try
                        Dim tryVal As Boolean = False
                        If appAdminMap.TryGetValue(launchPath, tryVal) Then
                            If tryVal Then
                                runMode = mdUACRun.RunMode.UseCurrent当前权限
                            Else
                                runMode = mdUACRun.RunMode.ForceDemote强制降权
                            End If
                        End If
                    Catch
                    End Try

                    mdUACRun.RunApp(launchPath, launchArgs, launchWorkDir, runMode)
                    Return
                Catch
                    ' 启动失败，继续回退逻辑
                End Try
            End If

            ' 回退：若解析得到目标则使用 Process.Start(path, args)，否则直接尝试 Process.Start(tag)（支持 .lnk）
            Try
                If Not String.IsNullOrEmpty(launchPath) AndAlso (Not String.Equals(launchPath, tag, StringComparison.OrdinalIgnoreCase) OrElse Not String.IsNullOrEmpty(launchArgs)) Then
                    System.Diagnostics.Process.Start(launchPath, launchArgs)
                Else
                    System.Diagnostics.Process.Start(tag)
                End If
                Return
            Catch
            End Try

            ' 最后使用 WScript.Shell.Run 作为后备（组合路径与参数）
            Try
                Dim wsh2 = CreateObject("WScript.Shell")
                If Not String.IsNullOrEmpty(launchPath) Then
                    Dim cmd = """" & launchPath & """"
                    If Not String.IsNullOrEmpty(launchArgs) Then cmd &= " " & launchArgs
                    wsh2.Run(cmd)
                Else
                    wsh2.Run("""" & tag & """")
                End If
            Catch
            End Try
        Catch
            ' 忽略所有启动错误
        End Try
    End Sub

    Private Sub LoadConfig()
        Try
            appAdminMap.Clear()
            If System.IO.File.Exists(configPath) Then
                Dim xd As New System.Xml.XmlDocument()
                xd.Load(configPath)
                Dim node As System.Xml.XmlNode = xd.SelectSingleNode("/Config/WorkFolder")
                If node IsNot Nothing Then
                    Dim path = node.InnerText
                    If Not String.IsNullOrEmpty(path) Then
                        cbWorkFolder.Text = path
                        lastSavedWorkFolder = path
                    End If
                End If
                ' 读取每个 App 的设置
                Dim appNodes As System.Xml.XmlNodeList = xd.SelectNodes("/Config/AppSettings/App")
                If appNodes IsNot Nothing Then
                    For Each an As System.Xml.XmlNode In appNodes
                        Dim pathAttr = an.Attributes?.GetNamedItem("Path")
                        If pathAttr Is Nothing Then Continue For
                        Dim p = pathAttr.Value
                        Dim raNode = an.SelectSingleNode("RunAdmin")
                        Dim val As Boolean = False
                        If raNode IsNot Nothing Then
                            If raNode.InnerText = "1" OrElse String.Equals(raNode.InnerText, "true", StringComparison.OrdinalIgnoreCase) Then
                                val = True
                            End If
                        End If
                        If Not String.IsNullOrEmpty(p) Then appAdminMap(p) = val
                    Next
                End If
            End If
        Catch ex As Exception
            ' 忽略配置读取错误
        End Try
    End Sub

    Private Sub cbRunAdmin_CheckedChanged(sender As Object, e As EventArgs) Handles cbRunAdmin.CheckedChanged
        If suppressCbRunAdminEvents Then Return
        Try
            Dim tn As TreeNode = twAppList.SelectedNode
            If tn Is Nothing Then Return

            ' 在任何选择时都启用 cbRunAdmin（UI 状态由 AfterSelect 设置）
            Try
                cbRunAdmin.Enabled = True
            Catch
            End Try

            ' 以 CheckState 决定目标状态；Checked -> True；Unchecked/Indeterminate -> False
            Dim newVal As Boolean = (cbRunAdmin.CheckState = CheckState.Checked)

            Dim isFile As Boolean = IsNodeAFile(tn)

            If isFile Then
                ' 选中文件时：仅应用到该文件自身
                Dim resolvedTarget As String = ResolveTargetFromNode(tn)
                If String.IsNullOrEmpty(resolvedTarget) Then
                    Dim tagStr = TryCast(tn.Tag, String)
                    If Not String.IsNullOrEmpty(tagStr) AndAlso System.IO.File.Exists(tagStr) Then
                        resolvedTarget = tagStr
                    End If
                End If
                If Not String.IsNullOrEmpty(resolvedTarget) Then
                    appAdminMap(resolvedTarget) = newVal
                    SaveConfig()
                End If
            Else
                ' 选中的是文件夹：应用到该文件夹下所有文件
                Dim targets = GetTargetsUnderNode(tn)
                For Each t In targets
                    If Not String.IsNullOrEmpty(t) Then appAdminMap(t) = newVal
                Next
                SaveConfig()
            End If
        Catch
        End Try
    End Sub

    ' 当处于 Indeterminate 时，用户点击应直接取消勾选（设为 Unchecked）
    Private Sub cbRunAdmin_Click(sender As Object, e As EventArgs) Handles cbRunAdmin.Click
        Try
            If cbRunAdmin.CheckState = CheckState.Indeterminate Then
                cbRunAdmin.CheckState = CheckState.Unchecked
            End If
        Catch
        End Try
    End Sub

    Private Function IsNodeAFile(tn As TreeNode) As Boolean
        Try
            If tn Is Nothing Then Return False
            Dim tag = TryCast(tn.Tag, String)
            If Not String.IsNullOrEmpty(tag) Then
                If String.Equals(System.IO.Path.GetExtension(tag), ".lnk", StringComparison.OrdinalIgnoreCase) Then
                    Try
                        Dim wsh = CreateObject("WScript.Shell")
                        Dim sc = wsh.CreateShortcut(tag)
                        If sc IsNot Nothing Then
                            If Not String.IsNullOrEmpty(sc.TargetPath) AndAlso System.IO.File.Exists(sc.TargetPath) Then
                                Return True
                            End If
                        End If
                    Catch
                    End Try
                ElseIf System.IO.File.Exists(tag) Then
                    Return True
                End If
            End If
        Catch
        End Try
        Return False
    End Function

    Private Function IsDirectoryNode(tn As TreeNode) As Boolean
        Try
            If tn Is Nothing Then Return False
            Dim tag = TryCast(tn.Tag, String)
            If Not String.IsNullOrEmpty(tag) AndAlso System.IO.Directory.Exists(tag) Then Return True
        Catch
        End Try
        Return False
    End Function

    Private Function GetTargetsUnderNode(tn As TreeNode) As System.Collections.Generic.List(Of String)
        Dim result As New System.Collections.Generic.List(Of String)()
        Try
            If tn Is Nothing Then Return result
            ' 如果节点自身是文件，则返回自身的 target
            If IsNodeAFile(tn) Then
                Dim t = ResolveTargetFromNode(tn)
                If Not String.IsNullOrEmpty(t) Then result.Add(t)
                Return result
            End If
            ' 否则递归遍历子节点，收集所有文件目标
            For Each child As TreeNode In tn.Nodes
                If IsNodeAFile(child) Then
                    Dim t = ResolveTargetFromNode(child)
                    If Not String.IsNullOrEmpty(t) Then result.Add(t)
                Else
                    Dim sublist = GetTargetsUnderNode(child)
                    For Each s In sublist
                        result.Add(s)
                    Next
                End If
            Next
        Catch
        End Try
        Return result
    End Function

    Private Sub SaveConfig(Optional workFolder As String = Nothing)
        Try
            If Not String.IsNullOrEmpty(workFolder) Then lastSavedWorkFolder = workFolder
            Dim xd As New System.Xml.XmlDocument()
            Dim decl = xd.CreateXmlDeclaration("1.0", "utf-8", Nothing)
            xd.AppendChild(decl)
            Dim root = xd.CreateElement("Config")
            xd.AppendChild(root)
            Dim wf = xd.CreateElement("WorkFolder")
            wf.InnerText = lastSavedWorkFolder
            root.AppendChild(wf)
            ' 写入 AppSettings
            Dim appsNode = xd.CreateElement("AppSettings")
            For Each kvp In appAdminMap
                Dim appNode = xd.CreateElement("App")
                Dim attr = xd.CreateAttribute("Path")
                attr.Value = kvp.Key
                appNode.Attributes.SetNamedItem(attr)
                Dim ra = xd.CreateElement("RunAdmin")
                ra.InnerText = If(kvp.Value, "1", "0")
                appNode.AppendChild(ra)
                appsNode.AppendChild(appNode)
            Next
            root.AppendChild(appsNode)
            xd.Save(configPath)
            ' lastSavedWorkFolder 已在开始时更新
        Catch ex As Exception
            ' 忽略保存错误或提示用户（此处静默）
        End Try
    End Sub

End Class
