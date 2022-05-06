using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramTestBot.BL.Questions;

namespace TelegramTestBot.BL
{
    public class Test
    {
        public string NameTest { get; set; }
        public List<AbstractQuestions> Questions { get; set; }

        public Test()
        {

        }

        public Test(string nameTest)
        {
            NameTest = nameTest;
            Questions = new List<AbstractQuestions>();
        }

        public void AddQuestion(string questionContent, int typeOfQuestion)
        {
            if (questionContent == "")
            {
                questionContent = "Введите вопрос";
            }
            if(typeOfQuestion < 0|| typeOfQuestion > 4)
            {
                throw new ArgumentException("Wrong index(type of question)");
            }
            switch (typeOfQuestion)
            {
                case 0: 
                    Questions.Add(new ChooseAnyQuestion(questionContent));
                    break;
                case 1:
                    Questions.Add(new ChooseOneQuestion(questionContent));
                    break;
                case 2:
                    Questions.Add(new CourseQuestion(questionContent));
                    break;
                case 3:
                    Questions.Add(new PollQuestion(questionContent));
                    break;
                case 4:
                    Questions.Add(new YesNoQuestion(questionContent));
                    break;
            }
        }

        public void EditQuestion(int indexOfQuestion, string question)
        {
            if (indexOfQuestion > -1 && indexOfQuestion < Questions.Count)
            {
                if (question == "")
                {
                    question = "Введите вопрос";
                }
                Questions[indexOfQuestion].ContentOfQuestion = question;
            }
        }

        public void DeleteQuestionByIndex(int indexOfQuestion)
        {
            if (Questions.Count > 1)
            {
                if (indexOfQuestion > -1 && indexOfQuestion < Questions.Count)
                {
                    Questions.RemoveAt(indexOfQuestion);
                }
            }
        }     


        public override bool Equals(object obj)
        {
            bool result = true;
            if (obj == null || !(obj is Test))
            {
                result = false;
            }
            Test test = (Test)obj;
            if(test.NameTest != NameTest)
            {
                result = false;
            }
            for(int i = 0; i < Questions.Count; i++)
            {
                if (!(test.Questions[i].Equals(Questions[i])))
                {
                    result = false;
                }                    
            }
            return result;
        }

        public override string ToString()
        {
            string result = "";
            result += $"{NameTest} ";
            foreach(AbstractQuestions question in Questions)
            {
                string qwe = question.ToString();
                result += $"{qwe} ";
            }
            return result;
        }
    }
}
