Imports System
Imports System.Text
Imports System.Security.Cryptography
Imports System.Text.Json
Imports System.Collections.Generic
Public Class JWTDecoder
    Public Shared Function DecodeToken(token As String, secret As String) As Dictionary(Of String, Object)
    ' Esto es divide el token por el '.' al parecer es ncesario poner c al final 
    
        Dim parts = token.Split("."c)
        ' El token tiene tres partes si esto es diferente al hacer el split no es valido 
        If parts.Length <> 3 Then            
            Throw New Exception("Formato JWT inválido")
        End If
        ' nota aqui se toma los valores asi (0) no como [0]
        Dim header = parts(0)
        Dim payload = parts(1)
        Dim signature = parts(2)
        Dim data = header & "." & payload        
        Dim keyBytes = Encoding.UTF8.GetBytes(secret)
        Dim dataBytes = Encoding.UTF8.GetBytes(data)
        ' aqui se valida la firma
        Using hmac As New HMACSHA256(keyBytes)
            Dim computedHash = hmac.ComputeHash(dataBytes)
            Dim computedSignature = Base64UrlEncode(computedHash)
            If computedSignature <> signature Then
                Throw New Exception("Firma inválida")
            End If
        End Using
        Dim payloadJson = Encoding.UTF8.GetString(Base64UrlDecode(payload))
        Dim result = JsonSerializer.Deserialize(Of Dictionary(Of String, Object))(payloadJson)
        Return result
    End Function

    Private Shared Function Base64UrlEncode(input As Byte()) As String
        Return Convert.ToBase64String(input).
            TrimEnd("="c).
            Replace("+"c, "-"c).
            Replace("/"c, "_"c)
    End Function


    Private Shared Function Base64UrlDecode(input As String) As Byte()
        input = input.Replace("-"c, "+"c).Replace("_"c, "/"c)
        Select Case input.Length Mod 4
            Case 2
                input &= "=="
            Case 3
                input &= "="
        End Select
        Return Convert.FromBase64String(input)
    End Function
End Class