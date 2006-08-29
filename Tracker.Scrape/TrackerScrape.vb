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
    Public Class Scrape
        Public Infohash() As String
        Public ScrapeAll As Boolean
        Public UseCompletes As Boolean
        Public Function ProcessScrape(ByVal PeerDictionary As EAD.Torrent.TorrentDictionary, ByRef CompletesDictionary As EAD.Torrent.TorrentDictionary, ByRef ReturnDictionary As EAD.Torrent.TorrentDictionary, ByRef FailureLocation As Integer) As Boolean
            If ScrapeAll = True Then
                For Each Swarm As String In PeerDictionary.Keys
                    Dim SwarmOutput As New EAD.Torrent.TorrentDictionary
                    Dim SwarmDownloaders As New EAD.Torrent.TorrentNumber
                    Dim SwarmUploaders As New EAD.Torrent.TorrentNumber
                    Dim SwarmDictionary As New EAD.Torrent.TorrentDictionary
                    SwarmDictionary = PeerDictionary.Value(Swarm)
                    For Each Peer As String In SwarmDictionary.Keys
                        Dim LocalPeerDict As New EAD.Torrent.TorrentDictionary
                        LocalPeerDict = SwarmDictionary(Peer)
                        Dim PeerLeft As New EAD.Torrent.TorrentNumber
                        PeerLeft = LocalPeerDict.Value("left")
                        If PeerLeft.Value = 0 Then
                            SwarmUploaders.Value = SwarmUploaders.Value + 1
                        Else
                            SwarmDownloaders.Value = SwarmDownloaders.Value + 1
                        End If
                    Next
                    SwarmOutput.Value("complete") = SwarmUploaders
                    SwarmOutput.Value("incomplete") = SwarmDownloaders
                    If UseCompletes = True Then
                        If CompletesDictionary.Contains(Swarm) Then
                            SwarmOutput.Value("downloaded") = CompletesDictionary.Value(Swarm)
                        End If
                    End If
                    ReturnDictionary.Value(Swarm) = SwarmOutput
                Next
            Else

            End If
        End Function
    End Class
End Namespace