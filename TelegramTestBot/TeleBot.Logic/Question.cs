using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleBot.Logic
{
    internal class Question
    {
        public string _question_content;

        public List<string> answers;

        public Question(string content)
        {
            _question_content = content;
        }

        public void AddAnswer(string answ)
        {
            answers.Add(answ);
        }
        public void DeleteAnswer()
        {

        }
    }
}
