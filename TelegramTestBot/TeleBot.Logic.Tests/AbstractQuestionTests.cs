using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TeleBot.Logic;

namespace TeleBot.Logic.Tests
{
    public class Tests
    {
        [TestCaseSource(typeof(AddAnswerTestSource))]
        public void AddAnswerTest(string answer)
        {

        }
    }
}