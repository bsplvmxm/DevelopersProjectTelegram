using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramTestBot.BL.Questions;

namespace TelegramTestBot.BL.Tests
{
    public class Tests
    {
        [TestCaseSource(typeof(AbstractQuestionTestSource))]
        public void ChooseCorrectTest(AbstractQuestions question, AbstractQuestions expected_question, string answer)
        {
            question.ChooseCorrect(answer);
            Assert.AreEqual(expected_question, question);
        }
    }
}