Imports Telegram.Bot.Types
Imports Telegram.Bot
Public Class StartCommand
    Inherits BotCommand

    Public Sub New(update As Update, api As Api)
        MyBase.New(update, api, Nothing)
    End Sub

    Public Overrides Sub Execute()
        Me._api.SendTextMessage(Me._update.Message.Chat.Id, "Bienvenido Usuario")
    End Sub

    
End Class
