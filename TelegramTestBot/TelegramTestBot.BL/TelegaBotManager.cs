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
        //private List<string> _users;
        //private List<Groups> _group;
        private string _others;
        


        public TelegaBotManager(string token, Action<string> onMessage)
        {
            _client = new TelegramBotClient(token);
            _onMessage = onMessage;
            //_ids = new List<long>();
            //_users = new List<string>();
            _others = "Others";
        }

        public void StartBot()
        {
            _client.StartReceiving(HandleUpdateAsync, HandleErrorAsync);        
        }

        public void StopBot()
        {
            
        }

        public async void StartingButton(long id)
        {
            if (BaseOfUsers.NameBase.ContainsKey(id))
            {
                var inlineKeyboard = new InlineKeyboardMarkup(new[]
                {
                InlineKeyboardButton.WithCallbackData("/start","startReg"),
                });

                await _client.SendTextMessageAsync(new ChatId(id), "Hello, this bot create for DevEdu", replyMarkup: inlineKeyboard);
            }
        }

        public void EditUserName(string username)
        {
            
        }

        public void OutputUser()
        {           
            foreach (KeyValuePair<long, string> regs in BaseOfUsers.NameBase)
            {
                if (BaseOfUsers.RegBase.ContainsKey(regs.Key))
                {
                    string regUsers = regs.Value;
                    _onMessage(regUsers);
                }
            }
        }

        public void AddUserInGroup(string nameOfGroup, string nameOfUser)
        {
            if (BaseOfUsers.GroupBase.ContainsKey(nameOfGroup) && !BaseOfUsers.GroupBase[nameOfGroup].Contains(nameOfUser))
            {
                foreach (KeyValuePair<string, List<string>> users in BaseOfUsers.GroupBase)
                {
                    if (users.Value.Contains(nameOfUser))
                    {
                        string name = users.Key;
                        DeleteUserFromGroup(name, nameOfUser);
                    }
                }

                BaseOfUsers.GroupBase[nameOfGroup].Add(nameOfUser);
            }            
        }

        public void DeleteUserFromGroup(string nameOfGroup, string nameOfUser)
        {
            if (BaseOfUsers.GroupBase.ContainsKey(nameOfGroup) && BaseOfUsers.GroupBase[nameOfGroup].Contains(nameOfUser))
            {
                BaseOfUsers.GroupBase[nameOfGroup].Remove(nameOfUser);
            }
        }

        public void CreateGroup(string nameOfGroup)
        {
            if (!BaseOfUsers.GroupBase.ContainsKey(nameOfGroup))
            {
                BaseOfUsers.GroupBase.Add(nameOfGroup, new List<string>());
            }
        }

        public void DeleteGroup(string nameOfGroup)
        {

            if (BaseOfUsers.GroupBase.ContainsKey(nameOfGroup))
            {
                foreach (var users in BaseOfUsers.GroupBase[nameOfGroup])
                {
                    BaseOfUsers.GroupBase[_others].Add(users);
                }

                BaseOfUsers.GroupBase.Remove(nameOfGroup);
            }
        }

        public void OutputUsersInGroup(string nameOfGroup)
        {
            //foreach (KeyValuePair<string, List<string>> users in BaseOfUsers.GroupBase)
            //{
            //    if (BaseOfUsers.GroupBase.ContainsKey(nameOfGroup))
            //    {
            //        for (int i = 0; i < users.Value.Count; i++)
            //        {                        
            //            string outputUsers = users.Value[i];
            //            _onMessage(outputUsers);
            //        }
            //    }
            //}
            if (BaseOfUsers.GroupBase.ContainsKey(nameOfGroup))
            {
                foreach (var items in BaseOfUsers.GroupBase[nameOfGroup])
                {
                    string outputUsers = items;
                    _onMessage(outputUsers);
                }
            }
        }

        public async void Registration(long id)
        {
            if (!BaseOfUsers.RegBase.ContainsKey(id))
            {
                var keyboard = new ReplyKeyboardMarkup(new[]
                {
                    new KeyboardButton("Registration"),
                });

                keyboard.OneTimeKeyboard = true;
                BaseOfUsers.RegBase.Add(id, false);
                
                await _client.SendTextMessageAsync(new ChatId(id), "Please choose the button", replyMarkup: keyboard);
            }
            else
            {
                await _client.SendTextMessageAsync(new ChatId(id), "Sorry, bro, u already saved in reg list");
            }
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {          
            if (update.Message != null && update.Message.Text != null && !BaseOfUsers.NameBase.ContainsKey(update.Message.Chat.Id))
            {              
                CreateGroup(_others);

                BaseOfUsers.NameBase.Add(update.Message.Chat.Id, update.Message.Chat.Username);
                
                StartingButton(update.Message.Chat.Id);               
            }
            else if (update.CallbackQuery != null)
            {
                await botClient.EditMessageTextAsync(
                    update.CallbackQuery.Message.Chat.Id,
                    update.CallbackQuery.Message.MessageId,
                    update.CallbackQuery.Message.Text,
                    replyMarkup: null);

                Registration(update.CallbackQuery.Message.Chat.Id);
            }
            else if (BaseOfUsers.RegBase.ContainsValue(false))
            {
                BaseOfUsers.GroupBase[_others].Add(update.Message.Chat.Username);
                BaseOfUsers.RegBase[update.Message.Chat.Id] = true;
                string s = update.Message.Chat.Username;
                _onMessage(s);

                await _client.SendTextMessageAsync(update.Message.Chat, "Registration successfull!", replyMarkup: null);
                return;
            }
        }

        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
