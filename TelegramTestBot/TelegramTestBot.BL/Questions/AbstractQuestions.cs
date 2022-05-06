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

        public AbstractQuestions(string contentOfQuestion)
        {
            if(contentOfQuestion == "")
            {
                contentOfQuestion = "Введите вопрос";
            }
            ContentOfQuestion = contentOfQuestion;
            Answers = new List<string>();
            UsersAnswers = new List<string>();
            TypeOfQuestion = -23;
            CorrectAnswer = "Правильный ответ";
        }

        public void AddAnswer(string newVariant)
        {
            if (newVariant == "")
            {
                newVariant = "Новый вариант";
            }
            newVariant = newVariant.Trim();
            Answers.Add(newVariant);
        }

        public void EditAnswer(int indexOfQuestion, string newAnswersContent)
        {
            if (Answers.Count == 0)
            {
                throw new Exception("Answers does not exist now");
            }
            if (indexOfQuestion < 0 || indexOfQuestion >= Answers.Count)
            {
                throw new ArgumentException("Wrong Index");
            }
            if (newAnswersContent == "")
            {
                newAnswersContent = "Введите ответ";
            }
            newAnswersContent = newAnswersContent.Trim();
            Answers[indexOfQuestion] = newAnswersContent;
        }

        public void DeleteAnswer(int indexOfQuestion)
        {
            if (Answers.Count == 0)
            {
                throw new Exception("Answers does not exist now");
            }
            if (indexOfQuestion < 0 || indexOfQuestion > Answers.Count)
            {
                throw new IndexOutOfRangeException("Wrong index");
            }
            Answers.RemoveAt(indexOfQuestion);
        }

        public void ChooseCorrect(string answer)
        {
            if (answer == "")
            {
                CorrectAnswer = "Верный ответ не выбран";
            }
            else
            {
                CorrectAnswer = answer;
            }
        }

        //переделать чтобы кушал стрингу
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
