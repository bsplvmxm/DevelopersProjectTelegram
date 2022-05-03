using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramTestBot.BL.Questions;

namespace TelegramTestBot.BL.Tests
{
    public class AbstractQuestionTests
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

        [TestCaseSource(typeof(EditAnswerEmptyAnswersTestSource))]
        public void EditAnswerTest_WhanAnswersAreEmpty_ShouldThrowException(AbstractQuestions question, int index, string answer)
        {
            Assert.Throws<Exception>(() => question.EditAnswer(index, answer));
        }

        [TestCaseSource(typeof(EditAnswerWrongIndexTestSource))]
        public void EditAnswerTest_WhanIndexIsWrong_ShouldThrowArgumentException(AbstractQuestions question, int index, string answer)
        {
            Assert.Throws<ArgumentException>(() => question.EditAnswer(index, answer));
        }



        [TestCaseSource(typeof(DeleteAnswerTestSource))]
        public void DeleteAnswerTest(AbstractQuestions question, AbstractQuestions expected_question, int index)
        {
            question.DeleteAnswer(index);
            Assert.AreEqual(expected_question, question);
        }

        [TestCaseSource(typeof(DeleteAnswerEmptyAnswersTestSource))]
        public void DeleteAnswerTest_WhenAnswersAreEmpty_ShouldThrowException(AbstractQuestions question, int index)
        {
            Assert.Throws<Exception>(() => question.DeleteAnswer(index));
        }

        [TestCaseSource(typeof(DeleteAnswerWrongIndexTestSource))]
        public void DeleteAnswerTest_WhenIndexIsWrong_ShoulThrowArgumentException(AbstractQuestions question, int index)
        {
            Assert.Throws<IndexOutOfRangeException>(() => question.DeleteAnswer(index));
        }



        [TestCaseSource(typeof(ChooseCorrectTestSource))]
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

        [TestCaseSource(typeof(DeleteAnswerEmptyAnswersTestSource))]
        public void CheckUserAnswerTest_WhenAnswersAreEmpty_ShouldThrowException(AbstractQuestions question, int index)
        {
            Assert.Throws<Exception>(() => question.CheckUserAnswer(index));
        }

        [TestCaseSource(typeof(DeleteAnswerWrongIndexTestSource))]
        public void CheckUserAnswerTest_WhenIndexIsWrong_ShoulThrowArgumentException(AbstractQuestions question, int index)
        {
            Assert.Throws<IndexOutOfRangeException>(() => question.CheckUserAnswer(index));
        }
    }
}