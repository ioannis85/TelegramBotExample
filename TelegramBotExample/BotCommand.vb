Imports Telegram.Bot
Imports Telegram.Bot.Types

Public MustInherit Class BotCommand
    Implements ICommand
    Protected _update As Update
    Protected _api As Api
    Protected _params As String()

    Public Sub New(update As Update, api As Api, params As String())
        Me._update = update
        Me._api = api
        Me._params = params
    End Sub

    Public MustOverride Sub Execute() Implements ICommand.Execute



End Class
