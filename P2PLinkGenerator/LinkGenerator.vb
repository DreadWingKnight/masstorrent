'--------------------------------------------------------
' LinkGenerator - LinkGenerator.VB
' P2P FileSharing Link Generator
' Harold Feit
'--------------------------------------------------------
Imports EAD.Torrent
Imports EAD.PeerToPeer

Public Class LinkGenerator
    Inherits System.Windows.Forms.Form
    Public Shared LocalPath As String

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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GetMagnet As System.Windows.Forms.CheckBox
    Friend WithEvents GetED2K As System.Windows.Forms.CheckBox
    Friend WithEvents SaveSettings As System.Windows.Forms.Button
    Friend WithEvents SaveAndLeave As System.Windows.Forms.Button
    Friend WithEvents GenerateLinks As System.Windows.Forms.Button
    Friend WithEvents BrowseForTorrent As System.Windows.Forms.Button
    Friend WithEvents TorrentFileOpen As System.Windows.Forms.OpenFileDialog
    Friend WithEvents LeaveNow As System.Windows.Forms.Button
    Friend WithEvents TorrentToParse As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(LinkGenerator))
        Me.Label1 = New System.Windows.Forms.Label
        Me.TorrentToParse = New System.Windows.Forms.TextBox
        Me.TorrentFileOpen = New System.Windows.Forms.OpenFileDialog
        Me.BrowseForTorrent = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.GetMagnet = New System.Windows.Forms.CheckBox
        Me.GetED2K = New System.Windows.Forms.CheckBox
        Me.GenerateLinks = New System.Windows.Forms.Button
        Me.SaveSettings = New System.Windows.Forms.Button
        Me.SaveAndLeave = New System.Windows.Forms.Button
        Me.LeaveNow = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(0, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(288, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Torrent file to generate links from:"
        '
        'TorrentToParse
        '
        Me.TorrentToParse.Location = New System.Drawing.Point(0, 24)
        Me.TorrentToParse.Name = "TorrentToParse"
        Me.TorrentToParse.Size = New System.Drawing.Size(288, 20)
        Me.TorrentToParse.TabIndex = 1
        Me.TorrentToParse.Text = ""
        '
        'TorrentFileOpen
        '
        Me.TorrentFileOpen.Filter = "Torrents|*.torrent"
        '
        'BrowseForTorrent
        '
        Me.BrowseForTorrent.Location = New System.Drawing.Point(288, 24)
        Me.BrowseForTorrent.Name = "BrowseForTorrent"
        Me.BrowseForTorrent.Size = New System.Drawing.Size(88, 24)
        Me.BrowseForTorrent.TabIndex = 2
        Me.BrowseForTorrent.Text = "Browse"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(0, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(288, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Type of Links to generate:"
        '
        'GetMagnet
        '
        Me.GetMagnet.Location = New System.Drawing.Point(0, 72)
        Me.GetMagnet.Name = "GetMagnet"
        Me.GetMagnet.Size = New System.Drawing.Size(104, 16)
        Me.GetMagnet.TabIndex = 4
        Me.GetMagnet.Text = "Magnet"
        '
        'GetED2K
        '
        Me.GetED2K.Location = New System.Drawing.Point(0, 88)
        Me.GetED2K.Name = "GetED2K"
        Me.GetED2K.Size = New System.Drawing.Size(104, 16)
        Me.GetED2K.TabIndex = 5
        Me.GetED2K.Text = "ED2K"
        '
        'GenerateLinks
        '
        Me.GenerateLinks.Location = New System.Drawing.Point(104, 72)
        Me.GenerateLinks.Name = "GenerateLinks"
        Me.GenerateLinks.Size = New System.Drawing.Size(88, 24)
        Me.GenerateLinks.TabIndex = 6
        Me.GenerateLinks.Text = "Generate"
        '
        'SaveSettings
        '
        Me.SaveSettings.Location = New System.Drawing.Point(200, 104)
        Me.SaveSettings.Name = "SaveSettings"
        Me.SaveSettings.Size = New System.Drawing.Size(88, 24)
        Me.SaveSettings.TabIndex = 7
        Me.SaveSettings.Text = "Save Settings"
        '
        'SaveAndLeave
        '
        Me.SaveAndLeave.Location = New System.Drawing.Point(200, 128)
        Me.SaveAndLeave.Name = "SaveAndLeave"
        Me.SaveAndLeave.Size = New System.Drawing.Size(176, 24)
        Me.SaveAndLeave.TabIndex = 8
        Me.SaveAndLeave.Text = "Save Settings and Exit"
        '
        'LeaveNow
        '
        Me.LeaveNow.Location = New System.Drawing.Point(288, 104)
        Me.LeaveNow.Name = "LeaveNow"
        Me.LeaveNow.Size = New System.Drawing.Size(88, 24)
        Me.LeaveNow.TabIndex = 9
        Me.LeaveNow.Text = "Exit"
        '
        'LinkGenerator
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(376, 157)
        Me.Controls.Add(Me.LeaveNow)
        Me.Controls.Add(Me.SaveAndLeave)
        Me.Controls.Add(Me.SaveSettings)
        Me.Controls.Add(Me.GenerateLinks)
        Me.Controls.Add(Me.GetED2K)
        Me.Controls.Add(Me.GetMagnet)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.BrowseForTorrent)
        Me.Controls.Add(Me.TorrentToParse)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "LinkGenerator"
        Me.Text = "Generate P2P Links From a Torrent"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub LinkGenerator_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim arguments As [String]() = Environment.GetCommandLineArgs()
        If arguments.GetLength(0) > 1 Then
            TorrentToParse.Text = arguments(1)
        End If
        Dim FileOffset As Integer
        FileOffset = Microsoft.VisualBasic.Left(arguments(0), Len(arguments(0))).LastIndexOf("\")
        LocalPath = System.IO.Path.GetFullPath(Microsoft.VisualBasic.Left(arguments(0), FileOffset)) + "\"
        If System.IO.File.Exists(LocalPath + "linkgen.configure") Then
            Dim loadsettings As Integer = FreeFile()
            Dim intermediarysettingdata As String
            FileOpen(loadsettings, LocalPath + "linkgen.configure", OpenMode.Binary, OpenAccess.Read, OpenShare.LockRead)
            intermediarysettingdata = Space(FileLen(LocalPath + "linkgen.configure"))
            FileGet(loadsettings, intermediarysettingdata)
            FileClose(loadsettings)
            Dim ConfigData As New TorrentDictionary
            ConfigData.Parse(intermediarysettingdata)
            Dim GenerateED2K As New TorrentNumber
            Dim GenerateMagnet As New TorrentNumber
            GenerateED2K = ConfigData.Value("ed2k")
            GenerateMagnet = ConfigData.Value("magnet")
            GetED2K.Checked = GenerateED2K.Value
            GetMagnet.Checked = GenerateMagnet.Value
        End If
    End Sub

    Private Sub SaveSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveSettings.Click
        Dim ConfigData As New TorrentDictionary
        If System.IO.File.Exists(LocalPath + "linkgen.configure") Then
            Dim loadsettings As Integer = FreeFile()
            Dim intermediarysettingdata As String
            FileOpen(loadsettings, LocalPath + "linkgen.configure", OpenMode.Binary, OpenAccess.Read, OpenShare.LockRead)
            intermediarysettingdata = Space(FileLen(LocalPath + "linkgen.configure"))
            FileGet(loadsettings, intermediarysettingdata)
            FileClose(loadsettings)
            ConfigData.Parse(intermediarysettingdata)
            Kill(LocalPath + "linkgen.configure")
        End If
        Dim GenerateED2K As New TorrentNumber
        Dim GenerateMagnet As New TorrentNumber
        GenerateED2K.Value = GetED2K.Checked
        GenerateMagnet.Value = GetMagnet.Checked
        ConfigData.Value("ed2k") = GenerateED2K
        ConfigData.Value("magnet") = GenerateMagnet
        Dim savesettings As Integer = FreeFile()
        FileOpen(savesettings, LocalPath + "linkgen.configure", OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
        FilePut(savesettings, ConfigData.Bencoded())
        FileClose(savesettings)
    End Sub

    Private Sub SaveAndLeave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAndLeave.Click
        Call SaveSettings_Click(sender, e)
        Call LeaveNow_Click(sender, e)
    End Sub

    Private Sub BrowseForTorrent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseForTorrent.Click
        TorrentFileOpen.ShowDialog()
    End Sub

    Private Sub LeaveNow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LeaveNow.Click
        End
    End Sub

    Private Sub GenerateLinks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GenerateLinks.Click
        Dim LoadTorrent As Integer = FreeFile()
        Dim TorrentRaw As String = Space(FileLen(TorrentToParse.Text))
        FileOpen(LoadTorrent, TorrentToParse.Text, OpenMode.Binary)
        FileGet(LoadTorrent, TorrentRaw)
        FileClose(LoadTorrent)
        Dim TorrentData As New TorrentDictionary
        Dim TorrentMeta As New TorrentMetaData
        Dim TorrentInfo As New TorrentDictionary
        TorrentData.Parse(TorrentRaw)
        TorrentMeta.Torrent = TorrentData
        TorrentInfo = TorrentData.Value("info")
        Dim LinkOutput As String
        Dim Linkbuild As New EAD.PeerToPeer.LinkGeneration
        Dim HasLinks As Boolean = False
        If TorrentMeta.MultiFile Then
            For Each FileToProcess As TorrentDictionary In TorrentInfo.Value("files")
                Linkbuild = New EAD.PeerToPeer.LinkGeneration
                Dim pathname As ArrayList = FileToProcess.Value("path").value
                For Each filename As TorrentString In pathname
                    Linkbuild.FileName = filename.Value
                Next
                Linkbuild.FileSize = FileToProcess.Value("length").value
                Dim LinkED2K As New TorrentString
                Dim LinkSHA1 As New TorrentString
                Dim LinkTiger As New TorrentString
                If FileToProcess.Contains("ed2k") Then
                    LinkED2k = FileToProcess.Value("ed2k")
                    Linkbuild.ED2KRaw = linked2k.value
                End If
                If FileToProcess.Contains("sha1") Then
                    LinkSHA1 = FileToProcess.Value("sha1")
                    Linkbuild.SHA1Raw = Linksha1.Value
                End If
                If FileToProcess.Contains("tiger") Then
                    linktiger = FileToProcess.Value("tiger")
                    Linkbuild.TigerRaw = linktiger.Value
                End If
                If GetMagnet.Checked Then
                    If Not Linkbuild.TTH = "" And Not Linkbuild.SHA1Hash = "" And Not Linkbuild.ED2KHex = "" Then
                        LinkOutput = LinkOutput + Linkbuild.MagnetBitPrintHybrid + Chr(13) + Chr(10)
                        HasLinks = True
                    ElseIf Not Linkbuild.TTH = "" And Not Linkbuild.SHA1Hash = "" And Linkbuild.ED2KHex = "" Then
                        LinkOutput = LinkOutput + Linkbuild.MagnetBitPrint + Chr(13) + Chr(10)
                        HasLinks = True
                    ElseIf Linkbuild.TTH = "" And Not Linkbuild.SHA1Hash = "" Then
                        LinkOutput = LinkOutput + Linkbuild.MagnetSHA1 + Chr(13) + Chr(10)
                        HasLinks = True
                    ElseIf Linkbuild.TTH = "" And Not Linkbuild.SHA1Hash = "" And Not Linkbuild.ED2KHex = "" Then
                        LinkOutput = LinkOutput + Linkbuild.MagnetSHA1Hybrid + Chr(13) + Chr(10)
                        HasLinks = True
                    ElseIf Linkbuild.TTH = "" And Linkbuild.SHA1Hash = "" And Not Linkbuild.ED2KHex = "" Then
                        LinkOutput = LinkOutput + Linkbuild.MagnetED2K + Chr(13) + Chr(10)
                        HasLinks = True
                    End If
                End If
                If GetED2K.Checked Then
                    If Not Linkbuild.ED2KHex = "" Then
                        LinkOutput = LinkOutput + Linkbuild.ClassicED2KLink + Chr(13) + Chr(10)
                        HasLinks = True
                    End If
                End If
            Next
        Else
            Linkbuild = New EAD.PeerToPeer.LinkGeneration
            Dim LinkED2K As New TorrentString
            Dim LinkSHA1 As New TorrentString
            Dim LinkTiger As New TorrentString
            If TorrentInfo.Contains("ed2k") Then
                LinkED2K = TorrentInfo.Value("ed2k")
                Linkbuild.ED2KRaw = LinkED2K.Value
            End If
            If TorrentInfo.Contains("sha1") Then
                LinkSHA1 = TorrentInfo.Value("sha1")
                Linkbuild.SHA1Raw = LinkSHA1.Value
            End If
            If TorrentInfo.Contains("tiger") Then
                LinkTiger = TorrentInfo.Value("tiger")
                Linkbuild.TigerRaw = LinkTiger.Value
            End If
            Linkbuild.FileName = TorrentInfo.Value("name").value
            Linkbuild.FileSize = TorrentInfo.Value("length").value
            If GetMagnet.Checked Then
                If Not Linkbuild.TTH = "" And Not Linkbuild.SHA1Hash = "" And Not Linkbuild.ED2KHex = "" Then
                    LinkOutput = LinkOutput + Linkbuild.MagnetBitPrintHybrid + Chr(13) + Chr(10)
                    HasLinks = True
                ElseIf Not Linkbuild.TTH = "" And Not Linkbuild.SHA1Hash = "" And Linkbuild.ED2KHex = "" Then
                    LinkOutput = LinkOutput + Linkbuild.MagnetBitPrint + Chr(13) + Chr(10)
                    HasLinks = True
                ElseIf Linkbuild.TTH = "" And Not Linkbuild.SHA1Hash = "" Then
                    LinkOutput = LinkOutput + Linkbuild.MagnetSHA1 + Chr(13) + Chr(10)
                    HasLinks = True
                ElseIf Linkbuild.TTH = "" And Not Linkbuild.SHA1Hash = "" And Not Linkbuild.ED2KHex = "" Then
                    LinkOutput = LinkOutput + Linkbuild.MagnetSHA1Hybrid + Chr(13) + Chr(10)
                    HasLinks = True
                ElseIf Linkbuild.TTH = "" And Linkbuild.SHA1Hash = "" And Not Linkbuild.ED2KHex = "" Then
                    LinkOutput = LinkOutput + Linkbuild.MagnetED2K + Chr(13) + Chr(10)
                    HasLinks = True
                End If
            End If
            If GetED2K.Checked Then
                If Not Linkbuild.ED2KHex = "" Then
                    LinkOutput = LinkOutput + Linkbuild.ClassicED2KLink + Chr(13) + Chr(10)
                    HasLinks = True
                End If
            End If
        End If
        If HasLinks = True Then
            Dim outputfilename As String = Microsoft.VisualBasic.Left(TorrentToParse.Text, Len(TorrentToParse.Text) - 7) + "links"
            Dim FileToOpen As Integer = FreeFile()
            If System.IO.File.Exists(outputfilename) Then Kill(outputfilename)
            FileOpen(FileToOpen, outputfilename, OpenMode.Binary)
            FilePut(FileToOpen, LinkOutput)
            FileClose(FileToOpen)
            MsgBox("Link file generated")
        Else
            MsgBox("No Links to generate, No compatible optional hashes were included in the torrent." + Chr(10) + "Compatible Optional hashes include: SHA1, ED2K, SHA1+Tiger")
        End If
    End Sub

    Private Sub TorrentFileOpen_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TorrentFileOpen.FileOk
        TorrentToParse.Text = TorrentFileOpen.FileName
    End Sub
End Class
