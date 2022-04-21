using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramTestBot.BL.Questions;

namespace TelegramTestBot.BL
{
    class Test
    {
        public string NameTest { get; set; }
        public List<AbstractQuestion> Questions { get; set; }

        public Test(string nameTest)
        {
            NameTest = nameTest;
            Questions = new List<AbstractQuestion>();
        }
        public void AddChooseAnyQuestion(string question)
        {
            if (question == "")
            {
                throw new NullReferenceException();
            }
            Questions.Add(new ChooseAnyQuestion(question));
        }
        public void AddChooseOneQuestion(string question)
        {
            if (question == "")
            {
                throw new NullReferenceException();
            }
            Questions.Add(new ChooseOneQuestion(question));
        }

        public void AddCourseQuestion(string question)
        {
            if (question == "")
            {
                throw new NullReferenceException();
            }
            Questions.Add(new CourseQuestion(question));
        }

        public void AddPollQuestion(string question)
        {
            if (question == "")
            {
                throw new NullReferenceException();
            }
            Questions.Add(new PollQuestion(question));
        }

        public void AddYeNoQuestion(string question)
        {
            if (question == "")
            {
                throw new NullReferenceException();
            }
            Questions.Add(new YeNoQuestion(question));
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
