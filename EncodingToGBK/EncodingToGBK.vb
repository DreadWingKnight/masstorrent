Imports EAD.torrent

Module Module1
    Sub Main()
        Dim MetaDataHolder As New TorrentMetaData
        Dim TorrentData As New TorrentDictionary
        Dim TorrentFileload As Integer
        Dim intermediarytorrentdata As String
        Dim fulltorrent As String
        'variables defined
        Dim TorrentFileName As String
        Dim arguments As [String]() = Environment.GetCommandLineArgs()
        If arguments.GetLength(0) > 1 Then
            TorrentFileName = arguments(1)
        End If

        TorrentFileload = FreeFile()
        FileOpen(TorrentFileload, TorrentFileName, OpenMode.Binary, OpenAccess.Default)
        intermediarytorrentdata = Space(FileLen(TorrentFileName))
        FileGet(TorrentFileload, intermediarytorrentdata)
        FileClose(TorrentFileload)
        TorrentData.Parse(intermediarytorrentdata)
        MetaDataHolder.Torrent = TorrentData

        If TorrentData.Contains("resume") Then TorrentData.Remove("resume")
        If TorrentData.Contains("tracker_cache") Then TorrentData.Remove("tracker_cache")
        If TorrentData.Contains("torrent filename") Then TorrentData.Remove("torrent filename")
        Dim EncodingtoSet As New TorrentString
        EncodingtoSet.Value = "GBK"
        TorrentData.Value("encoding") = EncodingtoSet
        fulltorrent = TorrentData.Bencoded()
        Kill(TorrentFileName)
        TorrentFileload = FreeFile()
        FileOpen(TorrentFileload, TorrentFileName, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
        FilePut(TorrentFileload, fulltorrent)
        FileClose(TorrentFileload)
        MsgBox("Torrent encoding adjusted")
    End Sub
End Module
