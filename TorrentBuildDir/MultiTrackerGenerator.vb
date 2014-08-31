Public Class MultiTrackerGenerator
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
    Friend WithEvents TornadoCommandLine As System.Windows.Forms.TextBox
    Friend WithEvents BitTornadoGUI As System.Windows.Forms.TextBox
    Friend WithEvents BTCMDSelect As System.Windows.Forms.RadioButton
    Friend WithEvents BTGuiSelect As System.Windows.Forms.RadioButton
    Friend WithEvents ReturnWithoutSave As System.Windows.Forms.Button
    Friend WithEvents ReturnWithSave As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.BTCMDSelect = New System.Windows.Forms.RadioButton
        Me.TornadoCommandLine = New System.Windows.Forms.TextBox
        Me.BTGuiSelect = New System.Windows.Forms.RadioButton
        Me.BitTornadoGUI = New System.Windows.Forms.TextBox
        Me.ReturnWithoutSave = New System.Windows.Forms.Button
        Me.ReturnWithSave = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'BTCMDSelect
        '
        Me.BTCMDSelect.Location = New System.Drawing.Point(0, 24)
        Me.BTCMDSelect.Name = "BTCMDSelect"
        Me.BTCMDSelect.Size = New System.Drawing.Size(688, 16)
        Me.BTCMDSelect.TabIndex = 0
        Me.BTCMDSelect.Text = "BitTornado Command Line Announce-List Paramater. Use Commas to separate trackers " & _
        "within a Tier and Pipes ""|"" to separate tiers."
        '
        'TornadoCommandLine
        '
        Me.TornadoCommandLine.Location = New System.Drawing.Point(0, 40)
        Me.TornadoCommandLine.Name = "TornadoCommandLine"
        Me.TornadoCommandLine.Size = New System.Drawing.Size(680, 20)
        Me.TornadoCommandLine.TabIndex = 1
        Me.TornadoCommandLine.Text = ""
        '
        'BTGuiSelect
        '
        Me.BTGuiSelect.Location = New System.Drawing.Point(0, 64)
        Me.BTGuiSelect.Name = "BTGuiSelect"
        Me.BTGuiSelect.Size = New System.Drawing.Size(680, 16)
        Me.BTGuiSelect.TabIndex = 2
        Me.BTGuiSelect.Text = "BitTornado GUI Torrent Maker Announce-List Option. One tier per line. Separate tr" & _
        "ackers within a tier with spaces."
        '
        'BitTornadoGUI
        '
        Me.BitTornadoGUI.Location = New System.Drawing.Point(0, 80)
        Me.BitTornadoGUI.Multiline = True
        Me.BitTornadoGUI.Name = "BitTornadoGUI"
        Me.BitTornadoGUI.Size = New System.Drawing.Size(680, 88)
        Me.BitTornadoGUI.TabIndex = 3
        Me.BitTornadoGUI.Text = ""
        '
        'ReturnWithoutSave
        '
        Me.ReturnWithoutSave.Location = New System.Drawing.Point(520, 168)
        Me.ReturnWithoutSave.Name = "ReturnWithoutSave"
        Me.ReturnWithoutSave.Size = New System.Drawing.Size(160, 24)
        Me.ReturnWithoutSave.TabIndex = 4
        Me.ReturnWithoutSave.Text = "Return Without Changing"
        '
        'ReturnWithSave
        '
        Me.ReturnWithSave.Location = New System.Drawing.Point(520, 192)
        Me.ReturnWithSave.Name = "ReturnWithSave"
        Me.ReturnWithSave.Size = New System.Drawing.Size(160, 24)
        Me.ReturnWithSave.TabIndex = 4
        Me.ReturnWithSave.Text = "Return Saving Changes"
        '
        'MultiTrackerGenerator
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(680, 221)
        Me.Controls.Add(Me.ReturnWithoutSave)
        Me.Controls.Add(Me.BitTornadoGUI)
        Me.Controls.Add(Me.BTGuiSelect)
        Me.Controls.Add(Me.TornadoCommandLine)
        Me.Controls.Add(Me.BTCMDSelect)
        Me.Controls.Add(Me.ReturnWithSave)
        Me.Name = "MultiTrackerGenerator"
        Me.Text = "MultiTrackerGenerator"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Shared MultiTrackerTiers As New EAD.Torrent.TorrentList

    Private Sub BTCMDSelect_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTCMDSelect.CheckedChanged
        Call ChangeSelected()
    End Sub

    Private Sub BTGuiSelect_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTGuiSelect.CheckedChanged
        Call ChangeSelected()
    End Sub

    Public Sub UpdateInput()
        Dim Updated As String
        For Each Tier As EAD.Torrent.TorrentList In MultiTrackerTiers.Value
            Dim Line As String
            For Each Tracker As EAD.Torrent.TorrentString In Tier.Value
                Line = Line + " " + Tracker.Value
            Next
            If Not Updated = "" Then Updated = Updated + Chr(13) + Chr(10) + Trim(Line) Else Updated = Trim(Line)
            Line = ""
        Next
        BitTornadoGUI.Text = Updated
        BTCMDSelect.Checked = False
        BTGuiSelect.Checked = True
        ChangeSelected()
    End Sub
    Private Sub ChangeSelected()
        If BTCMDSelect.Checked Then
            TornadoCommandLine.Enabled = True
            BitTornadoGUI.Enabled = False
            BTGuiSelect.Checked = False
        End If
        If BTGuiSelect.Checked Then
            TornadoCommandLine.Enabled = False
            BitTornadoGUI.Enabled = True
            BTCMDSelect.Checked = False
        End If
    End Sub

    Private Sub ReturnWithSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReturnWithSave.Click
        Dim StringsToProcess() As String
        Dim TiersToProcess() As String
        If BTCMDSelect.Checked Then
            StringsToProcess = Split(TornadoCommandLine.Text, "|")
            Dim Tiers As New ArrayList
            For Each StringLine As String In StringsToProcess
                StringLine = Trim(StringLine)
                Dim TierToAdd As New EAD.Torrent.TorrentList
                Dim NewTierArray As New ArrayList
                TiersToProcess = Split(StringLine, ",")
                For Each tracker As String In TiersToProcess
                    If Not Trim(tracker) = "" Then
                        Dim TrackerToAdd As New EAD.Torrent.TorrentString
                        TrackerToAdd.Value = Trim(tracker)
                        NewTierArray.Add(TrackerToAdd)
                    End If
                Next
                If newtierarray.Count > 0 Then
                    TierToAdd.Value = NewTierArray
                    Tiers.Add(TierToAdd)
                End If
            Next
            MultiTrackerTiers.Value = Tiers
        ElseIf BTGuiSelect.Checked Then
            StringsToProcess = Split(BitTornadoGUI.Text, Chr(10))
            Dim Tiers As New ArrayList
            For Each StringLine As String In StringsToProcess
                If Microsoft.VisualBasic.Strings.Right(stringline, 1) = Chr(13) Then stringline = Microsoft.VisualBasic.Strings.Left(stringline, Len(stringline) - 1)
                StringLine = Trim(StringLine)
                Dim TierToAdd As New EAD.Torrent.TorrentList
                Dim NewTierArray As New ArrayList
                TiersToProcess = Split(StringLine, " ")
                For Each tracker As String In TiersToProcess
                    If Not Trim(tracker) = "" Then
                        Dim TrackerToAdd As New EAD.Torrent.TorrentString
                        TrackerToAdd.Value = Trim(tracker)
                        NewTierArray.Add(TrackerToAdd)
                    End If
                Next
                If newtierarray.Count > 0 Then
                    TierToAdd.Value = NewTierArray
                    Tiers.Add(TierToAdd)
                End If
            Next
            MultiTrackerTiers.Value = Tiers
        End If
        Close()
    End Sub

    Private Sub ReturnWithoutSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReturnWithoutSave.Click
        Close()
    End Sub

    Private Sub MultiTrackerGenerator_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BTGuiSelect.Checked = True
        Call ChangeSelected()
    End Sub
End Class
