Imports System.Text

Namespace EAD
    Public Class Constants
        Public Const SourceBytes As Integer = 1
        Public Const SourceBinary As Integer = 2
        Public Const SourceHex As Integer = 3
        Public Const SourceBase32 As Integer = 4
    End Class

    Namespace Conversion
        Public Class EADCoreHash
            Inherits EAD.Constants

            Public Function HashConvert(ByVal hashbytes() As Byte, ByVal binary As String, ByVal hexadecimal As String, ByVal source As Integer) As Boolean
                HashConvert = False
                Dim buff As StringBuilder = New StringBuilder
                If source = SourceBytes Then
                    binary = ""
                    hexadecimal = ""
                    For Each HashByte As Byte In hashbytes
                        binary = binary + Chr(HashByte)
                        buff.AppendFormat("{0:x2}", HashByte)
                    Next
                    hexadecimal = buff.ToString
                    HashConvert = True
                End If
                If source = SourceBinary Then
                    Dim strtobytes As ASCIIEncoding
                    Erase hashbytes
                    hexadecimal = ""
                    hashbytes = strtobytes.GetBytes(binary)
                    For Each HashByte As Byte In hashbytes
                        buff.AppendFormat("{0:x2}", HashByte)
                    Next
                    hexadecimal = buff.ToString
                    HashConvert = True
                End If
                If source = SourceHex Then
                    Erase hashbytes
                    binary = ""
                    Dim hexblock As String
                    Dim spot As Int16 = 0
                    Do
                        hexblock = Mid(hexadecimal, spot, 2)
                        hashbytes(spot / 2) = Val("&h" & hexblock)
                        spot = spot + 2
                    Loop Until spot > Len(hexadecimal)
                    For Each HashByte As Byte In hashbytes
                        binary = binary + Chr(HashByte)
                    Next
                    HashConvert = True
                End If
            End Function
        End Class
    End Namespace
End Namespace