using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramTestBot.BL
{
    public class TelegaBotManager
    {
        private TelegramBotClient _client;
        private Action<string> _onMessage;
        //private List<long> _ids;  //для отправки сообщений от бота человеку


        public TelegaBotManager(string token, Action<string> onMessage)
        {
            _client = new TelegramBotClient(token);
            _onMessage = onMessage;
            //_ids = new List<long>();
        }

        public void StartBot()
        {
            _client.StartReceiving(HandleUpdateAsync, HandleErrorAsync);
        }

        //public void Send(string s)
        //{
        //    foreach (var id in _ids)
        //    {
        //        InlineKeyboardMarkup inlineKeyboard = new( //кнопочки под сообщением от бота
        //            new[]
        //            {
        //                new []
        //                {
        //                    InlineKeyboardButton.WithCallbackData("1","privet"),
        //                    InlineKeyboardButton.WithCallbackData("2","poka"),
        //                    InlineKeyboardButton.WithCallbackData("3","textttt"),
        //                },
        //                new[]
        //                {
        //                    InlineKeyboardButton.WithCallbackData("4","qgqg"),
        //                    InlineKeyboardButton.WithCallbackData("5","ggggg"),
        //                },
        //            });

        //        _client.SendTextMessageAsync(new ChatId(id), s, replyMarkup: inlineKeyboard);
        //    }
        //}

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {       
            if (update.Message != null && update.Message.Text != null)
            {
                //if (!_ids.Contains(update.Message.Chat.Id))
                //{
                //    _ids.Add(update.Message.Chat.Id);
                //}

                string s = update.Message.Chat.FirstName+" "+update.Message.Text;

                _onMessage(s);
            }
        }

        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
