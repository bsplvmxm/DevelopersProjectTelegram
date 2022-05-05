using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;
using TelegramTestBot.BL;
using TelegramTestBot.BL.Questions;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace TelegramTestBot.BL.Data
{
    public class TestsBase
    {
        private const string filePath = @"developersTB.tb";
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


        public void CreateTestReport(string nameOfGroup, Test currentTest)
        {
            string reportName = $"Отчёт по группе {nameOfGroup}, тест: {currentTest.NameTest}";
            //лист с именами пользователей
            List<string> usersNames = new List<string> { };
            List<List<string>> allAnswersOfUsers = new List<List<string>>(usersNames.Count);
            if (BaseOfUsers.GroupBase.ContainsKey(nameOfGroup))
            {
                foreach (var users in BaseOfUsers.NameBase)
                {
                    if (BaseOfUsers.GroupBase[nameOfGroup].Contains(users.Value))
                    {
                        usersNames.Add(users.Value);
                        allAnswersOfUsers.Add(BaseOfUsers.UserAnswers[users.Key]);
                    }
                }
            }
            //лист из листов с ответами
            //инициальзируем эксель
            Excel.Application oXL;//приложение
            Excel._Workbook report;//сам файл
            Excel._Worksheet oSheet;//лист
            Excel.Range oRng;//хз но надо
            oXL = new Excel.Application();
            oXL.Visible = true;
            oXL.SheetsInNewWorkbook = 2;//кол-во листов в книге
            report = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
            oSheet = (Excel._Worksheet)report.Worksheets[1];//выцепляем нужный лист
            oSheet.Cells[1, 1] = $"{reportName}";//А1 = название отчёта
            oSheet.Name = $"{currentTest.NameTest}";//имя листа(на вкладке снизу) = название отчёта
            //usersNames.Add("Il'ka");
            //заполняем первую строку именами юзеров, начаиния с В1 идём С1, D1 и т.д.
            //for(int i = 0; i <= usersNames.Count; i++)
            //{
            //    oSheet.Cells[i+2, 1] = $"{usersNames[i]}";
            //}
            int schetchik = 0;
            foreach(var user in usersNames)
            {
                oSheet.Cells[schetchik + 2, 1] = $"{usersNames[schetchik]}";
                schetchik++;

            }
            schetchik = 0;
            //заполняем первый столбец вопросами теста, начиная с А2 идём А3, А4 и т.д.
            for (int i = 0; i < currentTest.Questions.Count; i++)
            {
                //создаём стрингу с вопросом и вариантами ответов на него
                string currentQuestion = "";
                currentQuestion = currentTest.Questions[i].ContentOfQuestion;
                foreach(string answer in currentTest.Questions[i].Answers)
                {
                    currentQuestion += $" {answer}";
                }
                //заполняем ячейку
                oSheet.Cells[1,i+2] = $"{currentQuestion}";
            }
            //заполняем ответами начиная с В2 идём В3, В4 и т.д.
            for (int i = 0; i < usersNames.Count; i++)
            {
                for(int j = 0; j < currentTest.Questions.Count; j++)
                {
                    
                    if (j<allAnswersOfUsers[i].Count)
                    {
                        oSheet.Cells[i + 2, j + 2] = $"{allAnswersOfUsers[i][j]}";//ответ на конкретный вопрос
                    }
                    else
                    {
                        oSheet.Cells[i + 2, j + 2] = "Нет ответа";//если не успел
                    }
                }
            }
            // сохранение с именем которое в начале задавали
        }
    }
}