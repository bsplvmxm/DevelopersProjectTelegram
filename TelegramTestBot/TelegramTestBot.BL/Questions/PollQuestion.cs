using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramTestBot.BL.Questions
{
    public class PollQuestion : AbstractQuestions
    {
        public PollQuestion(string content) : base(content)
        {
            TypeOfQuestion = 3;
            CorrectAnswer = "Без правильного ответа";
        }
    }
}
