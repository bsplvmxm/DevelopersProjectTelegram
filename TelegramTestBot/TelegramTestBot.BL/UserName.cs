using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramTestBot.BL
{
    public class UserName
    {
        public static string Name { get; private set; }
        public long ChatId { get; private set; }

        public static void ChangeName(string realName)
        {
            if (realName == null)
            {
                throw new NullReferenceException();
            }

            Name = realName;
        }
    }
}
