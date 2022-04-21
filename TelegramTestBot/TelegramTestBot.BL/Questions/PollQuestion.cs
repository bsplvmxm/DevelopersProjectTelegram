using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramTestBot.BL.Questions
{
    internal class PollQuestion : AbstractQuestion
    {
        public string UserPollAnswer { get; set; }
        public PollQuestion(string content) : base(content)
        {
        }
    }
}
