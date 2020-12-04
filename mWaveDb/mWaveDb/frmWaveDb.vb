Public Class frmWaveDb
    Dim hWaveIn As Integer '输入设备
    Dim waveform As WAVEFORMAT '采集音频的格式，结构体
    Dim pBuffer1() As Byte '采集音频时的数据缓存
    Dim wHdr1 As WAVEHDR '采集音频时包含数据缓存的结构体
    Dim wait As UInt32
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click



    End Sub
    Public Function fnGetAudio() As Long
        fnGetAudio = 0
        On Error GoTo ErrLine
        Dim sa As SECURITY_ATTRIBUTES
        Dim wait = CreateEvent(sa, 0, 0, vbNull)
        With waveform
            .wFormatTag = WAVE_FORMAT_PCM '声音格式为PCM
            .nSamplesPerSec = 8000 '采样率，16000次/秒
            '.wBitsPerSample = 16 '采样比特，16bits/次
            .nChannels = 1 '采样声道数，2声道
            .nAvgBytesPerSec = 16384 '每秒的数据率，就是每秒能采集多少字节的数据
            .nBlockAlign = 2 '一个块的大小，采样bit的字节数乘以声道数
            '.cbSize = 0 '一般为0
            'C++ Code: waveInOpen(&hWaveIn, WAVE_MAPPER, &waveform, (DWORD_PTR)wait, 0L, CALLBACK_EVENT);
        End With
        waveInOpen(hWaveIn, WAVE_MAPPER, waveform, wait, CLng(0), CALLBACK_FUNCTION)
        Dim bufsize As UInt32 = 1024 '每次开辟1k的缓存存储录音数据

        '结合音频解码和网络传输可以修改为实时录音播放的机制以实现对讲功能
        ReDim pBuffer1(bufsize)

        wHdr1.lpData = BitConverter.ToString(pBuffer1)
        wHdr1.dwBufferLength = bufsize
        wHdr1.dwBytesRecorded = 0
        wHdr1.dwUser = 0
        wHdr1.dwFlags = 0
        wHdr1.dwLoops = 1

        waveInPrepareHeader(hWaveIn, wHdr1, Len(wHdr1)) '准备一个波形数据块头用于录音
        waveInAddBuffer(hWaveIn, wHdr1, Len(wHdr1)) '指定波形数据块为录音输入缓存
        waveInStart(hWaveIn) '开始录音
        Threading.Thread.Sleep(1000)

        Dim mic As String
        Dim sum As Integer = getPcmDB
        Int sum = getPcmDB(pBuffer1, 1024);
         mic.Format(_T("%d"), sum);
         edit.SetWindowTextW(mic);
         waveInReset(hWaveIn);//重置输入
         delete[]pBuffer1;
    // waveInClose(hWaveIn);

        Exit Function
ErrLine:
        Return Err.Number
    End Function
End Class
