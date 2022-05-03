using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramTestBot.BL.Questions;

namespace TelegramTestBot.BL.Tests.TestTests
{
    public class AddQuestionTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            Test test = new Test("0");
            Test expectedTest = new Test("0");
            string question = "Выбор нескольких";
            int index = 0;
            test.Questions = new List<AbstractQuestions>();            
            expectedTest.Questions = new List<AbstractQuestions>() { new ChooseAnyQuestion(question)};
            yield return new object[] { test, expectedTest, question, index };

            test = new Test("1");
            expectedTest = new Test("1");
            question = "Выбор одного";
            index = 1;
            test.Questions = new List<AbstractQuestions>();
            expectedTest.Questions = new List<AbstractQuestions>() { new ChooseOneQuestion(question) };
            yield return new object[] { test, expectedTest, question, index };

            test = new Test("2");
            expectedTest = new Test("2");
            question = "Сортировка";
            index = 2;
            test.Questions = new List<AbstractQuestions>();
            expectedTest.Questions = new List<AbstractQuestions>() { new CourseQuestion(question) };
            yield return new object[] { test, expectedTest, question, index };

            test = new Test("3");
            expectedTest = new Test("3");
            question = "Опрос";
            index = 3;
            test.Questions = new List<AbstractQuestions>();
            expectedTest.Questions = new List<AbstractQuestions>() { new PollQuestion(question) };
            yield return new object[] { test, expectedTest, question, index };

            test = new Test("4");
            expectedTest = new Test("4");
            question = "Да/Нет";
            index = 4;
            test.Questions = new List<AbstractQuestions>();
            expectedTest.Questions = new List<AbstractQuestions>() { new YesNoQuestion(question)};
            yield return new object[] { test, expectedTest, question, index };
        }
    }

    public class EditQuestionTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            Test test = new Test("0");
            Test expectedTest = new Test("0");

            string question = "Выбор нескольких";
            int index = 0;
            test.Questions = new List<AbstractQuestions>() { new ChooseAnyQuestion("1v"), new ChooseAnyQuestion("2v") };
            expectedTest.Questions = new List<AbstractQuestions>() { new ChooseAnyQuestion("Выбор нескольких"), new ChooseAnyQuestion("2v") };
            yield return new object[] { test, expectedTest, index, question };

            test = new Test("1");
            expectedTest = new Test("1");
            question = "New Q";
            index = 1;
            test.Questions = new List<AbstractQuestions>() { new ChooseAnyQuestion("1v"), new PollQuestion("2v") };
            expectedTest.Questions = new List<AbstractQuestions>() { new ChooseAnyQuestion("1v"), new PollQuestion("New Q") };
            yield return new object[] { test, expectedTest, index, question };

            test = new Test("1");
            expectedTest = new Test("1");
            question = "";
            index = 2;
            test.Questions = new List<AbstractQuestions>() { new ChooseAnyQuestion("1v"), new PollQuestion("2v"), new PollQuestion("3v") };
            expectedTest.Questions = new List<AbstractQuestions>() { new ChooseAnyQuestion("1v"), new PollQuestion("2v"), new PollQuestion("Введите вопрос") };
            yield return new object[] { test, expectedTest, index, question };
        }
    }

    public class DeleteQuestionByIndexTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            Test test = new Test("0");
            Test expectedTest = new Test("0");

            int index = 0;
            test.Questions = new List<AbstractQuestions>() { new ChooseAnyQuestion("1v"), new ChooseAnyQuestion("2v") };
            expectedTest.Questions = new List<AbstractQuestions>() { new ChooseAnyQuestion("2v") };
            yield return new object[] { test, expectedTest, index };

            test = new Test("1");
            expectedTest = new Test("1");
            index = 1;
            test.Questions = new List<AbstractQuestions>() { new ChooseAnyQuestion("1v"), new ChooseAnyQuestion("2v") };
            expectedTest.Questions = new List<AbstractQuestions>() { new ChooseAnyQuestion("1v") };
            yield return new object[] { test, expectedTest, index };

            test = new Test("1");
            expectedTest = new Test("1");
            index = -1;
            test.Questions = new List<AbstractQuestions>() { new ChooseAnyQuestion("1v"), new ChooseAnyQuestion("2v") };
            expectedTest.Questions = new List<AbstractQuestions>() { new ChooseAnyQuestion("1v"), new ChooseAnyQuestion("2v") };
            yield return new object[] { test, expectedTest, index };

            test = new Test("1");
            expectedTest = new Test("1");
            index = 2;
            test.Questions = new List<AbstractQuestions>() { new ChooseAnyQuestion("1v"), new ChooseAnyQuestion("2v") };
            expectedTest.Questions = new List<AbstractQuestions>() { new ChooseAnyQuestion("1v"), new ChooseAnyQuestion("2v") };
            yield return new object[] { test, expectedTest, index };

            test = new Test("1");
            expectedTest = new Test("1");
            index = 2;
            test.Questions = new List<AbstractQuestions>() {};
            expectedTest.Questions = new List<AbstractQuestions>() {};
            yield return new object[] { test, expectedTest, index };
        }
    }
}