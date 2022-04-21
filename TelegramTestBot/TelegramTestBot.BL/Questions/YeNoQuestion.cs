using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramTestBot.BL.Questions
{
    public class YeNoQuestion : AbstractQuestion
    {
        public YeNoQuestion(string content) : base(content)
        {
            Answers = new List<string>() { "No", "Yes" };
        }
    }
}
