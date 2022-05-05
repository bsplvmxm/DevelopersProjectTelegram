using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramTestBot.BL
{
    public static class BaseOfUsers
    {
        public static Dictionary<long, string> NameBase { get; set; } = new Dictionary<long, string>();
        public static Dictionary<long, bool> RegBase { get; set; } = new Dictionary<long, bool>();
        public static Dictionary<string, List<string>> GroupBase { get; set; } = new Dictionary<string, List<string>>();
        public static Dictionary<long, List<string>> UserAnswers { get; set; } = new Dictionary<long, List<string>>();
    }
}
