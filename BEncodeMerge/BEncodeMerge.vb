Imports EAD.Torrent

Public Class BEncodeMerge
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
    Friend WithEvents PrimaryFileOpen As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SecondaryFileOpen As System.Windows.Forms.OpenFileDialog
    Friend WithEvents PrimaryFilePath As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BrowseForPrimary As System.Windows.Forms.Button
    Friend WithEvents SecondaryFilePath As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents BrowseForSecondary As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents BrowseForOutput As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents OutputFilePath As System.Windows.Forms.TextBox
    Friend WithEvents OutputFileSave As System.Windows.Forms.SaveFileDialog
    Friend WithEvents MergeNow As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(BEncodeMerge))
        Me.PrimaryFileOpen = New System.Windows.Forms.OpenFileDialog
        Me.SecondaryFileOpen = New System.Windows.Forms.OpenFileDialog
        Me.PrimaryFilePath = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.BrowseForPrimary = New System.Windows.Forms.Button
        Me.SecondaryFilePath = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.BrowseForSecondary = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.BrowseForOutput = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.OutputFilePath = New System.Windows.Forms.TextBox
        Me.OutputFileSave = New System.Windows.Forms.SaveFileDialog
        Me.MergeNow = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'PrimaryFileOpen
        '
        '
        'SecondaryFileOpen
        '
        '
        'PrimaryFilePath
        '
        Me.PrimaryFilePath.Location = New System.Drawing.Point(0, 16)
        Me.PrimaryFilePath.Name = "PrimaryFilePath"
        Me.PrimaryFilePath.Size = New System.Drawing.Size(336, 20)
        Me.PrimaryFilePath.TabIndex = 0
        Me.PrimaryFilePath.Text = ""
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(416, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Primary File"
        '
        'BrowseForPrimary
        '
        Me.BrowseForPrimary.Location = New System.Drawing.Point(336, 16)
        Me.BrowseForPrimary.Name = "BrowseForPrimary"
        Me.BrowseForPrimary.Size = New System.Drawing.Size(80, 24)
        Me.BrowseForPrimary.TabIndex = 2
        Me.BrowseForPrimary.Text = "Browse..."
        '
        'SecondaryFilePath
        '
        Me.SecondaryFilePath.Location = New System.Drawing.Point(0, 56)
        Me.SecondaryFilePath.Name = "SecondaryFilePath"
        Me.SecondaryFilePath.Size = New System.Drawing.Size(336, 20)
        Me.SecondaryFilePath.TabIndex = 0
        Me.SecondaryFilePath.Text = ""
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(0, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(416, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Secondary File"
        '
        'BrowseForSecondary
        '
        Me.BrowseForSecondary.Location = New System.Drawing.Point(336, 56)
        Me.BrowseForSecondary.Name = "BrowseForSecondary"
        Me.BrowseForSecondary.Size = New System.Drawing.Size(80, 24)
        Me.BrowseForSecondary.TabIndex = 2
        Me.BrowseForSecondary.Text = "Browse..."
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(0, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(416, 32)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "This program will merge 2 bencoded files into one. Please note that values that a" & _
        "re present in both files will be overwritten by values in the Secondary file."
        '
        'BrowseForOutput
        '
        Me.BrowseForOutput.Location = New System.Drawing.Point(336, 136)
        Me.BrowseForOutput.Name = "BrowseForOutput"
        Me.BrowseForOutput.Size = New System.Drawing.Size(80, 24)
        Me.BrowseForOutput.TabIndex = 2
        Me.BrowseForOutput.Text = "Browse..."
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(0, 120)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(416, 16)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Output File"
        '
        'OutputFilePath
        '
        Me.OutputFilePath.Location = New System.Drawing.Point(0, 136)
        Me.OutputFilePath.Name = "OutputFilePath"
        Me.OutputFilePath.Size = New System.Drawing.Size(336, 20)
        Me.OutputFilePath.TabIndex = 0
        Me.OutputFilePath.Text = ""
        '
        'OutputFileSave
        '
        '
        'MergeNow
        '
        Me.MergeNow.Location = New System.Drawing.Point(312, 168)
        Me.MergeNow.Name = "MergeNow"
        Me.MergeNow.Size = New System.Drawing.Size(104, 24)
        Me.MergeNow.TabIndex = 4
        Me.MergeNow.Text = "Do The Merge!"
        '
        'BEncodeMerge
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(416, 197)
        Me.Controls.Add(Me.MergeNow)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.BrowseForPrimary)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PrimaryFilePath)
        Me.Controls.Add(Me.SecondaryFilePath)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.BrowseForSecondary)
        Me.Controls.Add(Me.BrowseForOutput)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.OutputFilePath)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "BEncodeMerge"
        Me.Text = "BEncode Dictionary File Merge"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub BrowseForPrimary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseForPrimary.Click
        PrimaryFileOpen.ShowDialog()
    End Sub

    Private Sub BrowseForSecondary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseForSecondary.Click
        SecondaryFileOpen.ShowDialog()
    End Sub

    Private Sub BrowseForOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseForOutput.Click
        OutputFileSave.ShowDialog()
    End Sub

    Private Sub PrimaryFileOpen_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles PrimaryFileOpen.FileOk
        PrimaryFilePath.Text = PrimaryFileOpen.FileName
    End Sub

    Private Sub SecondaryFileOpen_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SecondaryFileOpen.FileOk
        SecondaryFilePath.Text = SecondaryFileOpen.FileName
    End Sub

    Private Sub OutputFileSave_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OutputFileSave.FileOk
        OutputFilePath.Text = OutputFileSave.FileName
    End Sub

    Private Sub MergeNow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MergeNow.Click
        If Not System.IO.File.Exists(PrimaryFilePath.Text) Then
            MsgBox("ERROR: Filename is not a valid file", , "WHOOPSIE!")
            Exit Sub
        End If
        If Not System.IO.File.Exists(SecondaryFilePath.Text) Then
            MsgBox("ERROR: Filename is not a valid file", , "WHOOPSIE!")
            Exit Sub
        End If
        Dim IntermediaryPrimary As String
        Dim IntermediarySecondary As String
        Dim PrimaryBencode As New TorrentDictionary
        Dim SecondaryBencode As New TorrentDictionary
        Dim TorrentFileLoad As Integer
        TorrentFileload = FreeFile()
        FileOpen(TorrentFileLoad, PrimaryFilePath.Text, OpenMode.Binary, OpenAccess.Default)
        IntermediaryPrimary = Space(FileLen(PrimaryFilePath.Text))
        FileGet(TorrentFileLoad, IntermediaryPrimary)
        FileClose(TorrentFileload)
        PrimaryBencode.Parse(IntermediaryPrimary)
        MsgBox(Len(IntermediaryPrimary))
        TorrentFileLoad = FreeFile()
        FileOpen(TorrentFileLoad, SecondaryFilePath.Text, OpenMode.Binary, OpenAccess.Default)
        IntermediarySecondary = Space(FileLen(SecondaryFilePath.Text))
        FileGet(TorrentFileLoad, IntermediarySecondary)
        FileClose(TorrentFileLoad)
        SecondaryBencode.Parse(IntermediarySecondary)

        If System.IO.File.Exists(OutputFilePath.Text) Then Kill(OutputFilePath.Text)
        TorrentFileLoad = FreeFile()
        FileOpen(TorrentFileLoad, OutputFilePath.Text, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
        MsgBox(Len(PrimaryBencode.Bencoded))
        FilePut(TorrentFileLoad, PrimaryBencode.Bencoded)
        FileClose(TorrentFileLoad)
    End Sub
End Class
