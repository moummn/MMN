Public Class frmMain
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        mdUAC.StartAsNormalUser_Advanced("D:\Games\Genshin Impact\Genshin Impact Game\YuanShen.exe", , "D:\Games\Genshin Impact\Genshin Impact Game"， RunMode.ForceDemote强制降权
                                         )
    End Sub
End Class
