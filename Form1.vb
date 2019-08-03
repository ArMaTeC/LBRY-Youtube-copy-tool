Imports System.IO
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json.Linq
Imports System.Net
Imports System.ComponentModel
Imports System.Threading
Public Class Form1
    Dim WithEvents Wc2 As New WebClient
    Dim P As Integer = 0
    Dim sw As New Stopwatch
    Dim currentfiledownloading As String
    Dim speed As Double
    Public Delegate Sub DeleteFileThreadDelegate(ByVal path As String)
    Private Sub DeleteFileThread(ByVal path As String)
        Do
            Try
                File.Delete(path)
                Exit Do
            Catch ex As IOException
                Thread.Sleep(10000)
            End Try
        Loop
    End Sub
    Private Sub wc2_DownloadFileCompleted(ByVal sender As Object, ByVal e As AsyncCompletedEventArgs) Handles Wc2.DownloadFileCompleted
        ProgressBar1.Value = 0
        P = 0
        If e.Error Is Nothing Then
            sw.Stop()
            ProgressBar1.DisplayFormat = "Done!"
            Timer1.Stop()
        Else
            MsgBox("An error occured: " & e.Error.Message)
        End If
    End Sub
    Private Sub wc2_DownloadProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs) Handles Wc2.DownloadProgressChanged
        ProgressBar1.Maximum = 100
        ProgressBar1.Value = e.ProgressPercentage
        speed = (e.BytesReceived / 1024D / sw.Elapsed.TotalSeconds).ToString("0.00")
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim progstring As String = ""
        Dim progstring1 As String = ""
        If speed < 1024 Then
            progstring += "{0}% " + String.Format("Speed: {0:0.00} KB/s", speed)
        Else
            progstring += "{0}% " + String.Format("Speed: {0:0.00} MB/s", speed / 1024)
        End If
        If ProgressBar1.Value = 0 Then
            progstring1 = "Time Left: Estimating..."
        Else
            Dim secondsRemaining As Double = (100 - ProgressBar1.Value) * sw.Elapsed.Seconds / ProgressBar1.Value
            Dim ts As TimeSpan = TimeSpan.FromSeconds(secondsRemaining)
            progstring1 = String.Format("Time Left: {0}:{1}:{2}", ts.Hours, ts.Minutes, ts.Seconds)
            Select Case secondsRemaining
                Case Is < 60
                    progstring1 = String.Format("Time Left: {0} sec", ts.Seconds)
                Case Is < 3600
                    progstring1 = String.Format("Time Left: {0} min {1} sec", ts.Minutes, ts.Seconds)
                Case Is < 86400
                    progstring1 = String.Format("Time Left: {0} hours {1} min {2} sec", ts.Hours, ts.Minutes, ts.Seconds)
                Case Else
                    progstring1 = String.Format("Time Left: {0} days {1} hours {2} min {3} sec", ts.Days, ts.Hours, ts.Minutes, ts.Seconds)
            End Select
        End If
        ProgressBar1.DisplayFormat = progstring + " " + progstring1
    End Sub
    Function RemoveWhitespace(fullString As String) As String
        Return New String(fullString.Where(Function(x) Not Char.IsWhiteSpace(x)).ToArray())
    End Function
    Private Delegate Sub NameCallBack(ByVal varText As String)
    Public Sub UpdateTextBox(ByVal input As String)
        If InvokeRequired Then
            debugout.BeginInvoke(New NameCallBack(AddressOf UpdateTextBox), New Object() {input})
        Else
            debugout.Text = debugout.Text + Environment.NewLine + input
            Try
                debugout.Focus()
                debugout.Select(debugout.Text.Length, 0)
                debugout.ScrollToCaret()
            Catch ex As Exception

            End Try
        End If
    End Sub
    Public Sub Updateproxystringinfo(ByVal input As String)
        If InvokeRequired Then
            proxystringinfo.BeginInvoke(New NameCallBack(AddressOf Updateproxystringinfo), New Object() {input})
        Else
            proxystringinfo.Text = input
        End If
    End Sub
    Public Sub Updateconsoleoutput(ByVal input As String)
        If InvokeRequired Then
            consoleoutput.BeginInvoke(New NameCallBack(AddressOf Updateconsoleoutput), New Object() {input})
        Else
            Console.WriteLine(input & vbNewLine)
            consoleoutput.AppendText(input & vbNewLine)
        End If
    End Sub




    Private Function CheckIfLBRYRunning()
        Dim p() As Process
        p = Process.GetProcessesByName("LBRY")
        If p.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Dim proxylistselector() As String
    Dim proxycurrent As Integer = 0
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles upload_button.Click
        If Not lbrynetpath.Text.Contains("daemon\lbrynet.exe") Then
            MsgBox("Please set the daemon\lbrynet.exe" + vbNewLine + "Typically its located C:\Program Files\LBRY\resources\static\daemon\lbrynet.exe")
            Exit Sub
        End If
        If Not CheckIfLBRYRunning() Then
            MsgBox("Please run LBRY and login")
            Exit Sub
        End If
        upload_button.Enabled = False
        Button3.Enabled = False
        If userproxycheck.CheckState = CheckState.Checked Then
            Dim FILE_NAME As String = Application.StartupPath + "\proxy.txt"
            Dim TextLine As String = ""
            If System.IO.File.Exists(FILE_NAME) = True Then
                Dim objReader As New System.IO.StreamReader(FILE_NAME)
                Do While objReader.Peek() <> -1
                    TextLine = TextLine & objReader.ReadLine() & vbNewLine
                Loop
                proxylistselector = Split(TextLine, vbCrLf)
            Else
                MessageBox.Show("File Does Not Exist")
            End If
        End If
        debugout.AppendText("Please wait pulling youtube data via youtube-dl.exe " + vbNewLine)
        Dim pYoutube As New ProcessStartInfo With {
                    .FileName = Application.StartupPath + "\bin\youtube-dl.exe",
                    .Arguments = "-i -f mp4+bestvideo+bestaudio/best """ + youtubeplaylist.Text + """" + " -j",
                    .UseShellExecute = False,
                    .RedirectStandardOutput = True,
                    .WindowStyle = ProcessWindowStyle.Hidden,
                    .CreateNoWindow = True
                    }
        Updateconsoleoutput(pYoutube.Arguments)
        Dim proc0 As Process = Process.Start(pYoutube)
        While Not proc0.StandardOutput.EndOfStream
            Application.DoEvents()
            Dim YoutubeJsonLine As String = proc0.StandardOutput.ReadLine()
            Updateconsoleoutput(YoutubeJsonLine)
            Dim json As String = YoutubeJsonLine
            Dim ser As JObject = JObject.Parse(json)
            Dim data As List(Of JToken) = ser.Children().ToList
            Dim youtubetitle As String = ""
            Dim youtubedescription As String = ""
            Dim youtubedisplay_id As String = ""
            Dim youtubethumbnail As String = ""
            Dim youtubeurl As New Uri("https://github.com/ArMaTeC/LBRY-Youtube-copy-tool")
            Dim youtubeext As String = ""
            For Each item As JProperty In data
                item.CreateReader()
                Select Case item.Name
                    Case "title"
                        youtubetitle = item.Value
                    Case "description"
                        youtubedescription = item.Value
                    Case "display_id"
                        youtubedisplay_id = item.Value
                    Case "thumbnail"
                        youtubethumbnail = item.Value
                    Case "ext"
                        youtubeext = item.Value
                    Case "requested_formats"
                        For Each msg As JObject In item.Values
                            Updateconsoleoutput(item.Name + ":" + msg.ToString())
                            Dim ser1 As JObject = JObject.Parse(msg.ToString())
                            Dim data1 As List(Of JToken) = ser1.Children().ToList
                            For Each item1 As JProperty In data1
                                item1.CreateReader()
                                Select Case item1.Name
                                    Case "url"
                                        youtubeurl = item1.Value
                                        Exit For
                                End Select
                            Next
                            Exit For
                        Next
                End Select
            Next
            'debugout.AppendText("Current video " + youtubetitle + vbNewLine)
            'debugout.AppendText("Current description " + youtubedescription + vbNewLine)
            'debugout.AppendText("Current thumbnail " + youtubethumbnail + vbNewLine)
            'debugout.AppendText("Current display id " + youtubedisplay_id + vbNewLine)
            If (Not Streamlistjson.Checkvideotitleexsists(youtubetitle)) Then
                P = 1
                sw.Start()
                currentfiledownloading = Path.Combine(Application.StartupPath + "\temp\", Regex.Replace(youtubetitle, "[^a-zA-Z0-9]", "") + ".jpg")
                Wc2.DownloadFileAsync(New Uri(youtubethumbnail), currentfiledownloading)
                Timer1.Enabled = True
                Do While P = 1
                    Thread.Sleep(200)
                    Application.DoEvents()
                Loop
                Dim line1 As String = ""
                Dim jsonline1 As String = ""
                Dim thumbnailPath As String = ""
                If userproxycheck.CheckState = CheckState.Checked Then
                    While Not jsonline1.Contains("{""success""")
                        Dim pthumbupload As New ProcessStartInfo With {
                            .FileName = Application.StartupPath + "\bin\curl.exe",
                            .Arguments = " -F name=" + Regex.Replace(youtubetitle, "[^a-zA-Z0-9]", "") + DateTime.Now.ToString("yyyyMMddHHmmss") +
                                         " -F file=@""" + Path.Combine(Application.StartupPath + "\temp\", Regex.Replace(youtubetitle, "[^a-zA-Z0-9]", "") + ".jpg").Replace("\", "\\") + """" +
                                         " -x " + proxylistselector(proxycurrent) +
                                         " https://spee.ch/api/claim/publish",
                            .UseShellExecute = False,
                            .RedirectStandardOutput = True,
                            .WindowStyle = ProcessWindowStyle.Hidden,
                            .CreateNoWindow = True
                            }
                        proxycurrent += 1
                        If proxycurrent > (proxylistselector.Count - 1) Then
                            proxycurrent = 0
                        End If
                        Updateconsoleoutput(pthumbupload.Arguments)
                        Dim proc1 As Process = Process.Start(pthumbupload)
                        While Not proc1.StandardOutput.EndOfStream
                            line1 = proc1.StandardOutput.ReadLine()
                            Updateconsoleoutput(line1)
                            If line1.Contains("{""success""") Then
                                jsonline1 = line1
                                If File.Exists(Path.Combine(Application.StartupPath + "\temp\", Regex.Replace(youtubetitle, "[^a-zA-Z0-9]", "") + ".jpg")) = True Then
                                    Dim delInstance As DeleteFileThreadDelegate = New DeleteFileThreadDelegate(AddressOf DeleteFileThread)
                                    delInstance.BeginInvoke(Path.Combine(Application.StartupPath + "\temp\", Regex.Replace(youtubetitle, "[^a-zA-Z0-9]", "") + ".jpg"), Nothing, Nothing)
                                End If
                                Exit While
                            End If
                        End While
                    End While
                Else
                    Dim pthumbupload As New ProcessStartInfo With {
                            .FileName = Application.StartupPath + "\bin\curl.exe",
                            .Arguments = " -F name=" + Regex.Replace(youtubetitle, "[^a-zA-Z0-9]", "") + DateTime.Now.ToString("yyyyMMddHHmmss") +
                                         " -F file=@""" + Path.Combine(Application.StartupPath + "\temp\", Regex.Replace(youtubetitle, "[^a-zA-Z0-9]", "") + ".jpg").Replace("\", "\\") + """" +
                                         " https://spee.ch/api/claim/publish",
                            .UseShellExecute = False,
                            .RedirectStandardOutput = True,
                            .WindowStyle = ProcessWindowStyle.Hidden,
                            .CreateNoWindow = True
                            }
                    Updateconsoleoutput(pthumbupload.Arguments)
                    Dim proc1 As Process = Process.Start(pthumbupload)
                    While Not proc1.StandardOutput.EndOfStream
                        line1 = proc1.StandardOutput.ReadLine()
                        Updateconsoleoutput(line1)
                        If line1.Contains("{""success""") Then
                            jsonline1 = line1
                            If File.Exists(Path.Combine(Application.StartupPath + "\temp\", Regex.Replace(youtubetitle, "[^a-zA-Z0-9]", "") + ".jpg")) = True Then
                                Dim delInstance As DeleteFileThreadDelegate = New DeleteFileThreadDelegate(AddressOf DeleteFileThread)
                                delInstance.BeginInvoke(Path.Combine(Application.StartupPath + "\temp\", Regex.Replace(youtubetitle, "[^a-zA-Z0-9]", "") + ".jpg"), Nothing, Nothing)
                            End If
                            Exit While
                        End If
                    End While
                End If
                '{"success":false,"message":"No matching claim id could be found for that url"}
                Try
                    If jsonline1 <> "" Then
                        Updateconsoleoutput("jsonline " + jsonline1)
                        Dim json1 As String = jsonline1
                        Dim ser1 As JObject = JObject.Parse(json1)
                        Dim data1 As List(Of JToken) = ser1.Children().ToList
                        Dim imagesuccess As Boolean = False
                        Dim imageserveUrl As String = ""
                        For Each item As JProperty In data1
                            item.CreateReader()
                            Select Case item.Name
                                Case "success"
                                    imagesuccess = item.Value
                                Case "data"
                                    For Each msg As String In item.Values
                                        'imageserveUrl = msg("serveUrl")
                                        If msg.Contains(".jpg") Then
                                            Updateconsoleoutput(msg)
                                            imageserveUrl = msg
                                            Exit For
                                        End If
                                    Next
                                    Exit For
                            End Select
                        Next
                        '{"success":false,"message":"That claim name is already taken"}
                        If imagesuccess Then
                            thumbnailPath = imageserveUrl
                        End If
                    End If
                Catch ex As Exception
                    Updateconsoleoutput(ex.Message + " " + ex.Source)
                End Try
                P = 1
                debugout.AppendText("Current spee.ch thumb " + thumbnailPath + vbNewLine)
                sw.Start()
                currentfiledownloading = Path.Combine(Application.StartupPath + "\temp\", Regex.Replace(youtubetitle, "[^a-zA-Z0-9]", "") + "." + youtubeext)
                Wc2.DownloadFileAsync(youtubeurl, currentfiledownloading)
                Timer1.Enabled = True
                Do While P = 1
                    Thread.Sleep(200)
                    Application.DoEvents()
                Loop
                Dim tagliststring As String = ""
                For Each item As Object In CheckedListBoxTags.Items
                    If CheckedListBoxTags.CheckedItems.Contains(item) Then
                        tagliststring += " --tags=""" + item + """"

                    End If
                Next
                If downloadfee.Text = "0" Or downloadfee.Text = "0.0" Or downloadfee.Text = "" Then
                    Dim pLbrynet As New ProcessStartInfo With
                    {
                        .FileName = lbrynetpath.Text,
                        .Arguments = "publish " + Regex.Replace(youtubetitle, "[^a-zA-Z0-9]", "") + " " +
                                     " --bid=" + BID.Text +
                                     " --file_path=""" + Path.Combine(Application.StartupPath + "\temp\", Regex.Replace(youtubetitle, "[^a-zA-Z0-9]", "") + "." + youtubeext).Replace("\", "\\") + """" +
                                     tagliststring +
                                     " --title=""" + youtubetitle + """" +
                                     " --languages=en" +
                                     " --description=""" + youtubedescription + """" +
                                     " --license=""" + license.Text + """" +
                                     " --license_url=" + licenseurl.Text +
                                     " --thumbnail_url=""" + thumbnailPath + """" +
                                     " --channel_name=""" + ChannelID.Text + """",
                        .UseShellExecute = False,
                        .RedirectStandardOutput = True,
                        .WindowStyle = ProcessWindowStyle.Hidden,
                        .CreateNoWindow = True
                    }
                    Updateconsoleoutput(pLbrynet.Arguments)
                    Dim proc2 As Process = Process.Start(pLbrynet)
                    debugout.AppendText("Current LBRY return " + vbNewLine)
                    While Not proc2.StandardOutput.EndOfStream
                        Dim line2 As String = proc2.StandardOutput.ReadLine()
                        debugout.AppendText(line2 + vbNewLine)
                        Updateconsoleoutput(line2)
                    End While
                    If File.Exists(Path.Combine(Application.StartupPath + "\temp\", Regex.Replace(youtubetitle, "[^a-zA-Z0-9]", "") + "." + youtubeext)) = True Then
                        Dim delInstance As DeleteFileThreadDelegate = New DeleteFileThreadDelegate(AddressOf DeleteFileThread)
                        delInstance.BeginInvoke(Path.Combine(Application.StartupPath + "\temp\", Regex.Replace(youtubetitle, "[^a-zA-Z0-9]", "") + "." + youtubeext), Nothing, Nothing)
                    End If
                Else
                    Dim pLbrynet As New ProcessStartInfo With
                    {
                        .FileName = lbrynetpath.Text,
                        .Arguments = "publish " + Regex.Replace(youtubetitle, "[^a-zA-Z0-9]", "") + DateTime.Now.ToString("yyyyMMddHHmmss") + " " +
                                     " --bid=" + BID.Text +
                                     " --file_path=""" + Path.Combine(Application.StartupPath + "\temp\", Regex.Replace(youtubetitle, "[^a-zA-Z0-9]", "") + "." + youtubeext).Replace("\", "\\") + """" +
                                     tagliststring +
                                     " --fee_amount=" + downloadfee.Text +
                                     " --title=""" + youtubetitle + """" +
                                     " --languages=en" +
                                     " --description=""" + youtubedescription + """" +
                                     " --license=""" + license.Text + """" +
                                     " --license_url=" + licenseurl.Text +
                                     " --thumbnail_url=""" + thumbnailPath + """" +
                                     " --channel_name=""" + ChannelID.Text + """",
                        .UseShellExecute = False,
                        .RedirectStandardOutput = True,
                        .WindowStyle = ProcessWindowStyle.Hidden,
                        .CreateNoWindow = True
                    }
                    Updateconsoleoutput(pLbrynet.Arguments)
                    debugout.AppendText("Current LBRY args " + pLbrynet.Arguments + vbNewLine)
                    Dim proc2 As Process = Process.Start(pLbrynet)
                    debugout.AppendText("Current LBRY return " + vbNewLine)
                    While Not proc2.StandardOutput.EndOfStream
                        Dim line2 As String = proc2.StandardOutput.ReadLine()
                        debugout.AppendText(line2 + vbNewLine)
                        Updateconsoleoutput(line2)
                    End While
                    If File.Exists(Path.Combine(Application.StartupPath + "\temp\", Regex.Replace(youtubetitle, "[^a-zA-Z0-9]", "") + "." + youtubeext)) = True Then
                        Dim delInstance As DeleteFileThreadDelegate = New DeleteFileThreadDelegate(AddressOf DeleteFileThread)
                        delInstance.BeginInvoke(Path.Combine(Application.StartupPath + "\temp\", Regex.Replace(youtubetitle, "[^a-zA-Z0-9]", "") + "." + youtubeext), Nothing, Nothing)
                    End If
                End If
            Else
                debugout.AppendText(youtubetitle + " already found in your LBRY list" + vbNewLine)
            End If
        End While
        upload_button.Enabled = True
        Button3.Enabled = True
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim fileDialog As New OpenFileDialog
        With fileDialog
            .Title = "Select The lbrynet.exe To Run"
            If Trim(lbrynetpath.Text) <> "" Then _
            .InitialDirectory = Path.GetDirectoryName(lbrynetpath.Text)
            .Filter = "Program Files (lbrynet.exe)|lbrynet.exe"
            .FileName = Path.GetFileName(lbrynetpath.Text)
            .ValidateNames = False
            If .ShowDialog() <> DialogResult.OK Then _
                Exit Sub
            If .FileName = "" Then _
                Exit Sub
            lbrynetpath.Text = .FileName
            debugout.AppendText("lbrynet.exe set to " + lbrynetpath.Text + vbNewLine)
        End With
    End Sub
    Friend Shared Form1Instance As Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Form1Instance = Me
        If Not String.IsNullOrEmpty(My.Settings.TagsListChecked) Then
            My.Settings.TagsListChecked.Split(","c).ToList().ForEach(Function(item)
                                                                         CheckedListBoxTags.Items.Add(item, True)
                                                                         Return True
                                                                     End Function)
        End If
        If Not String.IsNullOrEmpty(My.Settings.TagsList) Then
            My.Settings.TagsList.Split(","c).ToList().ForEach(Function(item)
                                                                  CheckedListBoxTags.Items.Add(item, False)
                                                                  Return True
                                                              End Function)
        End If
        youtubeplaylist.Text = My.Settings.playlist
        lbrynetpath.Text = My.Settings.lbrynetpath
        BID.Text = My.Settings.BID
        downloadfee.Text = My.Settings.downloadfee
        ChannelID.Text = My.Settings.ChannelID
        licenseurl.Text = My.Settings.licenseurl
        license.Text = My.Settings.license
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        Wc2.Dispose()
        Dim notcheckeditems As New List(Of String)()
        For Each item As Object In CheckedListBoxTags.Items
            If Not CheckedListBoxTags.CheckedItems.Contains(item) Then
                notcheckeditems.Add(item)
            End If
        Next
        Dim indicesChecked = Me.CheckedListBoxTags.CheckedItems.Cast(Of String)().ToArray()
        My.Settings.TagsList = String.Join(",", notcheckeditems)
        My.Settings.TagsListChecked = String.Join(",", indicesChecked)
        My.Settings.playlist = youtubeplaylist.Text
        My.Settings.lbrynetpath = lbrynetpath.Text
        My.Settings.BID = BID.Text
        My.Settings.downloadfee = downloadfee.Text
        My.Settings.ChannelID = ChannelID.Text
        My.Settings.licenseurl = licenseurl.Text
        My.Settings.license = license.Text
        My.Settings.Save()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Button1.Enabled = False
        Button2.Enabled = False
        upload_button.Enabled = False
        ProxyChecker.Program.Check()
        Button1.Enabled = True
        Button2.Enabled = True
        upload_button.Enabled = True
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Button1.Enabled = False
        Button2.Enabled = False
        upload_button.Enabled = False
        ProxyChecker.Program.Scrape()
        Button1.Enabled = True
        Button2.Enabled = True
        upload_button.Enabled = True
    End Sub

    Private Sub Delcheckedtags_Click(sender As Object, e As EventArgs) Handles delcheckedtags.Click
        With CheckedListBoxTags
            If .CheckedItems.Count > 0 Then
                For checked As Integer = .CheckedItems.Count - 1 To 0 Step -1
                    .Items.Remove(.CheckedItems(checked))
                Next
            End If
        End With
    End Sub

    Private Sub Addtags_Click(sender As Object, e As EventArgs) Handles Addtags.Click
        CheckedListBoxTags.Items.Add(addtagtextbox.Text)
    End Sub
End Class
