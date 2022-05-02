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
        [TestCaseSource(typeof(ChooseCorrectTestSource))]
        public void ChooseCorrectTest(AbstractQuestions question, AbstractQuestions expected_question, string answer)
        {
            question.ChooseCorrect(answer);
            Assert.AreEqual(expected_question, question);
        }

        
        [TestCaseSource(typeof(AddAnswerTestSource))]
        public void AddAnswerTest(AbstractQuestions question, AbstractQuestions expected_question, string answer)
        {
            question.AddAnswer(answer);
            Assert.AreEqual(expected_question,question);
        }


    }
}