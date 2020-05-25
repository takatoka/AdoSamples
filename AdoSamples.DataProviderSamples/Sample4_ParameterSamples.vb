Imports System.Data.SqlClient

Public Class ParameterSamples

    Public Shared Sub GetRecords_AddParameters(minAge As Integer, maxAge As Integer)
        Using connection As New SqlConnection(My.MySettings.Default.ConStr)
            connection.Open()

            Using command As New SqlCommand()
                ' If you add parameters to SqlCommand, use '@ParameterName' placeholder, as in the following example.
                Dim query = "SELECT * FROM dbo.Employees WHERE Age >= @MinAge AND Age <= @MaxAge"
                ' If you use OleDbCommand or OdbcCommmand, you must use the question mark (?) placeholder, as in the following example.
                ' "SELECT * FROM dbo.Employees WHERE Age >= ? AND Age <= ?"

                ' Set properties.
                command.Connection = connection
                command.CommandText = query

                ' Add the input parameter and set its properties.
                Dim parameter As SqlParameter = New SqlParameter()
                parameter.ParameterName = "@MinAge"
                parameter.DbType = DbType.Int32
                parameter.Value = minAge
                ' Add Parameter to the Parameters collection.
                command.Parameters.Add(parameter)

                ' You can also add parameters, as in the following example.
                command.Parameters.Add(New SqlParameter("@MaxAge", DbType.Int32))
                command.Parameters("@MaxAge").Value = maxAge

                ' Execute
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim id As Integer = reader("EmployeeID")
                        Dim name As String = reader(1)
                        Dim age As Integer = CType(reader("Age"), Integer)
                        Dim email As String = reader.GetString(3)
                        Dim updatedDate As DateTime = reader.GetDateTime(4)

                        ' Display values.
                        Console.WriteLine($"{id}, {name}, {age}, {email}, {updatedDate}")
                    End While
                End Using
            End Using
        End Using
    End Sub
End Class
