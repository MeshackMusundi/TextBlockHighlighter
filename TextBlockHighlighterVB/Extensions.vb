Imports System.Runtime.CompilerServices

Module Extensions
    <Extension()>
    Function Contains(ByVal str As String, ByVal txt As String, ByVal c As StringComparison) As Boolean
        If txt IsNot Nothing Then Return str.IndexOf(txt, c) >= 0
        Return False
    End Function
End Module
