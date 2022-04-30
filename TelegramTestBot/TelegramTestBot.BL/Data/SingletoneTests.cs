using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramTestBot.BL.Data
{
    public class SingletoneTests
    {
        public List<Test> AllTests { get; private set; }

        private static SingletoneTests _instance;

        private SingletoneTests()
        {
            AllTests = new List<Test>();
        }

        public static SingletoneTests GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SingletoneTests();
            }
            return _instance;
        }
    }
}