Module Program

    Sub Main(args As String())
        ' siempre se delcara el tipo de variable
        Dim token As String = "eyJhbGciOiJIUzI1NiJ9.eyJ1c2VyX2lkIjoyMDg4MDI5NzIwfQ._OePb6HdtVJJdYBFbMi9a_cc5WD3avNONaRcr3qDQNE"
        Dim secret As String = "4R*ux6M3q2sXA$"
        ' lo mismo que Begin en ruby 
        Try
            ' #esto devuleve un diccinario pero es similar a un hash en ruby 
            Dim payload = JWTDecoder.DecodeToken(token, secret)
            ' forma de acceder en una linea            
            Console.WriteLine(payload("user_id"))
            ' forma de acceder mediante un each similar a payload.each do |key, value|
            For Each kvp In payload
                ' concatenar
                Console.WriteLine(kvp.Key & ": " & kvp.Value.ToString())
            Next
            Dim pais As String = IpService.DetectarPaisIp("191.103.73.226")
Console.WriteLine(pais)
        Catch ex As Exception
            Console.WriteLine("ERROR: " & ex.Message)
        End Try
    End Sub
End Module