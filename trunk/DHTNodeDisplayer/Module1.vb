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

Module Module1

    Sub Main()
        Dim PeerInfo As New EAD.Torrent.TorrentDictionary
        Dim TorrentFileLoad As Integer
        Dim TorrentFile As String = ".\dht.dat"
        Dim intermediarytorrentdata As String
        TorrentFileLoad = FreeFile()
        FileOpen(TorrentFileLoad, TorrentFile, OpenMode.Binary, OpenAccess.Default)
        intermediarytorrentdata = Space(FileLen(TorrentFile))
        FileGet(TorrentFileLoad, intermediarytorrentdata)
        FileClose(TorrentFileLoad)
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
