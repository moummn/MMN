Imports System.Runtime.InteropServices

Public Class frmKeyPressPrintScreen
    ' 声明 keybd_event API
    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    public Shared Sub keybd_event(bVk As Byte, bScan As Byte, dwFlags As UInteger, dwExtraInfo As UIntPtr)
    End Sub
    Private Const VK_SNAPSHOT As Byte = &H2C ' PrintScreen 键

    Private Sub frmKeyPressPrintScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 按下 PrintScreen 键
        keybd_event(VK_SNAPSHOT, 0, 0, UIntPtr.Zero)
        ' 释放 PrintScreen 键（KEYEVENTF_KEYUP 标志）
        keybd_event(VK_SNAPSHOT, 0, &H2, UIntPtr.Zero)
        Me.Close()
    End Sub

End Class
