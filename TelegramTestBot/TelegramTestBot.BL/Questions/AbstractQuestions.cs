using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramTestBot.BL
{
    public class AbstractQuestions
    {
        public string ContentOfQuestion { get; set; }

        public int TypeOfQuestion { get;  set; }

        public List<string> Answers { get; set; }

        public string CorrectAnswer { get;  set; }

        public List<string> UsersAnswers { get; set; }


        public AbstractQuestions()
        {

        }

        public AbstractQuestions(string content)
        {
            if(content == "")
            {
                content = "Введите вопрос";
            }
            ContentOfQuestion = content;
            Answers = new List<string>();
            UsersAnswers = new List<string>();
            TypeOfQuestion = -23;
            CorrectAnswer = "Правильный ответ";
        }

        public void AddAnswer(string answer)
        {
            if (answer == "")
            {
                answer = "Новый вариант";
            }
            answer = answer.Trim();
            Answers.Add(answer);
        }

        public void EditAnswer(int index, string answer)
        {
            if (Answers.Count == 0)
            {
                throw new Exception("Answers does not exist now");
            }
            if (index < 0 || index >= Answers.Count)
            {
                throw new ArgumentException("Wrong Index");
            }
            if (answer == "")
            {
                answer = "Введите ответ";
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
                CorrectAnswer = "Ответ не выбран";
            }
            else
            {
                CorrectAnswer = answer;
            }
        }

        public bool CheckUserAnswer(int index)
        {
            if(Answers.Count == 0)
            {
                throw new Exception("Answers are empty");
            }
            if(index < 0 || index > Answers.Count)
            {
                throw new IndexOutOfRangeException("Wrong Index");
            }
            bool result = false;
            string UserAnswerCheck = UsersAnswers[index].Trim();
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

        }



        public override string ToString()
        {
            string text;
            text = ContentOfQuestion;
            text += $"; Type: {TypeOfQuestion} ;";
            foreach(string s in Answers)
            {
                text += $" {s},";
            }
            text += $"; CorrAnsw {CorrectAnswer} ;";
            foreach(string s in UsersAnswers)
            {
                text += $" {s} ,";
            }
            return text;
        }

        public override bool Equals(object obj)
        {
            bool flag = true;
            if (obj == null || !(obj is AbstractQuestions))
            {
                flag = false;
            }
            AbstractQuestions question = (AbstractQuestions)obj;
            if ((question.ContentOfQuestion != this.ContentOfQuestion) ||
                (question.CorrectAnswer != this.CorrectAnswer) ||
                (question.TypeOfQuestion != this.TypeOfQuestion))
            {
                flag = false;
            }
            for(int i = 0; i<question.Answers.Count;i++)
            {
                if (question.Answers[i] != this.Answers[i])
                {
                    flag = false;
                }
            }
            for(int j = 0; j < question.UsersAnswers.Count;j++)
            {
                if(question.UsersAnswers[j] != this.UsersAnswers[j])
                {
                    flag = false;
                }
            }
            return flag;
        }
    }
}
