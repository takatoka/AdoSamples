Imports System.Data.SqlClient

Public Class TransactionSamples
    ''' <summary>
    ''' <para>What would happen if you don't use transacion.</para>
    ''' もしTransactionを使わなかったらどうなるか
    ''' </summary>
    Public Shared Sub IfYouDoNotUseTransaction()
        Using connection As New SqlConnection(My.MySettings.Default.ConStr)
            connection.Open()

            Try
                ' You are going to execute INSERT , UPADATE , DELETE in that order.
                ' INSERT, UPDATE, DELETEの順に実行する必要があるとする
                Using insertCommand = New SqlCommand("INSERT into dbo.Employees (EmployeeID, Name, Age,UpdatedDate) values(100, 'Test Taro', 22, '2020/05/01')", connection)
                    Dim result As Integer = insertCommand.ExecuteNonQuery()
                End Using
                Console.WriteLine("=================== Added !!! =================")

                ' Something Error occurs.
                ' 何かしらのエラーが発生
                Throw New Exception("Error Occurs.")

                ' These Command will not be executed.
                ' ここのコマンドは実行されない
                Using updateCommand = New SqlCommand("UPDATE dbo.Employees SET Name = 'Updated! Taro!', EmailAddress ='Updatedhoge@com.com' where EmployeeID = 100", connection)
                    Dim result As Integer = updateCommand.ExecuteNonQuery()
                End Using
                Using deleteCommand = New SqlCommand("DELETE FROM dbo.Employees where EmployeeID = 100", connection)
                    Dim result As Integer = deleteCommand.ExecuteNonQuery()
                End Using
            Catch ex As Exception
                Console.WriteLine(ex.Message)
                ' Only INSERT is executed because didn't use the transaction.
                ' INSERTだけが実行されてしまっている
                DataReaderSamples.GetRecords()
            End Try
        End Using
    End Sub

    ''' <summary>
    ''' <para>How to Rallback.</para>
    ''' ロールバックの方法
    ''' </summary>
    Public Shared Sub RollBack()
        Using connection As New SqlConnection(My.MySettings.Default.ConStr)
            connection.Open()

            ' Create instance of SqlTransaction and begin transaction.
            ' Transactionを開始する
            Dim transaction As SqlTransaction = connection.BeginTransaction()

            Try
                ' You are going to execute INSERT , Update , Delete in that order.
                Using insertCommand = New SqlCommand("INSERT into dbo.Employees (EmployeeID, Name, Age,UpdatedDate) values(100, 'Test Taro', 22, '2020/05/01')", connection)
                    insertCommand.Transaction = transaction
                    Dim result As Integer = insertCommand.ExecuteNonQuery()
                End Using
                Console.WriteLine("=================== Added !!! =================")

                ' Something Error occurs.
                Throw New Exception("Error Occurs.")

                ' These Command will not be executed.
                Using updateCommand = New SqlCommand("UPDATE dbo.Employees SET Name = 'Updated! Taro!', EmailAddress ='Updatedhoge@com.com' where EmployeeID = 100", connection)
                    updateCommand.Transaction = transaction
                    Dim result As Integer = updateCommand.ExecuteNonQuery()
                End Using
                Using deleteCommand = New SqlCommand("DELETE FROM dbo.Employees where EmployeeID = 100", connection)
                    deleteCommand.Transaction = transaction
                    Dim result As Integer = deleteCommand.ExecuteNonQuery()
                End Using
            Catch ex As Exception
                Console.WriteLine(ex.Message)

                ' Call Rollback() method to undo database.
                ' Rollbakcメソッドでデータベースを元に戻す
                transaction.Rollback()
                Console.WriteLine("================ Rollback !!! =================")
                DataReaderSamples.GetRecords()
            End Try

            transaction.Dispose()

        End Using
    End Sub

    ''' <summary>
    ''' <para>How to commit changes.</para>
    ''' </summary>
    Public Shared Sub Commit()
        Using connection As New SqlConnection(My.MySettings.Default.ConStr)
            connection.Open()

            ' Create instance of SqlTransaction and begin transaction.
            ' Transactionを開始する
            Dim transaction As SqlTransaction = connection.BeginTransaction()

            Try
                ' You are going to execute INSERT , Update , Delete in that order.
                Using insertCommand = New SqlCommand("INSERT into dbo.Employees (EmployeeID, Name, Age,UpdatedDate) values(100, 'Test Taro', 22, '2020/05/01')", connection)
                    insertCommand.Transaction = transaction
                    Dim result As Integer = insertCommand.ExecuteNonQuery()
                End Using
                Console.WriteLine("=================== Added !!! =================")

                Using updateCommand = New SqlCommand("UPDATE dbo.Employees SET Name = 'Updated! Taro!', EmailAddress ='Updatedhoge@com.com' where EmployeeID = 100", connection)
                    updateCommand.Transaction = transaction
                    Dim result As Integer = updateCommand.ExecuteNonQuery()
                End Using
                Console.WriteLine("=================== Updated !!! =================")

                Using deleteCommand = New SqlCommand("DELETE FROM dbo.Employees where EmployeeID = 100", connection)
                    deleteCommand.Transaction = transaction
                    Dim result As Integer = deleteCommand.ExecuteNonQuery()
                End Using
                Console.WriteLine("=================== Deleted !!! =================")

            Catch ex As Exception
                Console.WriteLine(ex.Message)

                ' If error occurs, call Rollback() method to undo database.
                ' Rollbakcメソッドでデータベースを元に戻す
                transaction.Rollback()
                Console.WriteLine("================ Rollback !!! =================")

                DataReaderSamples.GetRecords()
            End Try

            ' Save the changes if ther are no problems.
            ' 問題なければ変更を確定する
            transaction.Commit()
            Console.WriteLine("================ Commit !!! =================")
            DataReaderSamples.GetRecords()

            transaction.Dispose()
        End Using
    End Sub
End Class
