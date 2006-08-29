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

Public Class G2SourceGUI
    Inherits System.Windows.Forms.Form
    Dim ListenThread As New Thread(AddressOf ThreadMe)
    Public Shared LocalPath As String
    Public Shared FileSources As New TorrentDictionary
    Public Shared SourceForgeMirrors As New ArrayList
    Public Shared PHPNetMirrors As New ArrayList
    Public Shared ActiveThread As Boolean
    Public WithEvents ClientRecieved As SourceProcessor

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
    Friend WithEvents Port As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents StartMe As System.Windows.Forms.Button
    Friend WithEvents StopMe As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Leave As System.Windows.Forms.Button
    Friend WithEvents TrayIcon As System.Windows.Forms.NotifyIcon
    Friend WithEvents ToTray As System.Windows.Forms.Button
    Friend WithEvents ReloadDatabase As System.Windows.Forms.Timer
    Friend WithEvents ReloadCycle As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TotalRequest As System.Windows.Forms.TextBox
    Friend WithEvents LastResponse As System.Windows.Forms.TextBox
    Friend WithEvents Nickname As System.Windows.Forms.TextBox
    Friend WithEvents ActiveThreads As System.Windows.Forms.ProgressBar
    Friend WithEvents Logging As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(G2SourceGUI))
        Me.Port = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.StartMe = New System.Windows.Forms.Button
        Me.StopMe = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Leave = New System.Windows.Forms.Button
        Me.TrayIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ToTray = New System.Windows.Forms.Button
        Me.ReloadDatabase = New System.Windows.Forms.Timer(Me.components)
        Me.ReloadCycle = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TotalRequest = New System.Windows.Forms.TextBox
        Me.LastResponse = New System.Windows.Forms.TextBox
        Me.Nickname = New System.Windows.Forms.TextBox
        Me.ActiveThreads = New System.Windows.Forms.ProgressBar
        Me.Logging = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'Port
        '
        Me.Port.Location = New System.Drawing.Point(504, 280)
        Me.Port.Name = "Port"
        Me.Port.Size = New System.Drawing.Size(80, 20)
        Me.Port.TabIndex = 0
        Me.Port.Text = ""
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(504, 264)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Listen Port"
        '
        'StartMe
        '
        Me.StartMe.Location = New System.Drawing.Point(488, 328)
        Me.StartMe.Name = "StartMe"
        Me.StartMe.Size = New System.Drawing.Size(48, 24)
        Me.StartMe.TabIndex = 5
        Me.StartMe.Text = "Start"
        '
        'StopMe
        '
        Me.StopMe.Location = New System.Drawing.Point(536, 328)
        Me.StopMe.Name = "StopMe"
        Me.StopMe.Size = New System.Drawing.Size(48, 24)
        Me.StopMe.TabIndex = 4
        Me.StopMe.Text = "Stop"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(584, 16)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Last Request"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(0, 128)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(584, 16)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Last Response"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(0, 264)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(504, 16)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Display Nickname"
        '
        'Leave
        '
        Me.Leave.Location = New System.Drawing.Point(432, 328)
        Me.Leave.Name = "Leave"
        Me.Leave.Size = New System.Drawing.Size(56, 24)
        Me.Leave.TabIndex = 6
        Me.Leave.Text = "Exit"
        '
        'TrayIcon
        '
        Me.TrayIcon.Icon = CType(resources.GetObject("TrayIcon.Icon"), System.Drawing.Icon)
        Me.TrayIcon.Text = "EAD G2 Source Supplier"
        Me.TrayIcon.Visible = True
        '
        'ToTray
        '
        Me.ToTray.Location = New System.Drawing.Point(0, 328)
        Me.ToTray.Name = "ToTray"
        Me.ToTray.Size = New System.Drawing.Size(96, 24)
        Me.ToTray.TabIndex = 9
        Me.ToTray.Text = "Send to Tray"
        '
        'ReloadDatabase
        '
        '
        'ReloadCycle
        '
        Me.ReloadCycle.Location = New System.Drawing.Point(464, 304)
        Me.ReloadCycle.Name = "ReloadCycle"
        Me.ReloadCycle.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ReloadCycle.Size = New System.Drawing.Size(120, 20)
        Me.ReloadCycle.TabIndex = 3
        Me.ReloadCycle.Text = "30"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(288, 304)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(176, 16)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Database Reload Time (Minutes)"
        '
        'TotalRequest
        '
        Me.TotalRequest.Location = New System.Drawing.Point(0, 16)
        Me.TotalRequest.Multiline = True
        Me.TotalRequest.Name = "TotalRequest"
        Me.TotalRequest.Size = New System.Drawing.Size(584, 104)
        Me.TotalRequest.TabIndex = 13
        Me.TotalRequest.Text = ""
        '
        'LastResponse
        '
        Me.LastResponse.Location = New System.Drawing.Point(0, 144)
        Me.LastResponse.Multiline = True
        Me.LastResponse.Name = "LastResponse"
        Me.LastResponse.Size = New System.Drawing.Size(584, 104)
        Me.LastResponse.TabIndex = 13
        Me.LastResponse.Text = ""
        '
        'Nickname
        '
        Me.Nickname.Location = New System.Drawing.Point(0, 280)
        Me.Nickname.Name = "Nickname"
        Me.Nickname.Size = New System.Drawing.Size(504, 20)
        Me.Nickname.TabIndex = 14
        Me.Nickname.Text = ""
        '
        'ActiveThreads
        '
        Me.ActiveThreads.Location = New System.Drawing.Point(96, 328)
        Me.ActiveThreads.Name = "ActiveThreads"
        Me.ActiveThreads.Size = New System.Drawing.Size(328, 24)
        Me.ActiveThreads.TabIndex = 15
        '
        'Logging
        '
        Me.Logging.Location = New System.Drawing.Point(120, 304)
        Me.Logging.Name = "Logging"
        Me.Logging.Size = New System.Drawing.Size(160, 16)
        Me.Logging.TabIndex = 16
        Me.Logging.Text = "Log To logs\"
        '
        'G2SourceGUI
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(584, 357)
        Me.ControlBox = False
        Me.Controls.Add(Me.Logging)
        Me.Controls.Add(Me.ActiveThreads)
        Me.Controls.Add(Me.Nickname)
        Me.Controls.Add(Me.TotalRequest)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ReloadCycle)
        Me.Controls.Add(Me.ToTray)
        Me.Controls.Add(Me.Leave)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.StartMe)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Port)
        Me.Controls.Add(Me.StopMe)
        Me.Controls.Add(Me.LastResponse)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "G2SourceGUI"
        Me.Text = "EAD G2 Source Supplier"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Sub RequestProcessed(ByVal RequestString As String) Handles ClientRecieved.RequestProcessed
        TotalRequest.Text = RequestString
    End Sub
    Sub ResponseProcessed(ByVal ResponseString As String) Handles ClientRecieved.ResponseProcessed
        LastResponse.Text = ResponseString
    End Sub
    Sub ThreadCount(ByVal IncOrDec As Integer) Handles ClientRecieved.ThreadCount
        ActiveThreads.Value = ActiveThreads.Value + IncOrDec
    End Sub

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
            If ActiveThread = False Then
                objTcpl.Stop()
                Exit While
            End If
            Application.DoEvents()
            ClientRecieved = New SourceProcessor
            ClientRecieved.Nickname = Nickname.Text
            ClientRecieved.Logged = Logging.Checked
            ClientRecieved.objtcpc = objTcpl.AcceptTcpClient
            TotalRequest.Text = "request logged at: " + CStr(Now.Ticks)
            Dim ClientThread As New Thread(AddressOf ClientRecieved.ProcessSource)
            ClientThread.Start()
        End While
        StopMe.Enabled = False
        StartMe.Enabled = True
        Port.Enabled = True
        Nickname.Enabled = True
        ReloadDatabase.Enabled = False
        Logging.Enabled = True
    End Sub

    Public Shared Function GetSources(ByVal FileNameToGet As String, ByRef pass As Boolean) As FileSet
        Dim FileParse As New TorrentDictionary
        pass = False
        If FileSources.Contains(FileNameToGet) Then
            FileParse = FileSources.Value(FileNameToGet)
            If Not FileParse.Contains("sources") Then
                pass = False
            Else
                GetSources.Sources = FileParse.Value("sources").value
                pass = True
            End If
            GetSources.FileSize = FileParse.Value("size").value
            If FileParse.Contains("sf") Then
                GetSources.HasSourceForgeSource = True
                GetSources.SourceForgeProject = FileParse.Value("sf").value
                pass = True
            End If
            If FileParse.Contains("ed2k") Then GetSources.ED2K = FileParse.Value("ed2k").value
            If FileParse.Contains("sha1") Then GetSources.SHA1 = FileParse.Value("sha1").value
            If FileParse.Contains("tthroot") Then GetSources.TTHRoot = FileParse.Value("tthroot").value
        End If
    End Function

    Private Sub loadsources()
        If System.IO.File.Exists(LocalPath + "sources.data") Then
            Dim loaddata As Integer = FreeFile()
            Dim sources As String = Space(FileLen(LocalPath + "sources.data"))
            FileOpen(loaddata, LocalPath + "sources.data", OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
            FileGet(loaddata, sources)
            FileClose(loaddata)
            FileSources.Parse(sources)
        End If
    End Sub

    Private Sub StartMe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartMe.Click
        ActiveThread = True
        StopMe.Enabled = True
        StartMe.Enabled = False
        Port.Enabled = False
        Nickname.Enabled = False
        ReloadDatabase.Interval = CInt(ReloadCycle.Text) * 6000
        ReloadDatabase.Enabled = True
        ListenThread.Start()
        Logging.Enabled = False
    End Sub

    Private Sub StopMe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopMe.Click
        ActiveThread = False
        ListenThread.Abort()
        ListenThread = New Thread(AddressOf ThreadMe)
    End Sub

    Private Sub G2SourceGUI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim arguments As [String]() = Environment.GetCommandLineArgs()
        LocalPath = System.IO.Path.GetDirectoryName(arguments(0))
        StopMe.Enabled = False
        StartMe.Enabled = True
        Port.Enabled = True
        Nickname.Enabled = True
        Call AddSourceForgeMirrors()
        Call AddPHPNetMirrors()
    End Sub

    Private Sub AddPHPNetMirrors()
        PHPNetMirrors.Add("http://ca.php.net/distributions/")
        PHPNetMirrors.Add("http://ca3.php.net/distributions/")
        PHPNetMirrors.Add("http://ar.php.net/distributions/")
        PHPNetMirrors.Add("http://ar2.php.net/distributions/")
        PHPNetMirrors.Add("http://am.php.net/distributions/")
        PHPNetMirrors.Add("http://au.php.net/distributions/")
        PHPNetMirrors.Add("http://au2.php.net/distributions/")
        PHPNetMirrors.Add("http://at.php.net/distributions/")
        PHPNetMirrors.Add("http://at2.php.net/distributions/")
        PHPNetMirrors.Add("http://be.php.net/distributions/")
        PHPNetMirrors.Add("http://be2.php.net/distributions/")
        PHPNetMirrors.Add("http://bo.php.net/distributions/")
        PHPNetMirrors.Add("http://br.php.net/distributions/")
        PHPNetMirrors.Add("http://br2.php.net/distributions/")
        PHPNetMirrors.Add("http://bg.php.net/distributions/")
        PHPNetMirrors.Add("http://bg2.php.net/distributions/")
        PHPNetMirrors.Add("http://cl.php.net/distributions/")
        PHPNetMirrors.Add("http://cl2.php.net/distributions/")
        PHPNetMirrors.Add("http://cn.php.net/distributions/")
        PHPNetMirrors.Add("http://cn2.php.net/distributions/")
        PHPNetMirrors.Add("http://cr.php.net/distributions/")
        PHPNetMirrors.Add("http://cz.php.net/distributions/")
        PHPNetMirrors.Add("http://cz2.php.net/distributions/")
        PHPNetMirrors.Add("http://dk.php.net/distributions/")
        PHPNetMirrors.Add("http://dk2.php.net/distributions/")
        PHPNetMirrors.Add("http://ee.php.net/distributions/")
        PHPNetMirrors.Add("http://fi.php.net/distributions/")
        PHPNetMirrors.Add("http://fi2.php.net/distributions/")
        PHPNetMirrors.Add("http://fr.php.net/distributions/")
        PHPNetMirrors.Add("http://fr2.php.net/distributions/")
        PHPNetMirrors.Add("http://fr3.php.net/distributions/")
        PHPNetMirrors.Add("http://de.php.net/distributions/")
        PHPNetMirrors.Add("http://de2.php.net/distributions/")
        PHPNetMirrors.Add("http://de3.php.net/distributions/")
        PHPNetMirrors.Add("http://gr.php.net/distributions/")
        PHPNetMirrors.Add("http://gr2.php.net/distributions/")
        PHPNetMirrors.Add("http://hk.php.net/distributions/")
        PHPNetMirrors.Add("http://hk2.php.net/distributions/")
        PHPNetMirrors.Add("http://hu.php.net/distributions/")
        PHPNetMirrors.Add("http://hu2.php.net/distributions/")
        PHPNetMirrors.Add("http://is.php.net/distributions/")
        PHPNetMirrors.Add("http://is2.php.net/distributions/")
        PHPNetMirrors.Add("http://in.php.net/distributions/")
        PHPNetMirrors.Add("http://in2.php.net/distributions/")
        PHPNetMirrors.Add("http://id.php.net/distributions/")
        PHPNetMirrors.Add("http://id2.php.net/distributions/")
        PHPNetMirrors.Add("http://ir.php.net/distributions/")
        PHPNetMirrors.Add("http://ie.php.net/distributions/")
        PHPNetMirrors.Add("http://ie2.php.net/distributions/")
        PHPNetMirrors.Add("http://il.php.net/distributions/")
        PHPNetMirrors.Add("http://il2.php.net/distributions/")
        PHPNetMirrors.Add("http://it.php.net/distributions/")
        PHPNetMirrors.Add("http://it2.php.net/distributions/")
        PHPNetMirrors.Add("http://jp2.php.net/distributions/")
        PHPNetMirrors.Add("http://lv.php.net/distributions/")
        PHPNetMirrors.Add("http://lv2.php.net/distributions/")
        PHPNetMirrors.Add("http://li.php.net/distributions/")
        PHPNetMirrors.Add("http://li2.php.net/distributions/")
        PHPNetMirrors.Add("http://lt.php.net/distributions/")
        PHPNetMirrors.Add("http://lu.php.net/distributions/")
        PHPNetMirrors.Add("http://lu2.php.net/distributions/")
        PHPNetMirrors.Add("http://my2.php.net/distributions/")
        PHPNetMirrors.Add("http://mx.php.net/distributions/")
        PHPNetMirrors.Add("http://mx2.php.net/distributions/")
        PHPNetMirrors.Add("http://nl2.php.net/distributions/")
        PHPNetMirrors.Add("http://nl3.php.net/distributions/")
        PHPNetMirrors.Add("http://nz.php.net/distributions/")
        PHPNetMirrors.Add("http://nz2.php.net/distributions/")
        PHPNetMirrors.Add("http://no.php.net/distributions/")
        PHPNetMirrors.Add("http://no2.php.net/distributions/")
        PHPNetMirrors.Add("http://ph.php.net/distributions/")
        PHPNetMirrors.Add("http://pl.php.net/distributions/")
        PHPNetMirrors.Add("http://pt.php.net/distributions/")
        PHPNetMirrors.Add("http://kr.php.net/distributions/")
        PHPNetMirrors.Add("http://kr2.php.net/distributions/")
        PHPNetMirrors.Add("http://ro.php.net/distributions/")
        PHPNetMirrors.Add("http://ru.php.net/distributions/")
        PHPNetMirrors.Add("http://ru2.php.net/distributions/")
        PHPNetMirrors.Add("http://ru3.php.net/distributions/")
        PHPNetMirrors.Add("http://yu.php.net/distributions/")
        PHPNetMirrors.Add("http://yu2.php.net/distributions/")
        PHPNetMirrors.Add("http://sg.php.net/distributions/")
        PHPNetMirrors.Add("http://sg2.php.net/distributions/")
        PHPNetMirrors.Add("http://sk.php.net/distributions/")
        PHPNetMirrors.Add("http://sk2.php.net/distributions/")
        PHPNetMirrors.Add("http://si.php.net/distributions/")
        PHPNetMirrors.Add("http://si2.php.net/distributions/")
        PHPNetMirrors.Add("http://za2.php.net/distributions/")
        PHPNetMirrors.Add("http://es.php.net/distributions/")
        PHPNetMirrors.Add("http://es2.php.net/distributions/")
        PHPNetMirrors.Add("http://se.php.net/distributions/")
        PHPNetMirrors.Add("http://se2.php.net/distributions/")
        PHPNetMirrors.Add("http://ch2.php.net/distributions/")
        PHPNetMirrors.Add("http://tw.php.net/distributions/")
        PHPNetMirrors.Add("http://tw2.php.net/distributions/")
        PHPNetMirrors.Add("http://th.php.net/distributions/")
        PHPNetMirrors.Add("http://th2.php.net/distributions/")
        PHPNetMirrors.Add("http://tr.php.net/distributions/")
        PHPNetMirrors.Add("http://tr2.php.net/distributions/")
        PHPNetMirrors.Add("http://ua.php.net/distributions/")
        PHPNetMirrors.Add("http://ua2.php.net/distributions/")
        PHPNetMirrors.Add("http://uk.php.net/distributions/")
        PHPNetMirrors.Add("http://uk2.php.net/distributions/")
        PHPNetMirrors.Add("http://us2.php.net/distributions/")
        PHPNetMirrors.Add("http://us3.php.net/distributions/")
        PHPNetMirrors.Add("http://us4.php.net/distributions/")
        PHPNetMirrors.Add("http://www.php.net/distributions/")
        PHPNetMirrors.Add("http://ve.php.net/distributions/")
    End Sub

    Private Sub AddSourceForgeMirrors()
        SourceForgeMirrors.Add("http://easynews.dl.sourceforge.net/sourceforge/")
        SourceForgeMirrors.Add("http://voxel.dl.sourceforge.net/sourceforge/")
        SourceForgeMirrors.Add("http://keihanna.dl.sourceforge.net/sourceforge/")
        SourceForgeMirrors.Add("http://citkit.dl.sourceforge.net/sourceforge/")
        SourceForgeMirrors.Add("http://internap.dl.sourceforge.net/sourceforge/")
        SourceForgeMirrors.Add("http://surfnet.dl.sourceforge.net/sourceforge/")
        SourceForgeMirrors.Add("http://nchc.dl.sourceforge.net/sourceforge/")
        SourceForgeMirrors.Add("http://kent.dl.sourceforge.net/sourceforge/")
        SourceForgeMirrors.Add("http://puzzle.dl.sourceforge.net/sourceforge/")
        SourceForgeMirrors.Add("http://ovh.dl.sourceforge.net/sourceforge/")
        SourceForgeMirrors.Add("http://switch.dl.sourceforge.net/sourceforge/")
        SourceForgeMirrors.Add("http://jaist.dl.sourceforge.net/sourceforge/")
        SourceForgeMirrors.Add("http://heanet.dl.sourceforge.net/sourceforge/")
        SourceForgeMirrors.Add("http://ufpr.dl.sourceforge.net/sourceforge/")
        SourceForgeMirrors.Add("http://mesh.dl.sourceforge.net/sourceforge/")
        SourceForgeMirrors.Add("http://optusnet.dl.sourceforge.net/sourceforge/")
        SourceForgeMirrors.Add("http://osdn.dl.sourceforge.net/sourceforge/")
    End Sub

    Private Sub G2SourceGUI_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        ListenThread.Abort()
    End Sub

    Private Sub Leave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Leave.Click
        StopMe_Click(sender, e)
        End
    End Sub

    Private Sub ToTray_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToTray.Click
        TrayIcon.Visible = True
        Me.Visible = False
    End Sub

    Private Sub TrayIcon_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TrayIcon.DoubleClick
        Me.Visible = True
    End Sub

    Private Sub ReloadDatabase_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReloadDatabase.Tick
        Dim LoadSourceThread As New Thread(AddressOf loadsources)
        LoadSourceThread.Start()
    End Sub

End Class

Public Class Web
    Public Class General
        Public Shared Function HTTPNotFound(ByVal G2NickName As String) As String
            HTTPNotFound = "HTTP/1.0 404 Not Found" + Chr(13) + Chr(10)
            HTTPNotFound = HTTPNotFound + "Server: EAD G2 Source Provider" + Chr(13) + Chr(10)
            HTTPNotFound = HTTPNotFound + "Connection: close" + Chr(13) + Chr(10)
            HTTPNotFound = HTTPNotFound + "Connection: Close" + Chr(13) + Chr(10)
            HTTPNotFound = HTTPNotFound + "Content-Disposition: inline ; filename=404.txt" + Chr(13) + Chr(10)
            If Not G2NickName = "" Then HTTPNotFound = HTTPNotFound + "X-Nick: " + G2NickName + Chr(13) + Chr(10)
            HTTPNotFound = HTTPNotFound + Chr(13) + Chr(10)
            HTTPNotFound = HTTPNotFound + "The file you are seeking is not present on this server."
        End Function
    End Class
End Class

Public Class SourceProcessor
    Public Const Increment As Integer = 1
    Public Const Decrement As Integer = -1
    Public objtcpc As TcpClient
    Public Nickname As String
    Public Logged As Boolean
    Public Event RequestProcessed(ByVal ClientRequest As String)
    Public Event ResponseProcessed(ByVal ClientResponse As String)
    Public Event ThreadCount(ByVal IncorDec As Integer)

    Sub ProcessSource()
        Dim objNs As NetworkStream = objtcpc.GetStream
        Dim bytClientRequest(objtcpc.ReceiveBufferSize) As Byte
        objNs.Read(bytClientRequest, 0, objtcpc.ReceiveBufferSize)
        Dim strClientRequest As String = System.Text.Encoding.Default.GetString(bytClientRequest)
        Dim strRequestedDocument As String = strClientRequest.Split(" ")(1)
        Dim strRequestedFile() As String = strRequestedDocument.Split("/")
        Dim getme As String = strRequestedFile.GetUpperBound(0)
        Dim IconValue() As Byte
        RaiseEvent ThreadCount(Increment)
        RaiseEvent RequestProcessed(strClientRequest)
        'G2SourceGUI.TotalRequest.Text = strClientRequest
        'LastMessage.Text = strRequestedDocument
        Application.DoEvents()
        Dim sendresponse As String = ""
        sendresponse = sendresponse + "HTTP/1.0 206 Partial Content" + Chr(13) + Chr(10)
        sendresponse = sendresponse + "Server: EAD G2 Source Provider" + Chr(13) + Chr(10)
        sendresponse = sendresponse + "Connection: close" + Chr(13) + Chr(10)
        sendresponse = sendresponse + "Connection: Close" + Chr(13) + Chr(10)
        If Not Nickname = "" Then sendresponse = sendresponse + "X-Nick: " + Nickname + Chr(13) + Chr(10)
        Dim sourcestorespond As New ArrayList
        If strRequestedFile(1) = "favicon.ico" Then
            If System.IO.File.Exists(G2SourceGUI.LocalPath + "favicon.ico") Then
                sendresponse = "HTTP/1.0 200 OK" + Chr(13) + Chr(10)
                sendresponse = sendresponse + "Server: EAD G2 Source Provider" + Chr(13) + Chr(10)
                sendresponse = sendresponse + "Content-type: image/icon" + Chr(13) + Chr(10)
                sendresponse = sendresponse + "Connection: close" + Chr(13) + Chr(10)
                sendresponse = sendresponse + "Connection: Close" + Chr(13) + Chr(10)
                If Not Nickname = "" Then sendresponse = sendresponse + "X-Nick: " + Nickname + Chr(13) + Chr(10)
                Dim LoadIcon As Integer = FreeFile()
                FileOpen(LoadIcon, G2SourceGUI.LocalPath + "favicon.ico", OpenMode.Binary, OpenAccess.Read, )
                FileGet(LoadIcon, IconValue)
                FileClose(LoadIcon)
                sendresponse = sendresponse + Chr(13) + Chr(10)
            Else
                sendresponse = EAD.Web.General.HTTPNotFound(Nickname)
            End If
        ElseIf strRequestedFile(1) = "sfsource" Then
            Dim sftorespond As New ArrayList
            For Each mirrortolist As String In G2SourceGUI.SourceForgeMirrors
                sftorespond.Add(mirrortolist.Clone)
            Next
            Dim sourcecount As Integer = 0
            Do Until sourcestorespond.Count > 5
                sourcecount = sftorespond.Count
                If sourcecount = 0 Then Exit Do
                Randomize(sourcecount / (Now.Second + Now.Minute))
                Dim currentsource As Integer = Rnd() * sourcecount
                If currentsource = sourcecount Then currentsource = currentsource - 1
                sourcestorespond.Add(sftorespond(currentsource).clone)
                sftorespond.Remove(sftorespond(currentsource))
                sftorespond.TrimToSize()
            Loop
            For Each mirror As String In sourcestorespond
                sendresponse = sendresponse + "Alt-Location: " + mirror + strRequestedFile(2) + "/" + strRequestedFile(3) + Chr(13) + Chr(10)
            Next
            sendresponse = sendresponse + "X-Available-Ranges: bytes 0-0" + Chr(13) + Chr(10)
            sendresponse = sendresponse + Chr(13) + Chr(10)
            If strClientRequest.IndexOf("Mozilla") >= 0 Then sendresponse = EAD.Web.General.HTTPNotFound(Nickname)
        ElseIf strRequestedFile(1) = "phpsource" Then
            Dim PHPToRespond As New ArrayList
            For Each mirrortolist As String In G2SourceGUI.PHPNetMirrors
                PHPToRespond.Add(mirrortolist)
            Next
            Dim sourcecount As Integer = 0
            Do Until sourcestorespond.Count >= 5
                sourcecount = PHPToRespond.Count
                If sourcecount = 0 Then Exit Do
                Randomize(sourcecount / (Now.Second + Now.Minute))
                Dim CurrentSource As Integer = Rnd() * sourcecount
                If currentsource = sourcecount Then currentsource = currentsource - 1
                sourcestorespond.Add(PHPToRespond(CurrentSource))
                PHPToRespond.Remove(PHPToRespond(CurrentSource))
                PHPToRespond.TrimToSize()
            Loop
            For Each mirror As String In sourcestorespond
                If strRequestedFile(2) = "manual" Then
                    sendresponse = sendresponse + "Alt-Location: " + mirror + "manual/" + strRequestedFile(3) + Chr(13) + Chr(10)
                Else
                    sendresponse = sendresponse + "Alt-Location: " + mirror + strRequestedFile(2) + Chr(13) + Chr(10)
                End If
            Next
            sendresponse = sendresponse + "X-Available-Ranges: bytes 0-0" + Chr(13) + Chr(10)
            sendresponse = sendresponse + Chr(13) + Chr(10)
            If strClientRequest.IndexOf("Mozilla") >= 0 Then sendresponse = EAD.Web.General.HTTPNotFound(Nickname)
        ElseIf strRequestedFile(1) = "source" Then
            Dim SourcesToSend As New FileSet
            Dim pass As Boolean
            SourcesToSend = G2SourceGUI.GetSources(getme, pass)
            If pass = True Or SourcesToSend.HasSourceForgeSource = True Then
                MsgBox("hasmirrors")
                Dim GenericToRespond As New ArrayList
                If pass = True Then
                    MsgBox("localmirrors")
                    For Each source As Object In SourcesToSend.Sources
                        GenericToRespond.Add(source)
                    Next
                End If
                If SourcesToSend.HasSourceForgeSource Then
                    MsgBox("SourceForgeMirrors")
                    For Each SFMirror As String In G2SourceGUI.SourceForgeMirrors
                        Dim SFMirrorString As New TorrentString
                        SFMirrorString.Value = SFMirror + SourcesToSend.SourceForgeProject + "/"
                        GenericToRespond.Add(SFMirrorString)
                    Next
                End If
                Dim sourcecount As Integer = 0
                Do Until sourcestorespond.Count >= 5
                    sourcecount = GenericToRespond.Count
                    If sourcecount = 0 Then Exit Do
                    Randomize(sourcecount / (Now.Second + Now.Minute))
                    Dim CurrentSource As Integer = Rnd() * sourcecount
                    If currentsource = sourcecount Then currentsource = currentsource - 1
                    sourcestorespond.Add(GenericToRespond(CurrentSource))
                    GenericToRespond.Remove(GenericToRespond(CurrentSource))
                    GenericToRespond.TrimToSize()
                Loop
                For Each mirror As TorrentString In sourcestorespond
                    sendresponse = sendresponse + "Alt-Location: " + mirror.value + Chr(13) + Chr(10)
                Next
                Dim hashconvertme As New EAD.Conversion.HashChanger
                If Not SourcesToSend.ED2K.Empty Then
                    hashconvertme = New EAD.Conversion.HashChanger
                    hashconvertme.rawhash = SourcesToSend.ED2K
                    sendresponse = sendresponse + "X-Content-URN: urn:ed2k:" + hashconvertme.hexhash + Chr(13) + Chr(10)
                End If
                If Not SourcesToSend.SHA1.Empty Then
                    hashconvertme = New EAD.Conversion.HashChanger
                    hashconvertme.rawhash = SourcesToSend.SHA1
                    sendresponse = sendresponse + "X-Content-URN: urn:sha1:" + hashconvertme.base32 + Chr(13) + Chr(10)
                End If
                If Not SourcesToSend.TTHRoot.Empty Then
                    hashconvertme = New EAD.Conversion.HashChanger
                    hashconvertme.rawhash = SourcesToSend.TTHRoot
                    sendresponse = sendresponse + "X-Content-URN: urn:tree:tiger/:" + hashconvertme.base32 + Chr(13) + Chr(10)
                End If
            End If
            sendresponse = sendresponse + "X-Available-Ranges: bytes 0-0" + Chr(13) + Chr(10)
            sendresponse = sendresponse + Chr(13) + Chr(10)
            If strClientRequest.IndexOf("Mozilla") >= 0 Then sendresponse = EAD.Web.General.HTTPNotFound(Nickname)
        Else
            sendresponse = EAD.Web.General.HTTPNotFound(Nickname)
        End If
        RaiseEvent ResponseProcessed(sendresponse)
        RaiseEvent ThreadCount(Decrement)
        'G2SourceGUI.LastResponse.Text = sendresponse
        Dim cleanresponse As Byte() = System.Text.Encoding.Default.GetBytes(sendresponse)
        objNs.Write(cleanresponse, 0, cleanresponse.Length)
        If strRequestedFile(1) = "favicon.ico" Then
            objNs.Write(IconValue, 0, IconValue.Length)
        End If
        objNs.Close()
        objtcpc.Close()
        If Logged = True Then
            Dim logfile As String = ".\logs\" + CStr(Now.Ticks)
            Dim LogtoOut As Integer = FreeFile()
            FileOpen(LogtoOut, logfile + ".request.log", OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
            FilePut(LogtoOut, strClientRequest)
            FileClose(LogtoOut)
            LogtoOut = FreeFile()
            FileOpen(LogtoOut, logfile + ".response.log", OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
            FilePut(LogtoOut, sendresponse)
            FileClose(LogtoOut)
        End If
        sendresponse = Nothing
        cleanresponse = Nothing
        Application.DoEvents()
    End Sub
End Class

Public Class FileSet
    Public Sources As ArrayList
    Public ED2K As String
    Public SHA1 As String
    Public TTHRoot As String
    Public FileSize As Long
    Public HasSourceForgeSource As Boolean
    Public SourceForgeProject As String
End Class