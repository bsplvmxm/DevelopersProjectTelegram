using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramTestBot.BL.Questions
{
    internal class ChooseOneQuestion : AbstractQuestion
    {
        public ChooseOneQuestion(string content) : base(content)
        {
            TypeOfQuestion = 1;
        }
    }
}
