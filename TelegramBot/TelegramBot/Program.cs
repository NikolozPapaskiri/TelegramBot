using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

class Program
{
    // The token you received from BotFather to authenticate your bot with Telegram
    private static readonly string BotToken = "YOUR_BOT_API_TOKEN";
    // Initialize the TelegramBotClient with your bot token
    private static readonly TelegramBotClient BotClient = new TelegramBotClient(BotToken);

    static async Task Main(string[] args)
    {
        
    }

    // This method handles incoming updates from Telegram
    private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        // Check if the incoming update is a message and if that message contains text
        if (update.Message is not { Text: { } } message)
            return; // If not a text message, do nothing

        // Log the incomeing messages to the console
        Console.WriteLine($"Received a text message from {message.Chat.FirstName}: {message.Text}");

        // Replay to the received message with the same text
        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id, //the chat id to send the message to
            text: "Hello! You said: " + message.Text, //the message text
            cancellationToken: cancellationToken //Cancellation token for async operations
            );
    }

    // This method handles errors that occur during bot operations
    private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        // Determine the type of exception and format an appropriate error message
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}", // Handle API errors
            _ => exception.ToString() // Handle other types of exceptions
        };

        // Log the error message to the console
        Console.WriteLine(errorMessage);
        return Task.CompletedTask; // Complete the task without throwing
    }
}
