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

        public void AddQuestion(string question, int index)
        {
            if (question == "")
            {
                question = "Введите вопрос";
            }
            if(index < 0|| index > 4)
            {
                throw new ArgumentException("Wrong index(type of question)");
            }
            switch (index)
            {
                case 0: 
                    Questions.Add(new ChooseAnyQuestion(question));
                    break;
                case 1:
                    Questions.Add(new ChooseOneQuestion(question));
                    break;
                case 2:
                    Questions.Add(new CourseQuestion(question));
                    break;
                case 3:
                    Questions.Add(new PollQuestion(question));
                    break;
                case 4:
                    Questions.Add(new YesNoQuestion(question));
                    break;
            }
        }

        public void EditQuestion(int index, string question)
        {
            if (index > -1 && index < Questions.Count)
            {
                if (question == "")
                {
                    question = "Введите вопрос";
                }
                Questions[index].ContentOfQuestion = question;
            }
        }

        public void DeleteQuestionByIndex(int index)
        {
            if (Questions.Count > 1)
            {
                if (index > -1 && index < Questions.Count)
                {
                    Questions.RemoveAt(index);
                }
            }
        }

        


        public void StartTest()
        {
            foreach (AbstractQuestions question in Questions)
            {
                question.Send();
            }
        }

        public void FinishTest()
        {

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
