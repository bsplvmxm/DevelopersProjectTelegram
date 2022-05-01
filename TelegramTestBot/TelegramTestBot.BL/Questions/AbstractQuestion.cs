using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramTestBot.BL
{
    public abstract class AbstractQuestion
    {
        public string _question_content;

        public string UserAnswer { get; set; }

        public string CorrectAnswer { get; protected set; }

        public List<string> Answers { get; set; }

        public int TypeOfQuestion { get; protected set; }

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
            answer = answer.Trim();
            Answers.Add(answer);
            CorrectAnswer = "Введите правильный ответ";
        }
        public void EditAnswer(int index, string answer)
        {
            if (Answers.Count == 0)
            {
                throw new Exception("Answers does not exist now");
            }
            if (index < 0 || index >= Answers.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (answer == null)
            {
                throw new ArgumentNullException("Gotta write something");
            }
            answer = answer.Trim();
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

        public void ChooseCorrect(string answer)
        {
            if (answer == "")
            {
                throw new ArgumentNullException("Empty string");
            }
            CorrectAnswer = answer;
        }

        public bool CheckUserAnswer()
        {
            bool result = false;
            string UserAnswerCheck = UserAnswer.Trim();
            UserAnswerCheck = UserAnswerCheck.ToLower();
            string CorrectAnswerCheck = CorrectAnswer.Trim();
            CorrectAnswerCheck = CorrectAnswer.ToLower();
            if(UserAnswerCheck == CorrectAnswerCheck)
            {
                result = true;
            }
            return result;
        }

        public void Send()
        {
            UserAnswer = Console.ReadLine();
        }
    }
}
