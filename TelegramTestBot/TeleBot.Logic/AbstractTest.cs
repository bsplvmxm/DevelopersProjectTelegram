namespace TeleBot.Logic
{
    public abstract class AbstractTest
    {
        public string Name { get; set; }

        public List<Question> _questions;

        public AbstractTest(string name)
        {
            Name = name;
            _questions = new List<Question>();
        }

        public void AddQuestion(string question)
        {
            _questions.Add(new Question(question));
        }

        public void DeleteQuestion(int index)
        {
            _questions.RemoveAt(index);
        }

        public void EditQuestion(int index, string question)
        {
            _questions[index] = new Question(question);
        }
        public abstract void AddAnswer();
        public abstract void DeleteAnswer();
        public abstract void EditAnswer();
        public abstract void Run();
        public abstract void Stop();


    }
}