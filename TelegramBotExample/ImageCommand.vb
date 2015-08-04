Imports Telegram.Bot.Types
Imports Telegram.Bot

Public Class ImageCommand
    Inherits BotCommand

    Public Sub New(update As Update, api As Api)
        MyBase.New(update, api, Nothing)
    End Sub

    Public Overrides Sub Execute()
        Dim ms As New System.IO.FileStream("d:\ioannis.jpg", IO.FileMode.Open, IO.FileAccess.Read)
        ms.Position = 0
        Dim fileToSend As New FileToSend("ioannis.jpg", ms)
        Dim image = Me._api.SendPhoto(Me._update.Message.Chat.Id, fileToSend).Result
    End Sub
End Class
