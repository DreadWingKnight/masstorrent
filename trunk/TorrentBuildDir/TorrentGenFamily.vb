Imports System
Imports System.Io
Imports System.Security.Cryptography
Imports System.Text
Imports Microsoft.VisualBasic
Imports EAD.Torrent

Module TorrentGenFamily
    Public Function GetWebSeedData(ByVal returnarray As ArrayList) As Integer
        If System.IO.File.Exists(TorrentBuild.LocalPath + "wsa.configure") Then
            Dim loadsettings As Integer = FreeFile()
            Dim intermediarysettingdata As String
            FileOpen(loadsettings, TorrentBuild.LocalPath + "wsa.configure", OpenMode.Binary, OpenAccess.Read, OpenShare.LockRead)
            intermediarysettingdata = Space(FileLen(TorrentBuild.LocalPath + "wsa.configure"))
            FileGet(loadsettings, intermediarysettingdata)
            FileClose(loadsettings)
            Dim ConfigData As New TorrentDictionary
            ConfigData.Parse(intermediarysettingdata)
            If ConfigData.Contains("seedlist") Then
                Dim ReturnSet As New TorrentList
                ReturnSet = ConfigData.Value("seedlist")
                returnarray = ReturnSet.Value
                GetWebSeedData = returnarray.Count
            Else
                Dim webseed1load As New TorrentString
                Dim webseed2load As New TorrentString
                Dim webseed3load As New TorrentString
                Dim webseed4load As New TorrentString
                Dim webseed5load As New TorrentString
                webseed1load = ConfigData.Value("seed1")
                webseed2load = ConfigData.Value("seed2")
                webseed3load = ConfigData.Value("seed3")
                webseed4load = ConfigData.Value("seed4")
                webseed5load = ConfigData.Value("seed5")
                Dim WebSeedAddressArray As New ArrayList
                Dim WebSeedAddressList As New TorrentList
                If Not webseed1load.Value = "" Then returnarray.Add(webseed1load) : GetWebSeedData = GetWebSeedData + 1
                If Not webseed2load.Value = "" Then returnarray.Add(webseed2load) : GetWebSeedData = GetWebSeedData + 1
                If Not webseed3load.Value = "" Then returnarray.Add(webseed3load) : GetWebSeedData = GetWebSeedData + 1
                If Not webseed4load.Value = "" Then returnarray.Add(webseed4load) : GetWebSeedData = GetWebSeedData + 1
                If Not webseed5load.Value = "" Then returnarray.Add(webseed5load) : GetWebSeedData = GetWebSeedData + 1
            End If
            If TorrentBuild.GenerateVerbose Then MsgBox("Number of Webseeds loaded: " + CStr(GetWebSeedData))
        End If
    End Function
End Module
