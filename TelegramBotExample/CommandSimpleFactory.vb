Imports System.Reflection
Imports Telegram.Bot.Types
Imports Telegram.Bot

Public Class CommandSimpleFactory
    Private ReadOnly _aviableCommands As Dictionary(Of String, Type)

    Public Sub New()
        Me._aviableCommands = DiscoverCommandTypes()
    End Sub

    Private Function CreateInstance(type As Type, update As Update, api As Api) As Object
        Return Activator.CreateInstance(type, update, api)
    End Function

    Private Function CreateInstance(type As Type, update As Update, api As Api, params As String()) As Object
        If type.GetConstructors().Any(Function(constructor) constructor.GetParameters.Any(Function(p) p.Name = "params" AndAlso p.ParameterType Is GetType(String()))) Then
            Return Activator.CreateInstance(type, update, api, params)
        Else
            Return Activator.CreateInstance(type, update, api)
        End If
    End Function

    Public Function Create(commandText As String, update As Update, api As Api) As ICommand
        If String.IsNullOrWhiteSpace(commandText) Then
            Return New NotFoundCommand(update, api)
        End If
        Dim command As String = ExtractCommand(commandText)
        If Me._aviableCommands.ContainsKey(command) Then
            Dim parameters As String() = ExtractParameters(commandText)
            Return CreateInstance(Me._aviableCommands(command), update, api, parameters)
        Else
        Return New NotFoundCommand(update, api)
        End If
    End Function

    Private Function DiscoverCommandTypes() As Dictionary(Of String, Type)
        Dim currentAssembly As Assembly = Assembly.GetExecutingAssembly()
        Dim aviableCommands As Dictionary(Of String, Type) = currentAssembly.GetTypes.Where(Function(t) t.IsClass AndAlso t.Name <> "NotFoundCommand" AndAlso t.BaseType.Name = "BotCommand").ToDictionary(Of String, Type)(Function(t) GetCommandTransformation(t.Name), Function(t) t)
        Return aviableCommands
    End Function

    Private Function GetCommandTransformation(commandText As String) As String
        Return String.Format("/{0}", commandText.Replace("Command", "").ToLower())
    End Function


    Private Function ExtractParameters(commandText As String) As String()
        Dim parameters = commandText.Split(New String() {" "}, System.StringSplitOptions.RemoveEmptyEntries).Skip(1).ToArray()
        Return parameters
    End Function

    Private Function ExtractCommand(commandText As String) As String
        Return commandText.Split(New String() {" "}, System.StringSplitOptions.RemoveEmptyEntries).First().ToLower()
    End Function


End Class
