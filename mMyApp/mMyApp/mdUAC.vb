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

    ' ---------- 公共方法 ----------
    ' ShellExecuteEx P/Invoke 定义和相关常量
    ''' <summary>
    ''' 使用 ShellExecuteEx(runas) 来触发 UAC 
    ''' </summary>
    ''' <param name="lpExecInfo"></param>
    ''' <returns></returns>
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
    ''' 启动程序的三种模式：强制提权、强制降权（若目标需要 UAC 则会弹窗）、按当前权限运行
    ''' </summary>
    Public Sub StartAsNormalUser_Advanced(appPath As String,
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
                    ' 强制降权：优先尝试在交互用户会话创建非提升进程（WTSQueryUserToken + CreateProcessAsUser）
                    Dim launched As Boolean = False
                    Try
                        launched = TryLaunchAsUser(appPath, args, dir)
                    Catch exLaunch As Exception
                        launched = False
                    End Try

                    If Not launched Then
                        ' 回退到 explorer 的 Shell.Application（在某些场景下可能有效）
                        Try
                            Dim shell = CreateObject("Shell.Application")
                            If shell IsNot Nothing Then
                                shell.ShellExecute(appPath, If(String.IsNullOrEmpty(args), Nothing, args), dir, Nothing, SW_SHOWNORMAL)
                            Else
                                Dim psiFall As New ProcessStartInfo()
                                psiFall.FileName = appPath
                                psiFall.Arguments = args
                                psiFall.WorkingDirectory = dir
                                psiFall.UseShellExecute = True
                                Process.Start(psiFall)
                            End If
                        Catch exShell As Exception
                            Dim psiFall As New ProcessStartInfo()
                            psiFall.FileName = appPath
                            psiFall.Arguments = args
                            psiFall.WorkingDirectory = dir
                            psiFall.UseShellExecute = True
                            Process.Start(psiFall)
                        End Try
                    End If

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

    ' 尝试在当前控制台会话中以交互用户身份创建非提升进程，失败则返回 Fals

End Module