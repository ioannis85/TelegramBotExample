Imports Telegram.Bot
Imports Telegram.Bot.Types

Public Class LocationCommand
    Inherits BotCommand
    
    Public Sub New(update As Update, api As Api)
        MyBase.New(update, api, Nothing)
    End Sub

    Public Overrides Sub Execute()
        Me._api.SendLocation(Me._update.Message.Chat.Id, 24.8056641, -107.4024992)
    End Sub
End Class
