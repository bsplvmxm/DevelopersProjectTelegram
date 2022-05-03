using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramTestBot.BL.Questions;

namespace TelegramTestBot.BL.Tests.TestTests
{
    public class TestTests
    {
        [TestCaseSource(typeof(AddQuestionTestSource))]
        public void AddQuestionTest(Test test, Test expected_test, string question, int index)
        {
            test.AddQuestion(question, index);
            Assert.AreEqual(expected_test, test);
        }
    }
}
