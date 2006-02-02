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
