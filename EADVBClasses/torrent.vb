'--------------------------------------------------------
' EADVBClasses - torrent.vb
' Torrent Parsing and Generation
' Harold Feit
' Torrent Class library courtesy of http://sf.net/projects/writtorrent
' EAD Modifications by Harold Feit
'--------------------------------------------------------

Imports System
Imports System.Collections
Imports System.Collections.Specialized
' Namespace added for organization within the class library dll.
Namespace EAD.Torrent

    ' torrentString 
    Public Class TorrentString
        Private thisd As String
        Private thisstringlength As Integer
        Private thisheaderlength As Integer
        Private thisvalue As String
        Private ThisPassString As String
        '-------------------------------------------- String value of a TorrentString
        Public Property Value() As String
            Get
                Return thisvalue
            End Get
            Set(ByVal Value As String)
                thisvalue = Value
            End Set
        End Property


        '-------------------------------------------- BEncoded String value of a TorrentString
        Public ReadOnly Property BEncoded() As String
            Get
                Return thisvalue.Length & ":" & thisvalue
            End Get
        End Property


        '-------------------------------------------- Remainder of Parsing

        Public ReadOnly Property PassString() As String
            Get
                Return ThisPassString
            End Get
        End Property


        '-------------------------------------------- Parse a string from a Torrent file

        Public ReadOnly Property Parse(ByVal d As [String]) As Object
            Get
                thisheaderlength = (d.IndexOf(":") + 1)
                thisstringlength = Decimal.Parse(d.Substring(0, (d.IndexOf(":"))))
                thisvalue = d.Substring(thisheaderlength, thisstringlength)
                ThisPassString = d.Substring(thisheaderlength + thisstringlength, d.Length - (thisheaderlength + thisstringlength))
                Return Me
            End Get
        End Property

    End Class 'torrentstring ////////////////////////////////

    'torrentNumber //////////////////////////////
    Public Class TorrentNumber
        Private thisd As String
        Private thisNumber As Long


        '-------------------------------------------- Integer value of a TorrentNumber
        Public Property Value() As Long
            Get
                Return thisNumber
            End Get
            Set(ByVal Value As Long)
                thisNumber = Value
            End Set
        End Property

        '-------------------------------------------- Parse a number in a torrent string to a TorrentNumber
        Default Public ReadOnly Property Parse(ByVal d As [String]) As Object
            Get
                thisd = d
                thisNumber = Decimal.Parse(d.Substring(d.IndexOf("i") + 1, (d.IndexOf("e")) - (d.IndexOf("i")) - 1))
                Return Me
            End Get
        End Property

        '-------------------------------------------- Remainder string from parsing a torrent string

        Public ReadOnly Property PassString() As String
            Get
                Return thisd.Substring(thisd.IndexOf("e") + 1, thisd.Length - (thisd.IndexOf("e") + 1))
            End Get
        End Property

        '-------------------------------------------- BEncoded string of a TorrentNumber

        Public ReadOnly Property BEncoded() As String
            Get
                Return "i" & thisNumber & "e"
            End Get
        End Property


    End Class 'torrentnumber ////////////////////////////////

    'torrentList ////////////////////////////////
    Public Class TorrentList
        Inherits CollectionBase
        Private thispassstring As String

        '-------------------------------------------- parse a list from a torrent string
        Default Public ReadOnly Property Parse(ByVal d As [String]) As TorrentList
            Get
                d = d.Substring(1, d.Length - 1) ' drop the l
                While Not ((d.IndexOf("e") = 0))
                    Select Case d.Substring(0, 1)
                        Case "d"
                            Dim tvaluedict As New TorrentDictionary
                            InnerList.Add(tvaluedict.Parse(d))
                            thispassstring = tvaluedict.PassString
                            d = thispassstring
                        Case "l"
                            Dim tvaluedict As New TorrentList
                            InnerList.Add(tvaluedict.Parse(d))
                            thispassstring = tvaluedict.PassString
                            d = thispassstring
                        Case "i"
                            Dim ThisString As New TorrentNumber
                            InnerList.Add(ThisString.Parse(d))
                            d = ThisString.PassString()
                            thispassstring = d
                        Case Else
                            Dim ThisString As New TorrentString
                            InnerList.Add(ThisString.Parse(d))
                            d = ThisString.PassString()
                            thispassstring = d
                    End Select
                End While
                thispassstring = PassString.Substring(1, PassString.Length - 1) ' drop the e
                d = thispassstring
                Return Me
            End Get
        End Property

        '-------------------------------------------- get or set the actual value of the current list
        Public Property Value() As ArrayList
            '    Public Property Value() as ArrayList
            Get
                Return InnerList
            End Get
            Set(ByVal Value As ArrayList)
                Innerlist.Clear()
                For Each item As Object In Value
                    innerlist.Add(item)
                Next
            End Set
        End Property


        '-------------------------------------------- remainder of the torrent string after parsing
        Public ReadOnly Property PassString() As String
            Get
                Return thispassstring
            End Get
        End Property

        '-------------------------------------------- bencode this instance's list to a torrent string
        Public ReadOnly Property Bencoded() As String
            Get
                Dim thisstring As String = "l"
                For Each member As Object In InnerList
                    thisstring = thisstring & member.bencoded()
                Next
                Return thisstring & "e"
            End Get
        End Property
    End Class 'torrentlist ////////////////////////////////

    'torrentDictionary //////////////////////////
    Public Class TorrentDictionary
        Inherits NameObjectCollectionBase
        Private tkey As String
        Private keyobj As Object
        Private TorDict As String

        '-------------------------------------------- Parse a dictionary from torrent-string
        Public Function Parse(ByVal ThisTor As [String]) As TorrentDictionary
            TorDict = ThisTor.Substring(1, ThisTor.Length - 1) ' drop the d
            While (TorDict.IndexOf("e") > 0)
                Dim ThisString As New TorrentString
                keyobj = ThisString.Parse(TorDict)
                tkey = keyobj.Value()
                TorDict = ThisString.PassString()

                Select Case TorDict.Substring(0, 1)
                    Case "d"  'Parse a dictionary-within-a-dictionary
                        Dim tvaluedict As New TorrentDictionary
                        Me.Add(tkey, tvaluedict.Parse(TorDict))
                        TorDict = tvaluedict.PassString()
                    Case "i"  'parse an integer
                        Dim tvalueInt As New TorrentNumber
                        Me.Add(tkey, tvalueInt.Parse(TorDict))
                        TorDict = tvalueInt.PassString()
                    Case "l"  'parse a list
                        Dim tvalueList As New TorrentList
                        Me.Add(tkey, tvalueList.Parse(TorDict))
                        TorDict = tvalueList.PassString()
                    Case Else  'parse a string
                        Dim tvalue As New TorrentString
                        Me.Add(tkey, tvalue.Parse(TorDict))
                        TorDict = tvalue.PassString()
                End Select
            End While
            If Not TorDict.Length = 0 Then
                TorDict = TorDict.Substring(1, TorDict.Length - 1) ' drop the e... er... and make sure we're not at the end of the file already to do so
            End If
            Return Me
        End Function 'Parse


        '-------------------------------------------- return values from this current hashtable
        Default Public Property Value(ByVal key As [String]) As Object
            Get
                Return Me.BaseGet(key)
            End Get
            Set(ByVal Value As Object)
                Me.BaseSet(key, Value)
            End Set
        End Property

        '-------------------------------------------- remainder of a torrent string from parsing
        Public ReadOnly Property PassString() As String
            Get
                Return TorDict
            End Get
        End Property


        '-------------------------------------------- keys
        Public ReadOnly Property Keys() As ICollection
            Get
                Return Me.Keys
            End Get
        End Property 'Keys

        '-------------------------------------------- values
        Public ReadOnly Property Values() As ICollection
            Get
                Return Me.BaseGetAllValues
            End Get
        End Property 'Values()

        '-------------------------------------------- add items to this hashtable
        Public Sub Add(ByVal key As [String], ByVal value As [Object])
            Me.BaseAdd(key, value)
        End Sub 'Add

        '-------------------------------------------- check to see if this instance contains a key
        Public Function Contains(ByVal key As [String]) As Boolean
            Dim TheseKeys As New ArrayList
            For Each keyname As [String] In Me.BaseGetAllKeys
                TheseKeys.Add(keyname)
            Next
            If (TheseKeys.Contains(key)) Then
                Return True
            Else
                Return False
            End If
        End Function 'Contains

        '-------------------------------------------- remove an entry
        Public Sub Remove(ByVal key As [String])
            Me.BaseRemove(key)
        End Sub 'Remove

        '-------------------------------------------- bencode this torrentdictionary
        Public ReadOnly Property Bencoded() As String
            Get
                Dim thisstring As String = "d"
                Dim TheseKeys As New ArrayList

                For Each keyname As [String] In Me.BaseGetAllKeys
                    TheseKeys.Add(keyname)
                Next

                Dim myComparer = New CaseInsensitiveComparer   ' I *think* the preferred order is alphabetical
                TheseKeys.Sort(myComparer)

                For Each member As String In TheseKeys
                    Dim ThisKey As New TorrentString
                    ThisKey.Value = member
                    thisstring = thisstring & ThisKey.BEncoded()
                    ThisKey = Nothing
                    thisstring = thisstring & Me(member).bencoded()
                Next
                Return thisstring & "e"
            End Get
        End Property

        '-------------------------------------------- read a torrent from the console
        Public Sub GetConsole()
            Dim j As Integer
            Dim torrent As String
            While True
                j = Console.Read()
                If (j = -1) Then
                    Exit While
                End If
                torrent = torrent & Microsoft.VisualBasic.ChrW(j)
            End While
            Me.Parse(torrent)
        End Sub



    End Class 'TorrentDictionary

    'torrentMetaData ////////////////////////////////
    Public Class TorrentMetaData
        Private thisTorrent As TorrentDictionary

        '-------------------------------------------- get / set the torrent you'd like to analyze
        Public Property Torrent() As TorrentDictionary
            Get
                Return thisTorrent
            End Get
            Set(ByVal Value As TorrentDictionary)
                thisTorrent = Value
            End Set
        End Property


        '-------------------------------------------- get / set the announce string
        Public Property Announce() As String
            Get
                Return thisTorrent.Value("announce").value
            End Get
            Set(ByVal Value As String)
                thisTorrent.Value("announce").value = Value
            End Set
        End Property

        '-------------------------------------------- test for torrent multi-file or not
        Public ReadOnly Property MultiFile() As Boolean
            Get
                If thisTorrent("info").Contains("length") Then
                    Return False
                Else
                    Return True
                End If
            End Get
        End Property

        Public ReadOnly Property SingleFile() As Boolean
            Get
                If thisTorrent("info").Contains("files") Then
                    Return False
                Else
                    Return True
                End If
            End Get
        End Property

        Public Property Comment() As String
            Get
                If thisTorrent.Contains("comment") Then
                    Return thisTorrent.Value("comment").value
                Else
                    Return ""
                End If
            End Get
            Set(ByVal Value As String)
                If thisTorrent.Contains("comment") Then
                    thisTorrent.Value("comment").value = Value
                Else
                    Dim ThisComment As New TorrentString
                    ThisComment.Value = Value
                    thisTorrent.Add("comment", ThisComment)
                End If
            End Set
        End Property

        Public Property CreatedBy() As String
            Get
                If thisTorrent.Contains("created by") Then
                    Return thisTorrent.Value("created by").value
                Else
                    Return ""
                End If
            End Get
            Set(ByVal Value As String)
                If thisTorrent.Contains("created by") Then
                    thisTorrent.Value("created by").value = Value
                Else
                    Dim ThisComment As New TorrentString
                    ThisComment.Value = Value
                    thisTorrent.Add("created by", ThisComment)
                End If
            End Set
        End Property


    End Class ' torrent meta data //////////////////////
End Namespace
