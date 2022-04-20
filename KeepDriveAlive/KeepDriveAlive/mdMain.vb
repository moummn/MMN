Module mdMain
    Dim DebugMode As Boolean = False
    Dim FN() As String
    Dim TimeWait As Integer = 5
    Sub Main()
        '获得启动参数
        On Error GoTo ERRLINE
        DebugMode = False
        Dim NoPara As Boolean = True '启动时无参数
        Dim I As Integer
        Dim S As String = ""
        Dim ShowHelp As Boolean = False
        For Each Para As String In My.Application.CommandLineArgs
            NoPara = False
            Dim UPara As String = UCase(Para) 'Para参数全大写
            cWL(Para)
            If UPara = "-HELP" OrElse UPara = "-H" Then
                ShowHelp = True
                Exit For
            End If
            If UPara = "-DEBUG" Then
                DebugMode = True
                cWL("DEBUG 模式开启")
            End If
            If Left(UPara, 3) = "-D:" Then
                S = Mid(Para, 4, Len(Para) - 3)
                cWL(S)
                FN = Split(S, ",")
                For I = 0 To UBound(FN)
                    S = FN(I)
                    cWL(S)
                    If Len(S) = 1 Then
                        FN(I) = FN(I) & ":\KeepDriveAlive.kda"
                    End If
                Next
            End If
            If Left(UPara, 3) = "-T:" Then
                S = Mid(Para, 4, Len(Para) - 3)
                cWL(S)
                TimeWait = Val(S)
            End If
        Next

KEEPALIVELINE:
        'KeepAlive
        Do
            For Each S In FN
                cWL(Now.ToString & " - " & S)
                sbKeepAlive(S)
            Next
            Sleepex(TimeWait * 1000)
        Loop

SHOWHELPLINE:
        'ShowHelp
        If ShowHelp OrElse NoPara Then
            DebugMode = True
            cWL("Keep Drive Alive - 保持硬盘活动状态")
            cWL("通过定时对硬盘读写，让硬盘一直处活动状态")
            cWL("版本:2022.04.20")
            cWL()
            cWL("kdal -h / -help 显示此帮助文件")
            cWL("kdal -d:""DrivePath""[,DrivePath] [-t:Time]")
            cWL()
            cWL("DrivePath   指定硬盘及文件名路径，多个用(,)分割，默认文件名为")
            cWL("            KeepDriveAlive.kda")
            cWL("Time        间隔时长，单位秒，默认5")
            cWL()
            cWL("示例：")
            cWL("kdal -d:C,D -t:5")
            cWL("kdal -d:""C:\path1\kdal1.kda"",""D:\path2\kdal2.kda"" -t:5")
        End If
        Exit Sub
ERRLINE:
        DebugMode = True
        cWL("错误： " & Err.Description)
        ShowHelp = True
        GoTo SHOWHELPLINE
    End Sub
    ''' <summary>
    ''' Console.WriteLine("ContentText")
    ''' </summary>
    ''' <param name="ContentText">具体内容</param>
    Private Sub cWL(Optional ByVal ContentText As String = "")
        On Error Resume Next
        If DebugMode Then
            Console.WriteLine(ContentText)
            Debug.Print(ContentText)
        End If
    End Sub
    ''' <summary>
    ''' 线程休眠（等待）
    ''' </summary>
    ''' <param name="TimeLong">时长，单位毫秒</param>
    Private Sub Sleepex(ByVal TimeLong As Integer)
        On Error Resume Next
        Threading.Thread.Sleep(TimeLong)
    End Sub
    ''' <summary>
    ''' 保持硬盘活动
    ''' </summary>
    ''' <param name="FileName">文件名</param>
    Private Sub sbKeepAlive(ByVal FileName As String)
        On Error GoTo ERRLINE
        FileOpen(1, FileName, OpenMode.Output)
        Write(1, Now.ToString)
        FileClose(1)

        Exit Sub
ERRLINE:
        cWL("错误： " & Err.Description)
    End Sub
End Module
