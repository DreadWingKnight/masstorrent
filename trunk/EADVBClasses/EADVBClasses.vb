'--------------------------------------------------------
' EADVBClasses - EADVBClasses.vb
' Visual Basic Constants
' Harold Feit
'--------------------------------------------------------
Imports System.Text

Namespace EAD
    Namespace Torrent
        Public Class Announces
            Inherits EAD.VisualBasic.Constants
            Public Shared Function HTTPToUDPAnnounce(ByVal AnnounceURL As String, ByVal ProtocolType As Integer) As String
                HTTPToUDPAnnounce = "udp://" + Microsoft.VisualBasic.Strings.Right(AnnounceURL, Len(AnnounceURL) - ProtocolType)
            End Function
            Public Shared Function IsValidHTTPAnnounce(ByVal AnnounceURL As String) As Integer
                If Microsoft.VisualBasic.Strings.Left(AnnounceURL, 7) = "http://" Then
                    IsValidHTTPAnnounce = HTTPAnnounce
                ElseIf Microsoft.VisualBasic.Strings.Left(AnnounceURL, 8) = "https://" Then
                    IsValidHTTPAnnounce = HTTPSAnnounce
                Else
                    IsValidHTTPAnnounce = InvalidAnnounce
                End If
            End Function
            Public Shared Function IsValidAnnounce(ByVal AnnounceURL As String) As Integer
                If Microsoft.VisualBasic.Strings.Left(AnnounceURL, 6) = "udp://" Then
                    IsValidAnnounce = UDPAnnounce
                ElseIf Microsoft.VisualBasic.Strings.Left(AnnounceURL, 7) = "http://" Then
                    IsValidAnnounce = HTTPAnnounce
                ElseIf Microsoft.VisualBasic.Strings.Left(AnnounceURL, 8) = "https://" Then
                    IsValidAnnounce = HTTPSAnnounce
                Else
                    IsValidAnnounce = InvalidAnnounce
                End If
            End Function
        End Class
    End Namespace
    Namespace VisualBasic
        Public Class Constants
            Public Const AnnounceList As String = "announce-list"
            Public Const Webseed As String = "httpseeds"
            Public Const HTTPAnnounce As Integer = 7
            Public Const HTTPSAnnounce As Integer = 8
            Public Const UDPAnnounce As Integer = 6
            Public Const InvalidAnnounce As Integer = 0
            Public Const ED2KBlockSize As Long = 9728000
        End Class
    End Namespace
End Namespace