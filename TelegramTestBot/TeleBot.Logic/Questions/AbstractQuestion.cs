using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleBot.Logic.Questions
{
    public abstract class AbstractQuestion
    {
        public string _question_content;

        public int UserAnswer { get; set; }

        public int CorrectAnswer { get; set; }

        public List<string> Answers { get; set; }

        public AbstractQuestion(string content)
        {
            _question_content = content;
        }

        public void AddAnswer(string answer)
        {
            if (answer == null)
            {
                throw new ArgumentNullException("Gotta write something");
            }
            Answers.Add(answer);
        }
        public void EditAnswer(int index, string answer)
        {
            if (Answers.Count == 0)
            {
                throw new Exception("Answers does not exist now");
            }
            if (index< 0 || index >= Answers.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            Answers[index] = answer;
        }

        public void DeleteAnswer(int index)
        {
            if (Answers.Count == 0)
            {
                throw new Exception("Answers does not exist now");
            }
            if (index < 0 || index > Answers.Count)
            {
                throw new IndexOutOfRangeException("Wrong index");
            }
            Answers.RemoveAt(index);
        }

        public void ChooseCorrect(int index)
        {
            if (index <0 || index > Answers.Count)
            {
                throw new ArgumentException("This answer does not exist");
            }
            CorrectAnswer = index;
        }


    }
}
