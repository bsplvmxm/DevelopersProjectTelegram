using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramTestBot.BL
{
    public class UserModel
    {
        public string Name { get; set; }
        public bool Reg { get; set; }
        public List<string> UsersInGroup { get; set; } = new List<string>();
        public Chat Chat { get; set; }
        public string Test { get; set; }
        public long ChatId { get; set; }
        public List<string> Answers { get; set; } = new List<string>();
    }
}
