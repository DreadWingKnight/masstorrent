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

Module ConvertNow
    Sub Main()
        Dim Tier0 As New EAD.Torrent.TorrentList
        Dim Tier0array As New ArrayList
        Tier0.Value = Tier0array
        Dim EdwardK1 As New EAD.Torrent.TorrentString
        Dim EdwardK2 As New EAD.Torrent.TorrentString
        Dim EdwardK3 As New EAD.Torrent.TorrentString
        Dim EdwardK4 As New EAD.Torrent.TorrentString
        Dim EdwardK5 As New EAD.Torrent.TorrentString
        Dim EdwardK6 As New EAD.Torrent.TorrentString
        Dim EdwardK7 As New EAD.Torrent.TorrentString
        Dim EdwardK8 As New EAD.Torrent.TorrentString
        Dim EdwardK9 As New EAD.Torrent.TorrentString
        EdwardK1.Value = "http://bt.edwardk.info:6969/announce"
        EdwardK2.Value = "http://bt.edwardk.info:2710/announce"
        EdwardK3.Value = "http://bt.edwardk.info:942/announce"
        EdwardK4.Value = "http://bt.edwardk.info:4040/announce"
        EdwardK5.Value = "http://bt.edwardk.info:676/announce"
        EdwardK6.Value = "http://bt.edwardk.info:12891/announce"
        EdwardK7.Value = "http://bt.edwardk.info:63124/announce"
        EdwardK8.Value = "http://bt.edwardk.info:541/announce"
        EdwardK9.Value = "http://bt.edwardk.info:6767/announce"
        Tier0array.Add(EdwardK1)
        Tier0array.Add(EdwardK2)
        Tier0array.Add(EdwardK3)
        Tier0array.Add(EdwardK4)
        Tier0array.Add(EdwardK5)
        Tier0array.Add(EdwardK6)
        Tier0array.Add(EdwardK7)
        Tier0array.Add(EdwardK8)
        Tier0array.Add(EdwardK9)
        Tier0.Value = Tier0array
        Dim Tiers As New ArrayList
        Tiers.Add(Tier0)
        Dim announcelist As New EAD.Torrent.TorrentList
        announcelist.Value = Tiers

        Dim TorrentData As New EAD.Torrent.TorrentDictionary
        Dim TorrentFileload As Integer
        Dim intermediarytorrentdata As String
        Dim fulltorrent As String
        'variables defined
        Dim TorrentFileName As String
        Dim arguments As [String]() = Environment.GetCommandLineArgs()
        If arguments.GetLength(0) > 1 Then
            TorrentFileName = arguments(1)
        Else
            MsgBox("You must pass a filename to this program to process." + Chr(13) + "Dragging and dropping a .torrent will work")
            End
        End If

        TorrentFileload = FreeFile()
        FileOpen(TorrentFileload, TorrentFileName, OpenMode.Binary, OpenAccess.Default)
        intermediarytorrentdata = Space(FileLen(TorrentFileName))
        FileGet(TorrentFileload, intermediarytorrentdata)
        FileClose(TorrentFileload)
        TorrentData.Parse(intermediarytorrentdata)
        If TorrentData.Contains("resume") Then TorrentData.Remove("resume")
        If TorrentData.Contains("tracker_cache") Then TorrentData.Remove("tracker_cache")
        If TorrentData.Contains("torrent filename") Then TorrentData.Remove("torrent filename")
        TorrentData.Value("announce") = EdwardK1
        If TorrentData.Contains("announce-list") Then
            TorrentData.Remove("announce-list")
        End If
        TorrentData.Add("announce-list", announcelist)
        fulltorrent = TorrentData.Bencoded()
        Kill(TorrentFileName)
        TorrentFileload = FreeFile()
        FileOpen(TorrentFileload, TorrentFileName, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
        FilePut(TorrentFileload, fulltorrent)
        FileClose(TorrentFileload)
        MsgBox("Torrent announce list adjusted")
    End Sub
End Module
