Imports EAD.Torrent
Imports EAD.Torrent.Announces


Public Class DHTNodeAdder
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
    Friend WithEvents TorrentBasePath As System.Windows.Forms.TextBox
    Friend WithEvents TorrentBrowse As System.Windows.Forms.Button
    Friend WithEvents TorrentSelect As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ReplaceInList As System.Windows.Forms.Button
    Friend WithEvents RemoveFromList As System.Windows.Forms.Button
    Friend WithEvents AddToList As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Nodes As System.Windows.Forms.ListBox
    Friend WithEvents CurrentNode As System.Windows.Forms.TextBox
    Friend WithEvents SetNodes As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(DHTNodeAdder))
        Me.TorrentBasePath = New System.Windows.Forms.TextBox
        Me.TorrentBrowse = New System.Windows.Forms.Button
        Me.TorrentSelect = New System.Windows.Forms.OpenFileDialog
        Me.ReplaceInList = New System.Windows.Forms.Button
        Me.RemoveFromList = New System.Windows.Forms.Button
        Me.AddToList = New System.Windows.Forms.Button
        Me.CurrentNode = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Nodes = New System.Windows.Forms.ListBox
        Me.SetNodes = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'TorrentBasePath
        '
        Me.TorrentBasePath.Location = New System.Drawing.Point(2, 16)
        Me.TorrentBasePath.Name = "TorrentBasePath"
        Me.TorrentBasePath.Size = New System.Drawing.Size(288, 20)
        Me.TorrentBasePath.TabIndex = 3
        Me.TorrentBasePath.Text = ""
        '
        'TorrentBrowse
        '
        Me.TorrentBrowse.Location = New System.Drawing.Point(2, 40)
        Me.TorrentBrowse.Name = "TorrentBrowse"
        Me.TorrentBrowse.Size = New System.Drawing.Size(152, 24)
        Me.TorrentBrowse.TabIndex = 4
        Me.TorrentBrowse.Text = "Select Torrent"
        '
        'TorrentSelect
        '
        Me.TorrentSelect.Filter = "Torrent Files|*.torrent"
        '
        'ReplaceInList
        '
        Me.ReplaceInList.Location = New System.Drawing.Point(192, 184)
        Me.ReplaceInList.Name = "ReplaceInList"
        Me.ReplaceInList.Size = New System.Drawing.Size(96, 24)
        Me.ReplaceInList.TabIndex = 24
        Me.ReplaceInList.Text = "Replace"
        '
        'RemoveFromList
        '
        Me.RemoveFromList.Location = New System.Drawing.Point(96, 184)
        Me.RemoveFromList.Name = "RemoveFromList"
        Me.RemoveFromList.Size = New System.Drawing.Size(96, 24)
        Me.RemoveFromList.TabIndex = 23
        Me.RemoveFromList.Text = "Remove"
        '
        'AddToList
        '
        Me.AddToList.Location = New System.Drawing.Point(0, 184)
        Me.AddToList.Name = "AddToList"
        Me.AddToList.Size = New System.Drawing.Size(96, 24)
        Me.AddToList.TabIndex = 22
        Me.AddToList.Text = "Add"
        '
        'CurrentNode
        '
        Me.CurrentNode.Location = New System.Drawing.Point(0, 160)
        Me.CurrentNode.Name = "CurrentNode"
        Me.CurrentNode.Size = New System.Drawing.Size(288, 20)
        Me.CurrentNode.TabIndex = 21
        Me.CurrentNode.Text = ""
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(0, 72)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(272, 16)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Nodes:"
        '
        'Nodes
        '
        Me.Nodes.Location = New System.Drawing.Point(0, 88)
        Me.Nodes.Name = "Nodes"
        Me.Nodes.Size = New System.Drawing.Size(288, 69)
        Me.Nodes.TabIndex = 19
        '
        'SetNodes
        '
        Me.SetNodes.Location = New System.Drawing.Point(0, 224)
        Me.SetNodes.Name = "SetNodes"
        Me.SetNodes.Size = New System.Drawing.Size(288, 24)
        Me.SetNodes.TabIndex = 18
        Me.SetNodes.Text = "Set DHT Nodes"
        '
        'DHTNodeAdder
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Controls.Add(Me.ReplaceInList)
        Me.Controls.Add(Me.RemoveFromList)
        Me.Controls.Add(Me.AddToList)
        Me.Controls.Add(Me.CurrentNode)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Nodes)
        Me.Controls.Add(Me.SetNodes)
        Me.Controls.Add(Me.TorrentBasePath)
        Me.Controls.Add(Me.TorrentBrowse)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "DHTNodeAdder"
        Me.Text = "DHT Node Adder/Editor"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim LocalPath As String

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
        If TorrentData.Contains("nodes") Then
            For Each Seed As TorrentList In TorrentData.Value("nodes").value
                Dim SeedValues As New ArrayList
                SeedValues = Seed.Value
                Dim SeedHost As New TorrentString
                SeedHost = SeedValues(0)
                Dim SeedPort As New TorrentNumber
                SeedPort = SeedValues(1)
                'MsgBox("Values:" + Chr(13) + "Host: " + CStr(SeedHost.Value) + Chr(13) + "Port: " + CStr(SeedPort.Value))
                Nodes.Items.Add(CStr(SeedHost.Value) + ":" + CStr(SeedPort.Value))
            Next
        End If

    End Sub

    Private Sub DHTNodeAdder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim arguments As [String]() = Environment.GetCommandLineArgs()
        Dim FileOffset As Integer
        FileOffset = Microsoft.VisualBasic.Left(arguments(0), Len(arguments(0))).LastIndexOf("\")
        LocalPath = System.IO.Path.GetFullPath(Microsoft.VisualBasic.Left(arguments(0), FileOffset)) + "\"
        If arguments.GetLength(0) > 1 Then
            TorrentBasePath.Text = arguments(1)
            LoadTorrent(TorrentBasePath.Text)
        End If
    End Sub

    Private Sub Nodes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Nodes.SelectedIndexChanged
        CurrentNode.Text = Nodes.SelectedItem
    End Sub

    Private Sub RemoveFromList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveFromList.Click
        Nodes.Items.RemoveAt(Nodes.SelectedIndex)
    End Sub

    Private Sub ReplaceInList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReplaceInList.Click
        Nodes.Items.Item(Nodes.SelectedIndex) = CurrentNode.Text
    End Sub

    Private Sub AddToList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddToList.Click
        Nodes.Items.Add(CurrentNode.Text)
    End Sub

End Class
