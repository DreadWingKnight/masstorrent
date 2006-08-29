'Copyright (c) 2006, Depthstrike Entertainment.
'Module Author - Harold Feit - dwknight@depthstrike.com
'All rights reserved.
'
'Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
'
'* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
'* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
'* Neither the name of the Depthstrike Entertainment nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
'* Use of this package or module without this license agreement present is only permitted with the express permission of Depthstrike Entertainment administration or the author of the module, either in writing or electronically with the digital PGP/GPG signature of a Depthstrike Entertainment administrator or the author of the module.
'
' THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Globalization
Imports System.IO
Imports System.Text
Imports EAD.Torrent

Public Class WebSeedDaemon
    Inherits System.Windows.Forms.Form
    Dim ActiveFiles As New TorrentDictionary
    Dim ListenThread As New Thread(AddressOf ThreadMe)
    Dim FilePath As String
    Dim RejectResponse As New TorrentDictionary
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
    Friend WithEvents Port As System.Windows.Forms.TextBox
    Friend WithEvents Start As System.Windows.Forms.Button
    Friend WithEvents LastMessage As System.Windows.Forms.TextBox
    Friend WithEvents ShutDown As System.Windows.Forms.Button
    Friend WithEvents RetryDelay As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LeaveNow As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TorrentMFLocation As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents SeedFilePath As System.Windows.Forms.TextBox
    Friend WithEvents SaveConfig As System.Windows.Forms.Button
    Friend WithEvents ReloadTorrents As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(WebSeedDaemon))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Port = New System.Windows.Forms.TextBox
        Me.Start = New System.Windows.Forms.Button
        Me.LastMessage = New System.Windows.Forms.TextBox
        Me.ShutDown = New System.Windows.Forms.Button
        Me.RetryDelay = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.LeaveNow = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.TorrentMFLocation = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.SeedFilePath = New System.Windows.Forms.TextBox
        Me.SaveConfig = New System.Windows.Forms.Button
        Me.ReloadTorrents = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(0, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Listen Port"
        '
        'Port
        '
        Me.Port.Location = New System.Drawing.Point(0, 24)
        Me.Port.Name = "Port"
        Me.Port.Size = New System.Drawing.Size(96, 20)
        Me.Port.TabIndex = 1
        Me.Port.Text = ""
        '
        'Start
        '
        Me.Start.Location = New System.Drawing.Point(168, 8)
        Me.Start.Name = "Start"
        Me.Start.Size = New System.Drawing.Size(120, 24)
        Me.Start.TabIndex = 2
        Me.Start.Text = "Start System"
        '
        'LastMessage
        '
        Me.LastMessage.Location = New System.Drawing.Point(0, 256)
        Me.LastMessage.Name = "LastMessage"
        Me.LastMessage.Size = New System.Drawing.Size(288, 20)
        Me.LastMessage.TabIndex = 3
        Me.LastMessage.Text = ""
        '
        'ShutDown
        '
        Me.ShutDown.Location = New System.Drawing.Point(168, 32)
        Me.ShutDown.Name = "ShutDown"
        Me.ShutDown.Size = New System.Drawing.Size(120, 24)
        Me.ShutDown.TabIndex = 4
        Me.ShutDown.Text = "Shut Down System"
        '
        'RetryDelay
        '
        Me.RetryDelay.Location = New System.Drawing.Point(0, 64)
        Me.RetryDelay.Name = "RetryDelay"
        Me.RetryDelay.Size = New System.Drawing.Size(96, 20)
        Me.RetryDelay.TabIndex = 5
        Me.RetryDelay.Text = ""
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(0, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 16)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Retry Delay"
        '
        'LeaveNow
        '
        Me.LeaveNow.Location = New System.Drawing.Point(168, 56)
        Me.LeaveNow.Name = "LeaveNow"
        Me.LeaveNow.Size = New System.Drawing.Size(120, 24)
        Me.LeaveNow.TabIndex = 7
        Me.LeaveNow.Text = "Shut Down and Exit"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(0, 240)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(288, 16)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Last Request:"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(0, 96)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(288, 16)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Torrent Metafile Location"
        '
        'TorrentMFLocation
        '
        Me.TorrentMFLocation.Location = New System.Drawing.Point(0, 112)
        Me.TorrentMFLocation.Name = "TorrentMFLocation"
        Me.TorrentMFLocation.Size = New System.Drawing.Size(288, 20)
        Me.TorrentMFLocation.TabIndex = 10
        Me.TorrentMFLocation.Text = ""
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(0, 136)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(288, 16)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Seeding File Location:"
        '
        'SeedFilePath
        '
        Me.SeedFilePath.Location = New System.Drawing.Point(0, 152)
        Me.SeedFilePath.Name = "SeedFilePath"
        Me.SeedFilePath.Size = New System.Drawing.Size(288, 20)
        Me.SeedFilePath.TabIndex = 12
        Me.SeedFilePath.Text = ""
        '
        'SaveConfig
        '
        Me.SaveConfig.Location = New System.Drawing.Point(168, 176)
        Me.SaveConfig.Name = "SaveConfig"
        Me.SaveConfig.Size = New System.Drawing.Size(120, 24)
        Me.SaveConfig.TabIndex = 13
        Me.SaveConfig.Text = "Save Configuration"
        '
        'ReloadTorrents
        '
        Me.ReloadTorrents.Location = New System.Drawing.Point(0, 176)
        Me.ReloadTorrents.Name = "ReloadTorrents"
        Me.ReloadTorrents.Size = New System.Drawing.Size(120, 24)
        Me.ReloadTorrents.TabIndex = 14
        Me.ReloadTorrents.Text = "Reload Torrents"
        '
        'WebSeedDaemon
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(288, 273)
        Me.Controls.Add(Me.ReloadTorrents)
        Me.Controls.Add(Me.SaveConfig)
        Me.Controls.Add(Me.SeedFilePath)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TorrentMFLocation)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.LeaveNow)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.RetryDelay)
        Me.Controls.Add(Me.ShutDown)
        Me.Controls.Add(Me.LastMessage)
        Me.Controls.Add(Me.Start)
        Me.Controls.Add(Me.Port)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "WebSeedDaemon"
        Me.Text = "WebSeed Daemon"
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private Sub ThreadMe()
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture
        Dim objTcpl As New TcpListener(IPAddress.Any, CInt(Port.Text))
        Try
            objTcpl.Start()
        Catch ex As Exception
            MsgBox("Could not start, listen port error")
            Exit Sub
        End Try

        While (True)
            Dim objTcpc As TcpClient = objTcpl.AcceptTcpClient
            Dim objNs As NetworkStream = objTcpc.GetStream
            Dim bytClientRequest(objTcpc.ReceiveBufferSize) As Byte
            objNs.Read(bytClientRequest, 0, objTcpc.ReceiveBufferSize)
            Dim strClientRequest As String = System.Text.Encoding.Default.GetString(bytClientRequest)
            Dim strRequestedDocument As String = strClientRequest.Split(" ")(1)
            LastMessage.Text = strRequestedDocument
            Application.DoEvents()
            Dim sendresponse As String = ""
            sendresponse = sendresponse + "HTTP/1.0 200 OK" + Chr(13) + Chr(10)
            sendresponse = sendresponse + "Content-type: text/plain" + Chr(13) + Chr(10)
            Dim responseToGet() As String = Split(strRequestedDocument, "?")
            Dim strResponseParams() As String
            If responseToGet.Length > 1 Then strResponseParams = Split(responseToGet(1), "&")
            If responseToGet(0) = "/seed" Then
                Dim currentrequest As New WebSeedRequest
                For Each value As String In strResponseParams
                    Dim subvalue() As String = Split(value, "=")
                    If subvalue(0) = "info_hash" Then currentrequest.infohash = subvalue(1)
                    If subvalue(0) = "piece" Then currentrequest.piece = subvalue(1)
                    If subvalue(0) = "ranges" Then currentrequest.ranges = subvalue(1)
                Next
                Dim WebSeedPiece() As Byte
                Dim haswebseed As Boolean = GetWebSeedPiece(currentrequest.infohash, currentrequest.piece, WebSeedPiece, currentrequest.ranges)
                MsgBox(haswebseed)
                If haswebseed = True Then
                    MsgBox("completing headers")
                    sendresponse = sendresponse + "Content-length: " + CStr(WebSeedPiece.Length) + Chr(13) + Chr(10)
                    sendresponse = sendresponse + Chr(13) + Chr(10)
                    Dim response As Byte() = System.Text.Encoding.Default.GetBytes(sendresponse)
                    objNs.Write(response, 0, response.Length)
                    MsgBox("headers sent")
                    objNs.Write(WebSeedPiece, response.Length, WebSeedPiece.Length)
                    objTcpc.Close()
                    objNs.Close()
                    MsgBox("piece sent")
                    Exit While
                Else
                    sendresponse = "HTTP/1.0 503 Service Temporarily Unavailable" + Chr(13) + Chr(10)
                    sendresponse = sendresponse + "Content-type: text/plain" + Chr(13) + Chr(10)
                    sendresponse = sendresponse + Chr(13) + Chr(10)
                    sendresponse = sendresponse + RetryDelay.Text
                End If
            ElseIf responseToGet(0) = "/announce" Then
                sendresponse = sendresponse + Chr(13) + Chr(10)
                sendresponse = sendresponse + RejectResponse.Bencoded
            Else
                sendresponse = sendresponse + Chr(13) + Chr(10)
                sendresponse = sendresponse + "Returned response for: " + strRequestedDocument
            End If
            Dim cleanresponse As Byte() = System.Text.Encoding.Default.GetBytes(sendresponse)
            objNs.Write(cleanresponse, 0, cleanresponse.Length)
            objTcpc.Close()
            objNs.Close()
        End While
    End Sub

    Private Function GetWebSeedPiece(ByVal infohash As String, ByVal piecenumber As Long, ByRef output() As Byte, Optional ByVal byteranges As String = "") As Boolean
        Dim currentinfohash As String = System.Web.HttpUtility.UrlDecode(infohash, System.Text.Encoding.Default)
        If ActiveFiles.Contains(currentinfohash) Then
            Dim LoadTorrentInfo As New TorrentDictionary
            LoadTorrentInfo = ActiveFiles.Value(currentinfohash)
            Dim LoadTorrentSubInfo As New TorrentDictionary
            LoadTorrentSubInfo = LoadTorrentInfo.Value("info")
            Dim LoadTorrentMeta As New TorrentMetaData
            LoadTorrentMeta.Torrent = LoadTorrentInfo
            If LoadTorrentMeta.SingleFile Then
                Dim FileBEToLoad As New TorrentString
                FileBEToLoad = LoadTorrentSubInfo.Value("name")
                Dim FileToLoad As String = FileBEToLoad.Value
                Dim filebepiecesize As New TorrentNumber
                filebepiecesize = LoadTorrentSubInfo.Value("piece length")
                Dim FilePieceSize As Integer = filebepiecesize.Value
                If System.IO.File.Exists(FilePath + FileToLoad) Then
                    Dim outbytes() As Byte
                    Dim OpenFile As New System.IO.FileStream(FilePath + FileToLoad, FileMode.Open, FileAccess.Read)
                    OpenFile.Seek(FilePieceSize * piecenumber, SeekOrigin.Begin)
                    OpenFile.Read(outbytes, 0, FilePieceSize)
                    OpenFile.Close()
                    output = outbytes
                    MsgBox("piece returned")
                    GetWebSeedPiece = True
                Else
                    GetWebSeedPiece = False
                End If
            Else
                GetWebSeedPiece = False
            End If
        Else
        End If
    End Function

    Private Sub Start_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Start.Click
        If System.IO.Directory.Exists(SeedFilePath.Text) Then
            If Microsoft.VisualBasic.Strings.Right(SeedFilePath.Text, 1) = "\" Then FilePath = SeedFilePath.Text Else FilePath = SeedFilePath.Text + "\"
            Start.Enabled = False
            ListenThread.Start()
        Else
            MsgBox("Error: File source path does not exist")
        End If
    End Sub

    Private Sub ShutDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShutDown.Click
        ListenThread.Abort()
        ListenThread = New System.Threading.Thread(AddressOf ThreadMe)
        Start.Enabled = True
    End Sub

    Private Sub WebSeedDaemon_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim RejectReason As New TorrentString
        RejectReason.Value = "This is not a tracker, This is a WebSeed server."
        RejectResponse.Value("failure reason") = RejectReason
        Dim arguments As [String]() = Environment.GetCommandLineArgs()
        Dim FileOffset As Integer
        FileOffset = Microsoft.VisualBasic.Left(arguments(0), Len(arguments(0))).LastIndexOf("\")
        LocalPath = System.IO.Path.GetFullPath(Microsoft.VisualBasic.Left(arguments(0), FileOffset)) + "\"
        If System.IO.File.Exists(LocalPath + "webseeder.configure") Then
            Dim loadsettings As Integer = FreeFile()
            Dim intermediarysettingdata As String
            FileOpen(loadsettings, LocalPath + "webseeder.configure", OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
            intermediarysettingdata = Space(FileLen(LocalPath + "webseeder.configure"))
            FileGet(loadsettings, intermediarysettingdata)
            FileClose(loadsettings)
            Dim ConfigData As New TorrentDictionary
            ConfigData.Parse(intermediarysettingdata)
            If ConfigData.Contains("listenport") Then Port.Text = CStr(ConfigData.Value("listenport").value)
            If ConfigData.Contains("retrydelay") Then RetryDelay.Text = CStr(ConfigData.Value("retrydelay").value)
            If ConfigData.Contains("metafile") Then TorrentMFLocation.Text = ConfigData.Value("metafile").value
            If ConfigData.Contains("seedfile") Then SeedFilePath.Text = ConfigData.Value("seedfile").value
        End If
        If System.IO.File.Exists(LocalPath + "webseeder.data") Then
            Dim loaddata As Integer = FreeFile()
            Dim torrentdata As String = Space(FileLen(LocalPath + "webseeder.data"))
            FileOpen(loaddata, LocalPath + "webseeder.data", OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
            FileGet(loaddata, torrentdata)
            FileClose(loaddata)
            ActiveFiles.Parse(torrentdata)
        End If
    End Sub

    Private Sub LeaveNow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LeaveNow.Click
        Call ShutDown_Click(sender, e)
        End
    End Sub

    Private Sub SaveConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveConfig.Click
        Dim ConfigData As New TorrentDictionary
        Dim ConfigListenPort As New TorrentNumber
        Dim ConfigRetryDelay As New TorrentNumber
        Dim ConfigMetaFile As New TorrentString
        Dim ConfigSeedFile As New TorrentString
        ConfigListenPort.Value = CInt(Port.Text)
        ConfigRetryDelay.Value = CInt(RetryDelay.Text)
        ConfigMetaFile.Value = TorrentMFLocation.Text
        ConfigSeedFile.Value = SeedFilePath.Text
        ConfigData.Value("listenport") = ConfigListenPort
        ConfigData.Value("retrydelay") = ConfigRetryDelay
        ConfigData.Value("metafile") = ConfigMetaFile
        ConfigData.Value("seedfile") = ConfigSeedFile
        If System.IO.File.Exists(LocalPath + "webseeder.configure") Then Kill(LocalPath + "webseeder.configure")
        Dim savesettings As Integer = FreeFile()
        FileOpen(savesettings, LocalPath + "webseeder.configure", OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
        FilePut(savesettings, ConfigData.Bencoded())
        FileClose(savesettings)
        If System.IO.File.Exists(LocalPath + "webseeder.data") Then Kill(LocalPath + "webseeder.data")
        savesettings = FreeFile()
        FileOpen(savesettings, LocalPath + "webseeder.data", OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
        FilePut(savesettings, ActiveFiles.Bencoded)
        FileClose(savesettings)
    End Sub

    Private Sub ReloadTorrents_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReloadTorrents.Click
        ActiveFiles = New TorrentDictionary
        Dim LoadTorrent As Integer
        Dim TorrentRaw As String
        Dim sha1 As New System.Security.Cryptography.SHA1CryptoServiceProvider
        Dim BTIHConvert As New EAD.Conversion.HashChanger
        Dim TorrentsToProcess() As String = System.IO.Directory.GetFiles(TorrentMFLocation.Text, "*.torrent")
        For Each torrenthandle As String In TorrentsToProcess
            If LCase(Microsoft.VisualBasic.Strings.Right(torrenthandle, 7)) = "torrent" Then
                sha1 = New System.Security.Cryptography.SHA1CryptoServiceProvider
                BTIHConvert = New EAD.Conversion.HashChanger
                LoadTorrent = FreeFile()
                TorrentRaw = Space(FileLen(torrenthandle))
                FileOpen(LoadTorrent, torrenthandle, OpenMode.Binary)
                FileGet(LoadTorrent, TorrentRaw)
                FileClose(LoadTorrent)
                Dim TorrentToGetInfoHash As New TorrentDictionary
                TorrentToGetInfoHash.Parse(TorrentRaw)
                Dim TorrentInfoToHash As TorrentDictionary = TorrentToGetInfoHash.Value("info")
                BTIHConvert.bytehash = sha1.ComputeHash(System.Text.Encoding.Default.GetBytes(TorrentInfoToHash.Bencoded))
                MsgBox(BTIHConvert.rawhash)
                ActiveFiles.Value(BTIHConvert.rawhash) = TorrentToGetInfoHash
            End If
        Next
    End Sub
End Class

Public Class WebSeedRequest
    Public infohash As String
    Public piece As String
    Public ranges As String
End Class
