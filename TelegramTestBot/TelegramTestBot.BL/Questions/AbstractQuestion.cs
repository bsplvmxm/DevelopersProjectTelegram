using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramTestBot.BL
{
    public class AbstractQuestion
    {
        protected string _question_content;

        protected string _answer_from_user;

        protected string _correct_answer;

        protected List<string> _answers;

        public AbstractQuestion(string content)
        {
            _question_content = content;
        }

        public void AddAnswer(string answer)
        {
            if (answer == null)
            {
                throw new ArgumentNullException();
            }
            _answers.Add(answer);
        }
        public void EditAnswer(int index, string answer)
        {
            if (index < 0 || index >= _answers.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            _answers[index] = answer;
        }

        public void DeleteAnswer(int index)
        {
            if (_answers.Count == 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            _answers.RemoveAt(index);
        }


    }
}
