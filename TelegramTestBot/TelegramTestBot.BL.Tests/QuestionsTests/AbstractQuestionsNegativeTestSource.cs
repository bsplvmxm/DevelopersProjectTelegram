using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramTestBot.BL.Tests
{
    public class EditAnswerWrongIndexTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            AbstractQuestions question;
            int index;
            string new_answer;

            index = -1;
            new_answer = "Maksim";
            question = new AbstractQuestions("Kak Tebya zovut?");
            question.Answers = new List<string> { "Petya", "Sasha", "Ilya", "Vova" };
            question.UsersAnswers = new List<string> { "Ilya", "Vova" };
            yield return new object[] { question, index, new_answer };


            index = 5;
            new_answer = "Zaichik";
            question = new AbstractQuestions("Ti kto?");
            question.Answers = new List<string> { "Kotik", "Pesik", "Utka", "Cherepaha" };
            question.UsersAnswers = new List<string> { "Utka", "Kotik" };
            yield return new object[] { question, index, new_answer };
        }
    }

    public class EditAnswerEmptyAnswersTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            AbstractQuestions question;
            int index;
            string new_answer;

            index = 0;
            new_answer = "Maksim";
            question = new AbstractQuestions("Kak Tebya zovut?");
            question.Answers = new List<string> { };
            question.UsersAnswers = new List<string> { "Ilya", "Vova" };
            yield return new object[] { question, index, new_answer };


            index = 5;
            new_answer = "Zaichik";
            question = new AbstractQuestions("Ti kto?");
            question.Answers = new List<string> { };
            question.UsersAnswers = new List<string> { "Utka", "Kotik" };
            yield return new object[] { question, index, new_answer };
        }
    }

    public class DeleteAnswerWrongIndexTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            AbstractQuestions question;
            int index;

            index = -1;
            question = new AbstractQuestions("Kak Tebya zovut?");
            question.Answers = new List<string> { "Petya", "Sasha", "Ilya", "Vova" };
            question.UsersAnswers = new List<string> { "Ilya", "Vova" };
            yield return new object[] { question, index};

            index = 5;
            question = new AbstractQuestions("Ti kto?");
            question.Answers = new List<string> { "Kotik", "Pesik", "Utka", "Cherepaha" };
            question.UsersAnswers = new List<string> { "Utka", "Kotik" };
            yield return new object[] { question, index};
        }
    }

    public class DeleteAnswerEmptyAnswersTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            AbstractQuestions question;
            int index;

            index = 0;
            question = new AbstractQuestions("Kak Tebya zovut?");
            question.Answers = new List<string> { };
            question.UsersAnswers = new List<string> { "Ilya", "Vova" };
            yield return new object[] { question, index };

            index = 5;
            question = new AbstractQuestions("Ti kto?");
            question.Answers = new List<string> { };
            question.UsersAnswers = new List<string> { "Utka", "Kotik" };
            yield return new object[] { question, index };
        }
    }
}