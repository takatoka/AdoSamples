Imports System.Data.SqlClient

Public Class ConnectionSamples

    ''' <summary>
    ''' <para>How to connect to the database.</para>
    ''' データベースに接続する方法
    ''' </summary>
    Public Shared Sub ConnectToDatabase()
        ' Create an instance of SqlConnection class.
        ' SqlConnectionクラスのインスタンスを生成する。
        Dim connection As SqlConnection = New SqlConnection()
        connection.ConnectionString = My.MySettings.Default.ConStr

        ' Connect to the database using the created instace.
        ' データベースに接続する
        connection.Open()
        Console.WriteLine($"接続しました---{connection.State}")

        ' Cloase the connection to the database.
        ' データベースとの接続を終了する
        connection.Close()
        Console.WriteLine($"接続を終了しました---{connection.State}")

        ' ReConnect to the database.
        ' データベースに再接続する
        connection.Open()
        Console.WriteLine($"データベースに接続しました 2回目--{connection.State}")

        ' Dispose the instance of SqlConnection class.
        ' Dispose method close the connection to the database.
        ' インスタンスを破棄する
        ' データベースとの接続を終了する
        connection.Dispose()
        Console.WriteLine($"オブジェクトを破棄してデータベースとの接続を終了--{connection.State}")

        ' If you called Dispose() method, you have to create instance of SqlConnection class to connect to database.
        ' 再接続するにはインスタンスを作り直す必要がある。
        Try
            ' If you try to connect using a disposed instance, throwed exception.
            ' 破棄したインスタンスで接続しようとすると、例外が発生する
            connection.Open()
        Catch ex As Exception
            Console.WriteLine($"オブジェクトは破棄されているから接続できない--{connection.State}")
            Console.WriteLine($"Exception Message - {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' <para>How to connecto to the database with Using statement.</para>
    ''' Usingステートメントを利用してデータベースに接続する
    ''' </summary>
    Public Shared Sub ConnectToDatabase_WithUsingStatement()

        ' Create connection instance in Using block.
        ' Usingブロック内で生成
        Using connection = New SqlConnection(My.MySettings.Default.ConStr)
            ' Connect to the database using the created instace.
            ' データベースに接続する
            connection.Open()
            Console.WriteLine($"接続しました---{connection.State}")

            ' Cloase the connection to the database.
            ' データベースとの接続を終了する
            connection.Close()
            Console.WriteLine($"接続を終了しました---{connection.State}")

            ' The End Using statement disposes connection instace.
            ' Usingを抜けるときに自動でconnection.Disposeが呼ばれる
        End Using
    End Sub
End Class
