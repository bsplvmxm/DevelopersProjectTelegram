using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;
using TelegramTestBot.BL;
using TelegramTestBot.BL.Questions;

namespace TelegramTestBot.BL.Data
{
    public class TestsBase
    {
        private const string filePath = @"D:\Курсы\Домашка\DevelopersProjectTelegram\developersTB.tb";
        public List<Test> AllTests { get; private set; }

        private static TestsBase _instance;

        private TestsBase()
        {
            AllTests = new List<Test>();
        }

        public static TestsBase GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TestsBase();
            }
            return _instance;
        }

        private string Serialize(List<Test> tests)
        {
            return JsonSerializer.Serialize<List<Test>>(tests);
        }

        private List<Test> Deserialize(string json)
        {
            return JsonSerializer.Deserialize<List<Test>>(json);
        }

        public void Save(List<Test> tests)
        {
            string json = Serialize(tests);

            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                sw.WriteLine(json);
            }
        }

        public void Load()
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string json = sr.ReadLine();
                AllTests = Deserialize(json);
            }
        }

    }
}