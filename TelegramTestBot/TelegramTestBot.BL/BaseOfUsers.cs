using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramTestBot.BL
{
    public static class BaseOfUsers
    {
        public static Dictionary<long, UserModel> DataBase { get; set; } = new Dictionary<long, UserModel>();
        public static Dictionary<string, UserModel> NameBase { get; set; } = new Dictionary<string, UserModel>();
    }
}
