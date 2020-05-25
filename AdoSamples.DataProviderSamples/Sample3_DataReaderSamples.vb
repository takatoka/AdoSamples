Imports System.Data.SqlClient

Public Class DataReaderSamples

    ''' <summary>
    ''' <para>How to get records form the database using DataReader.</para>
    ''' DataReaderでデータベースから値を取得する
    ''' </summary>
    Public Shared Sub GetRecords()
        Using connection As New SqlConnection(My.MySettings.Default.ConStr)
            connection.Open()

            Using command As New SqlCommand("select * from dbo.Employees", connection)

                ' Create instance of SqlDataReader class.
                ' SqlDataReaderのインスタンスを生成
                Using reader As SqlDataReader = command.ExecuteReader()

                    ' If you use DataReader to get records, read one record at time.
                    ' Read() method advance the SqlDataReader to the next record.
                    ' Read() method return true, if there are more rows. otherwise false.
                    ' DataReaderは1レコードずつ値を取得する
                    ' Read()メソッドが呼ばれると、次の行にカーソルが進む
                    ' Read()メソッドはまだ行が残っている場合trueを返す, 残っていなければfalseを返す
                    While reader.Read()

                        ' Get value by the column name.
                        ' 列名で取得
                        Dim id As Integer = reader("EmployeeID")

                        ' Get value by the column index.
                        ' 列インデックスでも取得できる
                        Dim name As String = reader(1)

                        ' DataReader returns a value of type Object, so convert it if necessary.
                        ' DataReaderが返す値の型はObject型なので、必要ならキャストする
                        Dim age As Integer = CType(reader("Age"), Integer)

                        ' If the value got from the database may be DBNull, check DBNull or not
                        ' 取得した値がDBNUllの可能性がある場合、DBNullのチェックをしてください
                        Dim email As String
                        If IsDBNull(reader("EmailAddress")) Then
                            email = ""
                        Else
                            email = reader("EmailAddress").ToString()
                        End If

                        ' DataReader has Get'TypeName' method.
                        ' Don't need the explicit cast code.
                        ' DO NOT USE it, if it's potentially DBNull.
                        ' DataReaderはGetString, GetDateTime, GetInt32のようなメソッドを持っている
                        ' 明示的なキャストをしなくてもいい
                        ' DBNullが返ってくる可能性があるときは使用しないでください
                        Dim updatedDate As DateTime = reader.GetDateTime(4)

                        ' Display values.
                        Console.WriteLine($"{id}, {name}, {age}, {email}, {updatedDate}")
                    End While
                End Using
            End Using
        End Using
    End Sub
End Class
