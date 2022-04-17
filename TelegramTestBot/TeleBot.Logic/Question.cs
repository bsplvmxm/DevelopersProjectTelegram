using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleBot.Logic
{
    public class Question
    {
        protected string _question_content;

        protected List<string> _answers;

        public Question(string content)
        {
            _question_content = content;
        }

    }
}
