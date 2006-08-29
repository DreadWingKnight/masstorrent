'--------------------------------------------------------
' TorrentBuild - ChangeBlackListedFiles.vb
' Change Blacklisted Files and File Types
' Harold Feit
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

Imports EAD.Torrent

Public Class ChangeBlacklistedFiles
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
    Friend WithEvents Blacklisted As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents FileName As System.Windows.Forms.TextBox
    Friend WithEvents AddtoList As System.Windows.Forms.Button
    Friend WithEvents RemoveFromList As System.Windows.Forms.Button
    Friend WithEvents SaveToMain As System.Windows.Forms.Button
    Friend WithEvents SaveToMainWithClose As System.Windows.Forms.Button
    Friend WithEvents CloseThis As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ReplaceAbove As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(ChangeBlacklistedFiles))
        Me.Blacklisted = New System.Windows.Forms.ListBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.FileName = New System.Windows.Forms.TextBox
        Me.AddtoList = New System.Windows.Forms.Button
        Me.RemoveFromList = New System.Windows.Forms.Button
        Me.SaveToMain = New System.Windows.Forms.Button
        Me.SaveToMainWithClose = New System.Windows.Forms.Button
        Me.CloseThis = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.ReplaceAbove = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Blacklisted
        '
        Me.Blacklisted.Location = New System.Drawing.Point(0, 16)
        Me.Blacklisted.Name = "Blacklisted"
        Me.Blacklisted.Size = New System.Drawing.Size(408, 108)
        Me.Blacklisted.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(408, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Filenames And Extensions that are blacklisted:"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(0, 128)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(408, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Filename/Extension"
        '
        'FileName
        '
        Me.FileName.Location = New System.Drawing.Point(0, 144)
        Me.FileName.Name = "FileName"
        Me.FileName.Size = New System.Drawing.Size(232, 20)
        Me.FileName.TabIndex = 3
        Me.FileName.Text = ""
        '
        'AddtoList
        '
        Me.AddtoList.Location = New System.Drawing.Point(240, 144)
        Me.AddtoList.Name = "AddtoList"
        Me.AddtoList.Size = New System.Drawing.Size(56, 24)
        Me.AddtoList.TabIndex = 4
        Me.AddtoList.Text = "Add"
        '
        'RemoveFromList
        '
        Me.RemoveFromList.Location = New System.Drawing.Point(352, 144)
        Me.RemoveFromList.Name = "RemoveFromList"
        Me.RemoveFromList.Size = New System.Drawing.Size(56, 24)
        Me.RemoveFromList.TabIndex = 5
        Me.RemoveFromList.Text = "Remove"
        '
        'SaveToMain
        '
        Me.SaveToMain.Location = New System.Drawing.Point(272, 168)
        Me.SaveToMain.Name = "SaveToMain"
        Me.SaveToMain.Size = New System.Drawing.Size(136, 24)
        Me.SaveToMain.TabIndex = 6
        Me.SaveToMain.Text = "Save to Main"
        '
        'SaveToMainWithClose
        '
        Me.SaveToMainWithClose.Location = New System.Drawing.Point(272, 192)
        Me.SaveToMainWithClose.Name = "SaveToMainWithClose"
        Me.SaveToMainWithClose.Size = New System.Drawing.Size(136, 24)
        Me.SaveToMainWithClose.TabIndex = 7
        Me.SaveToMainWithClose.Text = "Save to Main and Close"
        '
        'CloseThis
        '
        Me.CloseThis.Location = New System.Drawing.Point(272, 216)
        Me.CloseThis.Name = "CloseThis"
        Me.CloseThis.Size = New System.Drawing.Size(136, 24)
        Me.CloseThis.TabIndex = 8
        Me.CloseThis.Text = "Close"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(0, 168)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(272, 80)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "When entering in file names, include the full filename. When entering in extensio" & _
        "ns, only include the extension, do not include any wildcards. Also, files that e" & _
        "nd in filenames that are listed here will be blocked. Changes are NOT permanent " & _
        "unless you save on the main window."
        '
        'ReplaceAbove
        '
        Me.ReplaceAbove.Location = New System.Drawing.Point(296, 144)
        Me.ReplaceAbove.Name = "ReplaceAbove"
        Me.ReplaceAbove.Size = New System.Drawing.Size(56, 24)
        Me.ReplaceAbove.TabIndex = 10
        Me.ReplaceAbove.Text = "Replace"
        '
        'ChangeBlacklistedFiles
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(410, 247)
        Me.Controls.Add(Me.ReplaceAbove)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.CloseThis)
        Me.Controls.Add(Me.SaveToMainWithClose)
        Me.Controls.Add(Me.SaveToMain)
        Me.Controls.Add(Me.RemoveFromList)
        Me.Controls.Add(Me.AddtoList)
        Me.Controls.Add(Me.FileName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Blacklisted)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ChangeBlacklistedFiles"
        Me.Text = "Change Blacklisted Filenames"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub ChangeBlacklistedFiles_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For Each Filename As TorrentString In TorrentBuild.BlackListedFiles
            Blacklisted.Items.Add(Filename.Value)
        Next
    End Sub

    Private Sub Blacklisted_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Blacklisted.SelectedIndexChanged
        FileName.Text = Blacklisted.SelectedItem
    End Sub

    Private Sub AddtoList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddtoList.Click
        Blacklisted.Items.Add(FileName.Text)
        FileName.Text = ""
    End Sub

    Private Sub ReplaceAbove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReplaceAbove.Click
        Blacklisted.SelectedItem = FileName.Text
    End Sub

    Private Sub RemoveFromList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveFromList.Click
        Blacklisted.Items.RemoveAt(Blacklisted.SelectedIndex)
    End Sub

    Private Sub SaveToMain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToMain.Click
        Dim ReplaceBlacklist As New ArrayList
        For Each textvalue As String In Blacklisted.Items
            Dim AddValueToBlacklist As New TorrentString
            AddValueToBlacklist.Value = textvalue
            ReplaceBlacklist.Add(AddValueToBlacklist)
        Next
        TorrentBuild.BlackListedFiles = ReplaceBlacklist
    End Sub

    Private Sub SaveToMainWithClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToMainWithClose.Click
        Call SaveToMain_Click(sender, e)
        Close()
    End Sub

    Private Sub CloseThis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseThis.Click
        Close()
    End Sub
End Class
