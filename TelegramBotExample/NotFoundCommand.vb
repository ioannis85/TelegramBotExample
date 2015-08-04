Imports Telegram.Bot.Types
Imports Telegram.Bot

Public Class NotFoundCommand
    Implements ICommand
    Private _update As Update
    Private _api As Api
    Public Sub New(update As Update, api As Api)
        Me._update = update
        Me._api = api
    End Sub

    Public Sub Execute() Implements ICommand.Execute
        Me._api.SendTextMessage(Me._update.Message.Chat.Id, String.Format("Petición Inválida"))
    End Sub
End Class
