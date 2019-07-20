Namespace ProxyChecker
    Public Class Variables
        Public Shared Type As String = "1"
        Public Shared Threads As Integer = 250
        Public Shared TimeOut As Integer = 5000
        Public Shared Alive As Integer = 0
        Public Shared Dead As Integer = 0
        Public Shared Proxies As List(Of String) = New List(Of String)()
        Public Shared ProxiesQueue As Queue(Of String) = New Queue(Of String)()
    End Class
End Namespace
