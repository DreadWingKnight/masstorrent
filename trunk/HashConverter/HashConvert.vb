Public Class HashConvert
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
    Friend WithEvents SHA1HexEntry As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents SHA1Base32Entry As System.Windows.Forms.TextBox
    Friend WithEvents ToBase32 As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.SHA1HexEntry = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.SHA1Base32Entry = New System.Windows.Forms.TextBox
        Me.ToBase32 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'SHA1HexEntry
        '
        Me.SHA1HexEntry.Location = New System.Drawing.Point(0, 24)
        Me.SHA1HexEntry.Name = "SHA1HexEntry"
        Me.SHA1HexEntry.Size = New System.Drawing.Size(280, 20)
        Me.SHA1HexEntry.TabIndex = 0
        Me.SHA1HexEntry.Text = ""
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(0, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(280, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Hexadeximal Value:"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(0, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(272, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Base32 Value"
        '
        'SHA1Base32Entry
        '
        Me.SHA1Base32Entry.Location = New System.Drawing.Point(0, 64)
        Me.SHA1Base32Entry.Name = "SHA1Base32Entry"
        Me.SHA1Base32Entry.Size = New System.Drawing.Size(280, 20)
        Me.SHA1Base32Entry.TabIndex = 3
        Me.SHA1Base32Entry.Text = ""
        '
        'ToBase32
        '
        Me.ToBase32.Location = New System.Drawing.Point(152, 88)
        Me.ToBase32.Name = "ToBase32"
        Me.ToBase32.Size = New System.Drawing.Size(128, 24)
        Me.ToBase32.TabIndex = 4
        Me.ToBase32.Text = "Convert to Base32"
        '
        'HashConvert
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(280, 117)
        Me.Controls.Add(Me.ToBase32)
        Me.Controls.Add(Me.SHA1Base32Entry)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.SHA1HexEntry)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "HashConvert"
        Me.Text = "Convert Data Hashes"
        Me.ResumeLayout(False)

    End Sub

#End Region
    Dim Convertme As New EAD.Conversion.HashChanger

    Private Sub ToBase32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToBase32.Click
        Convertme = New EAD.Conversion.HashChanger
        Convertme.hexhash = SHA1HexEntry.Text
        SHA1Base32Entry.Text = Convertme.base32
    End Sub

    Private Sub SHA1HexEntry_LocationChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SHA1HexEntry.LocationChanged
        SHA1HexEntry.Text = Trim(SHA1HexEntry.Text)
    End Sub
End Class
