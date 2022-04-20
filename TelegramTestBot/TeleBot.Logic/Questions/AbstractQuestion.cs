using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleBot.Logic.Questions
{
    public class AbstractQuestion
    {
        public string _question_content;

        public string UserAnswer { get; set; }

        public int _correct_answer;

        public List<string> _answers;

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
            if (index< 0 || index >= _answers.Count)
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

        public void ChooseCorrect(int index)
        {
            _correct_answer = index;
        }


    }
}
