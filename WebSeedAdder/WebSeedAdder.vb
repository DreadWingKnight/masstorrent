'--------------------------------------------------------
' WebSeedAdder - WebSeed.vb
' Webseed Support Code
' Harold Feit
' ToDo
' - Add support to read Webseeds from a torrent file on load.
'--------------------------------------------------------

Imports EAD.Torrent
Imports EAD.Torrent.Announces

Public Class WebSeedAdder
    Inherits System.Windows.Forms.Form

    Dim LocalPath As String

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
    Friend WithEvents TorrentBasePath As System.Windows.Forms.TextBox
    Friend WithEvents TorrentBrowse As System.Windows.Forms.Button
    Friend WithEvents AddWebSeed As System.Windows.Forms.Button
    Friend WithEvents TorrentSelect As System.Windows.Forms.OpenFileDialog
    Friend WithEvents LeaveApp As System.Windows.Forms.Button
    Friend WithEvents ExitWithSave As System.Windows.Forms.Button
    Friend WithEvents RemoveWebseeds As System.Windows.Forms.CheckBox
    Friend WithEvents WebSeeds As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CurrentSeed As System.Windows.Forms.TextBox
    Friend WithEvents AddToList As System.Windows.Forms.Button
    Friend WithEvents RemoveFromList As System.Windows.Forms.Button
    Friend WithEvents ReplaceInList As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(WebSeedAdder))
        Me.TorrentBasePath = New System.Windows.Forms.TextBox
        Me.TorrentBrowse = New System.Windows.Forms.Button
        Me.AddWebSeed = New System.Windows.Forms.Button
        Me.LeaveApp = New System.Windows.Forms.Button
        Me.TorrentSelect = New System.Windows.Forms.OpenFileDialog
        Me.ExitWithSave = New System.Windows.Forms.Button
        Me.RemoveWebseeds = New System.Windows.Forms.CheckBox
        Me.WebSeeds = New System.Windows.Forms.ListBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.CurrentSeed = New System.Windows.Forms.TextBox
        Me.AddToList = New System.Windows.Forms.Button
        Me.RemoveFromList = New System.Windows.Forms.Button
        Me.ReplaceInList = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'TorrentBasePath
        '
        Me.TorrentBasePath.Location = New System.Drawing.Point(0, 0)
        Me.TorrentBasePath.Name = "TorrentBasePath"
        Me.TorrentBasePath.Size = New System.Drawing.Size(288, 20)
        Me.TorrentBasePath.TabIndex = 1
        Me.TorrentBasePath.Text = ""
        '
        'TorrentBrowse
        '
        Me.TorrentBrowse.Location = New System.Drawing.Point(0, 24)
        Me.TorrentBrowse.Name = "TorrentBrowse"
        Me.TorrentBrowse.Size = New System.Drawing.Size(152, 24)
        Me.TorrentBrowse.TabIndex = 2
        Me.TorrentBrowse.Text = "Select Torrent"
        '
        'AddWebSeed
        '
        Me.AddWebSeed.Location = New System.Drawing.Point(0, 216)
        Me.AddWebSeed.Name = "AddWebSeed"
        Me.AddWebSeed.Size = New System.Drawing.Size(288, 24)
        Me.AddWebSeed.TabIndex = 3
        Me.AddWebSeed.Text = "Add/Remove Webseed"
        '
        'LeaveApp
        '
        Me.LeaveApp.Location = New System.Drawing.Point(0, 240)
        Me.LeaveApp.Name = "LeaveApp"
        Me.LeaveApp.Size = New System.Drawing.Size(128, 24)
        Me.LeaveApp.TabIndex = 4
        Me.LeaveApp.Text = "Exit"
        '
        'TorrentSelect
        '
        Me.TorrentSelect.Filter = "Torrent Files|*.torrent"
        '
        'ExitWithSave
        '
        Me.ExitWithSave.Location = New System.Drawing.Point(128, 240)
        Me.ExitWithSave.Name = "ExitWithSave"
        Me.ExitWithSave.Size = New System.Drawing.Size(160, 24)
        Me.ExitWithSave.TabIndex = 10
        Me.ExitWithSave.Text = "Exit Saving Seed Info"
        '
        'RemoveWebseeds
        '
        Me.RemoveWebseeds.Location = New System.Drawing.Point(0, 48)
        Me.RemoveWebseeds.Name = "RemoveWebseeds"
        Me.RemoveWebseeds.Size = New System.Drawing.Size(256, 16)
        Me.RemoveWebseeds.TabIndex = 11
        Me.RemoveWebseeds.Text = "Remove all Webseeds from Torrent?"
        '
        'WebSeeds
        '
        Me.WebSeeds.Location = New System.Drawing.Point(0, 80)
        Me.WebSeeds.Name = "WebSeeds"
        Me.WebSeeds.Size = New System.Drawing.Size(288, 69)
        Me.WebSeeds.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(0, 64)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(272, 16)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Webseeds:"
        '
        'CurrentSeed
        '
        Me.CurrentSeed.Location = New System.Drawing.Point(0, 152)
        Me.CurrentSeed.Name = "CurrentSeed"
        Me.CurrentSeed.Size = New System.Drawing.Size(288, 20)
        Me.CurrentSeed.TabIndex = 14
        Me.CurrentSeed.Text = ""
        '
        'AddToList
        '
        Me.AddToList.Location = New System.Drawing.Point(0, 176)
        Me.AddToList.Name = "AddToList"
        Me.AddToList.Size = New System.Drawing.Size(96, 24)
        Me.AddToList.TabIndex = 15
        Me.AddToList.Text = "Add"
        '
        'RemoveFromList
        '
        Me.RemoveFromList.Location = New System.Drawing.Point(96, 176)
        Me.RemoveFromList.Name = "RemoveFromList"
        Me.RemoveFromList.Size = New System.Drawing.Size(96, 24)
        Me.RemoveFromList.TabIndex = 16
        Me.RemoveFromList.Text = "Remove"
        '
        'ReplaceInList
        '
        Me.ReplaceInList.Location = New System.Drawing.Point(192, 176)
        Me.ReplaceInList.Name = "ReplaceInList"
        Me.ReplaceInList.Size = New System.Drawing.Size(96, 24)
        Me.ReplaceInList.TabIndex = 17
        Me.ReplaceInList.Text = "Replace"
        '
        'WebSeedAdder
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(290, 263)
        Me.Controls.Add(Me.ReplaceInList)
        Me.Controls.Add(Me.RemoveFromList)
        Me.Controls.Add(Me.AddToList)
        Me.Controls.Add(Me.CurrentSeed)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.WebSeeds)
        Me.Controls.Add(Me.RemoveWebseeds)
        Me.Controls.Add(Me.ExitWithSave)
        Me.Controls.Add(Me.TorrentBasePath)
        Me.Controls.Add(Me.LeaveApp)
        Me.Controls.Add(Me.AddWebSeed)
        Me.Controls.Add(Me.TorrentBrowse)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "WebSeedAdder"
        Me.Text = "Add or Remove Webseeds from Torrents"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub TorrentSelect_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TorrentSelect.FileOk
        TorrentBasePath.Text = TorrentSelect.FileName
        LoadTorrent(TorrentBasePath.Text)
    End Sub

    Private Sub TorrentBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TorrentBrowse.Click
        TorrentSelect.ShowDialog()
    End Sub

    Private Sub LoadTorrent(ByVal TorrentFile As String)
        Dim MetaDataHolder As New TorrentMetaData
        Dim TorrentData As New TorrentDictionary
        Dim TorrentFileload As Integer
        Dim intermediarytorrentdata As String
        Dim fulltorrent As String
        TorrentFileload = FreeFile()
        FileOpen(TorrentFileload, TorrentFile, OpenMode.Binary, OpenAccess.Default)
        intermediarytorrentdata = Space(FileLen(TorrentFile))
        FileGet(TorrentFileload, intermediarytorrentdata)
        FileClose(TorrentFileload)
        TorrentData.Parse(intermediarytorrentdata)
        MetaDataHolder.Torrent = TorrentData
        If TorrentData.Contains("httpseeds") Then
            For Each Seed As TorrentString In TorrentData.Value("httpseeds").value
                Dim loadthisseed As Boolean = True
                For Each loadedseed As String In WebSeeds.Items
                    If Seed.Value = loadedseed Then
                        loadthisseed = False
                    End If
                Next
                If loadthisseed Then
                    WebSeeds.Items.Add(Seed.Value)
                End If
            Next
        End If
    End Sub

    Private Sub AddWebSeed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddWebSeed.Click
        If Not System.IO.File.Exists(TorrentBasePath.Text) Then
            MsgBox("ERROR: Filename is not a valid file", , "WHOOPSIE!")
            Exit Sub
        End If
        Dim MetaDataHolder As New TorrentMetaData
        Dim TorrentData As New TorrentDictionary
        Dim TorrentFileload As Integer
        Dim intermediarytorrentdata As String
        Dim fulltorrent As String
        Dim WebSeedAddressArray As New ArrayList
        Dim WebSeedAddressList As New TorrentList
        For Each webseedtoadd As String In WebSeeds.Items
            Dim AddToList As New TorrentString
            AddToList.Value = webseedtoadd
            WebSeedAddressArray.Add(AddToList)
        Next

        'variables defined
        TorrentFileload = FreeFile()
        FileOpen(TorrentFileload, TorrentBasePath.Text, OpenMode.Binary, OpenAccess.Default)
        intermediarytorrentdata = Space(FileLen(TorrentBasePath.Text))
        FileGet(TorrentFileload, intermediarytorrentdata)
        FileClose(TorrentFileload)
        TorrentData.Parse(intermediarytorrentdata)
        MetaDataHolder.Torrent = TorrentData
        If TorrentData.Contains("httpseeds") Then
            TorrentData.Remove("httpseeds")
        End If

        If TorrentData.Contains("resume") Then
            TorrentData.Remove("resume")
        End If
        If TorrentData.Contains("tracker_cache") Then
            TorrentData.Remove("tracker_cache")
        End If
        If TorrentData.Contains("torrent filename") Then
            TorrentData.Remove("torrent filename")
        End If

        If RemoveWebseeds.Checked = False Then
            WebSeedAddressList.Value = WebSeedAddressArray
            TorrentData.Add("httpseeds", WebSeedAddressList)
        End If
        fulltorrent = TorrentData.Bencoded()
        Kill(TorrentBasePath.Text)
        TorrentFileload = FreeFile()
        FileOpen(TorrentFileload, TorrentBasePath.Text, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
        FilePut(TorrentFileload, fulltorrent)
        FileClose(TorrentFileload)
        MsgBox("Webseed Adjusted", , "Congratulations")
    End Sub

    Private Sub LeaveApp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LeaveApp.Click
        End
    End Sub

    Private Sub WebSeedAdder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim arguments As [String]() = Environment.GetCommandLineArgs()
        Dim FileOffset As Integer
        FileOffset = Microsoft.VisualBasic.Left(arguments(0), Len(arguments(0))).LastIndexOf("\")
        LocalPath = System.IO.Path.GetFullPath(Microsoft.VisualBasic.Left(arguments(0), FileOffset)) + "\"
        If System.IO.File.Exists(LocalPath + "wsa.configure") Then
            Dim loadsettings As Integer = FreeFile()
            Dim intermediarysettingdata As String
            FileOpen(loadsettings, LocalPath + "wsa.configure", OpenMode.Binary, OpenAccess.Read, OpenShare.LockRead)
            intermediarysettingdata = Space(FileLen(LocalPath + "wsa.configure"))
            FileGet(loadsettings, intermediarysettingdata)
            FileClose(loadsettings)
            Dim ConfigData As New TorrentDictionary
            ConfigData.Parse(intermediarysettingdata)
            If ConfigData.Contains("seeds") Then
                Dim LoadArray As New ArrayList
                Dim WebSeedsToLoad As New TorrentList
                WebSeedsToLoad = ConfigData.Value("seeds")
                LoadArray = WebSeedsToLoad.Value
                For Each WebSeed As TorrentString In LoadArray
                    WebSeeds.Items.Add(WebSeed.Value)
                Next
            Else
                Dim webseed1load As New TorrentString
                Dim webseed2load As New TorrentString
                Dim webseed3load As New TorrentString
                Dim webseed4load As New TorrentString
                Dim webseed5load As New TorrentString
                If ConfigData.Contains("seed1") Then webseed1load = ConfigData.Value("seed1")
                If ConfigData.Contains("seed2") Then webseed2load = ConfigData.Value("seed2")
                If ConfigData.Contains("seed3") Then webseed3load = ConfigData.Value("seed3")
                If ConfigData.Contains("seed4") Then webseed4load = ConfigData.Value("seed4")
                If ConfigData.Contains("seed5") Then webseed5load = ConfigData.Value("seed5")
                If Not webseed1load.Value = "" Then WebSeeds.Items.Add(webseed1load.Value)
                If Not webseed2load.Value = "" Then WebSeeds.Items.Add(webseed2load.Value)
                If Not webseed3load.Value = "" Then WebSeeds.Items.Add(webseed3load.Value)
                If Not webseed4load.Value = "" Then WebSeeds.Items.Add(webseed4load.Value)
                If Not webseed5load.Value = "" Then WebSeeds.Items.Add(webseed5load.Value)
            End If
        End If
        If arguments.GetLength(0) > 1 Then
            TorrentBasePath.Text = arguments(1)
            LoadTorrent(TorrentBasePath.Text)
        End If
    End Sub

    Private Sub ExitWithSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitWithSave.Click
        Dim WebSeedAddressArray As New ArrayList
        Dim WebSeedAddressList As New TorrentList
        For Each webseedtoadd As String In WebSeeds.Items
            Dim AddToList As New TorrentString
            AddToList.Value = webseedtoadd
            WebSeedAddressArray.Add(AddToList)
        Next
        Dim savedata As New TorrentDictionary
        WebSeedAddressList.Value = WebSeedAddressArray
        savedata.Add("seeds", WebSeedAddressList)
        Dim savesettings As Integer = FreeFile()
        If System.IO.File.Exists(LocalPath + "wsa.configure") Then Kill(LocalPath + "wsa.configure")
        FileOpen(savesettings, LocalPath + "wsa.configure", OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
        FilePut(savesettings, savedata.Bencoded())
        FileClose(savesettings)
        End
    End Sub

    Private Sub RemoveWebseeds_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveWebseeds.CheckedChanged
        If RemoveWebseeds.Checked = True Then
            WebSeeds.Enabled = False
            CurrentSeed.Enabled = False
            AddToList.Enabled = False
            RemoveFromList.Enabled = False
            ReplaceInList.Enabled = False
        Else
            WebSeeds.Enabled = True
            CurrentSeed.Enabled = True
            AddToList.Enabled = True
            RemoveFromList.Enabled = True
            ReplaceInList.Enabled = True
        End If
    End Sub

    Private Sub AddToList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddToList.Click
        If IsValidHTTPAnnounce(CurrentSeed.Text) = 0 Then
            MsgBox("Error, webseed value is not valid", , "Error")
            CurrentSeed.Focus()
            CurrentSeed.SelectAll()
        Else
            WebSeeds.Items.Add(CurrentSeed.Text)
        End If
    End Sub

    Private Sub WebSeeds_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WebSeeds.SelectedIndexChanged
        CurrentSeed.Text = WebSeeds.SelectedItem
    End Sub

    Private Sub RemoveFromList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveFromList.Click
        WebSeeds.Items.RemoveAt(WebSeeds.SelectedIndex)
    End Sub

    Private Sub ReplaceInList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReplaceInList.Click
        If IsValidHTTPAnnounce(CurrentSeed.Text) = 0 Then
            MsgBox("Error, webseed value is not valid", , "Error")
            CurrentSeed.Focus()
            CurrentSeed.SelectAll()
        Else
            WebSeeds.Items.Item(WebSeeds.SelectedIndex) = CurrentSeed.Text
        End If
    End Sub
End Class
