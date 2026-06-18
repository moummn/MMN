Public Class frmMain
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        mdUACRun.RunApp("D:\Games\Genshin Impact\Genshin Impact Game\YuanShen.exe", , "D:\Games\Genshin Impact\Genshin Impact Game", RunMode.ForceDemote强制降权)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        mdUACRun.RunApp("notepad.exe", "C:\Users\ZN\Desktop\123.txt", "C:\Users\ZN\Desktop", RunMode.ForceDemote强制降权)
    End Sub
End Class
