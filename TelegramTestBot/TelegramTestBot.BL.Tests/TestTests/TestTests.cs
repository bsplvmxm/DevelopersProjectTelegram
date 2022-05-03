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

        [TestCaseSource(typeof(AddQuestionWrongIndexTestSource))]
        public void AddQuestionTest_WhenIndexIsWrong_ShouldThrowArgumentException(Test test, string question, int index)
        {
            Assert.Throws<ArgumentException>(() => test.AddQuestion(question, index));
        }



        [TestCaseSource(typeof(EditQuestionTestSource))]
        public void EditQuestionTest(Test test, Test expected_test, int index, string question)
        {
            test.EditQuestion(index, question);
            Assert.AreEqual(expected_test, test);
        }



        [TestCaseSource(typeof(DeleteQuestionByIndexTestSource))]
        public void DeleteQuestionByIndexTest(Test test, Test expected_test, int index)
        {
            test.DeleteQuestionByIndex(index);
            Assert.AreEqual(expected_test, test);
        }

    }
}