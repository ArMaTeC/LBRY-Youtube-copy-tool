<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.upload_button = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.lbrynetpath = New System.Windows.Forms.TextBox()
        Me.BID = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.downloadfee = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ChannelID = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.license = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.licenseurl = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.youtubeplaylist = New System.Windows.Forms.TextBox()
        Me.debugout = New System.Windows.Forms.TextBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.userproxycheck = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.proxystringinfo = New System.Windows.Forms.Label()
        Me.CheckedListBoxTags = New System.Windows.Forms.CheckedListBox()
        Me.delcheckedtags = New System.Windows.Forms.Button()
        Me.Addtags = New System.Windows.Forms.Button()
        Me.addtagtextbox = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ProgressBar1 = New LBRY_Youtube_copy_tool.ProgressbarWithPercentage()
        Me.SuspendLayout()
        '
        'upload_button
        '
        Me.upload_button.Location = New System.Drawing.Point(513, 34)
        Me.upload_button.Name = "upload_button"
        Me.upload_button.Size = New System.Drawing.Size(196, 23)
        Me.upload_button.TabIndex = 0
        Me.upload_button.Text = "Upload"
        Me.upload_button.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(513, 5)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(275, 23)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "Select lbrynet.exe"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'lbrynetpath
        '
        Me.lbrynetpath.Location = New System.Drawing.Point(12, 7)
        Me.lbrynetpath.Name = "lbrynetpath"
        Me.lbrynetpath.Size = New System.Drawing.Size(495, 20)
        Me.lbrynetpath.TabIndex = 4
        Me.lbrynetpath.Text = "C:\Program Files\LBRY\resources\static\daemon\lbrynet.exe"
        Me.ToolTip1.SetToolTip(Me.lbrynetpath, "Location of the lbrynet.exe normally C:\Program Files\LBRY\resources\static\daemo" &
        "n\lbrynet.exe")
        '
        'BID
        '
        Me.BID.Location = New System.Drawing.Point(141, 37)
        Me.BID.Name = "BID"
        Me.BID.Size = New System.Drawing.Size(366, 20)
        Me.BID.TabIndex = 5
        Me.BID.Text = "0.5"
        Me.ToolTip1.SetToolTip(Me.BID, "Amount of LBC to back your upload")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(127, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Amount to back the claim"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(111, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Content download fee"
        '
        'downloadfee
        '
        Me.downloadfee.Location = New System.Drawing.Point(141, 60)
        Me.downloadfee.Name = "downloadfee"
        Me.downloadfee.Size = New System.Drawing.Size(366, 20)
        Me.downloadfee.TabIndex = 7
        Me.downloadfee.Text = "0.0"
        Me.ToolTip1.SetToolTip(Me.downloadfee, "Cost in LBC to download the content")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 87)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Publisher channel"
        '
        'ChannelID
        '
        Me.ChannelID.Location = New System.Drawing.Point(141, 84)
        Me.ChannelID.Name = "ChannelID"
        Me.ChannelID.Size = New System.Drawing.Size(366, 20)
        Me.ChannelID.TabIndex = 9
        Me.ChannelID.Text = "@CLRPG"
        Me.ToolTip1.SetToolTip(Me.ChannelID, "Your target chnnel")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 111)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Publication license"
        '
        'license
        '
        Me.license.Location = New System.Drawing.Point(141, 108)
        Me.license.Name = "license"
        Me.license.Size = New System.Drawing.Size(366, 20)
        Me.license.TabIndex = 11
        Me.license.Text = "Public Domain"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 134)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(109, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Publication license url"
        '
        'licenseurl
        '
        Me.licenseurl.Location = New System.Drawing.Point(141, 131)
        Me.licenseurl.Name = "licenseurl"
        Me.licenseurl.Size = New System.Drawing.Size(366, 20)
        Me.licenseurl.TabIndex = 13
        Me.licenseurl.Text = "http://public-domain.org"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 160)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(95, 13)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Youtube playlist url"
        '
        'youtubeplaylist
        '
        Me.youtubeplaylist.Location = New System.Drawing.Point(141, 157)
        Me.youtubeplaylist.Name = "youtubeplaylist"
        Me.youtubeplaylist.Size = New System.Drawing.Size(366, 20)
        Me.youtubeplaylist.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.youtubeplaylist, "Please enter a Youtube playlist url E.G. https://www.youtube.com/playlist?list=UU" &
        "4F3j3ed_To-M3H2YLLD5vw")
        '
        'debugout
        '
        Me.debugout.Location = New System.Drawing.Point(7, 284)
        Me.debugout.Multiline = True
        Me.debugout.Name = "debugout"
        Me.debugout.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.debugout.Size = New System.Drawing.Size(781, 125)
        Me.debugout.TabIndex = 19
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'userproxycheck
        '
        Me.userproxycheck.AutoSize = True
        Me.userproxycheck.Checked = True
        Me.userproxycheck.CheckState = System.Windows.Forms.CheckState.Checked
        Me.userproxycheck.Location = New System.Drawing.Point(715, 38)
        Me.userproxycheck.Name = "userproxycheck"
        Me.userproxycheck.Size = New System.Drawing.Size(73, 17)
        Me.userproxycheck.TabIndex = 22
        Me.userproxycheck.Text = "Use proxy"
        Me.userproxycheck.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(513, 141)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(134, 23)
        Me.Button1.TabIndex = 24
        Me.Button1.Text = "Scrape proxies"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(654, 141)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(134, 23)
        Me.Button2.TabIndex = 25
        Me.Button2.Text = "Check proxies"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'proxystringinfo
        '
        Me.proxystringinfo.AutoSize = True
        Me.proxystringinfo.Location = New System.Drawing.Point(543, 167)
        Me.proxystringinfo.Name = "proxystringinfo"
        Me.proxystringinfo.Size = New System.Drawing.Size(240, 13)
        Me.proxystringinfo.TabIndex = 26
        Me.proxystringinfo.Text = "Scraper | Total Scraped: 0 Duplicates removed: 0"
        '
        'CheckedListBoxTags
        '
        Me.CheckedListBoxTags.FormattingEnabled = True
        Me.CheckedListBoxTags.Location = New System.Drawing.Point(7, 214)
        Me.CheckedListBoxTags.MultiColumn = True
        Me.CheckedListBoxTags.Name = "CheckedListBoxTags"
        Me.CheckedListBoxTags.Size = New System.Drawing.Size(282, 64)
        Me.CheckedListBoxTags.TabIndex = 27
        '
        'delcheckedtags
        '
        Me.delcheckedtags.Location = New System.Drawing.Point(295, 213)
        Me.delcheckedtags.Name = "delcheckedtags"
        Me.delcheckedtags.Size = New System.Drawing.Size(58, 65)
        Me.delcheckedtags.TabIndex = 28
        Me.delcheckedtags.Text = "Delete Checked"
        Me.delcheckedtags.UseVisualStyleBackColor = True
        '
        'Addtags
        '
        Me.Addtags.Location = New System.Drawing.Point(8, 185)
        Me.Addtags.Name = "Addtags"
        Me.Addtags.Size = New System.Drawing.Size(134, 23)
        Me.Addtags.TabIndex = 29
        Me.Addtags.Text = "Add Tag"
        Me.Addtags.UseVisualStyleBackColor = True
        '
        'addtagtextbox
        '
        Me.addtagtextbox.Location = New System.Drawing.Point(146, 187)
        Me.addtagtextbox.Name = "addtagtextbox"
        Me.addtagtextbox.Size = New System.Drawing.Size(361, 20)
        Me.addtagtextbox.TabIndex = 30
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(7, 415)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.OverlayTextColor = System.Drawing.Color.Black
        Me.ProgressBar1.Percentage = 0R
        Me.ProgressBar1.PercentageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ProgressBar1.Size = New System.Drawing.Size(781, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar1.TabIndex = 21
        Me.ProgressBar1.TextColor = System.Drawing.Color.DarkRed
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(795, 445)
        Me.Controls.Add(Me.addtagtextbox)
        Me.Controls.Add(Me.Addtags)
        Me.Controls.Add(Me.delcheckedtags)
        Me.Controls.Add(Me.CheckedListBoxTags)
        Me.Controls.Add(Me.proxystringinfo)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.userproxycheck)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.debugout)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.youtubeplaylist)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.licenseurl)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.license)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ChannelID)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.downloadfee)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BID)
        Me.Controls.Add(Me.lbrynetpath)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.upload_button)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "LBRY Youtube copy tool"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents upload_button As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents lbrynetpath As TextBox
    Friend WithEvents BID As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents downloadfee As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents ChannelID As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents license As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents licenseurl As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents youtubeplaylist As TextBox
    Friend WithEvents debugout As TextBox
    Friend WithEvents Timer1 As Timer
    Friend WithEvents ProgressBar1 As ProgressbarWithPercentage
    Friend WithEvents userproxycheck As CheckBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents proxystringinfo As Label
    Friend WithEvents CheckedListBoxTags As CheckedListBox
    Friend WithEvents delcheckedtags As Button
    Friend WithEvents Addtags As Button
    Friend WithEvents addtagtextbox As TextBox
    Friend WithEvents ToolTip1 As ToolTip
End Class
