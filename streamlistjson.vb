Imports Newtonsoft.Json

Public Class Streamlistjson

    Public Class Rootobject
        Public Property Property1() As Class1
    End Class

    Public Class Class1
        Public Property address As String
        Public Property amount As String
        Public Property claim_id As String
        Public Property claim_op As String
        Public Property confirmations As Integer
        Public Property height As Integer
        Public Property is_change As Boolean
        Public Property is_channel_signature_valid As Boolean
        Public Property is_mine As Boolean
        Public Property name As String
        Public Property nout As Integer
        Public Property permanent_url As String
        Public Property signing_channel As Signing_Channel
        Public Property timestamp As Integer
        Public Property txid As String
        Public Property type As String
        Public Property value As Value1
        Public Property value_type As String
    End Class

    Public Class Signing_Channel
        Public Property claim_id As String
        Public Property name As String
        Public Property value As Value
    End Class

    Public Class Value
        Public Property cover As Cover
        Public Property description As String
        Public Property email As String
        Public Property public_key As String
        Public Property thumbnail As Thumbnail
        Public Property title As String
        Public Property website_url As String
    End Class

    Public Class Cover
        Public Property url As String
    End Class

    Public Class Thumbnail
        Public Property url As String
    End Class

    Public Class Value1
        Public Property description As String
        Public Property license As String
        Public Property license_url As String
        Public Property source As Source
        Public Property stream_type As String
        Public Property thumbnail As Thumbnail1
        Public Property title As String
        Public Property video As Video
    End Class

    Public Class Source
        Public Property hash As String
        Public Property media_type As String
        Public Property name As String
        Public Property sd_hash As String
        Public Property size As String
    End Class

    Public Class Thumbnail1
        Public Property url As String
    End Class

    Public Class Video
        Public Property duration As Integer
        Public Property height As Integer
        Public Property width As Integer
    End Class

    Public Shared dataoSource As List(Of Streamlistjson.Class1)

    Public Shared Function Checkvideotitleexsists(ByVal title As String) As Boolean
        Dim found As Boolean = False
        If IsNothing(dataoSource) Then
            Dim pLbrynetlist As New ProcessStartInfo With
                {
                .FileName = Form1.TxtFileName.Text,
                .Arguments = "stream list",
                .UseShellExecute = False,
                .RedirectStandardOutput = True,
                .RedirectStandardError = True,
                .WindowStyle = ProcessWindowStyle.Hidden,
                .CreateNoWindow = True
                }
            Console.WriteLine(pLbrynetlist.Arguments)
            Dim proc4 As Process = Process.Start(pLbrynetlist)
            Dim output As String = proc4.StandardOutput.ReadToEnd()
            If output.Contains("Could not connect to daemon") Then
                MsgBox("Please start LBRY desktop app")
                Return True
            End If
            Dim Err As String = proc4.StandardError.ReadToEnd()
            proc4.WaitForExit()
            Dim result = JsonConvert.DeserializeObject(output)
            dataoSource = JsonConvert.DeserializeObject(Of List(Of Streamlistjson.Class1))(output)
        End If
        For Each Source As Streamlistjson.Class1 In dataoSource
            If title.Contains(Source.value.title) Then
                found = True
                Exit For
            End If
        Next
        Return found
    End Function








End Class
