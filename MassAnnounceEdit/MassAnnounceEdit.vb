Imports System
Imports System.Io
Imports System.Text
Imports EAD.Torrent
Imports EAD.Torrent.Announces

Public Class MassAnnounceEdit
    Inherits System.Windows.Forms.Form
    Dim torrentfileload As Integer

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
    Friend WithEvents FolderToProcess As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents NewAnnounceUrl As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents UseAnnounceList As System.Windows.Forms.CheckBox
    Friend WithEvents ProcessNow As System.Windows.Forms.Button
    Friend WithEvents BrowseForFolder As System.Windows.Forms.Button
    Friend WithEvents FolderBrowse As System.Windows.Forms.FolderBrowserDialog
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(MassAnnounceEdit))
        Me.FolderToProcess = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.NewAnnounceUrl = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.UseAnnounceList = New System.Windows.Forms.CheckBox
        Me.ProcessNow = New System.Windows.Forms.Button
        Me.BrowseForFolder = New System.Windows.Forms.Button
        Me.FolderBrowse = New System.Windows.Forms.FolderBrowserDialog
        Me.SuspendLayout()
        '
        'FolderToProcess
        '
        Me.FolderToProcess.Location = New System.Drawing.Point(0, 24)
        Me.FolderToProcess.Name = "FolderToProcess"
        Me.FolderToProcess.Size = New System.Drawing.Size(272, 20)
        Me.FolderToProcess.TabIndex = 0
        Me.FolderToProcess.Text = ""
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(0, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(272, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Folder to process:"
        '
        'NewAnnounceUrl
        '
        Me.NewAnnounceUrl.Location = New System.Drawing.Point(0, 88)
        Me.NewAnnounceUrl.Name = "NewAnnounceUrl"
        Me.NewAnnounceUrl.Size = New System.Drawing.Size(272, 20)
        Me.NewAnnounceUrl.TabIndex = 2
        Me.NewAnnounceUrl.Text = ""
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(0, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(272, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Announce URL:"
        '
        'UseAnnounceList
        '
        Me.UseAnnounceList.Enabled = False
        Me.UseAnnounceList.Location = New System.Drawing.Point(0, 112)
        Me.UseAnnounceList.Name = "UseAnnounceList"
        Me.UseAnnounceList.Size = New System.Drawing.Size(272, 16)
        Me.UseAnnounceList.TabIndex = 4
        Me.UseAnnounceList.Text = "Include Announce List from MultiTracker Editor"
        '
        'ProcessNow
        '
        Me.ProcessNow.Location = New System.Drawing.Point(184, 128)
        Me.ProcessNow.Name = "ProcessNow"
        Me.ProcessNow.Size = New System.Drawing.Size(88, 24)
        Me.ProcessNow.TabIndex = 5
        Me.ProcessNow.Text = "Process"
        '
        'BrowseForFolder
        '
        Me.BrowseForFolder.Location = New System.Drawing.Point(184, 48)
        Me.BrowseForFolder.Name = "BrowseForFolder"
        Me.BrowseForFolder.Size = New System.Drawing.Size(88, 24)
        Me.BrowseForFolder.TabIndex = 6
        Me.BrowseForFolder.Text = "Browse"
        '
        'FolderBrowse
        '
        Me.FolderBrowse.ShowNewFolderButton = False
        '
        'MassAnnounceEdit
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(272, 151)
        Me.Controls.Add(Me.BrowseForFolder)
        Me.Controls.Add(Me.ProcessNow)
        Me.Controls.Add(Me.UseAnnounceList)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.NewAnnounceUrl)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.FolderToProcess)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "MassAnnounceEdit"
        Me.Text = "Force Announce URLs on a folder."
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub ProcessNow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProcessNow.Click
        NewAnnounceUrl.Text = Trim(NewAnnounceUrl.Text)
        If IsValidHTTPAnnounce(NewAnnounceUrl.Text) Then
            ProcessNow.Enabled = False
            Dim NewAnnounce As New TorrentString
            Dim ProcessedFiles As Integer = 0
            NewAnnounce.Value = NewAnnounceUrl.Text
            Dim FolderFiles() As String
            Dim fulltorrent As String
            FolderFiles = Directory.GetFiles(FolderToProcess.Text, "*.torrent")
            For Each TorrentFile As String In FolderFiles
                Dim TorrentData As New TorrentDictionary
                torrentfileload = FreeFile()
                FileOpen(torrentfileload, TorrentFile, OpenMode.Binary, OpenAccess.Default)
                Dim intermediarytorrentdata As String = Space(FileLen(TorrentFile))
                FileGet(torrentfileload, intermediarytorrentdata)
                FileClose(torrentfileload)
                TorrentData.Parse(intermediarytorrentdata)
                If TorrentData.Contains("announce-list") Then TorrentData.Remove("announce-list")
                If TorrentData.Contains("resume") Then TorrentData.Remove("resume")
                If TorrentData.Contains("tracker_cache") Then TorrentData.Remove("tracker_cache")
                If TorrentData.Contains("torrent filename") Then TorrentData.Remove("torrent filename")
                TorrentData.Value("announce") = NewAnnounce
                'TorrentData.Add("announce-list", AnnounceListTiers)
                fulltorrent = TorrentData.Bencoded()
                Kill(TorrentFile)
                torrentfileload = FreeFile()
                FileOpen(torrentfileload, TorrentFile, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
                FilePut(torrentfileload, fulltorrent)
                FileClose(torrentfileload)
                ProcessedFiles = ProcessedFiles + 1
                System.Windows.Forms.Application.DoEvents()
            Next
            MsgBox("Files Processed: " & ProcessedFiles)
            ProcessNow.Enabled = True
        Else
            MsgBox("Error: Invalid Announce URL", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub BrowseForFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseForFolder.Click
        Dim folderselect As DialogResult
        folderselect = FolderBrowse.ShowDialog()
        If folderselect = DialogResult.OK Then FolderToProcess.Text = FolderBrowse.SelectedPath
    End Sub
End Class
