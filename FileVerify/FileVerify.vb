'--------------------------------------------------------
' FileVerify - FileVerify.vb
' Torrent File Verification
' Harold Feit
' ToDo' - Available Hash Identification (single and multi file)' - Hash Check by SHA1' - Hash Check by MD5' - Hash Check by ED2K' - Hash Check by CRC32' - Hash Check by Tiger' - Hash Check by Torrent'--------------------------------------------------------
Imports EAD.Torrent
Imports System
Imports System.Io
Imports System.Security.Cryptography
Imports System.Text
Imports EAD.Cryptography.VisualBasic.CRC32
Imports Microsoft.VisualBasic

Public Class TorrentVerify
    Inherits System.Windows.Forms.Form
    Dim LocalPath As String
    Dim MultiFileFiles As New TorrentList

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
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TorrentFileToVerify As System.Windows.Forms.TextBox
    Friend WithEvents PathToVerify As System.Windows.Forms.TextBox
    Friend WithEvents VerifyCRC32 As System.Windows.Forms.CheckBox
    Friend WithEvents VerifyMD5 As System.Windows.Forms.CheckBox
    Friend WithEvents VerifySHA1 As System.Windows.Forms.CheckBox
    Friend WithEvents VerifyED2K As System.Windows.Forms.CheckBox
    Friend WithEvents VerifyTiger As System.Windows.Forms.CheckBox
    Friend WithEvents VerifyTorrent As System.Windows.Forms.CheckBox
    Friend WithEvents BrowseForTorrent As System.Windows.Forms.Button
    Friend WithEvents BrowseToVerify As System.Windows.Forms.Button
    Friend WithEvents FileHealthList As System.Windows.Forms.CheckedListBox
    Friend WithEvents CurrentHashProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents SaveSettings As System.Windows.Forms.Button
    Friend WithEvents SaveAndLeave As System.Windows.Forms.Button
    Friend WithEvents VerifyNow As System.Windows.Forms.Button
    Friend WithEvents TorrentFileOpen As System.Windows.Forms.OpenFileDialog
    Friend WithEvents VerifyFileOpen As System.Windows.Forms.OpenFileDialog
    Friend WithEvents VerifyFolderOpen As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents LeaveNow As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(TorrentVerify))
        Me.TorrentFileToVerify = New System.Windows.Forms.TextBox
        Me.PathToVerify = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.VerifyCRC32 = New System.Windows.Forms.CheckBox
        Me.VerifyMD5 = New System.Windows.Forms.CheckBox
        Me.VerifySHA1 = New System.Windows.Forms.CheckBox
        Me.VerifyED2K = New System.Windows.Forms.CheckBox
        Me.VerifyTiger = New System.Windows.Forms.CheckBox
        Me.VerifyTorrent = New System.Windows.Forms.CheckBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.BrowseForTorrent = New System.Windows.Forms.Button
        Me.BrowseToVerify = New System.Windows.Forms.Button
        Me.FileHealthList = New System.Windows.Forms.CheckedListBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.CurrentHashProgress = New System.Windows.Forms.ProgressBar
        Me.Label6 = New System.Windows.Forms.Label
        Me.SaveSettings = New System.Windows.Forms.Button
        Me.SaveAndLeave = New System.Windows.Forms.Button
        Me.LeaveNow = New System.Windows.Forms.Button
        Me.VerifyNow = New System.Windows.Forms.Button
        Me.TorrentFileOpen = New System.Windows.Forms.OpenFileDialog
        Me.VerifyFileOpen = New System.Windows.Forms.OpenFileDialog
        Me.VerifyFolderOpen = New System.Windows.Forms.FolderBrowserDialog
        Me.SuspendLayout()
        '
        'TorrentFileToVerify
        '
        Me.TorrentFileToVerify.Location = New System.Drawing.Point(0, 24)
        Me.TorrentFileToVerify.Name = "TorrentFileToVerify"
        Me.TorrentFileToVerify.Size = New System.Drawing.Size(288, 20)
        Me.TorrentFileToVerify.TabIndex = 0
        Me.TorrentFileToVerify.Text = ""
        '
        'PathToVerify
        '
        Me.PathToVerify.Location = New System.Drawing.Point(0, 64)
        Me.PathToVerify.Name = "PathToVerify"
        Me.PathToVerify.Size = New System.Drawing.Size(288, 20)
        Me.PathToVerify.TabIndex = 1
        Me.PathToVerify.Text = ""
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(0, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(280, 16)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Torrent To Verify"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(0, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(280, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Path to files to Verify"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(0, 88)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(288, 16)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Hashes to Verify"
        '
        'VerifyCRC32
        '
        Me.VerifyCRC32.Location = New System.Drawing.Point(96, 104)
        Me.VerifyCRC32.Name = "VerifyCRC32"
        Me.VerifyCRC32.Size = New System.Drawing.Size(96, 16)
        Me.VerifyCRC32.TabIndex = 5
        Me.VerifyCRC32.Text = "CRC32"
        '
        'VerifyMD5
        '
        Me.VerifyMD5.Location = New System.Drawing.Point(0, 104)
        Me.VerifyMD5.Name = "VerifyMD5"
        Me.VerifyMD5.Size = New System.Drawing.Size(96, 16)
        Me.VerifyMD5.TabIndex = 6
        Me.VerifyMD5.Text = "MD5"
        '
        'VerifySHA1
        '
        Me.VerifySHA1.Location = New System.Drawing.Point(0, 120)
        Me.VerifySHA1.Name = "VerifySHA1"
        Me.VerifySHA1.Size = New System.Drawing.Size(96, 16)
        Me.VerifySHA1.TabIndex = 7
        Me.VerifySHA1.Text = "SHA1"
        '
        'VerifyED2K
        '
        Me.VerifyED2K.Location = New System.Drawing.Point(96, 120)
        Me.VerifyED2K.Name = "VerifyED2K"
        Me.VerifyED2K.Size = New System.Drawing.Size(96, 16)
        Me.VerifyED2K.TabIndex = 8
        Me.VerifyED2K.Text = "ED2K"
        '
        'VerifyTiger
        '
        Me.VerifyTiger.Location = New System.Drawing.Point(192, 104)
        Me.VerifyTiger.Name = "VerifyTiger"
        Me.VerifyTiger.Size = New System.Drawing.Size(96, 16)
        Me.VerifyTiger.TabIndex = 9
        Me.VerifyTiger.Text = "Tiger"
        '
        'VerifyTorrent
        '
        Me.VerifyTorrent.Location = New System.Drawing.Point(192, 120)
        Me.VerifyTorrent.Name = "VerifyTorrent"
        Me.VerifyTorrent.Size = New System.Drawing.Size(96, 16)
        Me.VerifyTorrent.TabIndex = 10
        Me.VerifyTorrent.Text = "Torrent"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(0, 136)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(288, 32)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Note: Hashes that are not present in the .torrent metadata will be ignored"
        '
        'BrowseForTorrent
        '
        Me.BrowseForTorrent.Location = New System.Drawing.Point(288, 24)
        Me.BrowseForTorrent.Name = "BrowseForTorrent"
        Me.BrowseForTorrent.Size = New System.Drawing.Size(144, 24)
        Me.BrowseForTorrent.TabIndex = 12
        Me.BrowseForTorrent.Text = "Browse For Torrent"
        '
        'BrowseToVerify
        '
        Me.BrowseToVerify.Location = New System.Drawing.Point(288, 64)
        Me.BrowseToVerify.Name = "BrowseToVerify"
        Me.BrowseToVerify.Size = New System.Drawing.Size(144, 24)
        Me.BrowseToVerify.TabIndex = 13
        Me.BrowseToVerify.Text = "Browse for files to Verify"
        '
        'FileHealthList
        '
        Me.FileHealthList.Enabled = False
        Me.FileHealthList.Location = New System.Drawing.Point(0, 192)
        Me.FileHealthList.Name = "FileHealthList"
        Me.FileHealthList.Size = New System.Drawing.Size(288, 94)
        Me.FileHealthList.TabIndex = 14
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(0, 176)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(288, 16)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "File Verification:"
        '
        'CurrentHashProgress
        '
        Me.CurrentHashProgress.Location = New System.Drawing.Point(0, 304)
        Me.CurrentHashProgress.Name = "CurrentHashProgress"
        Me.CurrentHashProgress.Size = New System.Drawing.Size(288, 16)
        Me.CurrentHashProgress.TabIndex = 16
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(0, 288)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(288, 16)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Current Hash Progress"
        '
        'SaveSettings
        '
        Me.SaveSettings.Location = New System.Drawing.Point(288, 248)
        Me.SaveSettings.Name = "SaveSettings"
        Me.SaveSettings.Size = New System.Drawing.Size(144, 24)
        Me.SaveSettings.TabIndex = 18
        Me.SaveSettings.Text = "Save Settings"
        '
        'SaveAndLeave
        '
        Me.SaveAndLeave.Location = New System.Drawing.Point(288, 272)
        Me.SaveAndLeave.Name = "SaveAndLeave"
        Me.SaveAndLeave.Size = New System.Drawing.Size(144, 24)
        Me.SaveAndLeave.TabIndex = 18
        Me.SaveAndLeave.Text = "Save Settings and Exit"
        '
        'LeaveNow
        '
        Me.LeaveNow.Location = New System.Drawing.Point(288, 296)
        Me.LeaveNow.Name = "LeaveNow"
        Me.LeaveNow.Size = New System.Drawing.Size(144, 24)
        Me.LeaveNow.TabIndex = 18
        Me.LeaveNow.Text = "Exit"
        '
        'VerifyNow
        '
        Me.VerifyNow.Location = New System.Drawing.Point(288, 160)
        Me.VerifyNow.Name = "VerifyNow"
        Me.VerifyNow.Size = New System.Drawing.Size(144, 24)
        Me.VerifyNow.TabIndex = 19
        Me.VerifyNow.Text = "Verify Files"
        '
        'TorrentFileOpen
        '
        Me.TorrentFileOpen.DefaultExt = "torrent"
        Me.TorrentFileOpen.Filter = "Torrent Files|*.torrent"
        '
        'VerifyFileOpen
        '
        Me.VerifyFileOpen.Filter = "All Files|*.*"
        '
        'VerifyFolderOpen
        '
        Me.VerifyFolderOpen.ShowNewFolderButton = False
        '
        'TorrentVerify
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(432, 325)
        Me.Controls.Add(Me.VerifyNow)
        Me.Controls.Add(Me.SaveSettings)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.CurrentHashProgress)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.FileHealthList)
        Me.Controls.Add(Me.BrowseToVerify)
        Me.Controls.Add(Me.BrowseForTorrent)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.VerifyTorrent)
        Me.Controls.Add(Me.VerifyTiger)
        Me.Controls.Add(Me.VerifyED2K)
        Me.Controls.Add(Me.VerifySHA1)
        Me.Controls.Add(Me.VerifyMD5)
        Me.Controls.Add(Me.VerifyCRC32)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PathToVerify)
        Me.Controls.Add(Me.TorrentFileToVerify)
        Me.Controls.Add(Me.SaveAndLeave)
        Me.Controls.Add(Me.LeaveNow)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "TorrentVerify"
        Me.Text = "Verify Files from Torrent File"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub TorrentVerify_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim arguments As [String]() = Environment.GetCommandLineArgs()
        If arguments.GetLength(0) > 1 Then
            TorrentFileToVerify.Text = arguments(1)
        End If
        Dim FileOffset As Integer
        FileOffset = Microsoft.VisualBasic.Left(arguments(0), Len(arguments(0))).LastIndexOf("\")
        LocalPath = System.IO.Path.GetFullPath(Microsoft.VisualBasic.Left(arguments(0), FileOffset)) + "\"
        If System.IO.File.Exists(LocalPath + "FileVerify.configure") Then
            Dim loadsettings As Integer = FreeFile()
            Dim intermediarysettingdata As String
            FileOpen(loadsettings, LocalPath + "FileVerify.configure", OpenMode.Binary, OpenAccess.Read, OpenShare.LockRead)
            intermediarysettingdata = Space(FileLen(LocalPath + "FileVerify.configure"))
            FileGet(loadsettings, intermediarysettingdata)
            FileClose(loadsettings)
            Dim ConfigData As New TorrentDictionary
            ConfigData.Parse(intermediarysettingdata)

            Dim GetMD5 As New TorrentNumber
            Dim GetSHA1 As New TorrentNumber
            Dim GetED2K As New TorrentNumber
            Dim GetCRC32 As New TorrentNumber
            Dim GetTiger As New TorrentNumber
            Dim GetTorrent As New TorrentNumber
            GetMD5 = ConfigData.Value("md5")
            GetSHA1 = ConfigData.Value("sha1")
            GetED2K = ConfigData.Value("ed2k")
            GetCRC32 = ConfigData.Value("crc32")
            GetTiger = ConfigData.Value("tiger")
            GetTorrent = ConfigData.Value("torrent")

            VerifyMD5.Checked = GetMD5.Value
            VerifySHA1.Checked = GetSHA1.Value
            VerifyED2K.Checked = GetED2K.Value
            VerifyCRC32.Checked = GetCRC32.Value
            VerifyTiger.Checked = GetTiger.Value
            VerifyTorrent.Checked = GetTorrent.Value
        End If
    End Sub

    Private Sub SaveSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveSettings.Click
        Dim ConfigData As New TorrentDictionary
        If System.IO.File.Exists(LocalPath + "FileVerify.configure") Then
            Dim loadsettings As Integer = FreeFile()
            Dim intermediarysettingdata As String
            FileOpen(loadsettings, LocalPath + "fileverify.configure", OpenMode.Binary, OpenAccess.Read, OpenShare.LockRead)
            intermediarysettingdata = Space(FileLen(LocalPath + "tgen.configure"))
            FileGet(loadsettings, intermediarysettingdata)
            FileClose(loadsettings)
            ConfigData.Parse(intermediarysettingdata)
            Kill(LocalPath + "fileverify.configure")
        End If
        Dim GetMD5 As New TorrentNumber
        Dim GetSHA1 As New TorrentNumber
        Dim GetED2K As New TorrentNumber
        Dim GetCRC32 As New TorrentNumber
        Dim GetTiger As New TorrentNumber
        Dim GetTorrent As New TorrentNumber
        GetMD5.Value = VerifyMD5.Checked
        GetSHA1.Value = VerifySHA1.Checked
        GetED2K.Value = VerifyED2K.Checked
        GetCRC32.Value = VerifyCRC32.Checked
        GetTiger.Value = VerifyTiger.Checked
        GetTorrent.Value = VerifyTorrent.Checked
        ConfigData.Value("md5") = GetMD5
        ConfigData.Value("sha1") = GetSHA1
        ConfigData.Value("ed2k") = GetED2K
        ConfigData.Value("crc32") = GetCRC32
        ConfigData.Value("tiger") = GetTiger
        ConfigData.Value("torrent") = GetTorrent
        Dim savesettings As Integer = FreeFile()
        FileOpen(savesettings, LocalPath + "fileverify.configure", OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
        FilePut(savesettings, ConfigData.Bencoded())
        FileClose(SaveSettings)
    End Sub

    Private Sub SaveAndLeave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAndLeave.Click
        Call SaveSettings_Click(sender, e)
        Call leave_click(sender, e)
    End Sub

    Private Sub Leave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LeaveNow.Click
        End
    End Sub

    Private Sub BrowseForTorrent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseForTorrent.Click
        TorrentFileOpen.ShowDialog()
    End Sub

    Private Sub TorrentFileOpen_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TorrentFileOpen.FileOk
        FileHealthList.Items.Clear()
        Dim torrenttoparse As Integer = FreeFile()
        Dim intermediarytorrentdata As String
        intermediarytorrentdata = Space(FileLen(TorrentFileOpen.FileName))
        FileOpen(torrenttoparse, TorrentFileOpen.FileName, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
        FileGet(torrenttoparse, intermediarytorrentdata)
        FileClose(torrenttoparse)
        Dim TorrentData As New TorrentDictionary
        TorrentData.Parse(intermediarytorrentdata)
        Dim TorrentInfo As New TorrentDictionary
        TorrentInfo = TorrentData.Value("info")
        If TorrentInfo.Contains("files") Then
            Dim MultiFileFileList As New TorrentList
            MultiFileFiles = TorrentInfo.Value("files")
            MultiFileFileList.Value = MultiFileFiles.Value
            For Each FileDictionary As TorrentDictionary In MultiFileFiles
                Dim path As New TorrentList
                path = FileDictionary.Value("path")
                Dim PathArray As New ArrayList
                PathArray = path.Value
                Dim CurrentFile As String
                For Each pathlevel As TorrentString In PathArray
                    CurrentFile = CurrentFile + "\" + pathlevel.Value
                Next
                FileHealthList.Items.Add(CurrentFile)
                CurrentFile = ""
            Next
        Else
            Dim FileName As New TorrentString
            FileName = TorrentInfo.Value("name")
            FileHealthList.Items.Add(FileName.Value)
        End If
    End Sub
End Class
