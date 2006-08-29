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
Namespace Tracker
    Public Class Announce
        Public Infohash As String
        Public AnnouncePeerID As String
        Public AnnounceLeft As New EAD.Torrent.TorrentNumber
        Public AnnounceUploaded As New EAD.Torrent.TorrentNumber
        Public AnnounceDownloaded As New EAD.Torrent.TorrentNumber
        Public AnnounceEvent As String
        Public AnnounceKey As New EAD.Torrent.TorrentString
        Public AnnounceIP As New EAD.Torrent.TorrentString
        Public AnnouncePort As New EAD.Torrent.TorrentNumber
        Private CompactIP(4) As Byte
        Private CompactPort As String
        Private AnnounceDictionary As New EAD.Torrent.TorrentDictionary

        Public Function Processannounce(ByVal AnnounceDatabase As EAD.Torrent.TorrentDictionary, ByRef PeersToReply As EAD.Torrent.TorrentDictionary, ByRef FailureLocation As Integer) As Boolean
            Dim TorrentSwarm As New EAD.Torrent.TorrentDictionary
            Dim TorrentPeerList As New EAD.Torrent.TorrentDictionary
            If AnnounceDatabase.Contains(Infohash) Then
                TorrentPeerList = AnnounceDatabase.Value(Infohash)
                If TorrentPeerList.Contains(AnnouncePeerID) Then
                    TorrentSwarm = TorrentPeerList.Value(AnnouncePeerID)
                    Dim CompareKey As New EAD.Torrent.TorrentString
                    If TorrentSwarm.Contains("key") Then CompareKey = TorrentSwarm.Value("key")
                    If CompareKey.Value = AnnounceKey.Value Then
                        TorrentSwarm.Value("key") = AnnounceKey
                        TorrentSwarm.Value("left") = AnnounceLeft
                        TorrentSwarm.Value("downloaded") = AnnounceDownloaded
                        TorrentSwarm.Value("uploaded") = AnnounceUploaded
                        TorrentSwarm.Value("port") = AnnouncePort
                        TorrentPeerList.Value(AnnouncePeerID) = TorrentSwarm
                    Else
                        Processannounce = False
                        FailureLocation = EAD.Tracker.Constants.Failure_PeerID
                    End If
                Else
                    TorrentSwarm.Value("key") = AnnounceKey
                    TorrentSwarm.Value("left") = AnnounceLeft
                    TorrentSwarm.Value("downloaded") = AnnounceDownloaded
                    TorrentSwarm.Value("uploaded") = AnnounceUploaded
                    TorrentSwarm.Value("port") = AnnouncePort
                    TorrentPeerList.Value(AnnouncePeerID) = TorrentSwarm
                End If
            End If
            CompactIP(0) = Split(AnnounceIP.Value, ".")(0)
            CompactIP(1) = Split(AnnounceIP.Value, ".")(1)
            CompactIP(2) = Split(AnnounceIP.Value, ".")(2)
            CompactIP(3) = Split(AnnounceIP.Value, ".")(3)
            'Dim CompactPort As New EAD.Conversion.PortToByte
            'CompactPort.iPort = AnnouncePort.Value
        End Function
    End Class
End Namespace
