using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramTestBot.BL.Questions
{
    internal class ChooseAnyQuestion : AbstractQuestion
    {
        public ChooseAnyQuestion(string content) : base(content)
        {
            TypeOfQuestion = 0;
        }
    }
} 
