using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramTestBot.BL.Data
{
    public class TestBase
    {
        public List<Test> AllTests { get; private set; }

        private static TestBase _instance;

        private TestBase()
        {
            AllTests = new List<Test>();
        }

        public static TestBase GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TestBase();
            }
            return _instance;
        }
    }
}