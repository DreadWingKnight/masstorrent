'--------------------------------------------------------
' InternalRenamer - InternalRenamer.vb
' File Internal name changer code
' Harold Feit
'--------------------------------------------------------
Imports EAD.torrent

Public Class InternalRenamer
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
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TorrentToEdit As System.Windows.Forms.TextBox
    Friend WithEvents InternalName As System.Windows.Forms.TextBox
    Friend WithEvents SaveOverWrite As System.Windows.Forms.Button
    Friend WithEvents SaveNew As System.Windows.Forms.Button
    Friend WithEvents Browse As System.Windows.Forms.Button
    Friend WithEvents LeaveNow As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents SelectedFile As System.Windows.Forms.TextBox
    Friend WithEvents InternalFileNames As System.Windows.Forms.ListBox
    Friend WithEvents ChangeCurrent As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(InternalRenamer))
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.Browse = New System.Windows.Forms.Button
        Me.TorrentToEdit = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.InternalName = New System.Windows.Forms.TextBox
        Me.SaveOverWrite = New System.Windows.Forms.Button
        Me.SaveNew = New System.Windows.Forms.Button
        Me.LeaveNow = New System.Windows.Forms.Button
        Me.InternalFileNames = New System.Windows.Forms.ListBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.SelectedFile = New System.Windows.Forms.TextBox
        Me.ChangeCurrent = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Filter = "Torrent Files|*.torrent"
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.Filter = "Torrent Files|*.torrent"
        '
        'Browse
        '
        Me.Browse.Location = New System.Drawing.Point(264, 0)
        Me.Browse.Name = "Browse"
        Me.Browse.Size = New System.Drawing.Size(80, 32)
        Me.Browse.TabIndex = 0
        Me.Browse.Text = "Browse"
        '
        'TorrentToEdit
        '
        Me.TorrentToEdit.Enabled = False
        Me.TorrentToEdit.Location = New System.Drawing.Point(0, 16)
        Me.TorrentToEdit.Name = "TorrentToEdit"
        Me.TorrentToEdit.Size = New System.Drawing.Size(264, 20)
        Me.TorrentToEdit.TabIndex = 1
        Me.TorrentToEdit.Text = ""
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(264, 16)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Torrent To Edit:"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(0, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(264, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Torrent's Internal Name:"
        '
        'InternalName
        '
        Me.InternalName.Location = New System.Drawing.Point(0, 64)
        Me.InternalName.Name = "InternalName"
        Me.InternalName.Size = New System.Drawing.Size(264, 20)
        Me.InternalName.TabIndex = 4
        Me.InternalName.Text = ""
        '
        'SaveOverWrite
        '
        Me.SaveOverWrite.Location = New System.Drawing.Point(136, 328)
        Me.SaveOverWrite.Name = "SaveOverWrite"
        Me.SaveOverWrite.Size = New System.Drawing.Size(208, 24)
        Me.SaveOverWrite.TabIndex = 5
        Me.SaveOverWrite.Text = "Save changed torrent over existing"
        '
        'SaveNew
        '
        Me.SaveNew.Location = New System.Drawing.Point(136, 352)
        Me.SaveNew.Name = "SaveNew"
        Me.SaveNew.Size = New System.Drawing.Size(208, 24)
        Me.SaveNew.TabIndex = 6
        Me.SaveNew.Text = "Save changed torrent to new location"
        '
        'LeaveNow
        '
        Me.LeaveNow.Location = New System.Drawing.Point(0, 352)
        Me.LeaveNow.Name = "LeaveNow"
        Me.LeaveNow.Size = New System.Drawing.Size(136, 24)
        Me.LeaveNow.TabIndex = 7
        Me.LeaveNow.Text = "Exit"
        '
        'InternalFileNames
        '
        Me.InternalFileNames.Location = New System.Drawing.Point(0, 112)
        Me.InternalFileNames.Name = "InternalFileNames"
        Me.InternalFileNames.Size = New System.Drawing.Size(344, 134)
        Me.InternalFileNames.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(0, 96)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(256, 16)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Torrent's internal File Names:"
        '
        'SelectedFile
        '
        Me.SelectedFile.Location = New System.Drawing.Point(0, 264)
        Me.SelectedFile.Name = "SelectedFile"
        Me.SelectedFile.Size = New System.Drawing.Size(248, 20)
        Me.SelectedFile.TabIndex = 10
        Me.SelectedFile.Text = ""
        '
        'ChangeCurrent
        '
        Me.ChangeCurrent.Location = New System.Drawing.Point(240, 264)
        Me.ChangeCurrent.Name = "ChangeCurrent"
        Me.ChangeCurrent.Size = New System.Drawing.Size(104, 24)
        Me.ChangeCurrent.TabIndex = 11
        Me.ChangeCurrent.Text = "Change Current"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(0, 248)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(336, 16)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Change Selected Filename:"
        '
        'InternalRenamer
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(344, 375)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ChangeCurrent)
        Me.Controls.Add(Me.SelectedFile)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.InternalFileNames)
        Me.Controls.Add(Me.LeaveNow)
        Me.Controls.Add(Me.SaveNew)
        Me.Controls.Add(Me.SaveOverWrite)
        Me.Controls.Add(Me.InternalName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TorrentToEdit)
        Me.Controls.Add(Me.Browse)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "InternalRenamer"
        Me.Text = "Torrent Internal Name Changer"
        Me.ResumeLayout(False)

    End Sub

#End Region
    Dim TorrentData As New TorrentDictionary
    Dim TorrentDataInfo As New TorrentDictionary
    Dim TorrentInternalName As New TorrentString
    Dim MultiFileTorrent As Boolean = False
    Dim MultiFileFiles As New TorrentList

    Private Sub OpenFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        TorrentToEdit.Text = OpenFileDialog1.FileName
        If Not System.IO.File.Exists(TorrentToEdit.Text) Then
            MsgBox("ERROR: Filename is not a valid file", , "WHOOPSIE!")
            Exit Sub
        End If
        Dim intermediarytorrentdata As String
        Dim TorrentFileload As Integer
        TorrentFileload = FreeFile()
        FileOpen(TorrentFileload, TorrentToEdit.Text, OpenMode.Binary, OpenAccess.Default)
        intermediarytorrentdata = Space(FileLen(TorrentToEdit.Text))
        FileGet(TorrentFileload, intermediarytorrentdata)
        FileClose(TorrentFileload)
        TorrentData.Parse(intermediarytorrentdata)
        If TorrentData.Contains("resume") Then
            TorrentData.Remove("resume")
        End If
        If TorrentData.Contains("tracker_cache") Then
            TorrentData.Remove("tracker_cache")
        End If
        If TorrentData.Contains("torrent filename") Then
            TorrentData.Remove("torrent filename")
        End If
        TorrentDataInfo = TorrentData.Value("info")
        If TorrentDataInfo.Contains("files") Then MultiFileTorrent = True
        TorrentInternalName = TorrentDataInfo.Value("name")
        InternalName.Text = TorrentInternalName.Value
        If MultiFileTorrent Then
            InternalFileNames.Enabled = True
            SelectedFile.Enabled = True
            ChangeCurrent.Enabled = True
            Dim MultiFileFileList As New TorrentList
            MultiFileFiles = TorrentDataInfo.Value("files")
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
                InternalFileNames.Items.Add(CurrentFile)
                CurrentFile = ""
            Next
        Else
            InternalFileNames.Enabled = False
            SelectedFile.Enabled = False
            ChangeCurrent.Enabled = False
        End If
    End Sub

    Private Sub Leave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LeaveNow.Click
        End
    End Sub

    Private Sub SaveOverWrite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveOverWrite.Click
        If MsgBox("Are you sure you want to overwrite your existing torrent?" + Chr(10) + "This will replace the existing torrent AND will change the infohash.", MsgBoxStyle.YesNo, "Confirmation") = MsgBoxResult.Yes Then
            TorrentInternalName.Value = InternalName.Text
            If MultiFileTorrent Then MultiFileReEnter()
            TorrentData.Value("info") = TorrentDataInfo
            Dim fulltorrent As String = TorrentData.Bencoded
            Dim TorrentFileload As Integer
            Kill(TorrentToEdit.Text)
            TorrentFileload = FreeFile()
            FileOpen(TorrentFileload, TorrentToEdit.Text, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
            FilePut(TorrentFileload, fulltorrent)
            FileClose(TorrentFileload)
            MsgBox("Torrent File Internal name has been adjusted")
        End If
    End Sub

    Private Sub Browse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Browse.Click
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub SaveNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveNew.Click
        SaveFileDialog1.FileName = TorrentToEdit.Text
        SaveFileDialog1.ShowDialog()
        TorrentInternalName.Value = InternalName.Text
        If MultiFileTorrent Then MultiFileReEnter()
        TorrentData.Value("info") = TorrentDataInfo
        Dim fulltorrent As String = TorrentData.Bencoded
        If Not LCase(Microsoft.VisualBasic.Right(SaveFileDialog1.FileName, 8)) = ".torrent" Then
            If MsgBox("Are you sure you want to save your torrent with this name? It does not appear to be a .torrent file.", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Dim TorrentFileload As Integer
                If System.IO.File.Exists(SaveFileDialog1.FileName) Then Kill(SaveFileDialog1.FileName)
                TorrentFileload = FreeFile()
                FileOpen(TorrentFileload, SaveFileDialog1.FileName, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
                FilePut(TorrentFileload, fulltorrent)
                FileClose(TorrentFileload)
                MsgBox("Torrent File Internal name has been adjusted")
            End If
        Else
            Dim check As Boolean
            If System.IO.File.Exists(SaveFileDialog1.FileName) Then
                If MsgBox("Are you sure you want to overwrite this existing torrent file?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then check = True Else check = False
            Else
                check = True
            End If
            If check = True Then
                Dim TorrentFileload As Integer
                If System.IO.File.Exists(SaveFileDialog1.FileName) Then Kill(SaveFileDialog1.FileName)
                TorrentFileload = FreeFile()
                FileOpen(TorrentFileload, SaveFileDialog1.FileName, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
                FilePut(TorrentFileload, fulltorrent)
                FileClose(TorrentFileload)
                MsgBox("Torrent File Internal name has been adjusted")
            End If
        End If
    End Sub

    Private Sub InternalFileNames_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InternalFileNames.SelectedIndexChanged
        SelectedFile.Text = InternalFileNames.SelectedItem
    End Sub

    Private Sub ChangeCurrent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeCurrent.Click
        SelectedFile.Text = Replace(SelectedFile.Text, "/", "\")
        InternalFileNames.Items.Item(InternalFileNames.SelectedIndex) = SelectedFile.Text
    End Sub
    Sub MultiFileReEnter()
        Dim MultiFileFileList As New TorrentList
        MultiFileFiles = TorrentDataInfo.Value("files")
        MultiFileFileList.Value = MultiFileFiles.Value
        Dim PathTiers() As String
        Dim x As Integer = 0
        Dim FileNamesArray As New ArrayList

        For Each itemvalue As String In InternalFileNames.Items
            PathTiers = Split(itemvalue, "\")
            Dim PathArray As New ArrayList
            Dim FileNames As New TorrentList
            For Each tier As String In PathTiers
                If tier = "" Then GoTo notier
                Dim TierName As New TorrentString
                TierName.Value = tier
                PathArray.Add(TierName)
notier:
            Next
            FileNames.Value = PathArray
            FileNamesArray.Add(FileNames)
            Erase PathTiers
        Next

        Dim NewFileList As New TorrentList
        Dim NewFileArrayList As New ArrayList
        Dim y As Integer = 0
        For Each FileDictionary As TorrentDictionary In MultiFileFiles
            FileDictionary.Value("path") = FileNamesArray(y)
            NewFileArrayList.Add(FileDictionary)
            y = y + 1
        Next
        NewFileList.Value = NewFileArrayList
        TorrentDataInfo.Value("files") = NewFileList
    End Sub

    Private Sub InternalRenamer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim arguments As [String]() = Environment.GetCommandLineArgs()
        If arguments.GetLength(0) > 1 Then
            TorrentToEdit.Text = arguments(1)
        End If
    End Sub
End Class
