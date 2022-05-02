using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramTestBot.BL.Questions
{
    public class CourseQuestion : AbstractQuestions
    {
        public CourseQuestion(string content) : base(content)
        {
            TypeOfQuestion = 2;
            Answers = new List<string>()
            {
                "Элемент 1",
                "Элемент 2",
                "Элемент 3",
                "Элемент 4",
            };
        }
    }
}
