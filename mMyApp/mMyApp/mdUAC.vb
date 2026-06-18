Imports System.Runtime.InteropServices
Imports System.Diagnostics
Imports System.IO

''' <summary>
''' UAC 降权辅助模块（仅提供从高权限进程以普通权限启动子进程的功能）
''' </summary>
Public Module mdUAC


    ' ---------- API 常量 ----------
    Private Const TOKEN_DUPLICATE As UInteger = &H2
    Private Const TOKEN_QUERY As UInteger = &H8
    Private Const TOKEN_ASSIGN_PRIMARY As UInteger = &H1
    Private Const TOKEN_ADJUST_DEFAULT As UInteger = &H80
    Private Const TOKEN_ADJUST_SESSIONID As UInteger = &H100

    Private Const PROCESS_QUERY_INFORMATION As UInteger = &H400

    Private Const SecurityImpersonation As Integer = 2
    Private Const TokenPrimary As Integer = 1

    Private Const CREATE_NEW_CONSOLE As UInteger = &H10

    ' ---------- 结构体 ----------
    Private Structure STARTUPINFO
        Public cb As Integer
        Public lpReserved As String
        Public lpDesktop As String
        Public lpTitle As String
        Public dwX As Integer
        Public dwY As Integer
        Public dwXSize As Integer
        Public dwYSize As Integer
        Public dwXCountChars As Integer
        Public dwYCountChars As Integer
        Public dwFillAttribute As Integer
        Public dwFlags As Integer
        Public wShowWindow As Short
        Public cbReserved2 As Short
        Public lpReserved2 As IntPtr
        Public hStdInput As IntPtr
        Public hStdOutput As IntPtr
        Public hStdError As IntPtr
    End Structure

    Private Structure PROCESS_INFORMATION
        Public hProcess As IntPtr
        Public hThread As IntPtr
        Public dwProcessId As Integer
        Public dwThreadId As Integer
    End Structure

    ' ---------- API 函数声明 ----------
    <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
    Private Function CreateProcessWithTokenW(
        hToken As IntPtr,
        dwLogonFlags As UInteger,
        lpApplicationName As String,
        lpCommandLine As String,
        dwCreationFlags As UInteger,
        lpEnvironment As IntPtr,
        lpCurrentDirectory As String,
        ByRef lpStartupInfo As STARTUPINFO,
        ByRef lpProcessInformation As PROCESS_INFORMATION) As Boolean
    End Function

    <DllImport("advapi32.dll", SetLastError:=True)>
    Private Function DuplicateTokenEx(
        hExistingToken As IntPtr,
        dwDesiredAccess As UInteger,
        lpTokenAttributes As IntPtr,
        ImpersonationLevel As Integer,
        TokenType As Integer,
        ByRef phNewToken As IntPtr) As Boolean
    End Function

    <DllImport("advapi32.dll", SetLastError:=True)>
    Private Function OpenProcessToken(
        ProcessHandle As IntPtr,
        DesiredAccess As UInteger,
        ByRef TokenHandle As IntPtr) As Boolean
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Function OpenProcess(
        dwDesiredAccess As UInteger,
        bInheritHandle As Boolean,
        dwProcessId As Integer) As IntPtr
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Function CloseHandle(hObject As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Function GetShellWindow() As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Function GetWindowThreadProcessId(
        hWnd As IntPtr,
        ByRef lpdwProcessId As Integer) As Integer
    End Function

    Private Function TryLaunchAsUser(appPath As String, args As String, dir As String) As Boolean
        Dim userToken As IntPtr = IntPtr.Zero
        Dim primaryToken As IntPtr = IntPtr.Zero
        Dim envBlock As IntPtr = IntPtr.Zero
        Dim pi As PROCESS_INFORMATION
        Try
            Dim sessionId As UInteger = WTSGetActiveConsoleSessionId()
            If sessionId = &HFFFFFFFFUI Then
                Return False
            End If

            Dim ok As Boolean = WTSQueryUserToken(sessionId, userToken)
            If Not ok OrElse userToken = IntPtr.Zero Then
                Return False
            End If

            Dim desiredAccess As UInteger = TOKEN_QUERY Or TOKEN_DUPLICATE Or TOKEN_ASSIGN_PRIMARY Or TOKEN_ADJUST_DEFAULT Or TOKEN_ADJUST_SESSIONID
            ok = DuplicateTokenEx(userToken, desiredAccess, IntPtr.Zero, SecurityImpersonation, TokenPrimary, primaryToken)
            If Not ok OrElse primaryToken = IntPtr.Zero Then
                Return False
            End If

            ' 创建环境块
            If Not CreateEnvironmentBlock(envBlock, primaryToken, False) Then
                envBlock = IntPtr.Zero
            End If

            Dim si As New STARTUPINFO()
            si.cb = Marshal.SizeOf(GetType(STARTUPINFO))
            si.lpDesktop = "winsta0\default"

            Dim commandLine As String = If(String.IsNullOrEmpty(args), String.Format("""{0}""", appPath), String.Format("""{0}"""" {1}", appPath, args))

            Dim creationFlags As UInteger = CREATE_NEW_CONSOLE Or CREATE_UNICODE_ENVIRONMENT

            Dim success As Boolean = CreateProcessAsUserW(primaryToken, Nothing, commandLine, IntPtr.Zero, IntPtr.Zero, False, creationFlags, envBlock, dir, si, pi)
            If Not success Then
                Return False
            End If

            ' 关闭进程线程句柄
            If pi.hProcess <> IntPtr.Zero Then CloseHandle(pi.hProcess)
            If pi.hThread <> IntPtr.Zero Then CloseHandle(pi.hThread)

            Return True
        Finally
            If envBlock <> IntPtr.Zero Then DestroyEnvironmentBlock(envBlock)
            If primaryToken <> IntPtr.Zero Then CloseHandle(primaryToken)
            If userToken <> IntPtr.Zero Then CloseHandle(userToken)
        End Try
    End Function

    ' ---- CreateProcessWithTokenW 相关 P/Invoke ----

    Private Const PROCESS_VM_READ As UInteger = &H10
    Private Const PROCESS_QUERY_LIMITED_INFORMATION As UInteger = &H1000
    Private Const TOKEN_READ As UInteger = &H20008

    ' ---------- 公共方法 ----------

    ''' <summary>
    ''' 以普通用户权限启动一个程序（支持传递命令行参数和工作目录）
    ''' 原理：从当前会话的 explorer.exe 获取普通权限令牌，然后用该令牌创建进程
    ''' </summary>
    ''' <param name="appPath">可执行文件的完整路径</param>
    ''' <param name="args">命令行参数（可为空字符串）</param>
    ''' <param name="workingDirectory">工作目录（若为空，则使用 appPath 所在目录）</param>
    ''' <exception cref="Exception">启动失败时抛出，包含详细错误信息</exception>
    Public Sub StartAsNormalUser_Advanced(appPath As String,
                                          Optional args As String = "",
                                          Optional workingDirectory As String = "")
        Dim shellToken As IntPtr = IntPtr.Zero
        Dim primaryToken As IntPtr = IntPtr.Zero
        Dim shellProcessHandle As IntPtr = IntPtr.Zero

        Try
            ' 1. 获取 Shell 窗口 (explorer.exe) 的句柄
            Dim hShellWnd As IntPtr = GetShellWindow()
            If hShellWnd = IntPtr.Zero Then
                Throw New Exception("无法获取 Shell 窗口句柄。")
            End If

            ' 2. 获取 explorer.exe 的进程 ID
            Dim shellPid As Integer = 0
            GetWindowThreadProcessId(hShellWnd, shellPid)
            If shellPid = 0 Then
                Throw New Exception("无法获取 Shell 进程 ID。")
            End If

            ' 3. 打开 explorer.exe 进程（只请求查询信息权限）
            shellProcessHandle = OpenProcess(PROCESS_QUERY_INFORMATION, False, shellPid)
            If shellProcessHandle = IntPtr.Zero Then
                Throw New Exception("无法打开 Shell 进程。错误代码: " & Marshal.GetLastWin32Error())
            End If

            ' 4. 获取其访问令牌（此为模拟令牌）
            Dim success As Boolean = OpenProcessToken(shellProcessHandle, TOKEN_DUPLICATE, shellToken)
            If Not success Then
                Throw New Exception("无法获取 Shell 进程的令牌。错误代码: " & Marshal.GetLastWin32Error())
            End If

            ' 5. 将模拟令牌复制并转换为主令牌 (Primary Token)
            Dim desiredAccess As UInteger = TOKEN_QUERY Or
                                            TOKEN_ASSIGN_PRIMARY Or
                                            TOKEN_DUPLICATE Or
                                            TOKEN_ADJUST_DEFAULT Or
                                            TOKEN_ADJUST_SESSIONID
            success = DuplicateTokenEx(shellToken,
                                       desiredAccess,
                                       IntPtr.Zero,
                                       SecurityImpersonation,
                                       TokenPrimary,
                                       primaryToken)
            If Not success Then
                Throw New Exception("无法复制并转换令牌。错误代码: " & Marshal.GetLastWin32Error())
            End If

            ' 6. 准备启动信息
            Dim si As New STARTUPINFO()
            si.cb = Marshal.SizeOf(GetType(STARTUPINFO))
            Dim pi As New PROCESS_INFORMATION()

            ' 构建完整命令行（路径含空格时用引号包裹）
            Dim commandLine As String = """" & appPath & """"
            If Not String.IsNullOrEmpty(args) Then
                commandLine &= " " & args
            End If

            ' 确定工作目录（若未指定则使用 appPath 所在目录）
            Dim dir As String = workingDirectory
            If String.IsNullOrEmpty(dir) Then
                dir = Path.GetDirectoryName(appPath)
            End If

            ' 7. 使用主令牌创建进程
            success = CreateProcessWithTokenW(
                primaryToken,
                0,
                Nothing,                ' 应用程序名从 commandLine 解析
                commandLine,
                CREATE_NEW_CONSOLE,     ' 新进程创建新控制台窗口
                IntPtr.Zero,
                dir,
                si,
                pi)

            If Not success Then
                Throw New Exception("CreateProcessWithTokenW 失败。错误代码: " & Marshal.GetLastWin32Error())
            End If

            ' 关闭新进程的线程句柄（进程句柄可选关闭，此处关闭以避免句柄泄漏）
            CloseHandle(pi.hThread)
            CloseHandle(pi.hProcess)

        Catch ex As Exception
            Throw New Exception("高级降权启动失败: " & ex.Message)
        Finally
            ' 清理所有打开的资源
            If shellToken <> IntPtr.Zero Then CloseHandle(shellToken)
            If primaryToken <> IntPtr.Zero Then CloseHandle(primaryToken)
            If shellProcessHandle <> IntPtr.Zero Then CloseHandle(shellProcessHandle)
        End Try
    End Sub
    <System.Runtime.InteropServices.DllImport("shell32.dll", SetLastError:=True, CharSet:=System.Runtime.InteropServices.CharSet.Auto)>
    Private Function ShellExecuteEx(ByRef lpExecInfo As SHELLEXECUTEINFO) As Boolean
    End Function

    <System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet:=System.Runtime.InteropServices.CharSet.Auto)>
    Private Structure SHELLEXECUTEINFO
        Public cbSize As Integer
        Public fMask As UInteger
        Public hwnd As IntPtr
        <System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPTStr)> Public lpVerb As String
        <System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPTStr)> Public lpFile As String
        <System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPTStr)> Public lpParameters As String
        <System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPTStr)> Public lpDirectory As String
        Public nShow As Integer
        Public hInstApp As IntPtr
        Public lpIDList As IntPtr
        <System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPTStr)> Public lpClass As String
        Public hkeyClass As IntPtr
        Public dwHotKey As Integer
        Public hIcon As IntPtr
        Public hProcess As IntPtr
    End Structure

    Private Const SEE_MASK_NOCLOSEPROCESS As UInteger = &H40
    Private Const SW_SHOWNORMAL As Integer = 1
    ' ---- WTS / CreateProcessAsUser 降权启动支持 ----
    <System.Runtime.InteropServices.DllImport("wtsapi32.dll", SetLastError:=True)>
    Private Function WTSQueryUserToken(sessionId As UInteger, ByRef phToken As IntPtr) As Boolean
    End Function

    <System.Runtime.InteropServices.DllImport("kernel32.dll")>
    Private Function WTSGetActiveConsoleSessionId() As UInteger
    End Function

    <System.Runtime.InteropServices.DllImport("userenv.dll", SetLastError:=True)>
    Private Function CreateEnvironmentBlock(ByRef lpEnvironment As IntPtr, hToken As IntPtr, bInherit As Boolean) As Boolean
    End Function

    <System.Runtime.InteropServices.DllImport("userenv.dll", SetLastError:=True)>
    Private Function DestroyEnvironmentBlock(lpEnvironment As IntPtr) As Boolean
    End Function

    <System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
    Private Function CreateProcessAsUserW(hToken As IntPtr,
                                          lpApplicationName As String,
                                          lpCommandLine As String,
                                          lpProcessAttributes As IntPtr,
                                          lpThreadAttributes As IntPtr,
                                          bInheritHandles As Boolean,
                                          dwCreationFlags As UInteger,
                                          lpEnvironment As IntPtr,
                                          lpCurrentDirectory As String,
                                          ByRef lpStartupInfo As STARTUPINFO,
                                          ByRef lpProcessInformation As PROCESS_INFORMATION) As Boolean
    End Function

    Private Const CREATE_UNICODE_ENVIRONMENT As UInteger = &H400
    ' Anchor: no-op 注释，用于定位 ShellExecuteEx 相关代码（不改变逻辑）
    ''' <summary>
    ''' 启动程序的三种模式：0-按当前权限运行；1-强制提权；2-强制降权（若目标需要 UAC 则会弹窗）
    ''' </summary>
    Public Enum RunMode
        UseCurrent当前权限 = 0
        ForceElevate强制提权 = 1
        ForceDemote强制降权 = 2
    End Enum

    ''' <summary>
    ''' 启动程序的三种模式：按当前权限运行、强制提权、强制降权（若目标需要 UAC 则会弹窗）
    ''' </summary>
    Public Sub RunApp(appPath As String,
                      Optional args As String = "",
                      Optional workingDirectory As String = "",
                      Optional mode As RunMode = RunMode.UseCurrent当前权限)
        Try
            ' 确定工作目录（若未指定则使用 appPath 所在目录）
            Dim dir As String = workingDirectory
            If String.IsNullOrEmpty(dir) Then
                dir = Path.GetDirectoryName(appPath)
            End If

            Select Case mode
                Case RunMode.ForceElevate强制提权
                    ' 强制提权：使用 ShellExecuteEx runas 触发 UAC
                    Dim sei As New SHELLEXECUTEINFO()
                    sei.cbSize = Marshal.SizeOf(GetType(SHELLEXECUTEINFO))
                    sei.fMask = SEE_MASK_NOCLOSEPROCESS
                    sei.hwnd = IntPtr.Zero
                    sei.lpVerb = "runas"
                    sei.lpFile = appPath
                    sei.lpParameters = If(String.IsNullOrEmpty(args), Nothing, args)
                    sei.lpDirectory = dir
                    sei.nShow = SW_SHOWNORMAL

                    If Not ShellExecuteEx(sei) Then
                        Throw New Exception("ShellExecuteEx 失败。错误代码: " & Marshal.GetLastWin32Error())
                    End If

                    If sei.hProcess <> IntPtr.Zero Then
                        CloseHandle(sei.hProcess)
                    End If

                Case RunMode.ForceDemote强制降权

                    StartAsNormalUser_Advanced(appPath, args, dir)
                    ' 强制降权：尝试使用 explorer 的令牌通过 CreateProcessWithTokenW 创建非提升进程
                    'Dim shellToken As IntPtr = IntPtr.Zero
                    'Dim primaryToken As IntPtr = IntPtr.Zero
                    'Dim shellProcessHandle As IntPtr = IntPtr.Zero
                    'Dim pi As PROCESS_INFORMATION
                    'Dim created As Boolean = False
                    'Try
                    '    Dim hShellWnd As IntPtr = GetShellWindow()
                    '    If hShellWnd = IntPtr.Zero Then
                    '        Throw New Exception("无法获取 Shell 窗口句柄。")
                    '    End If

                    '    Dim shellPid As Integer = 0
                    '    GetWindowThreadProcessId(hShellWnd, shellPid)
                    '    If shellPid = 0 Then
                    '        Throw New Exception("无法获取 Shell 进程 ID。")
                    '    End If

                    '    shellProcessHandle = OpenProcess(PROCESS_QUERY_INFORMATION, False, shellPid)
                    '    If shellProcessHandle = IntPtr.Zero Then
                    '        Throw New Exception("无法打开 Shell 进程。错误代码: " & Marshal.GetLastWin32Error())
                    '    End If

                    '    Dim ok As Boolean = OpenProcessToken(shellProcessHandle, TOKEN_DUPLICATE, shellToken)
                    '    If Not ok OrElse shellToken = IntPtr.Zero Then
                    '        Throw New Exception("无法获取 Shell 进程的令牌。错误代码: " & Marshal.GetLastWin32Error())
                    '    End If

                    '    Dim desiredAccess As UInteger = TOKEN_QUERY Or TOKEN_ASSIGN_PRIMARY Or TOKEN_DUPLICATE Or TOKEN_ADJUST_DEFAULT Or TOKEN_ADJUST_SESSIONID
                    '    ok = DuplicateTokenEx(shellToken, desiredAccess, IntPtr.Zero, SecurityImpersonation, TokenPrimary, primaryToken)
                    '    If Not ok OrElse primaryToken = IntPtr.Zero Then
                    '        Throw New Exception("无法复制并转换令牌。错误代码: " & Marshal.GetLastWin32Error())
                    '    End If

                    '    Dim si As New STARTUPINFO()
                    '    si.cb = Marshal.SizeOf(GetType(STARTUPINFO))
                    '    si.lpDesktop = "winsta0\default"


                    '    ' 尝试为目标进程创建环境块，并用 CreateProcessWithTokenW 启动
                    '    Dim envBlock As IntPtr = IntPtr.Zero
                    '    Dim creationFlags As UInteger = CREATE_NEW_CONSOLE Or CREATE_UNICODE_ENVIRONMENT
                    '    Try
                    '        If Not CreateEnvironmentBlock(envBlock, primaryToken, False) Then
                    '            envBlock = IntPtr.Zero
                    '        End If

                    '        ' 把应用路径作为 lpApplicationName，命令行仅传递参数（避免命令行解析问题）
                    '        Dim appName As String = appPath
                    '        Dim cmdLine As String = If(String.IsNullOrEmpty(args), Nothing, args)

                    '        created = CreateProcessWithTokenW(primaryToken, 0, appName, cmdLine, creationFlags, envBlock, dir, si, pi)
                    '    Finally
                    '        If envBlock <> IntPtr.Zero Then DestroyEnvironmentBlock(envBlock)
                    '    End Try

                    '    If Not created Then
                    '        Dim err As Integer = Marshal.GetLastWin32Error()
                    '        If err = 740 Then
                    '            ' ERROR_ELEVATION_REQUIRED：目标需要提升，改用当前权限启动（UseCurrent）
                    '            Dim psiFall As New ProcessStartInfo()
                    '            psiFall.FileName = appPath
                    '            psiFall.Arguments = args
                    '            psiFall.WorkingDirectory = dir
                    '            psiFall.UseShellExecute = True
                    '            Process.Start(psiFall)
                    '            created = True
                    '        Else
                    '            Throw New Exception("CreateProcessWithTokenW 失败。错误代码: " & err)
                    '        End If
                    '    Else
                    '        ' 成功创建，关闭句柄
                    '        If pi.hThread <> IntPtr.Zero Then CloseHandle(pi.hThread)
                    '        If pi.hProcess <> IntPtr.Zero Then CloseHandle(pi.hProcess)
                    '    End If

                    'Finally
                    '    If shellToken <> IntPtr.Zero Then CloseHandle(shellToken)
                    '    If primaryToken <> IntPtr.Zero Then CloseHandle(primaryToken)
                    '    If shellProcessHandle <> IntPtr.Zero Then CloseHandle(shellProcessHandle)
                    'End Try

                Case RunMode.UseCurrent当前权限
                    ' 使用当前进程权限运行：直接 Process.Start，子进程将继承当前权限
                    Dim psi As New ProcessStartInfo()
                    psi.FileName = appPath
                    psi.Arguments = args
                    psi.WorkingDirectory = dir
                    psi.UseShellExecute = True
                    Process.Start(psi)

                Case Else
                    Throw New ArgumentException("未知的运行模式。")
            End Select

        Catch ex As Exception
            Throw New Exception("启动失败: " & ex.Message)
        End Try
    End Sub



End Module