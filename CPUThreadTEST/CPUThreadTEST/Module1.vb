Module Module1
    Dim Thrs() As Threading.Thread
    Dim ThrNum() As UInt64
    Private Function CreatThread(ByVal ThreadNumber As Integer)

        Dim thr As New Threading.Thread(AddressOf ThreadCalc)
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
        Dim SecPass As ULong = 0
        Dim L As UInt64
        Do
            SecPass += 1
            Threading.Thread.Sleep(1000)
            Console.CursorTop = 1
            Console.CursorLeft = 0
            Console.Write(CStr(SecPass) & "s")
            Console.CursorTop = 2
            Console.CursorLeft = 0
            Console.Write("T:")
            L = 0
            Try
                For I As Integer = 1 To AllThreads
                    L = L + ThrNum(I)
                Next
                Console.Write(CStr(L))
            Catch ex As Exception

            End Try

            For I As Integer = 1 To AllThreads
                Console.CursorTop = I + 2
                Console.CursorLeft = 0
                Console.Write(CStr(I) & ":" & CStr(ThrNum(I)))
            Next

        Loop
    End Sub
    Sub Main()
        Console.Write("Input threads:")
        Dim AllThreads As Int32 = Val(Console.ReadLine())
        ReDim Thrs(AllThreads)
        ReDim ThrNum(AllThreads)
        For I As Int32 = 1 To AllThreads
            Thrs(I) = CreatThread(I)
        Next
        Call MoniorThread(AllThreads)
        'Console.ReadKey()
    End Sub

End Module
