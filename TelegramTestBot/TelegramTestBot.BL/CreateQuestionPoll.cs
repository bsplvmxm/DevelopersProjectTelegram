using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramTestBot.BL
{
    class CreateQuestion
    {
        public string NameTest { get; set; }
        public List<AbstractQuestion> AbstractQuestions { get; set; }

        public CreateQuestion (string namePoll)
        {
            NameTest = namePoll;
            AbstractQuestions = new List<AbstractQuestion>();
        }
        public void AddQuestion(AbstractQuestion abstractQuestion)
        {
            if (abstractQuestion == null)
            {
                throw new NullReferenceException();
            }
            AbstractQuestions.Add(abstractQuestion);
        }
        public void DeleteQuestionPoll(int index)
        {
            if (AbstractQuestions.Count < 1)
            {
                throw new Exception("List is Empty");
            }
            AbstractQuestions.RemoveAt(index);
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
