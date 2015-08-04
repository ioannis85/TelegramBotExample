Imports Telegram.Bot.Types
Imports Telegram.Bot

Public Class GreetingCommand
    Inherits BotCommand

    Public Sub New(update As Update, api As Api)
        MyBase.New(update, api, Nothing)
    End Sub

    Public Overrides Sub Execute()
        Me._api.SendTextMessage(Me._update.Message.Chat.Id, String.Format("Hola {0} {1}", Me._update.Message.From.FirstName, Me._update.Message.From.LastName))
    End Sub
End Class
