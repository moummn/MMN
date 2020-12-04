Module mdmWaveDb
    Public Declare Function waveInOpen Lib "winmm.dll" Alias "waveInOpen" (lphWaveIn As Integer, ByVal uDeviceID As Integer, lpFormat As WAVEFORMAT, ByVal dwCallback As Integer, ByVal dwInstance As Integer, ByVal dwFlags As Integer) As Integer
    Public Declare Function CreateEvent Lib "kernel32" Alias "CreateEventA" (lpEventAttributes As SECURITY_ATTRIBUTES, ByVal bManualReset As Integer, ByVal bInitialState As Integer, ByVal lpName As String) As Integer
    Public Declare Function waveInPrepareHeader Lib "winmm.dll" Alias "waveInPrepareHeader" (ByVal hWaveIn As Integer, lpWaveInHdr As WAVEHDR, ByVal uSize As Integer) As Integer
    Public Declare Function waveInAddBuffer Lib "winmm.dll" Alias "waveInAddBuffer" (ByVal hWaveIn As Integer, lpWaveInHdr As WAVEHDR, ByVal uSize As Integer) As Integer
    Public Declare Function waveInStart Lib "winmm.dll" Alias "waveInStart" (ByVal hWaveIn As Integer) As Integer

    Public Const WAVE_MAPPER = -1&
    Public Const CALLBACK_FUNCTION = &H30000      '  dwCallback is a FARPROC
    Public Const WAVE_FORMAT_PCM = &H1
    Public Structure WAVEFORMATEX
        Dim wFormatTag As Int16
        Dim nChannels As Int16
        Dim nSamplesPerSec As Int32
        Dim nAvgBytesPerSec As Int32
        Dim nBlockAlign As Int16
        Dim wBitsPerSample As Int16
        Dim cbSize As Int16
    End Structure
    Public Structure WAVEFORMAT
        Dim wFormatTag As Int16
        Dim nChannels As Int16
        Dim nSamplesPerSec As Int32
        Dim nAvgBytesPerSec As Int32
        Dim nBlockAlign As Int16
    End Structure
    Public Structure SECURITY_ATTRIBUTES
        Dim nLength As Int32
        Dim lpSecurityDescriptor As Int32
        Dim bInheritHandle As Int32
    End Structure
    Public Structure WAVEHDR
        Dim lpData As String
        Dim dwBufferLength As Int32
        Dim dwBytesRecorded As Int32
        Dim dwUser As Int32
        Dim dwFlags As Int32
        Dim dwLoops As Int32
        Dim lpNext As Int32
        Dim Reserved As Int32
    End Structure
    Public Function fngetPcmDB(ByRef pcmdata As Char, ByVal size As Integer) As Long

        Dim db As Int32 = 0
        Dim value As Int16 = 0
        Dim sum As Double = 0
        For i As Integer = 0 To size Step 2

            memcpy(& value, pcmdata + i, 2) '获取2个字节的大小（值）  
            sum += abs(value); //绝对值求和  
            If i >= size Then Exit For
        Next
        sum = sum / (size / 2); //求平均值（2个字节表示一个振幅，所以振幅个数为：size/2个）  
     If (sum > 0)
     {
         db = (int)(20.0 * log10(sum));
     }
     Return db;
 }
End Module
