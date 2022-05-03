using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramTestBot.BL.Questions
{
    public class ChooseAnyQuestion : AbstractQuestions
    {
        public ChooseAnyQuestion(string content) : base(content)
        {
            TypeOfQuestion = 0;
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
