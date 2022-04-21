using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleBot.Logic.Tests
{
    public class AddAnswerTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[] { "What's your name?", new List<string> { }, new List<string> { "What's your name?" } };
        }
    }
}
