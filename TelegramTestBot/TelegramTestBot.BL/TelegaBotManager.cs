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
        private List<long> _ids;  //для отправки сообщений от бота человеку
        


        public TelegaBotManager(string token, Action<string> onMessage)
        {
            _client = new TelegramBotClient(token);
            _onMessage = onMessage;
            _ids = new List<long>();
        }

        public void StartBot()
        {
            _client.StartReceiving(HandleUpdateAsync, HandleErrorAsync);
            
        }

        public void StopBot()
        {
            
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

        public async void StartingButton()
        {
            foreach (var id in _ids)
            {
                var inlineKeyboard = new InlineKeyboardMarkup(new[]
                {
                    InlineKeyboardButton.WithCallbackData("/start","startReg"),
                });

                await _client.SendTextMessageAsync(new ChatId(id), "Hello, this bot create for DevEdu", replyMarkup: inlineKeyboard);
            }

        }

        public async void Registration()
        {
            foreach (var id in _ids)
            {
                var keyboard = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton("Registration"),
                });

                keyboard.OneTimeKeyboard = true;
                await _client.SendTextMessageAsync(new ChatId(id), "Please choose the button", replyMarkup: keyboard);
            }
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            int index = 0;
           
            if (update.Message != null && update.Message.Text != null && !_ids.Contains(update.Message.Chat.Id))
            {
                //if (update.CallbackQuery.Data == "startReg")
                //{
                //    await _client.SendTextMessageAsync(update.Message.Chat, "Please choose the button");
                //    Registration();
                //    return;
                //}

                
                _ids.Add(update.Message.Chat.Id);

                StartingButton();
                


                //if (update.Message.Text == "Registration")
                //{
                //    string s = BaseOfUsers.NameBase;
                //    _onMessage(s);

                //    await _client.SendTextMessageAsync(update.Message.Chat, "Registration successfull!");

                //    return;
                //}

            }
            else if (update.CallbackQuery != null)
            {
                await botClient.EditMessageTextAsync(
                    update.CallbackQuery.Message.Chat.Id,
                    update.CallbackQuery.Message.MessageId,
                    update.CallbackQuery.Message.Text,
                    replyMarkup: null);

                Registration();
            }
            else if (!BaseOfUsers.DataBase.ContainsKey(update.Message.Chat.Id) && update.Message.Text == "Registration")
            {
                BaseOfUsers.DataBase.Add(update.Message.Chat.Id, new UserModel() { Chat = update.Message.Chat });
                BaseOfUsers.NameBase.Add(update.Message.Chat.Id, new UserModel() { Name = update.Message.Chat.Username });

                string s = update.Message.Chat.Username;
                _onMessage(s);

                await _client.SendTextMessageAsync(update.Message.Chat, "Registration successfull!");
                return;
            }
        }

        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
