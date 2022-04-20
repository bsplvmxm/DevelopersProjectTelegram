using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleBot.Logic.Questions
{
    internal class PollQuestion : AbstractQuestion
    {
        public PollQuestion(string content) : base(content)
        {
        }
    }
}
