Imports System.Data.SqlClient

Public Class CommandSamples
    ''' <summary>
    ''' <para>How to create a instance of SqlCommand class.</para>
    ''' SqlCommandのインスタンスを生成する
    ''' </summary>
    Public Shared Sub CreatCommand()
        Using connection As New SqlConnection(My.MySettings.Default.ConStr)
            connection.Open()

            ' Basic =======================================================================================
            Dim command1 As New SqlCommand()
            ' Set connection
            command1.Connection = connection
            ' Set sql query to execute.
            command1.CommandText = "select count(*) from dbo.Employees"
            ' If you create command instance without Using statement, you have to call Dispose method.
            ' SqlCommandのインスタンスも、Disoposeが必要
            command1.Dispose()

            ' Use Using statement. ========================================================================
            ' Usingステートメントが使えます
            Using command2 As New SqlCommand("select count(*) from dbo.Employees", connection)
            End Using

            ' Call CreateCommand method of SqlConnection. =================================================
            ' SqlConnectionクラスのCreateCommand()メソッドでの精製も可能です
            Using command3 = connection.CreateCommand()
                command3.CommandText = "select count(*) from dbo.Employees"
            End Using
        End Using
    End Sub

    ''' <summary>
    ''' <para>How to get a single value like Count,Max,Average form a database.</para>
    ''' データベースから単一の値(Count, Max, Average)を取得する
    ''' </summary>
    Public Shared Sub GetSingleValue()
        Using connection As New SqlConnection(My.MySettings.Default.ConStr)
            connection.Open()

            ' Create instance of SqlCommand class to execute sql query.
            ' Sqlを実行するためのインスタンスを生成する
            Using countCommand = New SqlCommand("select count(*) from dbo.Employees", connection)

                ' Call ExecuteScalar method of SqlCommand class to get the Single Value.
                ' ExecuteScalar()メソッドは(Count, Max, Average)のような単一の値を取得する
                Dim result = countCommand.ExecuteScalar()

                ' The Result type of ExecuteScalar() method is Object, so convert it if necessary.
                ' ExecuteScalar()メソッドが返す値の型はObjectなので、必要であればキャストする
                Dim count = CType(result, Integer)

                ' Display.
                Console.WriteLine($"Count -- {count}")
            End Using

            ' Create instance of SqlCommand class to execute sql query.
            Using maxCommand = New SqlCommand("select Max(UpdatedDate) from dbo.Employees", connection)
                ' Call ExecuteScalar method of SqlCommand class to get the Single Value.
                Dim result = maxCommand.ExecuteScalar()
                ' The Result type of ExecuteScalar() method is Object, so convert it if necessary.
                Dim maxDate = CType(result, DateTime)
                ' Display.
                Console.WriteLine($"Max -- {maxDate}")
            End Using
        End Using
    End Sub

    Public Shared Sub InsertRecord()
        Using connection As New SqlConnection(My.MySettings.Default.ConStr)
            connection.Open()

            ' INSERT, UPDATE and DELTE Sql can all be executed using the ExecuteNonQury() method of SqlCommand class.
            ' INSERT, UPDATE, DELETE　全部ExecuteNonQuery()メソッドで実行できます

            Using insertCommand = New SqlCommand("INSERT into dbo.Employees (EmployeeID, Name, UpdatedDate) values(100, 'Test Taro', '2020/05/01')", connection)
                ' The ExecuteNonQuery method returns the count of records added to the table.
                ' 追加された行数が返ってくる
                Dim result As Integer = insertCommand.ExecuteNonQuery()
            End Using

        End Using
    End Sub

    Public Shared Sub UpdateRecord()
        Using connection As New SqlConnection(My.MySettings.Default.ConStr)
            connection.Open()

            Using updateCommand = New SqlCommand("UPDATE dbo.Employees SET Name = 'Updated! Taro!', EmailAddress ='Updatedhoge@com.com' where EmployeeID = 100", connection)
                ' The ExecuteNonQuery method returns the count of records updated.
                ' 更新された行数が返ってくる
                Dim result As Integer = updateCommand.ExecuteNonQuery()
            End Using

        End Using
    End Sub

    Public Shared Sub RemoveRecord()
        Using connection As New SqlConnection(My.MySettings.Default.ConStr)
            connection.Open()

            Using deleteCommand = New SqlCommand("DELETE FROM dbo.Employees where EmployeeID = 100", connection)
                ' The ExecuteNonQuery method returns the count of records deleted form the table.
                ' 削除された行数が返ってくる
                Dim result As Integer = deleteCommand.ExecuteNonQuery()
            End Using

        End Using
    End Sub
End Class
