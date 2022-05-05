using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramTestBot.BL.Questions
{
    public class YesNoQuestion : AbstractQuestions
    {
        public YesNoQuestion(string content) : base(content)
        {
            Answers = new List<string>()
            {
                "Нет",
                "Да",
            };
            TypeOfQuestion = 4;
        }
    }
}
