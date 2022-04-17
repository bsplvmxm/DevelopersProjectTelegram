using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


namespace TelegramTestBot.BL
{
    public class TelegaBot
    {
        private TelegramBotClient _client;
        private MainWindow _window;

        public TelegaBot()
        {
            _client = new TelegramBotClient("5277457802:AAG5dI1aiAEQYGt08OVjn5snSkX1qbzkc7s");

            _client.StartReceiving(HandleUpdateAsync, );
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type != UpdateType.Message)
            {
                return;
            }

            if (update.Message.Type != MessageType.Text)
            {
                return;
            }

            var chatId = update.Message.Chat.Id;
            var nickname = update.Message.Chat.Username;
            var messageText = update.Message.Text;
        }

        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
