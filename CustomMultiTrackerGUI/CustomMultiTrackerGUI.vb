'--------------------------------------------------------
' TrimTorrents TrimTorrents.vb
' Announce URL Timmer code
' Harold Feit
' ToDo
' - Add support for Announce-list
' - Add support for Command-Line execution
'--------------------------------------------------------

Imports EAD.Torrent

Public Class TrimTorrents
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents BrowseToTrim As System.Windows.Forms.Button
    Friend WithEvents TorrentFileToTrim As System.Windows.Forms.TextBox
    Friend WithEvents TrimTorrentNow As System.Windows.Forms.Button
    Friend WithEvents LeaveNow As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TorrentToTrim As System.Windows.Forms.OpenFileDialog
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(TrimTorrents))
        Me.BrowseToTrim = New System.Windows.Forms.Button
        Me.TorrentFileToTrim = New System.Windows.Forms.TextBox
        Me.TrimTorrentNow = New System.Windows.Forms.Button
        Me.LeaveNow = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.TorrentToTrim = New System.Windows.Forms.OpenFileDialog
        Me.SuspendLayout()
        '
        'BrowseToTrim
        '
        Me.BrowseToTrim.Location = New System.Drawing.Point(0, 40)
        Me.BrowseToTrim.Name = "BrowseToTrim"
        Me.BrowseToTrim.Size = New System.Drawing.Size(288, 24)
        Me.BrowseToTrim.TabIndex = 0
        Me.BrowseToTrim.Text = "Browse for Torrent to Trim"
        '
        'TorrentFileToTrim
        '
        Me.TorrentFileToTrim.Location = New System.Drawing.Point(0, 24)
        Me.TorrentFileToTrim.Name = "TorrentFileToTrim"
        Me.TorrentFileToTrim.Size = New System.Drawing.Size(288, 20)
        Me.TorrentFileToTrim.TabIndex = 1
        Me.TorrentFileToTrim.Text = ""
        '
        'TrimTorrentNow
        '
        Me.TrimTorrentNow.Location = New System.Drawing.Point(0, 64)
        Me.TrimTorrentNow.Name = "TrimTorrentNow"
        Me.TrimTorrentNow.Size = New System.Drawing.Size(152, 24)
        Me.TrimTorrentNow.TabIndex = 2
        Me.TrimTorrentNow.Text = "Trim Now"
        '
        'LeaveNow
        '
        Me.LeaveNow.Location = New System.Drawing.Point(152, 64)
        Me.LeaveNow.Name = "LeaveNow"
        Me.LeaveNow.Size = New System.Drawing.Size(136, 24)
        Me.LeaveNow.TabIndex = 3
        Me.LeaveNow.Text = "Exit"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(280, 16)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Torrent File to Trim:"
        '
        'TorrentToTrim
        '
        Me.TorrentToTrim.Filter = "Torrent Files|*.torrent"
        '
        'TrimTorrents
        '
        Me.AcceptButton = Me.TrimTorrentNow
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(288, 93)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LeaveNow)
        Me.Controls.Add(Me.TrimTorrentNow)
        Me.Controls.Add(Me.TorrentFileToTrim)
        Me.Controls.Add(Me.BrowseToTrim)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TrimTorrents"
        Me.Text = "Trim Torrent Files"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub LeaveNow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LeaveNow.Click
        End
    End Sub

    Private Sub BrowseToTrim_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseToTrim.Click
        TorrentToTrim.ShowDialog()
    End Sub

    Private Sub TorrentToTrim_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TorrentToTrim.FileOk
        TorrentFileToTrim.Text = TorrentToTrim.FileName
    End Sub

    Private Sub TrimTorrentNow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrimTorrentNow.Click
        If Not System.IO.File.Exists(TorrentFileToTrim.Text) Then
            MsgBox("ERROR: Filename is not a valid file", , "WHOOPSIE!")
            Exit Sub
        End If
        Dim TorrentData As New TorrentDictionary
        Dim TorrentFileload As Integer
        Dim fulltorrent As String
        Dim intermediarytorrentdata As String

        Dim tier0 As New EAD.Torrent.TorrentList
        Dim tier0array As ArrayList = New ArrayList
        Dim gotwoot As New EAD.Torrent.TorrentString
        Dim gotwoot1 As New EAD.Torrent.TorrentString
        Dim gotwoot2 As New EAD.Torrent.TorrentString
        gotwoot.Value = "http://www.point-blank.cc:6969/announce"
        gotwoot1.Value = "http://www.point-blank.cc:7000/announce"
        gotwoot1.Value = "http://www.point-blank.cc:7001/announce"
        tier0array.Add(gotwoot)
        tier0array.Add(gotwoot1)
        tier0array.Add(gotwoot2)
        tier0.Value = tier0array
        Dim Tier1 As New EAD.Torrent.TorrentList
        Dim tier1array As New ArrayList
        Dim pointblank0 As New EAD.Torrent.TorrentString
        pointblank0.Value = "http://tracker.scarywater.net:443/announce"
        tier1array.Add(pointblank0)
        Tier1.Value = tier1array
        Dim tiers As New ArrayList
        tiers.Add(tier0)
        tiers.Add(Tier1)
        Dim announcelist As New EAD.Torrent.TorrentList
        announcelist.Value = tiers

        TorrentFileload = FreeFile()
        FileOpen(TorrentFileload, TorrentFileToTrim.Text, OpenMode.Binary, OpenAccess.Default)
        intermediarytorrentdata = Space(FileLen(TorrentFileToTrim.Text))
        FileGet(TorrentFileload, intermediarytorrentdata)
        FileClose(TorrentFileload)

        TorrentData.Parse(intermediarytorrentdata)
        TorrentData.Value("announce") = gotwoot
        If TorrentData.Contains("announce-list") Then
            TorrentData.Value("announce-list") = announcelist
        Else
            TorrentData.Add("announce-list", announcelist)
        End If
        MsgBox(announcelist.Bencoded())

        If TorrentData.Contains("resume") Then TorrentData.Remove("resume")
        If TorrentData.Contains("tracker_cache") Then TorrentData.Remove("tracker_cache")
        If TorrentData.Contains("torrent filename") Then TorrentData.Remove("torrent filename")

        fulltorrent = TorrentData.Bencoded()
        Kill(TorrentFileToTrim.Text)
        TorrentFileload = FreeFile()
        FileOpen(TorrentFileload, TorrentFileToTrim.Text, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
        FilePut(TorrentFileload, fulltorrent)
        FileClose(TorrentFileload)
        MsgBox("Torrent File announce URL has been trimmed")
    End Sub

    Private Sub TrimTorrents_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim arguments As [String]() = Environment.GetCommandLineArgs()
        If arguments.GetLength(0) > 1 Then
            TorrentFileToTrim.Text = arguments(1)
        End If
    End Sub
End Class
