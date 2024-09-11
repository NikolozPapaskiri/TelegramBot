using System;
using Telegram.Bot;
using Telegram.Bot.Args;

class Program
{
    private static readonly string BotToken = "YOUR_BOT_API_TOKEN"; // Replace with your bot token
    private static readonly TelegramBotClient BotClient = new TelegramBotClient(BotToken);

    static void Task Main(string[] args)
    {
        BotClient.message += Bot_OnMessage;
        BotClient.StartReceiving();

        Console.WriteLine("Bot is up and running... Press any key to exit");
        Console.ReadKey();

        BotClient.StopReceiving();
    }

    private static async void Bot_OnMessage(object sender, MessageEventArgs e)
    {
        if (e.Message.Text != null)
        {
            Console.WriteLine($"Received a text message from {e.Message.Chat.FirstName}: {e.Message.Text}");

            await BotClient.SendTextMessageAsync(
                chatId: e.Message.Chat,
                text: "Hello! You said: " + e.Message.Text
            );
        }
    }
}
