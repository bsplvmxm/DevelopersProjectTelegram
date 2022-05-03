using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramTestBot.BL.Questions
{
    public class ChooseOneQuestion : AbstractQuestions
    {
        public ChooseOneQuestion(string content) : base(content)
        {
            TypeOfQuestion = 1;
            Answers = new List<string>()
            {
                "Вариант 1",
                "Вариант 2",
                "Вариант 3",
                "Вариант 4",
            };
        }
    }
}
