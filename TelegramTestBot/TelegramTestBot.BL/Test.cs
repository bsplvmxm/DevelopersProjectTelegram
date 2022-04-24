using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramTestBot.BL
{
    class Test
    {
        public string NameTest { get; set; }
        public List<AbstractQuestion> AbstractQuestions { get; set; }

        public Test(string nameTest)
        {
            NameTest = nameTest;
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
        public void DeleteQuestion(int index)
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
