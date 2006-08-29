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

Public Class G2SourceDatabase
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
    Friend WithEvents FileName As System.Windows.Forms.TextBox
    Friend WithEvents FilesInDatabase As System.Windows.Forms.ListBox
    Friend WithEvents SourcesForFile As System.Windows.Forms.ListBox
    Friend WithEvents FileSHA1 As System.Windows.Forms.TextBox
    Friend WithEvents FileTigerTree As System.Windows.Forms.TextBox
    Friend WithEvents FileED2K As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.FileName = New System.Windows.Forms.TextBox
        Me.FilesInDatabase = New System.Windows.Forms.ListBox
        Me.SourcesForFile = New System.Windows.Forms.ListBox
        Me.FileSHA1 = New System.Windows.Forms.TextBox
        Me.FileTigerTree = New System.Windows.Forms.TextBox
        Me.FileED2K = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'FileName
        '
        Me.FileName.Location = New System.Drawing.Point(248, 72)
        Me.FileName.Name = "FileName"
        Me.FileName.Size = New System.Drawing.Size(408, 20)
        Me.FileName.TabIndex = 0
        Me.FileName.Text = ""
        '
        'FilesInDatabase
        '
        Me.FilesInDatabase.Location = New System.Drawing.Point(0, 72)
        Me.FilesInDatabase.Name = "FilesInDatabase"
        Me.FilesInDatabase.Size = New System.Drawing.Size(248, 225)
        Me.FilesInDatabase.TabIndex = 1
        '
        'SourcesForFile
        '
        Me.SourcesForFile.Location = New System.Drawing.Point(248, 168)
        Me.SourcesForFile.Name = "SourcesForFile"
        Me.SourcesForFile.Size = New System.Drawing.Size(408, 134)
        Me.SourcesForFile.TabIndex = 2
        '
        'FileSHA1
        '
        Me.FileSHA1.Location = New System.Drawing.Point(312, 96)
        Me.FileSHA1.Name = "FileSHA1"
        Me.FileSHA1.Size = New System.Drawing.Size(344, 20)
        Me.FileSHA1.TabIndex = 3
        Me.FileSHA1.Text = ""
        '
        'FileTigerTree
        '
        Me.FileTigerTree.Location = New System.Drawing.Point(312, 120)
        Me.FileTigerTree.Name = "FileTigerTree"
        Me.FileTigerTree.Size = New System.Drawing.Size(344, 20)
        Me.FileTigerTree.TabIndex = 4
        Me.FileTigerTree.Text = ""
        '
        'FileED2K
        '
        Me.FileED2K.Location = New System.Drawing.Point(312, 144)
        Me.FileED2K.Name = "FileED2K"
        Me.FileED2K.Size = New System.Drawing.Size(344, 20)
        Me.FileED2K.TabIndex = 5
        Me.FileED2K.Text = ""
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(0, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(248, 16)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Files In Database:"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(248, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(360, 16)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Filename"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(256, 96)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(56, 16)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "SHA1:"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(256, 120)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 16)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "TTH:"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(256, 144)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 16)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "ED2K:"
        '
        'G2SourceDatabase
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(656, 301)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.FileED2K)
        Me.Controls.Add(Me.FileTigerTree)
        Me.Controls.Add(Me.FileSHA1)
        Me.Controls.Add(Me.SourcesForFile)
        Me.Controls.Add(Me.FilesInDatabase)
        Me.Controls.Add(Me.FileName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "G2SourceDatabase"
        Me.Text = "G2 Source Database Generator/Editor"
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class
