'--------------------------------------------------------
' TorrentBuild - AdvancedConfiguration.vb
' Torrent Builder Advanced Settings code
' Harold Feit
' ToDo
' - Add support for custom torrent output path
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
'--------------------------------------------------------

Public Class AdvancedConfiguration
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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ApplyChg As System.Windows.Forms.Button
    Friend WithEvents ApplyWithclose As System.Windows.Forms.Button
    Friend WithEvents closeonly As System.Windows.Forms.Button
    Public WithEvents VerboseGen As System.Windows.Forms.CheckBox
    Friend WithEvents WSAUse As System.Windows.Forms.CheckBox
    Friend WithEvents EnableDelay As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(AdvancedConfiguration))
        Me.VerboseGen = New System.Windows.Forms.CheckBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.WSAUse = New System.Windows.Forms.CheckBox
        Me.closeonly = New System.Windows.Forms.Button
        Me.ApplyWithclose = New System.Windows.Forms.Button
        Me.ApplyChg = New System.Windows.Forms.Button
        Me.EnableDelay = New System.Windows.Forms.CheckBox
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'VerboseGen
        '
        Me.VerboseGen.Location = New System.Drawing.Point(8, 16)
        Me.VerboseGen.Name = "VerboseGen"
        Me.VerboseGen.Size = New System.Drawing.Size(536, 16)
        Me.VerboseGen.TabIndex = 0
        Me.VerboseGen.Text = "Verbose Torrent Generation (Useful if you want to be informed between files)"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.EnableDelay)
        Me.GroupBox1.Controls.Add(Me.WSAUse)
        Me.GroupBox1.Controls.Add(Me.closeonly)
        Me.GroupBox1.Controls.Add(Me.ApplyWithclose)
        Me.GroupBox1.Controls.Add(Me.ApplyChg)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(552, 200)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Advanced Settings"
        '
        'WSAUse
        '
        Me.WSAUse.Location = New System.Drawing.Point(8, 32)
        Me.WSAUse.Name = "WSAUse"
        Me.WSAUse.Size = New System.Drawing.Size(536, 16)
        Me.WSAUse.TabIndex = 3
        Me.WSAUse.Text = "Use WebSeed values from wsa.config in torrent generation"
        '
        'closeonly
        '
        Me.closeonly.Location = New System.Drawing.Point(152, 168)
        Me.closeonly.Name = "closeonly"
        Me.closeonly.Size = New System.Drawing.Size(144, 24)
        Me.closeonly.TabIndex = 2
        Me.closeonly.Text = "Close"
        '
        'ApplyWithclose
        '
        Me.ApplyWithclose.Location = New System.Drawing.Point(8, 168)
        Me.ApplyWithclose.Name = "ApplyWithclose"
        Me.ApplyWithclose.Size = New System.Drawing.Size(144, 24)
        Me.ApplyWithclose.TabIndex = 1
        Me.ApplyWithclose.Text = "Apply Changes and Close"
        '
        'ApplyChg
        '
        Me.ApplyChg.Location = New System.Drawing.Point(8, 144)
        Me.ApplyChg.Name = "ApplyChg"
        Me.ApplyChg.Size = New System.Drawing.Size(144, 24)
        Me.ApplyChg.TabIndex = 0
        Me.ApplyChg.Text = "Apply Changes"
        '
        'EnableDelay
        '
        Me.EnableDelay.Location = New System.Drawing.Point(8, 48)
        Me.EnableDelay.Name = "EnableDelay"
        Me.EnableDelay.Size = New System.Drawing.Size(536, 16)
        Me.EnableDelay.TabIndex = 4
        Me.EnableDelay.Text = "Enable all delay notifications"
        '
        'AdvancedConfiguration
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(560, 205)
        Me.Controls.Add(Me.VerboseGen)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AdvancedConfiguration"
        Me.Text = "TorrentGen Advanced Configuration"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub ApplyChg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApplyChg.Click
        TorrentBuild.GenerateVerbose = VerboseGen.Checked
        TorrentBuild.UseWSAConfig = WSAUse.Checked
        TorrentBuild.DelayMessages = EnableDelay.Checked
    End Sub

    Private Sub ApplyWithclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApplyWithclose.Click
        Call ApplyChg_Click(sender, e)
        closeonly_Click(sender, e)
    End Sub

    Private Sub AdvancedConfig_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        VerboseGen.Checked = TorrentBuild.GenerateVerbose
        WSAUse.Checked = TorrentBuild.UseWSAConfig
        EnableDelay.Checked = TorrentBuild.DelayMessages
    End Sub

    Private Sub closeonly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles closeonly.Click
        Close()
    End Sub
End Class
