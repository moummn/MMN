Module Module1
    Dim Thrs() As Threading.Thread
    Dim ThrNum() As UInt64
    Private Function CreatThread(ByVal ThreadNumber As Integer)
        Threading.Thread.CurrentThread.Priority = Threading.ThreadPriority.AboveNormal

        Dim thr As New Threading.Thread(AddressOf ThreadCalc)
        thr.Priority = Threading.ThreadPriority.BelowNormal
        thr.Start(ThreadNumber)

        Return thr
    End Function

    Private Sub ThreadCalc(ByVal ThreadNumber As Integer)
        Do
            ThrNum(ThreadNumber) += 1
            If ThrNum(ThreadNumber) > UInt64.MaxValue - 1 Then
                Dim S As String = ""
                For L As Long = 1 To Len(CStr(ThrNum(ThreadNumber)))
                    S = S & " "
                Next
                S = CStr(ThreadNumber) & ":" & S
                Console.CursorTop = ThreadNumber + 1
                Console.CursorLeft = 0
                Console.Write(S)
                ThrNum(ThreadNumber) = UInt64.MinValue
            End If
        Loop
    End Sub
    Private Sub MoniorThread(ByVal AllThreads As Integer)
        Threading.Thread.CurrentThread.Priority = Threading.ThreadPriority.AboveNormal
        Dim SecPass As ULong = 0
        Dim L As UInt64
        Dim A As UInt64
        Do
            SecPass += 1
            Threading.Thread.Sleep(1000)
            Console.CursorTop = 1
            Console.CursorLeft = 0
            Console.Write(CStr(SecPass) & "s")
            L = 0
            For I As Integer = 1 To AllThreads
                A = ThrNum(I)
                Console.CursorTop = I + 1
                Console.CursorLeft = 0
                Console.Write("THREAD " & Format(I, "00") & " : " & FormatNumber(A, 0,,, TriState.True))
                Try
                    L = L + A
                Catch ex As Exception

                End Try
            Next
            Console.CursorTop = AllThreads + 2
            Console.CursorLeft = 0
            Console.Write("TOTAL     : " & FormatNumber(L, 0,,, TriState.True))

        Loop
    End Sub
    Sub Main()
        Threading.Thread.CurrentThread.Priority = Threading.ThreadPriority.AboveNormal

        Console.Write("INPUT THREADS:")
        Dim AllThreads As Int32 = Val(Console.ReadLine())
        ReDim Thrs(AllThreads)
        ReDim ThrNum(AllThreads)
        For I As Integer = 1 To AllThreads
            Console.WriteLine("CREATING... " & CStr(I) & "/" & CStr(AllThreads))
            Thrs(I) = CreatThread(I)
        Next

        Console.Clear()
        Console.WriteLine("THREADS:" & CStr(AllThreads))
        Call MoniorThread(AllThreads)
        'Console.ReadKey()
    End Sub

End Module
