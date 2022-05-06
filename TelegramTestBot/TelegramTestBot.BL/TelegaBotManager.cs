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
using TelegramTestBot.BL.Data;
using TelegramTestBot.BL.Questions;


namespace TelegramTestBot.BL
{
    public class TelegaBotManager
    {
        private TelegramBotClient _client;
        private Action<string> _onMessage;
        private string _others;
        public bool IsTesting { get; set; } = false;
        public int IndexOfTest { get; set; }
        private int _indexOfQuest;
        
        public TelegaBotManager(string token, Action<string> onMessage)
        {
            _client = new TelegramBotClient(token);
            _onMessage = onMessage;
            _others = "Others";
        }

        public void StartBot()
        {
            _client.StartReceiving(HandleUpdateAsync, HandleErrorAsync);
        }

        public void StopBot()
        {
            
        }

        public void AddUserInGroup(string nameOfGroup, string nameOfUser)
        {
            if (BaseOfUsers.GroupBase.ContainsKey(nameOfGroup) && !BaseOfUsers.GroupBase[nameOfGroup].Contains(nameOfUser))
            {
                foreach (var users in BaseOfUsers.GroupBase)
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
            if (BaseOfUsers.GroupBase.ContainsKey(nameOfGroup))
            {
                foreach (var items in BaseOfUsers.GroupBase[nameOfGroup])
                {
                    string outputUsers = items;
                    _onMessage(outputUsers);
                }
            }
        }

        public void ClearUserAnswers()
        {
            foreach (var user in BaseOfUsers.UserAnswers)
            {
                BaseOfUsers.UserAnswers.Remove(user.Key);
            }
        }

        public async void SendToUser(long id)
        {
            if (BaseOfUsers.NameBase.ContainsKey(id) && BaseOfUsers.RegBase[id] == true)
            {
                var inlineKeyboard = new InlineKeyboardMarkup(new[]
                {
                InlineKeyboardButton.WithCallbackData("ДА", "yes"),
                InlineKeyboardButton.WithCallbackData("НЕТ", "no"),
                });

                await _client.SendTextMessageAsync(new ChatId(id), "Ты хочешь начать тестирование?", replyMarkup: inlineKeyboard);
            }
            else
            {
                await _client.SendTextMessageAsync(new ChatId(id), "Sorry, bro, go for a walk, this test not to u");
            }
        }       

        private async void SendNextQuestion(long id, int i)
        {
            TestsBase tests = TestsBase.GetInstance();
            Test currentTest = tests.AllTests[IndexOfTest];
            
            if (i <= currentTest.Questions.Count-1 && BaseOfUsers.UserAnswers.ContainsKey(id))
            {
                string currentQuestion;
                int typeOfQuest;

                currentQuestion = currentTest.Questions[i].ContentOfQuestion;
                typeOfQuest = currentTest.Questions[i].TypeOfQuestion;

                switch (typeOfQuest)
                {
                    case 0:
                        {
                            await _client.SendTextMessageAsync(new ChatId(id), $"{currentQuestion} \n" +
                                $"Выберите несколько вариантов ответов: \n " +
                                $"1.{currentTest.Questions[i].Answers[0]} \n " +
                                $"2.{currentTest.Questions[i].Answers[1]} \n " +
                                $"3.{currentTest.Questions[i].Answers[2]} \n " +
                                $"4.{currentTest.Questions[i].Answers[3]}");
                            
                            break;
                        }
                    case 1:
                        {
                            var oneAnsKeyboard = new ReplyKeyboardMarkup(new[]
                            {
                                new[]
                                {
                                    new KeyboardButton($"{currentTest.Questions[i].Answers[0]}"),
                                    new KeyboardButton($"{currentTest.Questions[i].Answers[1]}"),
                                },
                                new[]
                                {
                                    new KeyboardButton($"{currentTest.Questions[i].Answers[2]}"),
                                    new KeyboardButton($"{currentTest.Questions[i].Answers[3]}"),
                                },                          
                            });

                            oneAnsKeyboard.OneTimeKeyboard = true;

                            await _client.SendTextMessageAsync(new ChatId(id), $"{currentQuestion}", replyMarkup: oneAnsKeyboard);

                            break;
                        }
                    case 2:
                        {
                            await _client.SendTextMessageAsync(new ChatId(id), $"{currentQuestion} \n " +
                                $"1.{currentTest.Questions[i].Answers[0]} \n " +
                                $"2.{currentTest.Questions[i].Answers[1]} \n " +
                                $"3.{currentTest.Questions[i].Answers[2]} \n " +
                                $"4.{currentTest.Questions[i].Answers[3]}");

                            break;
                        }
                    case 3:
                        {
                            await _client.SendTextMessageAsync(new ChatId(id), $"{currentQuestion}");

                            break;
                        }
                    case 4:
                        {
                            var yesNoKeyboard = new ReplyKeyboardMarkup(new[]
                            {
                                new KeyboardButton($"{currentTest.Questions[i].Answers[1]}"),
                                new KeyboardButton($"{currentTest.Questions[i].Answers[0]}"),
                            });

                            yesNoKeyboard.OneTimeKeyboard = true;

                            await _client.SendTextMessageAsync(new ChatId(id), $"{currentQuestion}", replyMarkup: yesNoKeyboard);

                            break;
                        }
                }            
            }
            else
            {
                await _client.SendTextMessageAsync(new ChatId(id), "Congratulation! Test complete, pls wait results!", replyMarkup: null);
            }
        }

        private async void StartingButton(long id)
        {
            if (BaseOfUsers.NameBase.ContainsKey(id))
            {
                var inlineKeyboard = new InlineKeyboardMarkup(new[]
                {
                InlineKeyboardButton.WithCallbackData("/start","startReg"),
                });

                await _client.SendStickerAsync(new ChatId(id), "CAACAgIAAxkBAAEEouxidGLH0wa7ThpPtGtfyWCqxNx1mgACshMAAhic8EgWPkoMU7RoeiQE");
                await _client.SendTextMessageAsync(new ChatId(id), "Hello, this bot create for DevEdu \nby Developers team", replyMarkup: inlineKeyboard);
            }
        }

        private async void Registration(long id)
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
                if (IsTesting == true && update.CallbackQuery.Data == "yes")
                {
                    await botClient.EditMessageTextAsync(
                        update.CallbackQuery.Message.Chat.Id,
                        update.CallbackQuery.Message.MessageId,
                        update.CallbackQuery.Message.Text,
                        replyMarkup: null);

                    BaseOfUsers.UserAnswers.Add(update.CallbackQuery.Message.Chat.Id, new List<string>());
                    
                    SendNextQuestion(update.CallbackQuery.Message.Chat.Id, 0);
                }
                else if (update.CallbackQuery.Data == "startReg")
                {
                    await botClient.EditMessageTextAsync(
                        update.CallbackQuery.Message.Chat.Id,
                        update.CallbackQuery.Message.MessageId,
                        update.CallbackQuery.Message.Text,
                        replyMarkup: null);

                    Registration(update.CallbackQuery.Message.Chat.Id);
                }
                else if (IsTesting == true && update.CallbackQuery.Data == "no")
                {
                    await botClient.EditMessageTextAsync(
                        update.CallbackQuery.Message.Chat.Id,
                        update.CallbackQuery.Message.MessageId,
                        update.CallbackQuery.Message.Text,
                        replyMarkup: null);

                    await botClient.SendStickerAsync(update.CallbackQuery.Message.Chat.Id, "CAACAgIAAxkBAAEEoupidGCkWBUOmj1KdSSsjeGADZhtWAACUhMAAjFuOUgEv3MHYx5zkyQE");
                }
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

            else if (IsTesting == true && BaseOfUsers.UserAnswers.ContainsKey(update.Message.Chat.Id) && update.Message.Text != null)
            {
                BaseOfUsers.UserAnswers[update.Message.Chat.Id].Add(update.Message.Text);
                _indexOfQuest = BaseOfUsers.UserAnswers[update.Message.Chat.Id].Count;

                SendNextQuestion(update.Message.Chat.Id, _indexOfQuest);
            }
        }

        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
