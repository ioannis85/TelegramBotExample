Imports Telegram.Bot
Imports System.Reactive
Imports System.Reactive.Linq
Imports System.Reactive.PlatformServices
Imports System.Reactive.Concurrency
Imports Telegram.Bot.Types

Module Module1
    Dim offset As Integer = 265939466
    Dim telegramBot As New Api("Enter your API key Here!!!!")
    Dim commandFactory As New CommandSimpleFactory()

    Sub Main()
        Dim mee = telegramBot.GetMe().Result
        Console.WriteLine(String.Format("Listening {0} Requests right now!!!", mee.FirstName))
        While True
            Dim updates = telegramBot.GetUpdates(offset).Result()
            If updates.Length > 0 Then
                offset = updates.Max(Function(u) u.Id) + 1
                Dim observableUpdate = updates.ToObservable(NewThreadScheduler.Default)
                observableUpdate.Subscribe(AddressOf ProcessUpdate, Sub() Console.WriteLine("All petitions Attended in thread:{0}", System.Threading.Thread.CurrentThread.ManagedThreadId))
            End If
            System.Threading.Thread.Sleep(200)
        End While
        Console.ReadLine()
    End Sub

    Sub ProcessUpdate(update As Update)
        Console.WriteLine("Request Received at {0: dd/MM/yyyy hh:mm:ss tt}  {1}", DateTime.UtcNow, TimeZone.CurrentTimeZone.StandardName)
        Dim command As ICommand = commandFactory.Create(update.Message.Text, update, telegramBot)
        command.Execute()
    End Sub

End Module

