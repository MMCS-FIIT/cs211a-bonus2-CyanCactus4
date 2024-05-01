using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Telegram;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyTelegramBotFunction
{
    public class Bot
    {
        string? code;
        TelegramBotClient client;

        public Bot(string line) => (code, client) = (line, new TelegramBotClient(line));

        public async Task Start() => client.StartReceiving(updateHandler, ErrorOptions);

        private async Task updateHandler(ITelegramBotClient client, Update happening, CancellationToken token)
        {
            try
            {
                switch (happening.Type)
                {
                    case UpdateType.Message:
                        var message = happening.Message;
                        if (message.Text == "/start")
                        {
                            var keyboard = new InlineKeyboardMarkup(
                                new List<InlineKeyboardButton[]>{
                                new InlineKeyboardButton[]
                                {
                                    InlineKeyboardButton.WithCallbackData(text: "Да, я готов!", callbackData: "start"),
                                    InlineKeyboardButton.WithCallbackData(text: "Нет, я чушпан...", callbackData: "end")
                                }
                                });

                            await client.SendAnimationAsync(message.Chat.Id, InputFile.FromUri("https://i.pinimg.com/originals/6e/d9/4e/6ed94eeba9ab632f3366c1cc387ba8fe.gif"));
                            await client.SendTextMessageAsync(message.Chat.Id, text: "Хеллоу, пользователь! Я - любитель игры в города. " +
                                "В любое время ты можешь посоревноваться со мной на знания разных уголков планеты. " +
                                "Ты готов сейчас сыграть со мной в игру?", replyMarkup: keyboard);
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                ErrorOptions(client, e, new CancellationToken());
            }
        }

        private Task ErrorOptions(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
