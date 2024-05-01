using Telegram;
using Telegram.Bot;
using MyTelegramBotFunction;

namespace MyTelegramBot
{
    class Program
    {
        static void Main()
        {
            string token = "6952128183:AAF1nG5NxlWR6FQYbOrOmvevOz_G75hRFsU";

            var client = new Bot(token);

            client.Start();

            Console.WriteLine("Bot is started!");

            Console.WriteLine("Нажмите любую клавишу, чтобы завершить работу Телеграм Бота");

            Console.ReadKey();

        }
    }
}