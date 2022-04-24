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
        public List<AbstractQuestion> Questions { get; set; }

        public Test(string nameTest)
        {
            NameTest = nameTest;
            Questions = new List<AbstractQuestion>();
        }

        public void AddQuestion(string question, int index)
        {
            if (question == "")
            {
                throw new NullReferenceException();
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
            if (question == null)
            {
                throw new Exception();
            }
            Questions[index]._question_content = question;
        }

        public void DeleteQuestionPoll(int index)
        {
            if (Questions.Count < 1)
            {
                throw new Exception("List is Empty");
            }
            Questions.RemoveAt(index);
        }


        public void StartTest()
        {
            foreach (AbstractQuestion question in Questions)
            {
                question.Send();
            }
        }

        public void FinishTest()
        {

        }
        public override string ToString()
        {
            return NameTest;
        }
    }
}
