Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Threading
Namespace ProxyChecker
    Friend Class Program
        Public Shared ReadOnly writer As Writer = New Writer()
        Shared Sub Form1debugout(ByVal fullstring As String)
            If fullstring Is Nothing Then
                Throw New ArgumentNullException(NameOf(fullstring))
            End If

            If (Form1.Form1Instance IsNot Nothing) AndAlso Not Form1.Form1Instance.IsDisposed Then
                Dim Evaluator = New Thread(Sub() Form1.Form1Instance.UpdateTextBox(fullstring))
                Evaluator.Start()
            End If
        End Sub
        Shared Sub Form1proxystringinfo(ByVal fullstring As String)
            If (Form1.Form1Instance IsNot Nothing) AndAlso Not Form1.Form1Instance.IsDisposed Then
                'Form1.Form1Instance.Updateproxystringinfo(fullstring)
                Dim Evaluator = New Thread(Sub() Form1.Form1Instance.Updateproxystringinfo(fullstring))
                Evaluator.Start()
                Application.DoEvents()
            End If
        End Sub
        Public Shared Sub Scrape()
            Dim total As Integer = 0
            Dim unique As Integer = 0
            If File.Exists("Sources.txt") = False Then
                Form1debugout("Cant scrape proxies as there is no file called Sources.txt! with a list of websites in side" + vbNewLine)
                Exit Sub
            End If
            Form1debugout("Scraping proxies from Sources.txt! (can take a while...)" + vbNewLine)
            Application.DoEvents()
            Dim list As List(Of String) = File.ReadLines("Sources.txt").ToList()
            ThreadPool.SetMinThreads(8, 8)
            ThreadPool.SetMaxThreads(500, 500)
            Dim unused = Parallel.ForEach(CType(list, IEnumerable(Of String)), CType((Sub(currentUrl)
                                                                                          Try
                                                                                              For Each match As Match In Regex.Matches(New WebClient().DownloadString(currentUrl), "\b(\d{1,3}\.){3}\d{1,3}\:\d{1,8}\b", RegexOptions.Singleline)
                                                                                                  Dim line As Match = match
                                                                                                  total += 1
                                                                                                  If Not Variables.Proxies.Any(CType((Function(o) String.Equals(line.Groups(0).Value, o, StringComparison.OrdinalIgnoreCase)), Func(Of String, Boolean))) Then
                                                                                                      unique += 1
                                                                                                      Variables.Proxies.Add(line.Groups(0).Value)
                                                                                                  End If
                                                                                                  Application.DoEvents()
                                                                                              Next
                                                                                              Form1proxystringinfo(String.Format("Scraper | Total Scraped: {0} Duplicates removed: {1}", CObj(total), CObj((total - unique))))
                                                                                              Application.DoEvents()
                                                                                          Catch ex As Exception
                                                                                          End Try
                                                                                          Form1proxystringinfo(String.Format("Scraper | Total Scraped: {0} Duplicates removed: {1}", CObj(total), CObj((total - unique))))
                                                                                          Form1.Updateconsoleoutput("Scraping from: " & currentUrl & " Done!")
                                                                                          Application.DoEvents()
                                                                                      End Sub), Action(Of String)))
            Form1proxystringinfo(String.Format("Total Scraped: {0} Duplicates removed: {1}", CObj(total), CObj((total - unique))))
            Dim str As String = "Scraped.txt"
            For Each proxy As String In Variables.Proxies
                Program.writer.FilePath = str
                Program.writer.AppendToFile(proxy)

                Application.DoEvents()
            Next
            Form1.Updateconsoleoutput("Done saving proxies to: " & str)
            Variables.TimeOut = Integer.Parse("5000")
            Form1.Updateconsoleoutput("1 HTTP/s")
            Form1.Updateconsoleoutput("2 Socks4")
            Form1.Updateconsoleoutput("3 Socks5")
            Variables.Type = 1
            Program.CheckProxies()
        End Sub
        Public Shared Sub Check()
            Form1.Updateconsoleoutput("1 HTTP/s")
            Form1.Updateconsoleoutput("2 Socks4")
            Form1.Updateconsoleoutput("3 Socks5")
            Variables.Type = 1
            Dim str As String = "proxy.txt"
            Variables.Proxies = File.ReadLines(If(Not str.Contains(""""), str, str.Replace("""", ""))).ToList()
            If (File.Exists(str)) Then
                File.Delete(str)
            End If
            Dim sw As StreamWriter = File.CreateText(str)
            sw.Close()
            sw.Dispose()
            Form1.Updateconsoleoutput("Loaded: " & CObj(Variables.Proxies.Count) & " From your file!")
            CheckProxies()
        End Sub
        Public Shared Sub CheckProxies()
            ConvertProxies()
            Try
                For index As Integer = 0 To Variables.Threads - 1
                    Dim thread As New Thread(AddressOf Program.Runner)
                    thread.Start()
                Next
            Catch ex As Exception
                Form1.Updateconsoleoutput("Error CheckProxies: " + ex.ToString)
            End Try
        End Sub
        Public Shared Sub ConvertProxies()
            For Each proxy As String In Variables.Proxies
                Variables.ProxiesQueue.Enqueue(proxy)
            Next
        End Sub
        Public Shared Sub Runner()
            Try
                While CUInt(Variables.ProxiesQueue.Count) > 0
                    CheckProxy(Variables.ProxiesQueue.Dequeue())
                    Application.DoEvents()
                End While
            Catch ex As Exception
            End Try
        End Sub
        Shared Function MyNullCheck(s As String) As Boolean
            If s Is Nothing Then
                Return False
            End If
            s = s.Trim(New String(vbNullChar, 1))
            Return String.IsNullOrWhiteSpace(s)
        End Function
        Public Shared Sub CheckProxy(ByVal line As String)
            If line Is Nothing Then
                Exit Sub
            End If
            line = line.Replace("\n", "").Replace("\r", "").Replace(vbNullChar, "")
            line = line.Trim(New String(vbNullChar, 1))
            If line = "" Then
                Exit Sub
            End If
            Dim myProxy As New WebProxy(line)
            Try
                Dim str As String = "proxy.txt"
                Dim request As HttpWebRequest = WebRequest.Create("http://witch.valdikss.org.ru/")
                request.Proxy = myProxy
                request.ContentType = "application/x-www-form-urlencoded"
                request.Timeout = Variables.TimeOut
                request.KeepAlive = True
                If Variables.Type.Contains("1") Then
                    Dim webresponse As WebResponse = request.GetResponse()
                    Dim sr As StreamReader = New StreamReader(webresponse.GetResponseStream())
                    Dim body As String = sr.ReadToEnd
                    Form1.Updateconsoleoutput(String.Format("{0} | Working proxy HTTP/s", body))
                    If Not body.Contains("No proxy detected") Then
                        writer.FilePath = str
                        writer.AppendToFile(String.Format("{0}", line))
                        Variables.Alive += 1
                        Form1.Updateconsoleoutput(String.Format("{0} | Working proxy HTTP/s", line))
                        Form1proxystringinfo(String.Format("Checker Alive: {0} Dead: {1}", Variables.Alive, Variables.Dead))
                        Application.DoEvents()
                        Form1.Updateconsoleoutput("(" & myProxy.ToString & ")Proxy detected and working " & TimeOfDay.ToString("h:mm:ss tt"))
                    Else

                        Form1.Updateconsoleoutput("(" & myProxy.ToString & ")Proxy error " & TimeOfDay.ToString("h:mm:ss tt"))
                    End If
                    webresponse.Close()
                End If
            Catch ex As Exception
                Variables.Dead += 1
                Form1proxystringinfo(String.Format("Checker Alive: {0} Dead: {1}", Variables.Alive, Variables.Dead))
                Form1.Updateconsoleoutput(String.Format("{0} | Dead proxy", line))
                Application.DoEvents()
            End Try
        End Sub
    End Class
End Namespace
