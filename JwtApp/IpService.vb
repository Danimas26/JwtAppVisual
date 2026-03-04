Imports MaxMind.GeoIP2
Public Class IpService
    Private Shared reader As DatabaseReader
    Shared Sub New()
        Dim dbPath As String = System.IO.Path.Combine(AppContext.BaseDirectory, "../../../db/GeoLite2-Country.mmdb")
        reader = New DatabaseReader(dbPath)
    End Sub
    
    Public Shared Function DetectarPaisIp(ip As String) As String
        Try
            Dim response = reader.Country(ip)
            If response IsNot Nothing Then
                Return response.Country.IsoCode
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return "N/A"
    End Function

End Class