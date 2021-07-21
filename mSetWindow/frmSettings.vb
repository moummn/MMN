Public Class frmSettings

    ''' <summary>
    ''' 刷新窗体
    ''' </summary>
    Public Sub sbFormRefresh()
        On Error Resume Next
        If FormWindowListInit = True Then Exit Sub
        With frmWindowList
            Me.Left = .Left + .Width
            Me.Top = .Top
            Me.Height = .Height

        End With
    End Sub
End Class