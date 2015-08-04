Imports Telegram.Bot.Types
Imports Telegram.Bot
Imports System.Net.Http

Public Class WheaterCommand
    Inherits BotCommand


    Public Sub New(update As Update, api As Api, params As String())
        MyBase.New(update, api, params)
    End Sub

    Private Const urlWheater As String = "http://www.webservicex.net/globalweather.asmx/GetWeather?CityName={0}&CountryName={1}"
    Private Const NoData As String = "Data Not Found"


    Public Overrides Sub Execute()

        If _params.Length < 2 Then
            Me._api.SendTextMessage(Me._update.Message.Chat.Id, "parametros imcompletos")
            Return
        End If

        Using httpClient As New HttpClient()
            Dim result = httpClient.GetAsync(String.Format(urlWheater, _params(0), _params(1))).Result()
            If result.StatusCode = Net.HttpStatusCode.OK Then
                Using stream As System.IO.Stream = result.Content.ReadAsStreamAsync().Result
                    Dim xdoc As XDocument = XDocument.Load(stream)
                    If xdoc.Root.Value = NoData Then
                        Me._api.SendTextMessage(Me._update.Message.Chat.Id, "No se encontró el información :(")
                    Else
                        Dim innerDoc As XDocument = XDocument.Parse(xdoc.Root.Value)
                        Dim temperatura As String = innerDoc.Root.Descendants.Single(Function(node) node.Name = "Temperature").Value
                        Dim ciudad As String = innerDoc.Root.Descendants.Single(Function(node) node.Name = "Location").Value
                        Me._api.SendTextMessage(_update.Message.Chat.Id, String.Format("La Temperatura es {0}  : {1}", temperatura, ciudad))
                    End If
                End Using
            End If

        End Using

    End Sub

End Class
