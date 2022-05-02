using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using TelegramTestBot.BL;
using TelegramTestBot.BL.Questions;

namespace TelegramTestBot.BL.Data
{
    public class TestsBaseDataController
    {
        public void SaveTests(TestsBase testsBase)
        {
            string json = JsonSerializer.Serialize(testsBase.AllTests);




        }
    }
}