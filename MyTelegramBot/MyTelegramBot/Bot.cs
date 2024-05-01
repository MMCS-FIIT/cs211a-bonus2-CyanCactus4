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

        public void Start() => this.client.StartReceiving(updateHandler, ErrorOptions);

        public async Task updateHandler(ITelegramBotClient client, Update happening, CancellationToken token)
        {
            long id = happening.Message.Chat.Id;

            Console.WriteLine($"{id}");

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
                                    InlineKeyboardButton.WithCallbackData(text: "Да, я готов!", callbackData: "start1"),
                                    InlineKeyboardButton.WithCallbackData(text: "Нет, я чушпан...", callbackData: "end1")
                                }
                                });

                            await client.SendAnimationAsync(id, InputFile.FromUri("https://i.pinimg.com/originals/6e/d9/4e/6ed94eeba9ab632f3366c1cc387ba8fe.gif"));
                            await client.SendTextMessageAsync(id, text: "Хеллоу, пользователь! Я - любитель игры в города. " +
                                "В любое время ты можешь посоревноваться со мной на знания разных уголков планеты. " +
                                "Ты готов сейчас сыграть со мной в игру?", replyMarkup: keyboard);

                        }

                        break;
                    case UpdateType.CallbackQuery:
                        var querry = happening.CallbackQuery;

                        await client.AnswerCallbackQueryAsync(querry.Id);

                        if (querry.Equals("start1"))
                        {

                        }
                        if (querry.Equals("end1"))
                        {

                            var keyboard = new InlineKeyboardMarkup(
                                new List<InlineKeyboardButton[]>{
                                new InlineKeyboardButton[]
                                {
                                    InlineKeyboardButton.WithCallbackData(text: "Я всё-таки хочу сыграть с тобой в игру", callbackData: "end2")
                                }
                                });

                            await client.SendAnimationAsync(id, InputFile.FromUri("https://i.pinimg.com/originals/59/6c/ef/596cef7702de5ab2f1559e6fa5236dcf.gif"));
                            await client.SendTextMessageAsync(id, text: "Очень жаль, я бы была очень рада с тобой поиграть в города... Если захочешь - возвращайся. До скорых встреч!", replyMarkup: keyboard);
                        }


                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public Task ErrorOptions(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
