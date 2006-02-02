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