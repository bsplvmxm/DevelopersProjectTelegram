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
        [TestCaseSource(typeof(AddAnswerTestSource))]
        public void AddAnswerTest(AbstractQuestions question, AbstractQuestions expected_question, string answer)
        {
            question.AddAnswer(answer);
            Assert.AreEqual(expected_question,question);
        }


        [TestCaseSource(typeof (EditAnswerTestSource))]
        public void EditAnswerTest(AbstractQuestions question, AbstractQuestions expected_question, int index, string answer)
        {
            question.EditAnswer(index, answer);
            Assert.AreEqual(expected_question,question);
        }


        [TestCaseSource(typeof(DeleteAnswerTestSource))]
        public void DeleteAnswerTest(AbstractQuestions question, AbstractQuestions expected_question, int index)
        {
            question.DeleteAnswer(index);
            Assert.AreEqual(expected_question, question);
        }


        [TestCaseSource(typeof(AbstractQuestionsTestSource))]
        public void ChooseCorrectTest(AbstractQuestions question, AbstractQuestions expected_question, string answer)
        {
            question.ChooseCorrect(answer);
            Assert.AreEqual(expected_question, question);
        }


        [TestCaseSource(typeof(CheckUserAnswerTestSource))]
        public void CheckUserAnswerTest(AbstractQuestions question, int index, bool expected_result, bool actual_result)
        {
            actual_result = question.CheckUserAnswer(index);
            Assert.AreEqual(expected_result, actual_result);
        }




    }
}