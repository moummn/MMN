Imports System.Numerics
Imports System.Runtime.InteropServices
Imports System.Threading

Module PiContinuousFillLine

    ' ==================== 全局参数 ====================
    Private exiting As Boolean = False           ' 安全终止标志

#Region "Windows 虚拟终端启用 (ANSI 转义码支持)"
    Private Const STD_OUTPUT_HANDLE As Integer = -11
    Private Const ENABLE_VIRTUAL_TERMINAL_PROCESSING As UInteger = &H4

    <DllImport("kernel32.dll")>
    Private Function GetStdHandle(nStdHandle As Integer) As IntPtr
    End Function

    <DllImport("kernel32.dll")>
    Private Function GetConsoleMode(hConsoleHandle As IntPtr, ByRef lpMode As UInteger) As Boolean
    End Function

    <DllImport("kernel32.dll")>
    Private Function SetConsoleMode(hConsoleHandle As IntPtr, dwMode As UInteger) As Boolean
    End Function

    Private Sub EnableVirtualTerminal()
        Try
            Dim handle As IntPtr = GetStdHandle(STD_OUTPUT_HANDLE)
            Dim mode As UInteger
            If GetConsoleMode(handle, mode) Then
                mode = mode Or ENABLE_VIRTUAL_TERMINAL_PROCESSING
                SetConsoleMode(handle, mode)
            End If
        Catch
        End Try
    End Sub
#End Region

#Region "ANSI 转义码快捷常量"
    Private Const ESC As String = Chr(27)
    Private Const SAVE_CURSOR As String = ESC & "[s"
    Private Const RESTORE_CURSOR As String = ESC & "[u"
    Private Const CLEAR_LINE As String = ESC & "[2K"
    Private Function GotoRowCol(row As Integer, col As Integer) As String
        Return $"{ESC}[{row};{col}H"
    End Function
    Private Function SetScrollRegion(top As Integer, bottom As Integer) As String
        Return $"{ESC}[{top};{bottom}r"
    End Function
#End Region

    Sub Main()
        EnableVirtualTerminal()
        Console.WriteLine("=== 圆周率连续计算（按窗口宽度动态填满每行）===")
        Console.WriteLine("数字流在上方区域滚动，填满每行，底部固定进度栏。")
        Console.WriteLine("按 Ctrl+C 可随时终止。")
        Console.WriteLine()

        AddHandler Console.CancelKeyPress, AddressOf OnCancelKeyPress

        Dim offset As Integer = 0               ' 已输出的小数位数
        Dim totalDigits As Integer = 0           ' 已计算的总小数位数（不含保护位）
        Dim decPart As String = ""               ' 当前计算得到的小数字符串

        EnterScrollRegion()

        Try
            ' 先输出整数部分与小数点
            Console.Write("3.")

            While Not exiting
                ' 1. 计算本行还能容纳多少字符（直到窗口右边界）
                Dim remaining As Integer = Console.WindowWidth - Console.CursorLeft
                If remaining <= 0 Then
                    Console.WriteLine()
                    Continue While
                End If

                Dim needed As Integer = remaining   ' 本次需要的新数字个数

                ' 2. 如果现有小数位数不够，就增加总精度并重新计算
                While offset + needed > decPart.Length
                    ' 策略：每次增加至少 1000 位，减少重算次数
                    totalDigits = Math.Max(totalDigits + 1000, offset + needed + 10)
                    Dim scale As Integer = totalDigits + 10
                    Dim piBig As BigInteger = ComputePiMachin(scale)
                    Dim piStr As String = FormatPi(piBig, scale, totalDigits)
                    decPart = piStr.Substring(piStr.IndexOf("."c) + 1)
                End While

                ' 3. 取出刚好 needed 位数字
                Dim newDigits As String = decPart.Substring(offset, needed)
                Console.Write(newDigits)      ' 此时正好到达行末
                offset += needed

                ' 4. 手动换行（下一行从行首开始）
                Console.WriteLine()

                ' 5. 更新底部进度栏
                UpdateBottomStatus(offset)

                Thread.Sleep(0)
            End While
        Catch ex As Exception
            Console.WriteLine()
            Console.WriteLine($"发生错误: {ex.Message}")
        Finally
            ExitScrollRegion()
        End Try

        Console.WriteLine()
        Console.WriteLine("计算终止。")
        Console.WriteLine("按任意键退出...")
        Console.ReadKey()
    End Sub

    ' ---------- 滚动区域控制 ----------
    Private Sub EnterScrollRegion()
        Dim totalLines As Integer = Console.WindowHeight
        If totalLines < 3 Then Return

        Console.WriteLine()
        Console.WriteLine()

        Console.Write(SetScrollRegion(1, totalLines - 1))
        Console.Write(GotoRowCol(totalLines - 1, 1))
        Console.Write(CLEAR_LINE)
        Console.Write(SAVE_CURSOR)
    End Sub

    Private Sub ExitScrollRegion()
        Dim totalLines As Integer = Console.WindowHeight
        If totalLines < 3 Then Return

        Console.Write(SetScrollRegion(0, totalLines))
        Console.Write(GotoRowCol(totalLines, 1))
        Console.Write(CLEAR_LINE)
        Console.Write(Environment.NewLine)
    End Sub

    Private Sub UpdateBottomStatus(digits As Integer)
        Console.Write(SAVE_CURSOR)
        Dim totalLines As Integer = Console.WindowHeight
        Console.Write(GotoRowCol(totalLines, 1))
        Console.Write(CLEAR_LINE)
        Console.Write($"已计算 {digits} 位")
        Console.Write(RESTORE_CURSOR)
    End Sub

    ' ---------- Ctrl+C 安全处理 ----------
    Private Sub OnCancelKeyPress(sender As Object, e As ConsoleCancelEventArgs)
        e.Cancel = True
        exiting = True
        Console.WriteLine()
        Console.WriteLine("正在停止...")
    End Sub

    ' ---------- 马青公式高精度计算 ----------
    Private Function ComputePiMachin(scale As Integer) As BigInteger
        Dim arctan5 As BigInteger = Arctan(5, scale)
        Dim arctan239 As BigInteger = Arctan(239, scale)
        Return 16 * arctan5 - 4 * arctan239
    End Function

    Private Function Arctan(x As Integer, scale As Integer) As BigInteger
        Dim scaleBig As BigInteger = BigInteger.Pow(10, scale)
        Dim sum As BigInteger = 0
        Dim power As BigInteger = x
        Dim xSqr As BigInteger = x * x
        Dim k As Integer = 0
        Dim sign As Integer = 1

        While True
            Dim denominator As BigInteger = (2 * k + 1) * power
            Dim term As BigInteger = scaleBig / denominator
            If term = 0 Then Exit While
            If sign = 1 Then
                sum += term
            Else
                sum -= term
            End If
            k += 1
            sign = -sign
            power *= xSqr
        End While
        Return sum
    End Function

    Private Function FormatPi(piBig As BigInteger, scale As Integer, targetDigits As Integer) As String
        Dim raw As String = piBig.ToString().PadLeft(scale + 1, "0"c)
        Dim intLen As Integer = raw.Length - scale
        Dim intPart As String = raw.Substring(0, intLen)
        Dim decPart As String = raw.Substring(intLen)
        If decPart.Length > targetDigits Then
            decPart = decPart.Substring(0, targetDigits)
        End If
        Return intPart & "." & decPart
    End Function

End Module