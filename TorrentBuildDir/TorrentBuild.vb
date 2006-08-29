'--------------------------------------------------------
' TorrentBuild - TorrentBuild.VB
' Torrent Builder code
' Harold Feit
' ToDo
' - Add nested folder support
' - Add multitracker support
' - Add support to record multiple announce urls for use
' - Add support for custom torrent output path
'--------------------------------------------------------

Imports System
Imports System.Io
Imports System.Security.Cryptography
Imports System.Text
Imports System.Threading
Imports EAD.Cryptography.VisualBasic.CRC32
Imports Microsoft.VisualBasic
Imports EAD.Torrent
Imports EAD.Torrent.Announces
Imports System.Web

Public Class TorrentBuild
    Inherits System.Windows.Forms.Form
    Public Shared GenerateVerbose As Boolean = True
    Public Shared UseWSAConfig As Boolean = False
    Public Shared DelayMessages As Boolean = True
    Public Shared LocalPath As String
    Dim TigerHash As New EAD.Cryptography.ThexCS.ThexThreaded
    Dim sha1 As New System.Security.Cryptography.SHA1CryptoServiceProvider
    Dim md5 As New System.Security.Cryptography.MD5CryptoServiceProvider
    Dim md4 As New Mono.Security.Cryptography.MD4Managed
    Dim piecesizetouse As Long
    Dim FileHandling As FileStream
    'Dim urlprocessor As System.Web.HttpServerUtility
    Public Shared CountMultiplier As Integer
    Public Shared BlackListedFiles As New ArrayList
    Dim Advanced As New AdvancedConfiguration
    Dim Multitracker As New MultiTrackerGenerator

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
    Friend WithEvents FileNameToMake As System.Windows.Forms.TextBox
    Friend WithEvents BrowseForFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SelectFile As System.Windows.Forms.Button
    Friend WithEvents PieceSize As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents IncludeSHA1 As System.Windows.Forms.CheckBox
    Friend WithEvents BuildTorrentNow As System.Windows.Forms.Button
    Friend WithEvents AnnounceURL As System.Windows.Forms.TextBox
    Friend WithEvents ExitWithSave As System.Windows.Forms.Button
    Friend WithEvents ExitWithoutSave As System.Windows.Forms.Button
    Friend WithEvents BrowseForFolder As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents SelectFolder As System.Windows.Forms.Button
    Friend WithEvents MakeSeparateTorrents As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents IncludeTorrents As System.Windows.Forms.CheckBox
    Friend WithEvents AutomaticPieceSize As System.Windows.Forms.CheckBox
    Friend WithEvents IncludeMD5 As System.Windows.Forms.CheckBox
    Friend WithEvents IncludeCRC32 As System.Windows.Forms.CheckBox
    Friend WithEvents IncludeED2K As System.Windows.Forms.CheckBox
    Friend WithEvents IncludeBlacklisted As System.Windows.Forms.CheckBox
    Friend WithEvents TorrentProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents AdvSet As System.Windows.Forms.Button
    Friend WithEvents SaveSettings As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents BlacklistingScreen As System.Windows.Forms.Button
    Friend WithEvents OptionalHashProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TorrentComment As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents HashProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents MakeExternals As System.Windows.Forms.CheckBox
    Friend WithEvents IncludeTiger As System.Windows.Forms.CheckBox
    Friend WithEvents PrivateTorrent As System.Windows.Forms.CheckBox
    Friend WithEvents MultiTrackerEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents MultiTrackerSettings As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(TorrentBuild))
        Me.FileNameToMake = New System.Windows.Forms.TextBox
        Me.BrowseForFile = New System.Windows.Forms.OpenFileDialog
        Me.SelectFile = New System.Windows.Forms.Button
        Me.PieceSize = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.AnnounceURL = New System.Windows.Forms.TextBox
        Me.IncludeMD5 = New System.Windows.Forms.CheckBox
        Me.IncludeSHA1 = New System.Windows.Forms.CheckBox
        Me.BuildTorrentNow = New System.Windows.Forms.Button
        Me.ExitWithSave = New System.Windows.Forms.Button
        Me.ExitWithoutSave = New System.Windows.Forms.Button
        Me.BrowseForFolder = New System.Windows.Forms.FolderBrowserDialog
        Me.SelectFolder = New System.Windows.Forms.Button
        Me.MakeSeparateTorrents = New System.Windows.Forms.CheckBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.IncludeTorrents = New System.Windows.Forms.CheckBox
        Me.AutomaticPieceSize = New System.Windows.Forms.CheckBox
        Me.IncludeCRC32 = New System.Windows.Forms.CheckBox
        Me.IncludeED2K = New System.Windows.Forms.CheckBox
        Me.IncludeBlacklisted = New System.Windows.Forms.CheckBox
        Me.TorrentProgress = New System.Windows.Forms.ProgressBar
        Me.AdvSet = New System.Windows.Forms.Button
        Me.SaveSettings = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.BlacklistingScreen = New System.Windows.Forms.Button
        Me.OptionalHashProgress = New System.Windows.Forms.ProgressBar
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.TorrentComment = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.HashProgress = New System.Windows.Forms.ProgressBar
        Me.MakeExternals = New System.Windows.Forms.CheckBox
        Me.IncludeTiger = New System.Windows.Forms.CheckBox
        Me.PrivateTorrent = New System.Windows.Forms.CheckBox
        Me.MultiTrackerEnabled = New System.Windows.Forms.CheckBox
        Me.MultiTrackerSettings = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'FileNameToMake
        '
        Me.FileNameToMake.AllowDrop = True
        Me.FileNameToMake.Location = New System.Drawing.Point(0, 24)
        Me.FileNameToMake.Name = "FileNameToMake"
        Me.FileNameToMake.Size = New System.Drawing.Size(504, 20)
        Me.FileNameToMake.TabIndex = 0
        Me.FileNameToMake.Text = ""
        '
        'BrowseForFile
        '
        Me.BrowseForFile.Filter = "All Files|*.*"
        '
        'SelectFile
        '
        Me.SelectFile.Location = New System.Drawing.Point(0, 48)
        Me.SelectFile.Name = "SelectFile"
        Me.SelectFile.Size = New System.Drawing.Size(248, 24)
        Me.SelectFile.TabIndex = 1
        Me.SelectFile.Text = "Select File To Generate Torrent For"
        '
        'PieceSize
        '
        Me.PieceSize.Items.AddRange(New Object() {"32768", "65536", "121072", "262144", "524288", "1048576", "2097152"})
        Me.PieceSize.Location = New System.Drawing.Point(0, 88)
        Me.PieceSize.Name = "PieceSize"
        Me.PieceSize.Size = New System.Drawing.Size(120, 21)
        Me.PieceSize.TabIndex = 2
        Me.PieceSize.Text = "262144"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(0, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(328, 16)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Full path to the file or folder you want to generate torrent(s) for:"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(0, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(120, 16)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Piece Size"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(0, 144)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(376, 16)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Announce URL"
        '
        'AnnounceURL
        '
        Me.AnnounceURL.Location = New System.Drawing.Point(0, 160)
        Me.AnnounceURL.Name = "AnnounceURL"
        Me.AnnounceURL.Size = New System.Drawing.Size(376, 20)
        Me.AnnounceURL.TabIndex = 6
        Me.AnnounceURL.Text = ""
        '
        'IncludeMD5
        '
        Me.IncludeMD5.Location = New System.Drawing.Point(0, 240)
        Me.IncludeMD5.Name = "IncludeMD5"
        Me.IncludeMD5.Size = New System.Drawing.Size(72, 16)
        Me.IncludeMD5.TabIndex = 7
        Me.IncludeMD5.Text = "MD5"
        '
        'IncludeSHA1
        '
        Me.IncludeSHA1.Location = New System.Drawing.Point(0, 256)
        Me.IncludeSHA1.Name = "IncludeSHA1"
        Me.IncludeSHA1.Size = New System.Drawing.Size(72, 16)
        Me.IncludeSHA1.TabIndex = 8
        Me.IncludeSHA1.Text = "SHA1"
        '
        'BuildTorrentNow
        '
        Me.BuildTorrentNow.Location = New System.Drawing.Point(360, 272)
        Me.BuildTorrentNow.Name = "BuildTorrentNow"
        Me.BuildTorrentNow.Size = New System.Drawing.Size(144, 24)
        Me.BuildTorrentNow.TabIndex = 9
        Me.BuildTorrentNow.Text = "Build Torrent"
        '
        'ExitWithSave
        '
        Me.ExitWithSave.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ExitWithSave.Location = New System.Drawing.Point(360, 424)
        Me.ExitWithSave.Name = "ExitWithSave"
        Me.ExitWithSave.Size = New System.Drawing.Size(144, 24)
        Me.ExitWithSave.TabIndex = 10
        Me.ExitWithSave.Text = "Exit - Save Settings"
        '
        'ExitWithoutSave
        '
        Me.ExitWithoutSave.Location = New System.Drawing.Point(360, 400)
        Me.ExitWithoutSave.Name = "ExitWithoutSave"
        Me.ExitWithoutSave.Size = New System.Drawing.Size(144, 24)
        Me.ExitWithoutSave.TabIndex = 11
        Me.ExitWithoutSave.Text = "Exit - Don't Save Settings"
        '
        'SelectFolder
        '
        Me.SelectFolder.Location = New System.Drawing.Point(248, 48)
        Me.SelectFolder.Name = "SelectFolder"
        Me.SelectFolder.Size = New System.Drawing.Size(256, 24)
        Me.SelectFolder.TabIndex = 12
        Me.SelectFolder.Text = "Select Folder to Generate Torrent(s) for"
        '
        'MakeSeparateTorrents
        '
        Me.MakeSeparateTorrents.Location = New System.Drawing.Point(176, 72)
        Me.MakeSeparateTorrents.Name = "MakeSeparateTorrents"
        Me.MakeSeparateTorrents.Size = New System.Drawing.Size(328, 16)
        Me.MakeSeparateTorrents.TabIndex = 13
        Me.MakeSeparateTorrents.Text = "Generate 1 torrent per file"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(0, 224)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(344, 16)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Optional Hashes (some used by non-torrent peer to peer networks):"
        '
        'IncludeTorrents
        '
        Me.IncludeTorrents.Location = New System.Drawing.Point(176, 88)
        Me.IncludeTorrents.Name = "IncludeTorrents"
        Me.IncludeTorrents.Size = New System.Drawing.Size(328, 16)
        Me.IncludeTorrents.TabIndex = 15
        Me.IncludeTorrents.Text = "Include nested .torrent files in generation"
        '
        'AutomaticPieceSize
        '
        Me.AutomaticPieceSize.Checked = True
        Me.AutomaticPieceSize.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AutomaticPieceSize.Location = New System.Drawing.Point(0, 112)
        Me.AutomaticPieceSize.Name = "AutomaticPieceSize"
        Me.AutomaticPieceSize.Size = New System.Drawing.Size(136, 16)
        Me.AutomaticPieceSize.TabIndex = 16
        Me.AutomaticPieceSize.Text = "Automatic Piece Size"
        '
        'IncludeCRC32
        '
        Me.IncludeCRC32.Location = New System.Drawing.Point(72, 240)
        Me.IncludeCRC32.Name = "IncludeCRC32"
        Me.IncludeCRC32.Size = New System.Drawing.Size(72, 16)
        Me.IncludeCRC32.TabIndex = 7
        Me.IncludeCRC32.Text = "CRC32"
        '
        'IncludeED2K
        '
        Me.IncludeED2K.Location = New System.Drawing.Point(72, 256)
        Me.IncludeED2K.Name = "IncludeED2K"
        Me.IncludeED2K.Size = New System.Drawing.Size(72, 16)
        Me.IncludeED2K.TabIndex = 7
        Me.IncludeED2K.Text = "ED2K"
        '
        'IncludeBlacklisted
        '
        Me.IncludeBlacklisted.Location = New System.Drawing.Point(176, 104)
        Me.IncludeBlacklisted.Name = "IncludeBlacklisted"
        Me.IncludeBlacklisted.Size = New System.Drawing.Size(328, 16)
        Me.IncludeBlacklisted.TabIndex = 19
        Me.IncludeBlacklisted.Text = "Include normally blacklisted files in generation"
        '
        'TorrentProgress
        '
        Me.TorrentProgress.Location = New System.Drawing.Point(0, 384)
        Me.TorrentProgress.Name = "TorrentProgress"
        Me.TorrentProgress.Size = New System.Drawing.Size(504, 16)
        Me.TorrentProgress.TabIndex = 20
        '
        'AdvSet
        '
        Me.AdvSet.Location = New System.Drawing.Point(0, 400)
        Me.AdvSet.Name = "AdvSet"
        Me.AdvSet.Size = New System.Drawing.Size(144, 24)
        Me.AdvSet.TabIndex = 21
        Me.AdvSet.Text = "Advanced Settings"
        '
        'SaveSettings
        '
        Me.SaveSettings.Location = New System.Drawing.Point(0, 424)
        Me.SaveSettings.Name = "SaveSettings"
        Me.SaveSettings.Size = New System.Drawing.Size(144, 24)
        Me.SaveSettings.TabIndex = 22
        Me.SaveSettings.Text = "Save All Settings"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(0, 368)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(504, 16)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "Progress - Torrent Data Hashing:"
        '
        'BlacklistingScreen
        '
        Me.BlacklistingScreen.Location = New System.Drawing.Point(296, 120)
        Me.BlacklistingScreen.Name = "BlacklistingScreen"
        Me.BlacklistingScreen.Size = New System.Drawing.Size(208, 24)
        Me.BlacklistingScreen.TabIndex = 24
        Me.BlacklistingScreen.Text = "Blacklisted Files/Extensions"
        '
        'OptionalHashProgress
        '
        Me.OptionalHashProgress.Location = New System.Drawing.Point(0, 320)
        Me.OptionalHashProgress.Name = "OptionalHashProgress"
        Me.OptionalHashProgress.Size = New System.Drawing.Size(504, 16)
        Me.OptionalHashProgress.TabIndex = 25
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(0, 304)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(504, 16)
        Me.Label6.TabIndex = 26
        Me.Label6.Text = "Progress - Optional Data Hashes"
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(0, 184)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(504, 16)
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "Torrent Comment"
        '
        'TorrentComment
        '
        Me.TorrentComment.Location = New System.Drawing.Point(0, 200)
        Me.TorrentComment.Name = "TorrentComment"
        Me.TorrentComment.Size = New System.Drawing.Size(504, 20)
        Me.TorrentComment.TabIndex = 28
        Me.TorrentComment.Text = ""
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(0, 336)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(504, 16)
        Me.Label8.TabIndex = 29
        Me.Label8.Text = "Progress - Current File's ED2K Hash:"
        '
        'HashProgress
        '
        Me.HashProgress.Location = New System.Drawing.Point(0, 352)
        Me.HashProgress.Name = "HashProgress"
        Me.HashProgress.Size = New System.Drawing.Size(504, 16)
        Me.HashProgress.TabIndex = 30
        '
        'MakeExternals
        '
        Me.MakeExternals.Location = New System.Drawing.Point(0, 272)
        Me.MakeExternals.Name = "MakeExternals"
        Me.MakeExternals.Size = New System.Drawing.Size(208, 16)
        Me.MakeExternals.TabIndex = 31
        Me.MakeExternals.Text = "Make External Hash Files"
        '
        'IncludeTiger
        '
        Me.IncludeTiger.Location = New System.Drawing.Point(144, 240)
        Me.IncludeTiger.Name = "IncludeTiger"
        Me.IncludeTiger.Size = New System.Drawing.Size(64, 16)
        Me.IncludeTiger.TabIndex = 32
        Me.IncludeTiger.Text = "Tiger"
        '
        'PrivateTorrent
        '
        Me.PrivateTorrent.Location = New System.Drawing.Point(400, 224)
        Me.PrivateTorrent.Name = "PrivateTorrent"
        Me.PrivateTorrent.Size = New System.Drawing.Size(104, 16)
        Me.PrivateTorrent.TabIndex = 33
        Me.PrivateTorrent.Text = "Private Torrent"
        '
        'MultiTrackerEnabled
        '
        Me.MultiTrackerEnabled.Location = New System.Drawing.Point(376, 144)
        Me.MultiTrackerEnabled.Name = "MultiTrackerEnabled"
        Me.MultiTrackerEnabled.Size = New System.Drawing.Size(128, 16)
        Me.MultiTrackerEnabled.TabIndex = 34
        Me.MultiTrackerEnabled.Text = "Multitracker Torrent"
        '
        'MultiTrackerSettings
        '
        Me.MultiTrackerSettings.Location = New System.Drawing.Point(376, 160)
        Me.MultiTrackerSettings.Name = "MultiTrackerSettings"
        Me.MultiTrackerSettings.Size = New System.Drawing.Size(128, 24)
        Me.MultiTrackerSettings.TabIndex = 35
        Me.MultiTrackerSettings.Text = "Multitracker Settings"
        '
        'TorrentBuild
        '
        Me.AcceptButton = Me.BuildTorrentNow
        Me.AllowDrop = True
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.ExitWithSave
        Me.ClientSize = New System.Drawing.Size(506, 447)
        Me.Controls.Add(Me.MultiTrackerSettings)
        Me.Controls.Add(Me.MultiTrackerEnabled)
        Me.Controls.Add(Me.PrivateTorrent)
        Me.Controls.Add(Me.IncludeTiger)
        Me.Controls.Add(Me.MakeExternals)
        Me.Controls.Add(Me.HashProgress)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TorrentComment)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.OptionalHashProgress)
        Me.Controls.Add(Me.BlacklistingScreen)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.SaveSettings)
        Me.Controls.Add(Me.AdvSet)
        Me.Controls.Add(Me.TorrentProgress)
        Me.Controls.Add(Me.IncludeBlacklisted)
        Me.Controls.Add(Me.AutomaticPieceSize)
        Me.Controls.Add(Me.IncludeTorrents)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.MakeSeparateTorrents)
        Me.Controls.Add(Me.SelectFolder)
        Me.Controls.Add(Me.ExitWithoutSave)
        Me.Controls.Add(Me.ExitWithSave)
        Me.Controls.Add(Me.BuildTorrentNow)
        Me.Controls.Add(Me.IncludeSHA1)
        Me.Controls.Add(Me.IncludeMD5)
        Me.Controls.Add(Me.AnnounceURL)
        Me.Controls.Add(Me.FileNameToMake)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PieceSize)
        Me.Controls.Add(Me.SelectFile)
        Me.Controls.Add(Me.IncludeCRC32)
        Me.Controls.Add(Me.IncludeED2K)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TorrentBuild"
        Me.Text = "Build A Torrent"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub SelectFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectFile.Click
        BrowseForFile.ShowDialog()
    End Sub

    Private Sub SelectFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectFolder.Click
        Dim folderselect As DialogResult
        folderselect = BrowseForFolder.ShowDialog()
        If folderselect = DialogResult.OK Then FileNameToMake.Text = BrowseForFolder.SelectedPath
    End Sub

    Private Sub BrowseForFile_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles BrowseForFile.FileOk
        FileNameToMake.Text = BrowseForFile.FileName
    End Sub

    Private Sub MakeTorrentsFromFolder()
        Dim FolderFiles() As String
        FolderFiles = Directory.GetFiles(FileNameToMake.Text)
        'MsgBox(FolderFiles.GetLength(0))
        Dim FileName As String
        For Each FileName In FolderFiles
            If IsFileCleared(FileName) Then MakeTorrentFromFile(FileName)
        Next
    End Sub

    Private Function getautopiecesize(ByVal filesize As Long) As Long
        If filesize < 5248800 Then
            getautopiecesize = 32768
        ElseIf filesize < 157286400 Then
            getautopiecesize = 65536
        ElseIf filesize < 367001600 Then
            getautopiecesize = 131072
        ElseIf filesize < 53687092 Then
            getautopiecesize = 262144
        ElseIf filesize < 1073741824 Then
            getautopiecesize = 524288
        ElseIf filesize < 2147483648 Then
            getautopiecesize = 1048576
        Else
            getautopiecesize = 2097152
        End If
    End Function

    Public Sub GetED2KHash(ByVal Filename As String, ByRef plaintext As String, ByRef binaryvalue As String)
        Dim TotalSize As Long = FileLen(Filename)
        Dim FileOffsetToHash As Long = 0
        Dim numbytes As Long
        Dim Piecehash As String
        Dim numpieces As Long = TotalSize / EAD.VisualBasic.Constants.ED2KBlockSize
        If numpieces * EAD.VisualBasic.Constants.ED2KBlockSize < TotalSize Then numpieces = numpieces + 1
        Dim hashbytetotal As Long = numpieces * 16
        Dim ED2KCompund(hashbytetotal) As Byte
        Dim currentpos As Long = 0
        Dim compund As Long = 0
        Dim buff As StringBuilder = New StringBuilder
        HashProgress.Value = 0
        FileHandling = File.Open(Filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
        If TotalSize > EAD.VisualBasic.Constants.ED2KBlockSize Then
            Dim ed2kcompound As String
            Do
                Dim LoadData(EAD.VisualBasic.Constants.ED2KBlockSize) As Byte
                HashProgress.Value = FileOffsetToHash / TotalSize * 100
                System.Windows.Forms.Application.DoEvents()
                numbytes = FileHandling.Read(LoadData, 0, EAD.VisualBasic.Constants.ED2KBlockSize)
                Dim datatohash(numbytes - 1) As Byte
                For dataindex As Long = 0 To numbytes - 1
                    datatohash(dataindex) = LoadData(dataindex)
                Next
                Erase LoadData
                Dim advanced() As Byte
                advanced = md4.ComputeHash(datatohash)
                Erase datatohash
                ed2kcompound = ""
                Dim valueindex As Integer = 0
                For Each value As Byte In advanced
                    ed2kcompound = ed2kcompound + System.Text.Encoding.Default.GetString(advanced, valueindex, 1)
                    ED2KCompund(currentpos) = value
                    currentpos = currentpos + 1
                    valueindex = valueindex + 1
                Next
                Erase advanced
                FileOffsetToHash = FileOffsetToHash + numbytes
            Loop Until FileOffsetToHash >= TotalSize
            HashProgress.Value = 100
            Dim rehash(ED2KCompund.GetUpperBound(0) - 1) As Byte
            For rehashindex As Integer = 0 To ED2KCompund.GetUpperBound(0) - 1
                rehash(rehashindex) = ED2KCompund(rehashindex)
            Next

            Dim fullhash() As Byte = md4.ComputeHash(rehash)
            Dim Convertme As New EAD.Conversion.HashChanger
            Convertme.bytehash = fullhash
            binaryvalue = Convertme.rawhash
            plaintext = Convertme.hexhash
            GC.Collect()
        Else
            Dim LoadData(EAD.VisualBasic.Constants.ED2KBlockSize) As Byte
            numbytes = FileHandling.Read(LoadData, 0, EAD.VisualBasic.Constants.ED2KBlockSize)
            Dim datatohash(numbytes - 1) As Byte
            For dataindex As Long = 0 To numbytes - 1
                datatohash(dataindex) = LoadData(dataindex)
            Next
            Erase LoadData
            Dim fullhash() As Byte = md4.ComputeHash(datatohash)
            Dim Convertme As New EAD.Conversion.HashChanger
            Convertme.bytehash = fullhash
            binaryvalue = Convertme.rawhash
            plaintext = Convertme.hexhash
            Erase datatohash
            HashProgress.Value = 100
            GC.Collect()
        End If
        FileHandling.Close()
    End Sub

    Private Function IsFileCleared(ByVal checkname As String) As Boolean
        IsFileCleared = True
        If LCase(Microsoft.VisualBasic.Right(checkname, 9)) = "thumbs.db" Then IsFileCleared = False
        If LCase(Microsoft.VisualBasic.Right(checkname, 11)) = "sthumbs.dat" Then IsFileCleared = False
        If LCase(Microsoft.VisualBasic.Right(checkname, 9)) = ".ds_store" Then IsFileCleared = False
        If LCase(Microsoft.VisualBasic.Right(checkname, 8)) = ".torrent" And IncludeTorrents.Checked = False Then IsFileCleared = False
        If Not IncludeBlacklisted.Checked Then
            For Each BlackListedFile As TorrentString In BlackListedFiles
                If LCase(Microsoft.VisualBasic.Right(checkname, Len(BlackListedFile.Value))) = LCase(BlackListedFile.Value) Then IsFileCleared = False
            Next
        End If
        GC.Collect()
    End Function

    Public Sub MakeOneTorrentFromFolder()
        Dim FolderFiles() As String
        FolderFiles = Directory.GetFiles(FileNameToMake.Text)
        If Not Microsoft.VisualBasic.Right(FileNameToMake.Text, 1) = "\" Then FileNameToMake.Text = FileNameToMake.Text + "\"
        Dim torrentMFinfo As New TorrentDictionary
        Dim TorrentFiles As New TorrentList
        Dim TorrentFilesArray As New ArrayList
        Dim piecesizetouse As Long
        Dim calculatedtotal As Long
        Dim TotalFiles As Long = 0
        Dim TotalSize As Long
        OptionalHashProgress.Maximum = FolderFiles.GetLength(0) * CountMultiplier
        For Each FolderFileName As String In FolderFiles
            If IsFileCleared(FolderFileName) Then
                Dim FileInfo As New TorrentDictionary
                Dim FilesSize As New TorrentNumber
                Dim FilePath As New TorrentList
                Dim FilePathArray As New ArrayList
                Dim FilePathName As New TorrentString

                FilesSize.Value = FileLen(FolderFileName)
                TotalSize = TotalSize + FileLen(FolderFileName)
                If AutomaticPieceSize.Checked Then calculatedtotal = calculatedtotal + FilesSize.Value
                FilePathName.Value = Microsoft.VisualBasic.Right(FolderFileName, Len(FolderFileName) - Len(FileNameToMake.Text))
                FilePathArray.Add(FilePathName)
                FilePath.Value = FilePathArray
                FileInfo.Add("length", FilesSize)
                FileInfo.Add("path", FilePath)

                Dim filecheck As FileStream
                Dim hash As Byte()
                Dim buff As StringBuilder = New StringBuilder
                Dim Convertme As EAD.Conversion.HashChanger = New EAD.Conversion.HashChanger
                Dim hashByte As Byte
                Dim valueindex As Integer

                If FilesSize.Value <= 4707319808 Then
                    If IncludeTiger.Checked Then
                        TigerHash = New EAD.Cryptography.ThexCS.ThexThreaded
                        Dim FileTiger As New TorrentString
                        Dim TigerRawHash As String
                        hash = TigerHash.GetTTH_Value(FolderFileName)
                        System.Windows.Forms.Application.DoEvents()
                        valueindex = 0
                        For Each hashByte In hash
                            TigerRawHash = TigerRawHash + System.Text.Encoding.Default.GetString(hash, valueindex, 1)
                            valueindex = valueindex + 1
                        Next
                        FileTiger.Value = TigerRawHash
                        TigerRawHash = ""
                        Erase hash
                        OptionalHashProgress.Value = OptionalHashProgress.Value + 1
                        FileInfo.Add("tiger", FileTiger)
                    End If
                    GC.Collect()
                Else
                    OptionalHashProgress.Value = OptionalHashProgress.Value + 1
                End If

                If IncludeSHA1.Checked Then
                    Dim filesha1 As New TorrentString
                    filecheck = New FileStream(FolderFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                    Dim sha1raw As String
                    sha1.ComputeHash(filecheck)
                    filecheck.Close()
                    System.Windows.Forms.Application.DoEvents()
                    hash = sha1.Hash
                    valueindex = 0
                    For Each hashByte In hash
                        buff.AppendFormat("{0:x2}", hashByte)
                        sha1raw = sha1raw + System.Text.Encoding.Default.GetString(hash, valueindex, 1)
                    Next
                    filesha1.Value = sha1raw
                    buff.Remove(0, buff.Length)
                    sha1raw = ""
                    Erase hash
                    OptionalHashProgress.Value = OptionalHashProgress.Value + 1
                    FileInfo.Add("sha1", filesha1)
                    GC.Collect()
                End If
                If IncludeMD5.Checked Then
                    Dim filemd5 As New TorrentString
                    filecheck = New FileStream(FolderFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                    buff = New StringBuilder
                    md5.ComputeHash(filecheck)
                    System.Windows.Forms.Application.DoEvents()
                    filecheck.Close()
                    hash = md5.Hash
                    For Each hashByte In hash
                        buff.AppendFormat("{0:x2}", hashByte)
                    Next
                    filemd5.Value = buff.ToString
                    buff.Remove(0, buff.Length)
                    Erase hash
                    OptionalHashProgress.Value = OptionalHashProgress.Value + 1
                    FileInfo.Add("md5sum", filemd5)
                    GC.Collect()
                End If
                If IncludeCRC32.Checked Then
                    Dim FileCRC32 As New TorrentString
                    Dim c As New EAD.Cryptography.VisualBasic.CRC32
                    Dim crc As Integer = 0
                    filecheck = New FileStream(FolderFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                    crc = c.GetCrc32(filecheck)
                    filecheck.Close()
                    System.Windows.Forms.Application.DoEvents()
                    FileCRC32.Value = String.Format("{0:X8}", crc)
                    OptionalHashProgress.Value = OptionalHashProgress.Value + 1
                    FileInfo.Add("crc32", FileCRC32)
                    GC.Collect()
                End If
                If IncludeED2K.Checked Then
                    Dim fileed2k As New TorrentString
                    Dim ed2khashvalue As String
                    Dim ed2kplaintext As String
                    Call GetED2KHash(FolderFileName, ed2kplaintext, ed2khashvalue)
                    OptionalHashProgress.Value = OptionalHashProgress.Value + 1
                    fileed2k.Value = ed2khashvalue
                    FileInfo.Add("ed2k", fileed2k)
                    GC.Collect()
                End If
                TorrentFilesArray.Add(FileInfo)
            Else
                OptionalHashProgress.Maximum = OptionalHashProgress.Maximum - CountMultiplier
            End If

        Next

        If AutomaticPieceSize.Checked Then
            piecesizetouse = getautopiecesize(calculatedtotal)
        Else
            piecesizetouse = CInt(PieceSize.Text)
        End If
        TorrentFiles.Value = TorrentFilesArray
        Dim PieceLength As New TorrentNumber
        PieceLength.Value = piecesizetouse
        torrentMFinfo.Add("files", TorrentFiles)
        torrentMFinfo.Add("piece length", PieceLength)

        Dim numfiles As Long = FolderFiles.GetUpperBound(0)
        Dim currentfile As Long = 0
        Dim currentsize As Long = 0
        Dim PieceShortSet As Long
        Dim HashBytes(piecesizetouse) As Byte
        Dim numbytes As Long
        Dim hashdatastring As String
        Dim hashdata(20) As Byte

        ' code to generate pieces
reloadfile0:
        If IsFileCleared(FolderFiles(currentfile)) Then
            FileHandling = File.Open(FolderFiles(currentfile), FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            If GenerateVerbose Then MsgBox("Current File: " + FolderFiles(currentfile) + Chr(10) + " File offset for Piece 0 of this file: " + CStr(currentsize))
        Else
            currentfile = currentfile + 1
            GoTo reloadfile0
        End If

        Do
            Thread.Sleep(10)
            System.Windows.Forms.Application.DoEvents()
            TorrentProgress.Value = currentsize / TotalSize * 100
            Dim DataToHash(piecesizetouse - 1) As Byte
reopen:
            numbytes = FileHandling.Read(DataToHash, PieceShortSet, piecesizetouse - PieceShortSet)
            PieceShortSet = PieceShortSet + numbytes
            If currentfile <= numfiles And PieceShortSet < piecesizetouse Then
                currentfile = currentfile + 1
nextfile:
                If Not currentfile > FolderFiles.GetUpperBound(0) Then
                    If Not IsFileCleared(FolderFiles(currentfile)) Then
                        currentfile = currentfile + 1
                        GoTo nextfile
                    End If
                    FileHandling.Close()
                    FileHandling = File.Open(FolderFiles(currentfile), FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                    If GenerateVerbose Then MsgBox("Current File: " + FolderFiles(currentfile) + Chr(10) + " File offset for Piece 0 of this file: " + CStr(currentsize))
                End If

                GoTo reopen
            End If
nofilesleft:
            Dim RehashData(PieceShortSet - 1) As Byte
            For dataindex As Long = 0 To (PieceShortSet - 1)
                RehashData(dataindex) = DataToHash(dataindex)
            Next

            Erase DataToHash
            hashdata = sha1.ComputeHash(RehashData)
            For dataindex As Integer = 0 To 19
                hashdatastring = hashdatastring + System.Text.Encoding.Default.GetString(hashdata, dataindex, 1)
            Next
            Erase hashdata
            currentsize = currentsize + PieceShortSet
            PieceShortSet = 0
            GC.Collect()
        Loop Until currentsize >= TotalSize
        FileHandling.Close()
        If GenerateVerbose = True Then MsgBox("Size of files hashed: " + CStr(currentsize) + " Size of files compared: " + CStr(TotalSize) + Chr(10) + "Now Generating .torrent file")
        TorrentProgress.Value = 100

        ' end code to generate pieces
        Dim torrentfilegenerate As String = Microsoft.VisualBasic.Left(FileNameToMake.Text, Len(FileNameToMake.Text) - 1) + ".torrent"
        Dim torrentinternalname As String
        Dim torrentpieces As New TorrentString
        torrentpieces.Value = hashdatastring
        torrentMFinfo.Add("pieces", torrentpieces)
        Dim FileOffset As Integer
        Dim FileNameLength As Integer
        FileOffset = Microsoft.VisualBasic.Left(FileNameToMake.Text, Len(FileNameToMake.Text) - 1).LastIndexOf("\")
        FileNameLength = Len(FileNameToMake.Text) - FileOffset
        torrentinternalname = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(FileNameToMake.Text, Len(FileNameToMake.Text) - 1), FileNameLength - 2)
        Dim TorrentFolderName As New TorrentString
        TorrentFolderName.Value = torrentinternalname
        Dim TorrentMeta As New TorrentDictionary
        Dim torrentannounce As New TorrentString
        torrentMFinfo.Add("name", TorrentFolderName)
        torrentannounce.Value = AnnounceURL.Text
        If UseWSAConfig = True Then
            Dim WebSeedCount As Integer
            Dim WebSeeds As New ArrayList
            Dim WebSeedsList As New TorrentList
            WebSeedCount = GetWebSeedData(WebSeeds)
            WebSeedsList.Value = WebSeeds
            If WebSeedCount >= 1 Then TorrentMeta.Add("httpseeds", WebSeedsList)
        End If
        Dim CreatedBy As New TorrentString
        CreatedBy.Value = "Torrent Generated by VB.Net TorrentGen - Written by DWKnight"
        TorrentMeta.Add("created by", CreatedBy)

        Dim CommentOfTorrent As New TorrentString
        If Not Trim(TorrentComment.Text) = "" Then
            CommentOfTorrent.Value = Trim(TorrentComment.Text)
            TorrentMeta.Add("comment", CommentOfTorrent)
        End If

        If Not AnnounceURL.Text = "" Then TorrentMeta.Add("announce", torrentannounce)
        If PrivateTorrent.Checked Then
            Dim TorrentIsPrivate As New TorrentNumber
            TorrentIsPrivate.Value = 1
            torrentMFinfo.Value("private") = TorrentIsPrivate
        End If
        TorrentMeta.Add("info", torrentMFinfo)
        Dim torrentencoding As New TorrentString
        torrentencoding.Value = "UTF8"
        TorrentMeta.Add("encoding", torrentencoding)
        If MultiTrackerEnabled.Checked Then
            CheckMultitTrackerTiers()
            TorrentMeta.Add("announce-list", Multitracker.MultiTrackerTiers)
        End If
        If File.Exists(torrentfilegenerate) Then Kill(torrentfilegenerate)
        Dim torrentout As Integer = FreeFile()
        FileOpen(torrentout, torrentfilegenerate, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
        FilePut(torrentout, TorrentMeta.Bencoded)
        FileClose(torrentout)
        GC.Collect()

        If MakeExternals.Checked Then
            Dim SFVtoMake As String
            Dim MD5toMake As String
            Dim SHA1toMake As String
            Dim TigerToMake As String
            Dim ED2KtoMake As String
            Dim basefilename As String = Microsoft.VisualBasic.Left(FileNameToMake.Text, Len(FileNameToMake.Text) - 1)
            Dim md5filename As String = basefilename + ".md5"
            Dim sha1filename As String = basefilename + ".sha1"
            Dim ed2kfilename As String = basefilename + ".ed2k"
            Dim tigerfilename As String = basefilename + ".tiger"
            Dim sfvfilename As String
            Dim pathtiers() As String
            pathtiers = Split(basefilename, "\")
            pathtiers(pathtiers.GetUpperBound(0)) = Replace(pathtiers(pathtiers.GetUpperBound(0)), " ", ".")
            pathtiers(pathtiers.GetUpperBound(0)) = Replace(pathtiers(pathtiers.GetUpperBound(0)), "_", ".")
            For Each tier As String In pathtiers
                sfvfilename = sfvfilename + "\" + tier
            Next
            sfvfilename = Microsoft.VisualBasic.Mid(sfvfilename, 2, Len(sfvfilename) - 1) + ".sfv"

            Dim LinkGenerator As New EAD.PeerToPeer.LinkGeneration
            Dim HashConverter As New EAD.Conversion.HashChanger
            For Each list As TorrentDictionary In TorrentFilesArray
                LinkGenerator = New EAD.PeerToPeer.LinkGeneration
                Dim FileNameForCheck As New TorrentString
                For Each filename As TorrentString In list.Value("path").value
                    FileNameForCheck = filename
                Next
                If IncludeMD5.Checked Then
                    Dim md5file As New TorrentString
                    md5file = list.Value("md5sum")
                    MD5toMake = MD5toMake + FileNameForCheck.Value + " " + md5file.Value + Chr(13) + Chr(10)
                End If
                If IncludeSHA1.Checked Then
                    HashConverter = New EAD.Conversion.HashChanger
                    HashConverter.rawhash = list.Value("sha1").value
                    SHA1toMake = SHA1toMake + FileNameForCheck.Value + " " + HashConverter.hexhash + Chr(13) + Chr(10)
                End If
                If IncludeTiger.Checked Then
                    If list.Contains("tiger") Then
                        HashConverter = New EAD.Conversion.HashChanger
                        HashConverter.rawhash = list.Value("tiger").value
                        TigerToMake = TigerToMake + FileNameForCheck.Value + " " + HashConverter.base32 + Chr(13) + Chr(10)
                    End If
                End If
                If IncludeCRC32.Checked Then
                    SFVtoMake = SFVtoMake + FileNameForCheck.Value + " " + list.Value("crc32").value + Chr(13) + Chr(10)
                End If
                If IncludeED2K.Checked Then
                    LinkGenerator.ED2KRaw = list.Value("ed2k").Value
                    LinkGenerator.FileSize = list.Value("length").Value
                    LinkGenerator.FileName = FileNameForCheck.Value
                    ED2KtoMake = ED2KtoMake + LinkGenerator.ClassicED2KLink + Chr(13) + Chr(10)
                    ED2KtoMake = ED2KtoMake + FileNameForCheck.Value + " " + LinkGenerator.ED2KHex + Chr(13) + Chr(10)
                End If
                GC.Collect()
            Next
            If IncludeMD5.Checked Then
                If File.Exists(md5filename) Then Kill(md5filename)
                Dim md5out As Integer = FreeFile()
                FileOpen(md5out, md5filename, OpenMode.Output)
                Print(md5out, MD5toMake)
                FileClose(md5out)
            End If
            If IncludeTiger.Checked Then
                If File.Exists(tigerfilename) Then Kill(tigerfilename)
                Dim tigerout As Integer = FreeFile()
                FileOpen(tigerout, tigerfilename, OpenMode.Output)
                Print(tigerout, TigerToMake)
                FileClose(tigerout)
            End If
            If IncludeSHA1.Checked Then
                If File.Exists(sha1filename) Then Kill(sha1filename)
                Dim sha1out As Integer = FreeFile()
                FileOpen(sha1out, sha1filename, OpenMode.Output)
                Print(sha1out, SHA1toMake)
                FileClose(sha1out)
            End If
            If IncludeED2K.Checked Then
                If File.Exists(ed2kfilename) Then Kill(ed2kfilename)
                Dim ed2kout As Integer = FreeFile()
                FileOpen(ed2kout, ed2kfilename, OpenMode.Output)
                Print(ed2kout, ED2KtoMake)
                FileClose(ed2kout)
            End If
            If IncludeCRC32.Checked Then
                If File.Exists(sfvfilename) Then Kill(sfvfilename)
                Dim sfvout As Integer = FreeFile()
                FileOpen(sfvout, sfvfilename, OpenMode.Output)
                Print(sfvout, SFVtoMake)
                FileClose(sfvout)
            End If
        End If
        GC.Collect()
        'FileHandling.Close()
    End Sub

    Public Sub MakeTorrentFromFile(ByVal NameOfFile As String)
        Dim filesize As Long = FileLen(NameOfFile)
        Dim filesha1 As New TorrentString
        Dim filemd5 As New TorrentString
        Dim fileed2k As New TorrentString
        Dim FileCRC32 As New TorrentString
        Dim FileTiger As New TorrentString
        Dim filecheck As FileStream
        Dim fileed2khex As String
        Dim FileSHA1Hex As String
        Dim FileTigerBase32 As String
        Dim ed2khashvalue As String
        Dim buff As StringBuilder = New StringBuilder
        Dim HashConverter As New EAD.Conversion.HashChanger
        OptionalHashProgress.Maximum = CountMultiplier
        OptionalHashProgress.Value = 0
        If filesize <= 4707319808 Then
            If IncludeTiger.Checked Then
                TigerHash = New EAD.Cryptography.ThexCS.ThexThreaded
                HashConverter = New EAD.Conversion.HashChanger
                HashConverter.bytehash = TigerHash.GetTTH_Value(NameOfFile)
                System.Windows.Forms.Application.DoEvents()
                FileTiger.Value = HashConverter.rawhash
                FileTigerBase32 = HashConverter.base32
                OptionalHashProgress.Value = OptionalHashProgress.Value + 1
            End If
        Else
            OptionalHashProgress.Value = OptionalHashProgress.Value + 1
        End If
        If IncludeSHA1.Checked Then
            HashConverter = New EAD.Conversion.HashChanger
            filecheck = New FileStream(NameOfFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            sha1.ComputeHash(filecheck)
            filecheck.Close()
            System.Windows.Forms.Application.DoEvents()
            HashConverter.bytehash = sha1.Hash
            filesha1.Value = HashConverter.rawhash
            FileSHA1Hex = HashConverter.hexhash
            OptionalHashProgress.Value = OptionalHashProgress.Value + 1
        End If
        If IncludeMD5.Checked Then
            HashConverter = New EAD.Conversion.HashChanger
            filecheck = New FileStream(NameOfFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            md5.ComputeHash(filecheck)
            filecheck.Close()
            HashConverter.bytehash = md5.Hash
            System.Windows.Forms.Application.DoEvents()
            filemd5.Value = HashConverter.hexhash
            OptionalHashProgress.Value = OptionalHashProgress.Value + 1
        End If
        If IncludeCRC32.Checked Then
            Dim c As New EAD.Cryptography.VisualBasic.CRC32
            Dim crc As Integer = 0
            filecheck = New FileStream(NameOfFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            crc = c.GetCrc32(filecheck)
            filecheck.Close()
            FileCRC32.Value = String.Format("{0:X8}", crc)
            System.Windows.Forms.Application.DoEvents()
            OptionalHashProgress.Value = OptionalHashProgress.Value + 1
        End If
        If IncludeED2K.Checked Then
            Call GetED2KHash(NameOfFile, fileed2khex, ed2khashvalue)
            OptionalHashProgress.Value = OptionalHashProgress.Value + 1
            fileed2k.Value = ed2khashvalue
        End If
        FileHandling = File.Open(NameOfFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
        Dim hashdata(20) As Byte
        Dim fileoffsettoscan As Long
        If AutomaticPieceSize.Checked = True Then piecesizetouse = getautopiecesize(filesize) Else piecesizetouse = CInt(PieceSize.Text)
        Dim hashes As String
        Dim temphashdata As String
        Dim updatehash As String
        Dim numbytes As Long
        fileoffsettoscan = 0
        FileHandling.Seek(0, SeekOrigin.Begin)
        Do Until fileoffsettoscan >= filesize
            TorrentProgress.Value = fileoffsettoscan / filesize * 100
            System.Windows.Forms.Application.DoEvents()
            Dim DataToHash(piecesizetouse) As Byte
            numbytes = FileHandling.Read(DataToHash, 0, piecesizetouse)
            Dim ReHashData(numbytes - 1) As Byte
            For dataindex As Long = 0 To numbytes - 1
                ReHashData(dataindex) = DataToHash(dataindex)
            Next
            hashdata = sha1.ComputeHash(ReHashData)
            For dataindex As Integer = 0 To 19
                hashes = hashes + System.Text.Encoding.Default.GetString(hashdata, dataindex, 1)
            Next
            fileoffsettoscan = fileoffsettoscan + piecesizetouse
            Erase ReHashData
            Erase DataToHash
            Thread.Sleep(10)
            GC.Collect()
        Loop
        TorrentProgress.Value = 100
        FileHandling.Close()
        Dim FileName As String
        Dim FilePath As String
        Dim FileOffset As Integer
        Dim FileNameLength As Integer
        Dim torrentpieces As New TorrentString
        FileOffset = NameOfFile.LastIndexOf("\")
        FileNameLength = Len(NameOfFile) - FileOffset
        FileName = Microsoft.VisualBasic.Right(NameOfFile, FileNameLength - 1)
        FilePath = Microsoft.VisualBasic.Left(NameOfFile, FileOffset)
        Dim torrentfilename As New TorrentString
        torrentfilename.Value = FileName
        Dim torrentencoding As New TorrentString
        Dim torrentlength As New TorrentNumber
        torrentlength.Value = filesize
        Dim torrentpiecelength As New TorrentNumber
        torrentpiecelength.Value = piecesizetouse
        torrentpieces.Value = hashes
        torrentencoding.Value = "UTF8"
        'MsgBox("configuring data dictionaries")
        Dim torrentroot As New TorrentDictionary
        Dim torrentinfo As New TorrentDictionary
        Dim torrentannounce As New TorrentString
        torrentannounce.Value = AnnounceURL.Text
        torrentinfo.Add("length", torrentlength)
        torrentinfo.Add("name", torrentfilename)
        torrentinfo.Add("pieces", torrentpieces)
        torrentinfo.Add("piece length", torrentpiecelength)
        If MultiTrackerEnabled.Checked Then
            CheckMultitTrackerTiers()
            torrentroot.Add("announce-list", Multitracker.MultiTrackerTiers)
        End If
        If IncludeSHA1.Checked Then torrentinfo.Add("sha1", filesha1)
        If IncludeMD5.Checked Then torrentinfo.Add("md5sum", filemd5)
        If IncludeCRC32.Checked Then torrentinfo.Add("crc32", FileCRC32)
        If IncludeED2K.Checked Then torrentinfo.Add("ed2k", fileed2k)
        If IncludeTiger.Checked Then torrentinfo.Add("tiger", FileTiger)
        If UseWSAConfig = True Then
            Dim WebSeedCount As Integer
            Dim WebSeeds As New ArrayList
            Dim WebSeedsList As New TorrentList
            WebSeedCount = GetWebSeedData(WebSeeds)
            WebSeedsList.Value = WebSeeds
            If WebSeedCount >= 1 Then torrentroot.Add("httpseeds", WebSeedsList)
        End If
        Dim CreatedBy As New TorrentString
        CreatedBy.Value = "Torrent Generated by VB.Net TorrentGen - Written by DWKnight"
        torrentroot.Add("created by", CreatedBy)
        Dim CommentOfTorrent As New TorrentString
        If Not Trim(TorrentComment.Text) = "" Then
            CommentOfTorrent.Value = Trim(TorrentComment.Text)
            torrentroot.Add("comment", CommentOfTorrent)
        End If

        If PrivateTorrent.Checked Then
            Dim TorrentIsPrivate As New TorrentNumber
            TorrentIsPrivate.Value = 1
            torrentinfo.Value("private") = TorrentIsPrivate
        End If
        torrentroot.Add("info", torrentinfo)
        If Not AnnounceURL.Text = "" Then torrentroot.Add("announce", torrentannounce)
        torrentroot.Add("encoding", torrentencoding)
        Dim torrentout As Integer = FreeFile()
        Dim torrentfilegenerate As String = NameOfFile + ".torrent"
        FileOpen(torrentout, torrentfilegenerate, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
        FilePut(torrentout, torrentroot.Bencoded)
        FileClose(torrentout)
        Erase hashdata
        If MakeExternals.Checked Then
            If IncludeSHA1.Checked Then
                Dim sha1make As Integer = FreeFile()
                FileOpen(sha1make, NameOfFile + ".sha1", OpenMode.Output)
                PrintLine(sha1make, torrentfilename.Value + " " + FileSHA1Hex)
                FileClose(sha1make)
            End If
            If filesize <= 4707319808 Then
                If IncludeTiger.Checked Then
                    Dim tigermake As Integer = FreeFile()
                    FileOpen(tigermake, NameOfFile + ".tiger", OpenMode.Output)
                    PrintLine(tigermake, torrentfilename.Value + " " + FileTigerBase32)
                    FileClose(tigermake)
                End If
            End If
            If IncludeMD5.Checked Then
                Dim md5make As Integer = FreeFile()
                FileOpen(md5make, NameOfFile + ".md5", OpenMode.Output)
                PrintLine(md5make, torrentfilename.Value + " " + filemd5.Value)
                FileClose(md5make)
            End If
            If IncludeCRC32.Checked Then
                Dim sfvmake As Integer = FreeFile()
                FileOpen(sfvmake, NameOfFile + ".sfv", OpenMode.Output)
                PrintLine(sfvmake, torrentfilename.Value + " " + FileCRC32.Value)
                FileClose(sfvmake)
            End If
            If IncludeED2K.Checked Then
                Dim LinkGenerator As New EAD.PeerToPeer.LinkGeneration
                LinkGenerator.FileName = torrentfilename.Value
                LinkGenerator.FileSize = torrentlength.Value
                LinkGenerator.ED2KHex = fileed2khex
                Dim ed2kmake As Integer = FreeFile()
                FileOpen(ed2kmake, NameOfFile + ".ed2k", OpenMode.Output)
                PrintLine(ed2kmake, LinkGenerator.ClassicED2KLink())
                PrintLine(ed2kmake, (torrentfilename.Value + " " + LinkGenerator.ED2KHex + Chr(13) + Chr(10)))
                FileClose(ed2kmake)
            End If
        End If
        GC.Collect()
    End Sub

    Private Sub BuildTorrentNow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BuildTorrentNow.Click
        CountMultiplier = 0
        OptionalHashProgress.Value = 0
        AnnounceURL.Text = Trim(AnnounceURL.Text)
        If IsValidAnnounce(AnnounceURL.Text) = InvalidAnnounce Then
            If Not AnnounceURL.Text = "" Then
                MsgBox("AnnounceURL is invalid", MsgBoxStyle.Exclamation, "Error")
                GoTo unlock
            Else
                If MsgBox("Announce URL is blank, continue anyway", MsgBoxStyle.YesNo, "Warning") = MsgBoxResult.No Then GoTo unlock
            End If
        End If
        If DelayMessages Then
            Dim warninglevel As Integer
            If IncludeCRC32.Checked Then warninglevel = warninglevel + 1
            If IncludeMD5.Checked Then warninglevel = warninglevel + 2
            If IncludeSHA1.Checked Then warninglevel = warninglevel + 2
            If warninglevel > 2 Then MsgBox("The number of additional hashes you have requested may cause the torrent's generation to take an excessive amount of time." + Chr(10) + "This notification can be turned off in the advanced settings screen.")
        End If
        If GenerateVerbose Then MsgBox("Verbose torrent generation is active, additional information will be given between each file." + Chr(10) + "This setting can be turned off in the advanced settings creen.")
        If IncludeCRC32.Checked Then CountMultiplier = CountMultiplier + 1
        If IncludeMD5.Checked Then CountMultiplier = CountMultiplier + 1
        If IncludeSHA1.Checked Then CountMultiplier = CountMultiplier + 1
        If IncludeED2K.Checked Then CountMultiplier = CountMultiplier + 1
        If IncludeTiger.Checked Then CountMultiplier = CountMultiplier + 1
        If System.IO.Directory.Exists(FileNameToMake.Text) Then
            BuildTorrentNow.Enabled = False
            ExitWithoutSave.Enabled = False
            ExitWithSave.Enabled = False
            If MakeSeparateTorrents.Checked = True Then
                'Dim Linkthread As New Thread(AddressOf MakeTorrentsFromFolder)
                'linkthread.Start()
                'Linkthread.Join()
                Call MakeTorrentsFromFolder()
            Else
                'Dim Linkthread As New Thread(AddressOf MakeOneTorrentFromFolder)
                'linkthread.Start()
                'Linkthread.Join()
                Call MakeOneTorrentFromFolder()
            End If
            MsgBox("Torrent Generated sucessfully")
            BuildTorrentNow.Enabled = True
            ExitWithSave.Enabled = True
            ExitWithoutSave.Enabled = True
            Exit Sub
        End If
        If Not System.IO.File.Exists(FileNameToMake.Text) Then
            MsgBox("ERROR: Filename is not a valid file, can't make it", , "WHOOPSIE!")
            Exit Sub
        End If
        BuildTorrentNow.Enabled = False
        ExitWithoutSave.Enabled = False
        ExitWithSave.Enabled = False
        MakeTorrentFromFile(FileNameToMake.Text)
        MsgBox("Torrent Generated sucessfully")
unlock:
        BuildTorrentNow.Enabled = True
        ExitWithSave.Enabled = True
        ExitWithoutSave.Enabled = True
        GC.Collect()
    End Sub

    Private Sub PieceSize_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles PieceSize.LostFocus
        Dim piecevalue As Integer
        piecevalue = CInt(PieceSize.Text)
        piecevalue = Math.Round(piecevalue / 16384, 0) * 16384
        If piecevalue < 32768 Then piecevalue = 32768
        PieceSize.Text = CStr(piecevalue)
    End Sub

    Private Sub TorrentBuild_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim arguments As [String]() = Environment.GetCommandLineArgs()
        If arguments.GetLength(0) > 1 Then
            FileNameToMake.Text = arguments(1)
        End If
        Dim BlackListSFV As New TorrentString
        Dim BlackListCDP As New TorrentString
        Dim BlackListCDT As New TorrentString
        BlackListSFV.Value = ".sfv"
        BlackListCDP.Value = ".cdp"
        BlackListCDT.Value = ".cdt"
        BlackListedFiles.Add(BlackListSFV)
        BlackListedFiles.Add(BlackListCDP)
        BlackListedFiles.Add(BlackListCDT)
        ' moved the above here to troubleshoot blacklisting files issues.
        Dim FileOffset As Integer
        FileOffset = Microsoft.VisualBasic.Left(arguments(0), Len(arguments(0))).LastIndexOf("\")
        LocalPath = System.IO.Path.GetFullPath(Microsoft.VisualBasic.Left(arguments(0), FileOffset)) + "\"
        If System.IO.File.Exists(LocalPath + "tgen.configure") Then
            Dim loadsettings As Integer = FreeFile()
            Dim intermediarysettingdata As String
            FileOpen(loadsettings, LocalPath + "tgen.configure", OpenMode.Binary, OpenAccess.Read, OpenShare.LockRead)
            intermediarysettingdata = Space(FileLen(LocalPath + "tgen.configure"))
            FileGet(loadsettings, intermediarysettingdata)
            FileClose(loadsettings)
            Dim ConfigData As New TorrentDictionary
            ConfigData.Parse(intermediarysettingdata)
            Dim TrackerAnnounce As New TorrentString
            Dim ConfigPieceSize As New TorrentNumber
            Dim UseSHA1 As New TorrentNumber
            Dim UseMD5 As New TorrentNumber
            Dim UseCRC32 As New TorrentNumber
            Dim UseComment As New TorrentString
            Dim UseED2K As New TorrentNumber
            Dim UseTiger As New TorrentNumber
            Dim AutoPiece As New TorrentNumber
            Dim FolderGen As New TorrentNumber
            Dim GenExtra As New TorrentNumber
            TrackerAnnounce = ConfigData.Value("tracker")
            ConfigPieceSize = ConfigData.Value("piecesize")
            If ConfigData.Contains("externals") Then
                GenExtra = ConfigData.Value("externals")
                MakeExternals.Checked = GenExtra.Value
            End If
            If ConfigData.Contains("folder") Then
                FolderGen = ConfigData.Value("folder")
                MakeSeparateTorrents.Checked = FolderGen.Value
            End If
            If ConfigData.Contains("sha1") Then
                UseSHA1 = ConfigData.Value("sha1")
                IncludeSHA1.Checked = UseSHA1.Value
            End If
            If ConfigData.Contains("md5") Then
                UseMD5 = ConfigData.Value("md5")
                IncludeMD5.Checked = UseMD5.Value
            End If
            If ConfigData.Contains("crc") Then
                UseCRC32 = ConfigData.Value("crc")
                IncludeCRC32.Checked = UseCRC32.Value
            End If
            If ConfigData.Contains("ed2k") Then
                UseED2K = ConfigData.Value("ed2k")
                IncludeED2K.Checked = UseED2K.Value
            End If
            If ConfigData.Contains("tiger") Then
                UseTiger = ConfigData.Value("tiger")
                IncludeTiger.Checked = UseTiger.Value
            End If
            If ConfigData.Contains("comment") Then
                UseComment = ConfigData.Value("comment")
                TorrentComment.Text = UseComment.Value
            End If
            If ConfigData.Contains("advanced") Then
                Dim AdvancedConfigData As New TorrentDictionary
                AdvancedConfigData = ConfigData.Value("advanced")
                Dim VerboseGenerate As New TorrentNumber
                Dim UseWSASettings As New TorrentNumber
                Dim EnableDelayCfg As New TorrentNumber
                VerboseGenerate = AdvancedConfigData.Value("verbose")
                UseWSASettings = AdvancedConfigData.Value("usewsa")
                EnableDelayCfg = AdvancedConfigData.Value("delay")
                GenerateVerbose = VerboseGenerate.Value
                UseWSAConfig = UseWSASettings.Value
                DelayMessages = EnableDelayCfg.Value
            Else
                GenerateVerbose = True
                UseWSAConfig = False
                DelayMessages = True
            End If
            If ConfigData.Contains("blankblacklist") Then
                Dim blankblacklist As New TorrentNumber
                If blankblacklist.Value = True Then
                    BlackListedFiles = New ArrayList
                End If
            End If
            If ConfigData.Contains("blacklist") Then
                Dim BlackListNames As New TorrentList
                BlackListNames = ConfigData.Value("blacklist")
                BlackListedFiles = BlackListNames.Value
            End If
            If ConfigData.Contains("multitracker") Then
                Multitracker.MultiTrackerTiers = ConfigData.Value("multitracker")
                Multitracker.UpdateInput()
            End If
            If ConfigData.Contains("usemultitracker") Then
                Dim GetMultiTracker As New TorrentNumber
                GetMultiTracker = ConfigData.Value("usemultitracker")
                MultiTrackerEnabled.Checked = GetMultiTracker.Value
            End If
            AutoPiece = ConfigData.Value("autopiece")
            AutomaticPieceSize.Checked = AutoPiece.Value
            AnnounceURL.Text = TrackerAnnounce.Value
            PieceSize.Text = CStr(ConfigPieceSize.Value)
        End If
        AutomaticPieceSize_CheckedChanged(sender, e)
        GC.Collect()
    End Sub

    Private Sub ExitWithSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitWithSave.Click
        Call SaveSettings_Click(sender, e)
        End
    End Sub

    Private Sub ExitWithoutSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitWithoutSave.Click
        End
    End Sub

    Private Sub AutomaticPieceSize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutomaticPieceSize.CheckedChanged
        If AutomaticPieceSize.Checked = True Then
            PieceSize.Enabled = False
        Else
            PieceSize.Enabled = True
        End If
    End Sub

    Private Sub AdvSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdvSet.Click
        Dim Advanced As New AdvancedConfiguration
        Advanced.Show()
    End Sub

    Private Sub SaveSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveSettings.Click
        AnnounceURL.Text = Trim(AnnounceURL.Text)
        Dim TrackerAnnounce As New TorrentString
        Dim ConfigPieceSize As New TorrentNumber
        Dim UseSHA1 As New TorrentNumber
        Dim UseMD5 As New TorrentNumber
        Dim UseCRC32 As New TorrentNumber
        Dim UseED2K As New TorrentNumber
        Dim UseTiger As New TorrentNumber
        Dim genextra As New TorrentNumber
        Dim AutoPiece As New TorrentNumber
        Dim FolderGen As New TorrentNumber
        Dim advancedsettings As New TorrentDictionary
        Dim VerboseGenerate As New TorrentNumber
        Dim UseWSASettings As New TorrentNumber
        Dim EnableDelayCfg As New TorrentNumber
        Dim BlackListNames As New TorrentList
        Dim UseComment As New TorrentString
        Dim UseMultiTracker As New TorrentNumber
        BlackListNames.Value = BlackListedFiles
        VerboseGenerate.Value = GenerateVerbose
        UseWSASettings.Value = UseWSAConfig
        EnableDelayCfg.Value = DelayMessages
        advancedsettings.Add("verbose", VerboseGenerate)
        advancedsettings.Add("usewsa", UseWSASettings)
        advancedsettings.Add("delay", EnableDelayCfg)
        TrackerAnnounce.Value = AnnounceURL.Text
        ConfigPieceSize.Value = CInt(PieceSize.Text)
        FolderGen.Value = MakeSeparateTorrents.Checked
        UseSHA1.Value = IncludeSHA1.Checked
        UseMD5.Value = IncludeMD5.Checked
        UseCRC32.Value = IncludeCRC32.Checked
        UseED2K.Value = IncludeED2K.Checked
        UseTiger.Value = IncludeTiger.Checked
        genextra.Value = MakeExternals.Checked
        AutoPiece.Value = AutomaticPieceSize.Checked
        UseComment.Value = Trim(TorrentComment.Text)
        UseMultiTracker.Value = MultiTrackerEnabled.Checked
        Dim SaveData As New TorrentDictionary
        Dim blacklistblank As New TorrentNumber
        Dim blanktorrentlist As New TorrentList
        blanktorrentlist.Value = New ArrayList
        If Not BlackListNames.Bencoded = blanktorrentlist.Bencoded Then
            SaveData.Add("blacklist", BlackListNames)
            blacklistblank.Value = False
        Else
            blacklistblank.Value = True
        End If
        If Not Multitracker.MultiTrackerTiers.Bencoded = blanktorrentlist.Bencoded Then
            CheckMultitTrackerTiers()
            SaveData.Add("multitracker", Multitracker.MultiTrackerTiers)
        End If
        SaveData.Add("usemultitracker", UseMultiTracker)
        SaveData.Add("blankblacklist", blacklistblank)
        SaveData.Add("advanced", advancedsettings)
        SaveData.Add("tracker", TrackerAnnounce)
        SaveData.Add("piecesize", ConfigPieceSize)
        SaveData.Add("sha1", UseSHA1)
        SaveData.Add("md5", UseMD5)
        SaveData.Add("crc", UseCRC32)
        SaveData.Add("ed2k", UseED2K)
        SaveData.Add("tiger", UseTiger)
        SaveData.Add("externals", genextra)
        SaveData.Add("autopiece", AutoPiece)
        SaveData.Add("folder", FolderGen)
        SaveData.Add("comment", UseComment)
        Dim savesettings As Integer = FreeFile()
        If System.IO.File.Exists(LocalPath + "tgen.configure") Then Kill(LocalPath + "tgen.configure")
        FileOpen(savesettings, LocalPath + "tgen.configure", OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
        FilePut(savesettings, SaveData.Bencoded())
        FileClose(savesettings)
        GC.Collect()
    End Sub

    Private Sub BlacklistingScreen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BlacklistingScreen.Click
        Dim blacklistwindow As New ChangeBlacklistedFiles
        blacklistwindow.Show()
    End Sub

    Private Sub FileNameToMake_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles FileNameToMake.DragDrop
        FileNameToMake.Text = e.Data.GetData("System.String", False)
    End Sub

    Private Sub MultiTrackerSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MultiTrackerSettings.Click
        Dim SaveTiers As New EAD.Torrent.TorrentList
        SaveTiers = Multitracker.MultiTrackerTiers
        Multitracker = New MultiTrackerGenerator
        Multitracker.MultiTrackerTiers = SaveTiers
        Multitracker.UpdateInput()
        Multitracker.Show()
    End Sub

    Private Sub CheckMultitTrackerTiers()
        For Each TierToCheck As EAD.Torrent.TorrentList In Multitracker.MultiTrackerTiers.Value
            For Each AnnounceToCheck As EAD.Torrent.TorrentString In TierToCheck.Value
                If AnnounceToCheck.Value = Trim(AnnounceURL.Text) Then Exit Sub
            Next
        Next
        Dim AddAnnounce As New EAD.Torrent.TorrentString
        AddAnnounce.Value = Trim(AnnounceURL.Text)
        Dim NewTier As New EAD.Torrent.TorrentList
        NewTier.Value.Add(AddAnnounce)
        Multitracker.MultiTrackerTiers.Value.Add(NewTier)
        GC.Collect()
    End Sub
End Class
