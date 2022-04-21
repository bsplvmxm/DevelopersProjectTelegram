using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramTestBot.BL
{
    class User
    {
        public string UserName { get; set; }

        public long ChatId { get; private set; }

        public User(string userName)
        {
            UserName = userName;
        }

        public override string ToString()
        {
            return UserName;
        }

    }
}
