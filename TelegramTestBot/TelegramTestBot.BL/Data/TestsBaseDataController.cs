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
    public class TestsBaseDataController
    {
        //private const string filePath = @"D:\Курсы\Домашка\DevelopersProjectTelegram\developersTB.tb";
        //public string Serialize(TestsBase tests)
        //{
        //    return JsonSerializer.Serialize<TestsBase>(tests);
        //}

        //public TestsBase Deserialize(string json)
        //{
        //    return JsonSerializer.Deserialize<TestsBase>(json);
        //}

        //public void Save(TestsBase myBase)
        //{
        //    string json = Serialize(myBase);

        //    using(StreamWriter sw = new StreamWriter(filePath, false))
        //    {
        //        sw.WriteLine(json);
        //    }
        //}

        //public TestsBase Load()
        //{
        //    using (StreamReader sr = new StreamReader(filePath))
        //    {
        //        string json = sr.ReadLine();
        //        return Deserialize(json);
        //    }
        //}
    }
}