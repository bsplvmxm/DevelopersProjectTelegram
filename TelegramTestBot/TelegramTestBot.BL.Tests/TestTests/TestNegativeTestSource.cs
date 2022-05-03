using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramTestBot.BL.Tests.TestTests
{
    public class AddQuestionWrongIndexTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            Test test = new Test("0");
            string question = "Выбор нескольких";
            int index = -2;
            test.Questions = new List<AbstractQuestions>();
            yield return new object[] { test, question, index };

            index = 5;
            yield return new object[] { test, question, index };
        }
    }
}
