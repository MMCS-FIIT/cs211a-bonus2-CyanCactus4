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

        public void End() => this.client.CloseAsync();

        public async Task updateHandler(ITelegramBotClient client, Update happening, CancellationToken token)
        {

            UpdateType upd = happening.Type;

            Console.WriteLine($"{upd}");

            try
            {
                switch (upd)
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
                            long idm = happening.Message.Chat.Id;
                            await client.SendAnimationAsync(idm, InputFile.FromUri("https://i.pinimg.com/originals/6e/d9/4e/6ed94eeba9ab632f3366c1cc387ba8fe.gif"));
                            await client.SendTextMessageAsync(idm, text: "Хеллоу, пользователь! Я - любитель игры в города. " +
                                "В любое время ты можешь посоревноваться со мной на знания разных уголков планеты. " +
                                "Ты готов сейчас сыграть со мной в игру?", replyMarkup: keyboard);

                        }

                        break;
                    case UpdateType.CallbackQuery:
                        var callback = happening.CallbackQuery;

                        await client.AnswerCallbackQueryAsync(callback.Id);

                        if (callback.Data.Equals("start1"))
                        {

                        }
                        if (callback.Data.Equals("end1"))
                        {
                            var idc = callback.Message.Chat.Id;
                            var keyboard = new InlineKeyboardMarkup(
                                new List<InlineKeyboardButton[]>{
                                new InlineKeyboardButton[]
                                {
                                    InlineKeyboardButton.WithCallbackData(text: "Я всё-таки хочу сыграть с тобой в игру", callbackData: "end2")
                                }
                                });

                            await client.SendAnimationAsync(idc, InputFile.FromUri("https://i.pinimg.com/originals/59/6c/ef/596cef7702de5ab2f1559e6fa5236dcf.gif"));
                            await client.SendTextMessageAsync(idc, text: "Очень жаль, я бы была очень рада с тобой поиграть в города... Если захочешь - возвращайся. До скорых встреч!", replyMarkup: keyboard);
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
