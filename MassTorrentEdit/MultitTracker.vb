'--------------------------------------------------------
' MultiTrackerEdit - Multitracker.vb
' Torrent Editor code
' Harold Feit
' August 3rd, 2004 - Added UDP tracker support on a per-
' Tracker basis
' ToDo
' - Add support for all multitracker variants.
'--------------------------------------------------------
Imports EAD.torrent
Imports EAD.Torrent.Announces

Public Class TorrentMakeMain
    Inherits System.Windows.Forms.Form
    
    Dim TrackerList As String
    Dim UDPTrackerList As String
    Dim reannounce As String
    Dim launchstring As String
    Dim loopvalue As Integer
    Dim n As Integer
    Dim FileName As String
    Dim FilePath As String
    Dim FileOffset As Integer
    Dim FileNameLength As Integer
    Dim ne As Integer
    Dim UDPOn As Boolean
    Dim protocoltype As Integer
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
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TorrentNumberLabel As System.Windows.Forms.Label
    Friend WithEvents SourceSelect As System.Windows.Forms.Button
    Friend WithEvents TrackerGroup As System.Windows.Forms.GroupBox
    Friend WithEvents TorrentMake As System.Windows.Forms.Button
    Friend WithEvents TrackerHub As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TorrentBasePath As System.Windows.Forms.TextBox
    Friend WithEvents Tracker1 As System.Windows.Forms.TextBox
    Friend WithEvents Tracker2 As System.Windows.Forms.TextBox
    Friend WithEvents Tracker3 As System.Windows.Forms.TextBox
    Friend WithEvents Tracker4 As System.Windows.Forms.TextBox
    Friend WithEvents Tracker5 As System.Windows.Forms.TextBox
    Friend WithEvents Tracker6 As System.Windows.Forms.TextBox
    Friend WithEvents Tracker7 As System.Windows.Forms.TextBox
    Friend WithEvents Tracker8 As System.Windows.Forms.TextBox
    Friend WithEvents Tracker9 As System.Windows.Forms.TextBox
    Friend WithEvents Tracker10 As System.Windows.Forms.TextBox
    Friend WithEvents TorrentBaseFolder As System.Windows.Forms.Label
    Friend WithEvents NumTrackers As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents BaseAnnounce As System.Windows.Forms.Label
    Friend WithEvents DestinationFolder As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents CloseApp As System.Windows.Forms.Button
    Friend WithEvents UDPT1 As System.Windows.Forms.CheckBox
    Friend WithEvents UDPT2 As System.Windows.Forms.CheckBox
    Friend WithEvents UDPT3 As System.Windows.Forms.CheckBox
    Friend WithEvents UDPT4 As System.Windows.Forms.CheckBox
    Friend WithEvents UDPT5 As System.Windows.Forms.CheckBox
    Friend WithEvents UDPT6 As System.Windows.Forms.CheckBox
    Friend WithEvents UDPT7 As System.Windows.Forms.CheckBox
    Friend WithEvents UDPT8 As System.Windows.Forms.CheckBox
    Friend WithEvents UDPT9 As System.Windows.Forms.CheckBox
    Friend WithEvents UDPT10 As System.Windows.Forms.CheckBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents exitwithoutsave As System.Windows.Forms.Button
    Friend WithEvents UsesMultiScript As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(TorrentMakeMain))
        Me.NumTrackers = New System.Windows.Forms.NumericUpDown
        Me.TorrentNumberLabel = New System.Windows.Forms.Label
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.SourceSelect = New System.Windows.Forms.Button
        Me.TrackerGroup = New System.Windows.Forms.GroupBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.UDPT10 = New System.Windows.Forms.CheckBox
        Me.UDPT9 = New System.Windows.Forms.CheckBox
        Me.UDPT8 = New System.Windows.Forms.CheckBox
        Me.UDPT7 = New System.Windows.Forms.CheckBox
        Me.UDPT6 = New System.Windows.Forms.CheckBox
        Me.UDPT5 = New System.Windows.Forms.CheckBox
        Me.UDPT4 = New System.Windows.Forms.CheckBox
        Me.UDPT3 = New System.Windows.Forms.CheckBox
        Me.UDPT2 = New System.Windows.Forms.CheckBox
        Me.UDPT1 = New System.Windows.Forms.CheckBox
        Me.Tracker1 = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Tracker2 = New System.Windows.Forms.TextBox
        Me.Tracker3 = New System.Windows.Forms.TextBox
        Me.Tracker4 = New System.Windows.Forms.TextBox
        Me.Tracker5 = New System.Windows.Forms.TextBox
        Me.Tracker6 = New System.Windows.Forms.TextBox
        Me.Tracker7 = New System.Windows.Forms.TextBox
        Me.Tracker8 = New System.Windows.Forms.TextBox
        Me.Tracker9 = New System.Windows.Forms.TextBox
        Me.Tracker10 = New System.Windows.Forms.TextBox
        Me.TorrentMake = New System.Windows.Forms.Button
        Me.TrackerHub = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TorrentBasePath = New System.Windows.Forms.TextBox
        Me.TorrentBaseFolder = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.BaseAnnounce = New System.Windows.Forms.Label
        Me.DestinationFolder = New System.Windows.Forms.FolderBrowserDialog
        Me.CloseApp = New System.Windows.Forms.Button
        Me.exitwithoutsave = New System.Windows.Forms.Button
        Me.UsesMultiScript = New System.Windows.Forms.CheckBox
        CType(Me.NumTrackers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TrackerGroup.SuspendLayout()
        Me.SuspendLayout()
        '
        'NumTrackers
        '
        Me.NumTrackers.Location = New System.Drawing.Point(320, 24)
        Me.NumTrackers.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.NumTrackers.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumTrackers.Name = "NumTrackers"
        Me.NumTrackers.Size = New System.Drawing.Size(32, 20)
        Me.NumTrackers.TabIndex = 0
        Me.NumTrackers.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'TorrentNumberLabel
        '
        Me.TorrentNumberLabel.Location = New System.Drawing.Point(152, 24)
        Me.TorrentNumberLabel.Name = "TorrentNumberLabel"
        Me.TorrentNumberLabel.Size = New System.Drawing.Size(168, 16)
        Me.TorrentNumberLabel.TabIndex = 1
        Me.TorrentNumberLabel.Text = "Number of Trackers (Max 10)"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "torrent"
        Me.OpenFileDialog1.Filter = "Torrent Files|*.torrent"
        '
        'SourceSelect
        '
        Me.SourceSelect.Location = New System.Drawing.Point(0, 24)
        Me.SourceSelect.Name = "SourceSelect"
        Me.SourceSelect.Size = New System.Drawing.Size(152, 24)
        Me.SourceSelect.TabIndex = 2
        Me.SourceSelect.Text = "Select Source Torrent"
        '
        'TrackerGroup
        '
        Me.TrackerGroup.Controls.Add(Me.Label16)
        Me.TrackerGroup.Controls.Add(Me.Label15)
        Me.TrackerGroup.Controls.Add(Me.UDPT10)
        Me.TrackerGroup.Controls.Add(Me.UDPT9)
        Me.TrackerGroup.Controls.Add(Me.UDPT8)
        Me.TrackerGroup.Controls.Add(Me.UDPT7)
        Me.TrackerGroup.Controls.Add(Me.UDPT6)
        Me.TrackerGroup.Controls.Add(Me.UDPT5)
        Me.TrackerGroup.Controls.Add(Me.UDPT4)
        Me.TrackerGroup.Controls.Add(Me.UDPT3)
        Me.TrackerGroup.Controls.Add(Me.UDPT2)
        Me.TrackerGroup.Controls.Add(Me.UDPT1)
        Me.TrackerGroup.Controls.Add(Me.Tracker1)
        Me.TrackerGroup.Controls.Add(Me.Label2)
        Me.TrackerGroup.Controls.Add(Me.Label3)
        Me.TrackerGroup.Controls.Add(Me.Label4)
        Me.TrackerGroup.Controls.Add(Me.Label5)
        Me.TrackerGroup.Controls.Add(Me.Label6)
        Me.TrackerGroup.Controls.Add(Me.Label7)
        Me.TrackerGroup.Controls.Add(Me.Label8)
        Me.TrackerGroup.Controls.Add(Me.Label9)
        Me.TrackerGroup.Controls.Add(Me.Label10)
        Me.TrackerGroup.Controls.Add(Me.Label11)
        Me.TrackerGroup.Controls.Add(Me.Tracker2)
        Me.TrackerGroup.Controls.Add(Me.Tracker3)
        Me.TrackerGroup.Controls.Add(Me.Tracker4)
        Me.TrackerGroup.Controls.Add(Me.Tracker5)
        Me.TrackerGroup.Controls.Add(Me.Tracker6)
        Me.TrackerGroup.Controls.Add(Me.Tracker7)
        Me.TrackerGroup.Controls.Add(Me.Tracker8)
        Me.TrackerGroup.Controls.Add(Me.Tracker9)
        Me.TrackerGroup.Controls.Add(Me.Tracker10)
        Me.TrackerGroup.Location = New System.Drawing.Point(0, 136)
        Me.TrackerGroup.Name = "TrackerGroup"
        Me.TrackerGroup.Size = New System.Drawing.Size(392, 200)
        Me.TrackerGroup.TabIndex = 3
        Me.TrackerGroup.TabStop = False
        Me.TrackerGroup.Text = "Trackers"
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(112, 16)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(264, 16)
        Me.Label16.TabIndex = 25
        Me.Label16.Text = "Announce URI (Include http:// ):"
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(64, 16)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(40, 16)
        Me.Label15.TabIndex = 24
        Me.Label15.Text = "UDP?"
        '
        'UDPT10
        '
        Me.UDPT10.Location = New System.Drawing.Point(72, 176)
        Me.UDPT10.Name = "UDPT10"
        Me.UDPT10.Size = New System.Drawing.Size(16, 16)
        Me.UDPT10.TabIndex = 23
        '
        'UDPT9
        '
        Me.UDPT9.Location = New System.Drawing.Point(72, 160)
        Me.UDPT9.Name = "UDPT9"
        Me.UDPT9.Size = New System.Drawing.Size(16, 16)
        Me.UDPT9.TabIndex = 22
        '
        'UDPT8
        '
        Me.UDPT8.Location = New System.Drawing.Point(72, 144)
        Me.UDPT8.Name = "UDPT8"
        Me.UDPT8.Size = New System.Drawing.Size(16, 16)
        Me.UDPT8.TabIndex = 21
        '
        'UDPT7
        '
        Me.UDPT7.Location = New System.Drawing.Point(72, 128)
        Me.UDPT7.Name = "UDPT7"
        Me.UDPT7.Size = New System.Drawing.Size(16, 16)
        Me.UDPT7.TabIndex = 20
        '
        'UDPT6
        '
        Me.UDPT6.Location = New System.Drawing.Point(72, 112)
        Me.UDPT6.Name = "UDPT6"
        Me.UDPT6.Size = New System.Drawing.Size(16, 16)
        Me.UDPT6.TabIndex = 19
        '
        'UDPT5
        '
        Me.UDPT5.Location = New System.Drawing.Point(72, 96)
        Me.UDPT5.Name = "UDPT5"
        Me.UDPT5.Size = New System.Drawing.Size(16, 16)
        Me.UDPT5.TabIndex = 18
        '
        'UDPT4
        '
        Me.UDPT4.Location = New System.Drawing.Point(72, 80)
        Me.UDPT4.Name = "UDPT4"
        Me.UDPT4.Size = New System.Drawing.Size(16, 16)
        Me.UDPT4.TabIndex = 17
        '
        'UDPT3
        '
        Me.UDPT3.Location = New System.Drawing.Point(72, 64)
        Me.UDPT3.Name = "UDPT3"
        Me.UDPT3.Size = New System.Drawing.Size(16, 16)
        Me.UDPT3.TabIndex = 16
        '
        'UDPT2
        '
        Me.UDPT2.Location = New System.Drawing.Point(72, 48)
        Me.UDPT2.Name = "UDPT2"
        Me.UDPT2.Size = New System.Drawing.Size(16, 16)
        Me.UDPT2.TabIndex = 15
        '
        'UDPT1
        '
        Me.UDPT1.Location = New System.Drawing.Point(72, 32)
        Me.UDPT1.Name = "UDPT1"
        Me.UDPT1.Size = New System.Drawing.Size(16, 16)
        Me.UDPT1.TabIndex = 14
        '
        'Tracker1
        '
        Me.Tracker1.Location = New System.Drawing.Point(112, 32)
        Me.Tracker1.Name = "Tracker1"
        Me.Tracker1.Size = New System.Drawing.Size(272, 20)
        Me.Tracker1.TabIndex = 1
        Me.Tracker1.Text = ""
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Tracker 1"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(8, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 16)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Tracker 2"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(8, 64)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 16)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Tracker 3"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(8, 80)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(100, 16)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Tracker 4"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(8, 96)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 16)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Tracker 5"
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(8, 112)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(100, 16)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Tracker 6"
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(8, 128)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(100, 16)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Tracker 7"
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(8, 144)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(100, 16)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Tracker 8"
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(8, 160)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(100, 16)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Tracker 9"
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(8, 176)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(100, 16)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Tracker 10"
        '
        'Tracker2
        '
        Me.Tracker2.Enabled = False
        Me.Tracker2.Location = New System.Drawing.Point(112, 48)
        Me.Tracker2.Name = "Tracker2"
        Me.Tracker2.Size = New System.Drawing.Size(272, 20)
        Me.Tracker2.TabIndex = 1
        Me.Tracker2.Text = ""
        '
        'Tracker3
        '
        Me.Tracker3.Enabled = False
        Me.Tracker3.Location = New System.Drawing.Point(112, 64)
        Me.Tracker3.Name = "Tracker3"
        Me.Tracker3.Size = New System.Drawing.Size(272, 20)
        Me.Tracker3.TabIndex = 1
        Me.Tracker3.Text = ""
        '
        'Tracker4
        '
        Me.Tracker4.Enabled = False
        Me.Tracker4.Location = New System.Drawing.Point(112, 80)
        Me.Tracker4.Name = "Tracker4"
        Me.Tracker4.Size = New System.Drawing.Size(272, 20)
        Me.Tracker4.TabIndex = 1
        Me.Tracker4.Text = ""
        '
        'Tracker5
        '
        Me.Tracker5.Enabled = False
        Me.Tracker5.Location = New System.Drawing.Point(112, 96)
        Me.Tracker5.Name = "Tracker5"
        Me.Tracker5.Size = New System.Drawing.Size(272, 20)
        Me.Tracker5.TabIndex = 1
        Me.Tracker5.Text = ""
        '
        'Tracker6
        '
        Me.Tracker6.Enabled = False
        Me.Tracker6.Location = New System.Drawing.Point(112, 112)
        Me.Tracker6.Name = "Tracker6"
        Me.Tracker6.Size = New System.Drawing.Size(272, 20)
        Me.Tracker6.TabIndex = 1
        Me.Tracker6.Text = ""
        '
        'Tracker7
        '
        Me.Tracker7.Enabled = False
        Me.Tracker7.Location = New System.Drawing.Point(112, 128)
        Me.Tracker7.Name = "Tracker7"
        Me.Tracker7.Size = New System.Drawing.Size(272, 20)
        Me.Tracker7.TabIndex = 1
        Me.Tracker7.Text = ""
        '
        'Tracker8
        '
        Me.Tracker8.Enabled = False
        Me.Tracker8.Location = New System.Drawing.Point(112, 144)
        Me.Tracker8.Name = "Tracker8"
        Me.Tracker8.Size = New System.Drawing.Size(272, 20)
        Me.Tracker8.TabIndex = 1
        Me.Tracker8.Text = ""
        '
        'Tracker9
        '
        Me.Tracker9.Enabled = False
        Me.Tracker9.Location = New System.Drawing.Point(112, 160)
        Me.Tracker9.Name = "Tracker9"
        Me.Tracker9.Size = New System.Drawing.Size(272, 20)
        Me.Tracker9.TabIndex = 1
        Me.Tracker9.Text = ""
        '
        'Tracker10
        '
        Me.Tracker10.Enabled = False
        Me.Tracker10.Location = New System.Drawing.Point(112, 176)
        Me.Tracker10.Name = "Tracker10"
        Me.Tracker10.Size = New System.Drawing.Size(272, 20)
        Me.Tracker10.TabIndex = 1
        Me.Tracker10.Text = ""
        '
        'TorrentMake
        '
        Me.TorrentMake.Location = New System.Drawing.Point(400, 312)
        Me.TorrentMake.Name = "TorrentMake"
        Me.TorrentMake.Size = New System.Drawing.Size(208, 24)
        Me.TorrentMake.TabIndex = 5
        Me.TorrentMake.Text = "Make Torrent Batch"
        '
        'TrackerHub
        '
        Me.TrackerHub.Location = New System.Drawing.Point(112, 352)
        Me.TrackerHub.Name = "TrackerHub"
        Me.TrackerHub.Size = New System.Drawing.Size(272, 20)
        Me.TrackerHub.TabIndex = 4
        Me.TrackerHub.Text = ""
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 352)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 16)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Tracker Hub"
        '
        'TorrentBasePath
        '
        Me.TorrentBasePath.Location = New System.Drawing.Point(0, 0)
        Me.TorrentBasePath.Name = "TorrentBasePath"
        Me.TorrentBasePath.Size = New System.Drawing.Size(608, 20)
        Me.TorrentBasePath.TabIndex = 7
        Me.TorrentBasePath.Text = ""
        '
        'TorrentBaseFolder
        '
        Me.TorrentBaseFolder.Location = New System.Drawing.Point(0, 64)
        Me.TorrentBaseFolder.Name = "TorrentBaseFolder"
        Me.TorrentBaseFolder.Size = New System.Drawing.Size(608, 72)
        Me.TorrentBaseFolder.TabIndex = 8
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(0, 48)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(248, 16)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "Announce List (as will be entered in the torrent):"
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(400, 152)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(200, 16)
        Me.Label13.TabIndex = 10
        Me.Label13.Text = "Base Announce"
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(112, 376)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(216, 24)
        Me.Label14.TabIndex = 11
        Me.Label14.Text = "This will not be used unless multiple trackers are set and Script use is enabled"
        '
        'BaseAnnounce
        '
        Me.BaseAnnounce.Location = New System.Drawing.Point(400, 168)
        Me.BaseAnnounce.Name = "BaseAnnounce"
        Me.BaseAnnounce.Size = New System.Drawing.Size(200, 16)
        Me.BaseAnnounce.TabIndex = 12
        '
        'DestinationFolder
        '
        Me.DestinationFolder.Description = "Browse for destination"
        Me.DestinationFolder.RootFolder = System.Environment.SpecialFolder.MyComputer
        Me.DestinationFolder.SelectedPath = "c:\"
        '
        'CloseApp
        '
        Me.CloseApp.Location = New System.Drawing.Point(400, 344)
        Me.CloseApp.Name = "CloseApp"
        Me.CloseApp.Size = New System.Drawing.Size(208, 24)
        Me.CloseApp.TabIndex = 13
        Me.CloseApp.Text = "Exit - Save Tracker settings"
        '
        'exitwithoutsave
        '
        Me.exitwithoutsave.Location = New System.Drawing.Point(400, 376)
        Me.exitwithoutsave.Name = "exitwithoutsave"
        Me.exitwithoutsave.Size = New System.Drawing.Size(208, 24)
        Me.exitwithoutsave.TabIndex = 15
        Me.exitwithoutsave.Text = "Exit - No save of tracker Settings"
        '
        'UsesMultiScript
        '
        Me.UsesMultiScript.Location = New System.Drawing.Point(424, 24)
        Me.UsesMultiScript.Name = "UsesMultiScript"
        Me.UsesMultiScript.Size = New System.Drawing.Size(184, 16)
        Me.UsesMultiScript.TabIndex = 16
        Me.UsesMultiScript.Text = "Uses Multitracker PHP Script"
        '
        'TorrentMakeMain
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(614, 407)
        Me.Controls.Add(Me.UsesMultiScript)
        Me.Controls.Add(Me.exitwithoutsave)
        Me.Controls.Add(Me.CloseApp)
        Me.Controls.Add(Me.BaseAnnounce)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.TorrentBaseFolder)
        Me.Controls.Add(Me.TorrentBasePath)
        Me.Controls.Add(Me.TrackerHub)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TorrentMake)
        Me.Controls.Add(Me.TrackerGroup)
        Me.Controls.Add(Me.SourceSelect)
        Me.Controls.Add(Me.TorrentNumberLabel)
        Me.Controls.Add(Me.NumTrackers)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TorrentMakeMain"
        Me.Text = "Generate Multitracker Torrents"
        CType(Me.NumTrackers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TrackerGroup.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private Sub OpenFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        ' Copy the file path from the open dialog.
        TorrentBasePath.Text = OpenFileDialog1.FileName
    End Sub

    Private Sub TorrentMake_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TorrentMake.Click
        If Not System.IO.File.Exists(TorrentBasePath.Text) Then
            MsgBox("ERROR: Filename is not a valid file", , "WHOOPSIE!")
            Exit Sub
        End If

        UDPOn = False
        TrackerList = Tracker1.Text
        Dim Tracker1String As New TorrentString
        Dim UDPTier As New ArrayList
        Dim HTTPTier As New ArrayList
        Tracker1.Text = Trim(Tracker1.Text)
        Tracker2.Text = Trim(Tracker2.Text)
        Tracker3.Text = Trim(Tracker3.Text)
        Tracker4.Text = Trim(Tracker4.Text)
        Tracker5.Text = Trim(Tracker5.Text)
        Tracker6.Text = Trim(Tracker6.Text)
        Tracker7.Text = Trim(Tracker7.Text)
        Tracker8.Text = Trim(Tracker8.Text)
        Tracker9.Text = Trim(Tracker9.Text)
        Tracker10.Text = Trim(Tracker10.Text)

        protocoltype = IsValidHTTPAnnounce(Tracker1.Text)
        If protocoltype = InvalidAnnounce Then
            TorrentBaseFolder.Text = "Error: Tracker 1 is not a valid http:// or https:// URL"
            Exit Sub
        End If

        Tracker1String.Value = Tracker1.Text
        HTTPTier.Add(Tracker1String)
        If UDPT1.Checked = True Then
            Dim UDPTracker1String As New TorrentString
            UDPOn = True
            UDPTracker1String.Value = HTTPToUDPAnnounce(Tracker1.Text, protocoltype)
            UDPTrackerList = UDPTracker1String.Value
            UDPTier.Add(UDPTracker1String)
        End If

        If NumTrackers.Value > 1 Then
            If TrackerHub.Text = "" Then
                BaseAnnounce.Text = Tracker1.Text
            Else
                protocoltype = IsValidHTTPAnnounce(TrackerHub.Text)
                If protocoltype = InvalidAnnounce Then
                    TorrentBaseFolder.Text = "Error: Tracker hub is not a valid http:// or https:// URL"
                    Exit Sub
                End If
                BaseAnnounce.Text = TrackerHub.Text
            End If
        Else
            BaseAnnounce.Text = Tracker1.Text
        End If

        If NumTrackers.Value >= 2 Then
            protocoltype = IsValidHTTPAnnounce(Tracker2.Text)
            If protocoltype = InvalidAnnounce Then
                TorrentBaseFolder.Text = "Error: Tracker 2 is not a valid http:// or https:// URL"
                Exit Sub
            End If
            Dim Tracker2String As New TorrentString
            TrackerList = TrackerList + "," + Tracker2.Text
            Tracker2String.Value = Tracker2.Text
            HTTPTier.Add(Tracker2String)
            If UDPT2.Checked = True Then
                UDPOn = True
                Dim UDPTracker2String As New TorrentString
                UDPTracker2String.Value = HTTPToUDPAnnounce(Tracker2.Text, protocoltype)
                UDPTrackerList = UDPTrackerList + "," + UDPTracker2String.Value
                UDPTier.Add(UDPTracker2String)
            End If
        End If
        If NumTrackers.Value >= 3 Then
            protocoltype = IsValidHTTPAnnounce(Tracker3.Text)
            If protocoltype = InvalidAnnounce Then
                TorrentBaseFolder.Text = "Error: Tracker 3 is not a valid http:// or https:// URL"
                Exit Sub
            End If
            Dim Tracker3String As New TorrentString
            TrackerList = TrackerList + "," + Tracker3.Text
            Tracker3String.Value = Tracker3.Text
            HTTPTier.Add(Tracker3String)
            If UDPT3.Checked = True Then
                UDPOn = True
                Dim UDPTracker3String As New TorrentString
                UDPTracker3String.Value = HTTPToUDPAnnounce(Tracker3.Text, protocoltype)
                UDPTrackerList = UDPTrackerList + "," + UDPTracker3String.Value
                UDPTier.Add(UDPTracker3String)
            End If
        End If
        If NumTrackers.Value >= 4 Then
            protocoltype = IsValidHTTPAnnounce(Tracker4.Text)
            If protocoltype = InvalidAnnounce Then
                TorrentBaseFolder.Text = "Error: Tracker 4 is not a valid http:// or https:// URL"
                Exit Sub
            End If
            TrackerList = TrackerList + "," + Tracker4.Text
            Dim Tracker4String As New TorrentString
            Tracker4String.Value = Tracker4.Text
            HTTPTier.Add(Tracker4String)
            If UDPT4.Checked = True Then
                UDPOn = True
                Dim UDPTracker4String As New TorrentString
                UDPTracker4String.Value = HTTPToUDPAnnounce(Tracker4.Text, protocoltype)
                UDPTrackerList = UDPTrackerList + "," + UDPTracker4String.Value
                UDPTier.Add(UDPTracker4String)
            End If
        End If
        If NumTrackers.Value >= 5 Then
            protocoltype = IsValidHTTPAnnounce(Tracker5.Text)
            If protocoltype = InvalidAnnounce Then
                TorrentBaseFolder.Text = "Error: Tracker 5 is not a valid http:// or https:// URL"
                Exit Sub
            End If
            TrackerList = TrackerList + "," + Tracker5.Text
            Dim Tracker5String As New TorrentString
            Tracker5String.Value = Tracker5.Text
            HTTPTier.Add(Tracker5String)
            If UDPT5.Checked = True Then
                UDPOn = True
                Dim UDPTracker5String As New TorrentString
                UDPTracker5String.Value = HTTPToUDPAnnounce(Tracker5.Text, protocoltype)
                UDPTrackerList = UDPTrackerList + "," + UDPTracker5String.Value
                UDPTier.Add(UDPTracker5String)
            End If
        End If
        If NumTrackers.Value >= 6 Then
            protocoltype = IsValidHTTPAnnounce(Tracker6.Text)
            If protocoltype = InvalidAnnounce
                    TorrentBaseFolder.Text = "Error: Tracker 6 is not a valid http:// or https:// URL"
                    Exit Sub
            End If
            TrackerList = TrackerList + "," + Tracker6.Text
            Dim Tracker6String As New TorrentString
            Tracker6String.Value = Tracker6.Text
            HTTPTier.Add(Tracker6String)
            If UDPT6.Checked = True Then
                UDPOn = True
                Dim UDPTracker6String As New TorrentString
                UDPTracker6String.Value = HTTPToUDPAnnounce(Tracker6.Text, protocoltype)
                UDPTrackerList = UDPTrackerList + "," + UDPTracker6String.Value
                UDPTier.Add(UDPTracker6String)
            End If
        End If
        If NumTrackers.Value >= 7 Then
            protocoltype = IsValidHTTPAnnounce(Tracker7.Text)
            If protocoltype = InvalidAnnounce Then
                TorrentBaseFolder.Text = "Error: Tracker 7 is not a valid http:// or https:// URL"
                Exit Sub
            End If
            TrackerList = TrackerList + "," + Tracker7.Text
            Dim Tracker7String As New TorrentString
            Tracker7String.Value = Tracker7.Text
            HTTPTier.Add(Tracker7String)
            If UDPT7.Checked = True Then
                UDPOn = True
                Dim UDPTracker7String As New TorrentString
                UDPTracker7String.Value = HTTPToUDPAnnounce(Tracker7.Text, protocoltype)
                UDPTrackerList = UDPTrackerList + "," + UDPTracker7String.Value
                UDPTier.Add(UDPTracker7String)
            End If
        End If
        If NumTrackers.Value >= 8 Then
            protocoltype = IsValidHTTPAnnounce(Tracker8.Text)
            If protocoltype = InvalidAnnounce Then
                TorrentBaseFolder.Text = "Error: Tracker 8 is not a valid http:// or https:// URL"
                Exit Sub
            End If
            TrackerList = TrackerList + "," + Tracker8.Text
            Dim Tracker8String As New TorrentString
            Tracker8String.Value = Tracker8.Text
            HTTPTier.Add(Tracker8String)
            If UDPT8.Checked = True Then
                UDPOn = True
                Dim UDPTracker8String As New TorrentString
                UDPTracker8String.Value = HTTPToUDPAnnounce(Tracker8.Text, protocoltype)
                UDPTrackerList = UDPTrackerList + "," + UDPTracker8String.Value
                UDPTier.Add(UDPTracker8String)
            End If
        End If
        If NumTrackers.Value >= 9 Then
            protocoltype = IsValidHTTPAnnounce(Tracker9.Text)
            If protocoltype = InvalidAnnounce Then
                TorrentBaseFolder.Text = "Error: Tracker 9 is not a valid http:// or https:// URL"
                Exit Sub
            End If
            TrackerList = TrackerList + "," + Tracker9.Text
            Dim Tracker9String As New TorrentString
            Tracker9String.Value = Tracker9.Text
            HTTPTier.Add(Tracker9String)
            If UDPT9.Checked = True Then
                UDPOn = True
                Dim UDPTracker9String As New TorrentString
                UDPTracker9String.Value = HTTPToUDPAnnounce(Tracker9.Text, protocoltype)
                UDPTrackerList = UDPTrackerList + "," + UDPTracker9String.Value
                UDPTier.Add(UDPTracker9String)
            End If
        End If
        If NumTrackers.Value >= 10 Then
            protocoltype = IsValidHTTPAnnounce(Tracker10.Text)
            If protocoltype = InvalidAnnounce Then
                TorrentBaseFolder.Text = "Error: Tracker 10 is not a valid http:// or https:// URL"
                Exit Sub
            End If
            TrackerList = TrackerList + "," + Tracker10.Text
            Dim Tracker10String As New TorrentString
            Tracker10String.Value = Tracker10.Text
            HTTPTier.Add(Tracker10String)
            If UDPT10.Checked = True Then
                UDPOn = True
                Dim UDPTracker10String As New TorrentString
                UDPTracker10String.Value = HTTPToUDPAnnounce(Tracker10.Text, protocoltype)
                UDPTrackerList = UDPTrackerList + "," + UDPTracker10String.Value
                UDPTier.Add(UDPTracker10String)
            End If
        End If
        Dim TrackerLists As New TorrentList
        Dim TrackerTiers As New ArrayList
        Dim HTTPTrackerList As New TorrentList
        HTTPTrackerList.Value = HTTPTier
        If UDPOn = True Then
            Dim UDPTrackerLists As New TorrentList
            UDPTrackerLists.Value = UDPTier
            TorrentBaseFolder.Text = UDPTrackerList + "|" + TrackerList
            TrackerTiers.Add(UDPTrackerLists)
        Else
            TorrentBaseFolder.Text = TrackerList
        End If
        TrackerTiers.Add(HTTPTrackerList)
        Dim AnnounceListTiers As New TorrentList
        AnnounceListTiers.Value = TrackerTiers
        n = 1
        FileOffset = TorrentBasePath.Text.LastIndexOf("\")
        FileNameLength = Len(TorrentBasePath.Text) - FileOffset
        FileName = Microsoft.VisualBasic.Right(TorrentBasePath.Text, FileNameLength)
        FilePath = Microsoft.VisualBasic.Left(TorrentBasePath.Text, FileOffset)
        'Load Torrent into memory
        Dim MetaDataHolder As New TorrentMetaData
        Dim TorrentData As New TorrentDictionary
        Dim TorrentFileload As Integer
        Dim intermediarytorrentdata As String
        Dim fulltorrent As String
        'variables defined
        TorrentFileload = FreeFile()
        FileOpen(TorrentFileload, TorrentBasePath.Text, OpenMode.Binary, OpenAccess.Default)
        intermediarytorrentdata = Space(FileLen(TorrentBasePath.Text))
        FileGet(TorrentFileload, intermediarytorrentdata)
        FileClose(TorrentFileload)
        TorrentData.Parse(intermediarytorrentdata)
        MetaDataHolder.Torrent = TorrentData

        If TorrentData.Contains("resume") Then TorrentData.Remove("resume")
        If TorrentData.Contains("tracker_cache") Then TorrentData.Remove("tracker_cache")
        If TorrentData.Contains("torrent filename") Then TorrentData.Remove("torrent filename")
        
        If NumTrackers.Value = 1 Then
            If TorrentData.Contains("announce-list") Then
                TorrentData.Remove("announce-list")
            End If
            If UDPOn = True Then
                MetaDataHolder.Announce = CStr(BaseAnnounce.Text)
                TorrentData.Add("announce-list", AnnounceListTiers)
                fulltorrent = TorrentData.Bencoded()
                Kill(TorrentBasePath.Text)
                TorrentFileload = FreeFile()
                FileOpen(TorrentFileload, TorrentBasePath.Text, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
                FilePut(TorrentFileload, fulltorrent)
                FileClose(TorrentFileload)
            Else
                MetaDataHolder.Announce = CStr(BaseAnnounce.Text)
                fulltorrent = TorrentData.Bencoded()
                Kill(TorrentBasePath.Text)
                TorrentFileload = FreeFile()
                FileOpen(TorrentFileload, TorrentBasePath.Text, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
                FilePut(TorrentFileload, fulltorrent)
                FileClose(TorrentFileload)
            End If
        Else
            If UsesMultiScript.Checked = True Then
                MetaDataHolder.Announce = CStr(BaseAnnounce.Text)
                fulltorrent = TorrentData.Bencoded()
                Kill(TorrentBasePath.Text)
                TorrentFileload = FreeFile()
                FileOpen(TorrentFileload, TorrentBasePath.Text, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
                FilePut(TorrentFileload, fulltorrent)
                FileClose(TorrentFileload)
            Else
                MetaDataHolder.Announce = CStr(Tracker1.Text)
                If TorrentData.Contains("announce-list") Then
                    TorrentData.Remove("announce-list")
                End If
                TorrentData.Add("announce-list", AnnounceListTiers)
                fulltorrent = TorrentData.Bencoded()
                Kill(TorrentBasePath.Text)
                TorrentFileload = FreeFile()
                FileOpen(TorrentFileload, TorrentBasePath.Text, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
                FilePut(TorrentFileload, fulltorrent)
                FileClose(TorrentFileload)
            End If
        End If
        If NumTrackers.Value > 1 Then
            If TorrentData.Contains("announce-list") Then
                TorrentData.Remove("announce-list")
            End If
            TorrentData.Add("announce-list", AnnounceListTiers)
            If UsesMultiScript.Checked = True Then
                Do
                    If n = 1 Then
                        MetaDataHolder.Announce = CStr(Tracker1.Text)
                    ElseIf n = 2 Then
                        MetaDataHolder.Announce = CStr(Tracker2.Text)
                    ElseIf n = 3 Then
                        MetaDataHolder.Announce = CStr(Tracker3.Text)
                    ElseIf n = 4 Then
                        MetaDataHolder.Announce = CStr(Tracker4.Text)
                    ElseIf n = 5 Then
                        MetaDataHolder.Announce = CStr(Tracker5.Text)
                    ElseIf n = 6 Then
                        MetaDataHolder.Announce = CStr(Tracker6.Text)
                    ElseIf n = 7 Then
                        MetaDataHolder.Announce = CStr(Tracker7.Text)
                    ElseIf n = 8 Then
                        MetaDataHolder.Announce = CStr(Tracker8.Text)
                    ElseIf n = 9 Then
                        MetaDataHolder.Announce = CStr(Tracker9.Text)
                    ElseIf n = 10 Then
                        MetaDataHolder.Announce = CStr(Tracker10.Text)
                    End If

                    If (System.IO.Directory.Exists(FilePath + "\" + CStr(n)) = False) Then
                        MkDir(FilePath + "\" + CStr(n))
                    End If
                    fulltorrent = TorrentData.Bencoded()
                    If System.IO.File.Exists(FilePath + "\" + CStr(n) + FileName) Then
                        Kill(FilePath + "\" + CStr(n) + FileName)
                    End If
                    TorrentFileload = FreeFile()
                    FileOpen(TorrentFileload, FilePath + "\" + CStr(n) + "\" + FileName, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
                    FilePut(TorrentFileload, fulltorrent)
                    FileClose(TorrentFileload)
                    n += 1
                Loop Until n = NumTrackers.Value + 1
            End If
        End If
        MsgBox("Torrent(s) made", , "Congrats!")
    End Sub

    Private Sub NumTrackers_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumTrackers.ValueChanged
        ' Make sure the correct number of tracker fields are enabled.
        Tracker2.Enabled = False
        Tracker3.Enabled = False
        Tracker4.Enabled = False
        Tracker5.Enabled = False
        Tracker6.Enabled = False
        Tracker7.Enabled = False
        Tracker8.Enabled = False
        Tracker9.Enabled = False
        Tracker10.Enabled = False
        UDPT2.Enabled = False
        UDPT3.Enabled = False
        UDPT4.Enabled = False
        UDPT5.Enabled = False
        UDPT6.Enabled = False
        UDPT7.Enabled = False
        UDPT8.Enabled = False
        UDPT9.Enabled = False
        UDPT10.Enabled = False
        If NumTrackers.Value >= 2 Then
            Tracker2.Enabled = True
            Tracker3.Enabled = False
            Tracker4.Enabled = False
            Tracker5.Enabled = False
            Tracker6.Enabled = False
            Tracker7.Enabled = False
            Tracker8.Enabled = False
            Tracker9.Enabled = False
            Tracker10.Enabled = False
            UDPT2.Enabled = True
            UDPT3.Enabled = False
            UDPT4.Enabled = False
            UDPT5.Enabled = False
            UDPT6.Enabled = False
            UDPT7.Enabled = False
            UDPT8.Enabled = False
            UDPT9.Enabled = False
            UDPT10.Enabled = False
        End If
        If NumTrackers.Value >= 3 Then
            Tracker3.Enabled = True
            Tracker4.Enabled = False
            Tracker5.Enabled = False
            Tracker6.Enabled = False
            Tracker7.Enabled = False
            Tracker8.Enabled = False
            Tracker9.Enabled = False
            Tracker10.Enabled = False
            UDPT3.Enabled = True
            UDPT4.Enabled = False
            UDPT5.Enabled = False
            UDPT6.Enabled = False
            UDPT7.Enabled = False
            UDPT8.Enabled = False
            UDPT9.Enabled = False
            UDPT10.Enabled = False
        End If
        If NumTrackers.Value >= 4 Then
            Tracker4.Enabled = True
            Tracker5.Enabled = False
            Tracker6.Enabled = False
            Tracker7.Enabled = False
            Tracker8.Enabled = False
            Tracker9.Enabled = False
            Tracker10.Enabled = False
            UDPT4.Enabled = True
            UDPT5.Enabled = False
            UDPT6.Enabled = False
            UDPT7.Enabled = False
            UDPT8.Enabled = False
            UDPT9.Enabled = False
            UDPT10.Enabled = False
        End If
        If NumTrackers.Value >= 5 Then
            Tracker5.Enabled = True
            Tracker6.Enabled = False
            Tracker7.Enabled = False
            Tracker8.Enabled = False
            Tracker9.Enabled = False
            Tracker10.Enabled = False
            UDPT5.Enabled = True
            UDPT6.Enabled = False
            UDPT7.Enabled = False
            UDPT8.Enabled = False
            UDPT9.Enabled = False
            UDPT10.Enabled = False
        End If
        If NumTrackers.Value >= 6 Then
            Tracker6.Enabled = True
            Tracker7.Enabled = False
            Tracker8.Enabled = False
            Tracker9.Enabled = False
            Tracker10.Enabled = False
            UDPT6.Enabled = True
            UDPT7.Enabled = False
            UDPT8.Enabled = False
            UDPT9.Enabled = False
            UDPT10.Enabled = False
        End If
        If NumTrackers.Value >= 7 Then
            Tracker7.Enabled = True
            Tracker8.Enabled = False
            Tracker9.Enabled = False
            Tracker10.Enabled = False
            UDPT7.Enabled = True
            UDPT8.Enabled = False
            UDPT9.Enabled = False
            UDPT10.Enabled = False
        End If
        If NumTrackers.Value >= 8 Then
            Tracker8.Enabled = True
            Tracker9.Enabled = False
            Tracker10.Enabled = False
            UDPT8.Enabled = True
            UDPT9.Enabled = False
            UDPT10.Enabled = False
        End If
        If NumTrackers.Value >= 9 Then
            Tracker9.Enabled = True
            Tracker10.Enabled = False
            UDPT9.Enabled = True
            UDPT10.Enabled = False
        End If
        If NumTrackers.Value >= 10 Then
            Tracker10.Enabled = True
            UDPT10.Enabled = True
        End If
    End Sub

    Private Sub SourceSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SourceSelect.Click
        ' Trigger the base Torrent Selector
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub TorrentMakeMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim arguments As [String]() = Environment.GetCommandLineArgs()
        If arguments.GetLength(0) > 1 Then
            TorrentBasePath.Text = arguments(1)
        End If
        Dim FileOffset As Integer
        FileOffset = Microsoft.VisualBasic.Left(arguments(0), Len(arguments(0))).LastIndexOf("\")
        LocalPath = System.IO.Path.GetFullPath(Microsoft.VisualBasic.Left(arguments(0), FileOffset)) + "\"
        If System.IO.File.Exists(LocalPath + "mte.configure") Then
            Dim loadsettings As Integer = FreeFile()
            Dim intermediarysettingdata As String
            FileOpen(loadsettings, LocalPath + "mte.configure", OpenMode.Binary, OpenAccess.Read, OpenShare.LockRead)
            intermediarysettingdata = Space(FileLen(LocalPath + "mte.configure"))
            FileGet(loadsettings, intermediarysettingdata)
            FileClose(loadsettings)
            Dim ConfigData As New TorrentDictionary
            ConfigData.Parse(intermediarysettingdata)
            Dim tracker1load As New TorrentString
            Dim tracker2load As New TorrentString
            Dim tracker3load As New TorrentString
            Dim tracker4load As New TorrentString
            Dim tracker5load As New TorrentString
            Dim tracker6load As New TorrentString
            Dim tracker7load As New TorrentString
            Dim tracker8load As New TorrentString
            Dim tracker9load As New TorrentString
            Dim tracker10load As New TorrentString
            Dim trackerhubload As New TorrentString
            Dim numtrackersload As New TorrentNumber
            Dim tracker1udpload As New TorrentNumber
            Dim tracker2udpload As New TorrentNumber
            Dim tracker3udpload As New TorrentNumber
            Dim tracker4udpload As New TorrentNumber
            Dim tracker5udpload As New TorrentNumber
            Dim tracker6udpload As New TorrentNumber
            Dim tracker7udpload As New TorrentNumber
            Dim tracker8udpload As New TorrentNumber
            Dim tracker9udpload As New TorrentNumber
            Dim tracker10udpload As New TorrentNumber
            Dim multitrackerscript As New TorrentNumber
            tracker1load = ConfigData.Value("tracker1")
            tracker2load = ConfigData.Value("tracker2")
            tracker3load = ConfigData.Value("tracker3")
            tracker4load = ConfigData.Value("tracker4")
            tracker5load = ConfigData.Value("tracker5")
            tracker6load = ConfigData.Value("tracker6")
            tracker7load = ConfigData.Value("tracker7")
            tracker8load = ConfigData.Value("tracker8")
            tracker9load = ConfigData.Value("tracker9")
            tracker10load = ConfigData.Value("tracker10")
            trackerhubload = ConfigData.Value("trackerhub")
            numtrackersload = ConfigData.Value("numtrackers")
            tracker1udpload = ConfigData.Value("UDP1")
            tracker2udpload = ConfigData.Value("UDP2")
            tracker3udpload = ConfigData.Value("UDP3")
            tracker4udpload = ConfigData.Value("UDP4")
            tracker5udpload = ConfigData.Value("UDP5")
            tracker6udpload = ConfigData.Value("UDP6")
            tracker7udpload = ConfigData.Value("UDP7")
            tracker8udpload = ConfigData.Value("UDP8")
            tracker9udpload = ConfigData.Value("UDP9")
            tracker10udpload = ConfigData.Value("UDP10")
            multitrackerscript = ConfigData.Value("MTScript")
            Tracker1.Text = tracker1load.Value
            Tracker2.Text = tracker2load.Value
            Tracker3.Text = tracker3load.Value
            Tracker4.Text = tracker4load.Value
            Tracker5.Text = tracker5load.Value
            Tracker6.Text = tracker6load.Value
            Tracker7.Text = tracker7load.Value
            Tracker8.Text = tracker8load.Value
            Tracker9.Text = tracker9load.Value
            Tracker10.Text = tracker10load.Value
            TrackerHub.Text = trackerhubload.Value
            NumTrackers.Value = numtrackersload.Value
            UDPT1.Checked = tracker1udpload.Value
            UDPT2.Checked = tracker2udpload.Value
            UDPT3.Checked = tracker3udpload.Value
            UDPT4.Checked = tracker4udpload.Value
            UDPT5.Checked = tracker5udpload.Value
            UDPT6.Checked = tracker6udpload.Value
            UDPT7.Checked = tracker7udpload.Value
            UDPT8.Checked = tracker8udpload.Value
            UDPT9.Checked = tracker9udpload.Value
            UDPT10.Checked = tracker10udpload.Value
            UsesMultiScript.Checked = multitrackerscript.Value
        End If
        NumTrackers_ValueChanged(sender, e)
    End Sub

    Private Sub CloseApp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseApp.Click
        Tracker1.Text = Trim(Tracker1.Text)
        Tracker2.Text = Trim(Tracker2.Text)
        Tracker3.Text = Trim(Tracker3.Text)
        Tracker4.Text = Trim(Tracker4.Text)
        Tracker5.Text = Trim(Tracker5.Text)
        Tracker6.Text = Trim(Tracker6.Text)
        Tracker7.Text = Trim(Tracker7.Text)
        Tracker8.Text = Trim(Tracker8.Text)
        Tracker9.Text = Trim(Tracker9.Text)
        Tracker10.Text = Trim(Tracker10.Text)
        TrackerHub.Text = Trim(TrackerHub.Text)
        Dim tracker1save As New TorrentString
        Dim tracker2save As New TorrentString
        Dim tracker3save As New TorrentString
        Dim tracker4save As New TorrentString
        Dim tracker5save As New TorrentString
        Dim tracker6save As New TorrentString
        Dim tracker7save As New TorrentString
        Dim tracker8save As New TorrentString
        Dim tracker9save As New TorrentString
        Dim tracker10save As New TorrentString
        Dim trackerhubsave As New TorrentString
        Dim numtrackerssave As New TorrentNumber
        Dim tracker1udpsave As New TorrentNumber
        Dim tracker2udpsave As New TorrentNumber
        Dim tracker3udpsave As New TorrentNumber
        Dim tracker4udpsave As New TorrentNumber
        Dim tracker5udpsave As New TorrentNumber
        Dim tracker6udpsave As New TorrentNumber
        Dim tracker7udpsave As New TorrentNumber
        Dim tracker8udpsave As New TorrentNumber
        Dim tracker9udpsave As New TorrentNumber
        Dim tracker10udpsave As New TorrentNumber
        Dim multitrackerscriptsave As New TorrentNumber
        tracker1save.Value = Tracker1.Text
        tracker2save.Value = Tracker2.Text
        tracker3save.Value = Tracker3.Text
        tracker4save.Value = Tracker4.Text
        tracker5save.Value = Tracker5.Text
        tracker6save.Value = Tracker6.Text
        tracker7save.Value = Tracker7.Text
        tracker8save.Value = Tracker8.Text
        tracker9save.Value = Tracker9.Text
        tracker10save.Value = Tracker10.Text
        trackerhubsave.Value = TrackerHub.Text
        numtrackerssave.Value = CInt(NumTrackers.Value)
        tracker1udpsave.Value = CInt(UDPT1.Checked)
        tracker2udpsave.Value = CInt(UDPT2.Checked)
        tracker3udpsave.Value = CInt(UDPT3.Checked)
        tracker4udpsave.Value = CInt(UDPT4.Checked)
        tracker5udpsave.Value = CInt(UDPT5.Checked)
        tracker6udpsave.Value = CInt(UDPT6.Checked)
        tracker7udpsave.Value = CInt(UDPT7.Checked)
        tracker8udpsave.Value = CInt(UDPT8.Checked)
        tracker9udpsave.Value = CInt(UDPT9.Checked)
        tracker10udpsave.Value = CInt(UDPT10.Checked)
        multitrackerscriptsave.Value = CInt(UsesMultiScript.Checked)
        Dim savedata As New TorrentDictionary
        savedata.Add("tracker1", tracker1save)
        savedata.Add("tracker2", tracker2save)
        savedata.Add("tracker3", tracker3save)
        savedata.Add("tracker4", tracker4save)
        savedata.Add("tracker5", tracker5save)
        savedata.Add("tracker6", tracker6save)
        savedata.Add("tracker7", tracker7save)
        savedata.Add("tracker8", tracker8save)
        savedata.Add("tracker9", tracker9save)
        savedata.Add("tracker10", tracker10save)
        savedata.Add("trackerhub", trackerhubsave)
        savedata.Add("numtrackers", numtrackerssave)
        savedata.Add("udp1", tracker1udpsave)
        savedata.Add("udp2", tracker2udpsave)
        savedata.Add("udp3", tracker3udpsave)
        savedata.Add("udp4", tracker4udpsave)
        savedata.Add("udp5", tracker5udpsave)
        savedata.Add("udp6", tracker6udpsave)
        savedata.Add("udp7", tracker7udpsave)
        savedata.Add("udp8", tracker8udpsave)
        savedata.Add("udp9", tracker9udpsave)
        savedata.Add("udp10", tracker10udpsave)
        savedata.Add("MTScript", multitrackerscriptsave)
        Dim savesettings As Integer = FreeFile()
        If System.IO.File.Exists(LocalPath + "mte.configure") Then
            Kill(LocalPath + "mte.configure")
        End If
        FileOpen(savesettings, LocalPath + "mte.configure", OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
        FilePut(savesettings, savedata.Bencoded())
        FileClose(savesettings)
        End
    End Sub

    Private Sub exitwithoutsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles exitwithoutsave.Click
        End
    End Sub
End Class
