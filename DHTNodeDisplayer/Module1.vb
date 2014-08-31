Module Module1

    Sub Main()
        Dim PeerInfo As New EAD.Torrent.TorrentDictionary
        Dim TorrentFileLoad As Integer
        Dim TorrentFile As String = ".\dht.dat"
        Dim intermediarytorrentdata As String
        TorrentFileload = FreeFile()
        FileOpen(TorrentFileload, TorrentFile, OpenMode.Binary, OpenAccess.Default)
        intermediarytorrentdata = Space(FileLen(TorrentFile))
        FileGet(TorrentFileload, intermediarytorrentdata)
        FileClose(TorrentFileload)
        PeerInfo.Parse(intermediarytorrentdata)
        If PeerInfo.Contains("nodes") Then
            Dim Nodes As New EAD.Torrent.TorrentString
            Nodes = PeerInfo.Value("nodes")
            Dim Index = 0
            Do
                Dim CurrentNode As String
                CurrentNode = Nodes.Value.Substring(Index, 26)
                Dim PortBytes() As Byte
                PortBytes = System.Text.Encoding.ASCII.GetBytes(CurrentNode.Substring(24, 2))
                Dim temp As Byte() = {0, 0, 0, 0}
                Array.Copy(PortBytes, 0, temp, 0, 2)
                'Array.Reverse(temp)
                Dim port As Int32 = BitConverter.ToInt32(temp, 0)
                Dim AddressBytes() As Byte
                AddressBytes = System.Text.Encoding.ASCII.GetBytes(CurrentNode.Substring(20, 4))
                Dim Address As String
                For offset As Int16 = 0 To 3
                    If offset = 0 Then Address = Address + CStr(AddressBytes(offset)) Else Address = Address + "." + CStr(AddressBytes(offset))
                Next
                Console.WriteLine("Peer Obtained as:")
                Console.WriteLine("IP: " + CStr(Address))
                Console.WriteLine("Port: " + CStr(port))
                Address = Nothing
                port = Nothing
                CurrentNode = Nothing
                PortBytes = Nothing
                AddressBytes = Nothing
                GC.Collect()
                Index = Index + 26
            Loop Until Index >= Len(Nodes.Value)
        End If

    End Sub
End Module
