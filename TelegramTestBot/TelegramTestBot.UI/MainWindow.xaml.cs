using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using TelegramTestBot.BL;
using TelegramTestBot.BL.Data;

namespace TelegramTestBot.UI
{
    public partial class MainWindow : Window
    {
        private TelegaBotManager _telegaManager;
        private const string _token = "5277457802:AAG5dI1aiAEQYGt08OVjn5snSkX1qbzkc7s";
        private List<string> _labels;
        private DispatcherTimer _timer;  //счетчик времени
        private TestsBase MyTests = TestsBase.GetInstance();

        public MainWindow()
        {
            _telegaManager = new TelegaBotManager(_token, OnMessage);
            _labels = new List<string>();
            
            InitializeComponent();
            
            LB_Users.ItemsSource = _labels;
            CB_groups.Items.Add("Others");
            CB_GroupList.Items.Add("Others");
            
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += OnTimerTick;
            _timer.Start();

            MainMenu.Visibility = Visibility.Hidden;
            CreateQuestTest.Visibility = Visibility.Hidden;
            StartTest.Visibility = Visibility.Hidden;
            Button_AddAnswers.Visibility = Visibility.Hidden;
            TB_CorrectAnswer.Visibility = Visibility.Hidden;
        }

        public void OnMessage(string s)
        {
            _labels.Add(s);          
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            LB_Users.Items.Refresh();
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            _telegaManager.StartBot();
            CB_groups.SelectedIndex = 0;
        }

        private void EditNameButton_Click(object sender, RoutedEventArgs e)
        {
            string oldName = (string)LB_Users.SelectedItem;
            string newName = TB_Name.Text;
            string nameOfGroup = (string)CB_groups.SelectedItem;

            if (TB_Name.Text != "" && LB_Users.SelectedItem != null)
            {
                foreach (var users in BaseOfUsers.NameBase)
                {
                    if (oldName == users.Value)
                    {
                        int index = _labels.IndexOf(users.Value);
                        BaseOfUsers.NameBase[users.Key] = newName;
                        _labels[index] = newName;
                    }
                }
                
                if (BaseOfUsers.GroupBase.ContainsKey(nameOfGroup))
                {
                    if (BaseOfUsers.GroupBase[nameOfGroup].Contains(oldName))
                    {
                        int index = BaseOfUsers.GroupBase[nameOfGroup].IndexOf(oldName);
                        BaseOfUsers.GroupBase[nameOfGroup][index] = newName;
                    }                   
                }
            }   
            else
            {
                MessageBox.Show("Are you stupid?");
            }

            LB_Users.Items.Refresh();
            TB_Name.Clear();
            TB_Name.Text = "Enter new name";
        }

        private void AddUserButt_Click(object sender, RoutedEventArgs e)
        {
            string userName = (string)LB_Users.SelectedItem;
            string groupName = (string)CB_GroupList.SelectedItem;

            if (LB_Users.SelectedItem != null && CB_GroupList.SelectedItem != null && groupName != "" && CB_groups.Items.Contains(groupName))
            {
                _telegaManager.AddUserInGroup(groupName, userName);
                _labels.RemoveAt(_labels.IndexOf(userName));
            }
            else
            {
                MessageBox.Show("Are you stupid?");
            }

            LB_Users.Items.Refresh();
        }

        private void AddGroupButt_Click(object sender, RoutedEventArgs e)
        {
            string groupName = TB_GroupName.Text;

            if (groupName != "" && !BaseOfUsers.GroupBase.ContainsKey(groupName))
            {
                _telegaManager.CreateGroup(groupName);
                CB_groups.Items.Add(groupName);
                CB_GroupList.Items.Add(groupName);
                CB_SelectGroup.Items.Add(groupName);
            }
            else
            {
                MessageBox.Show("Are you stupid?");
            }

            LB_Users.Items.Refresh();
            CB_groups.Items.Refresh();
            CB_GroupList.Items.Refresh();
            TB_GroupName.Clear();
            TB_GroupName.Text = "Enter group name";
        }

        private void CB_groups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string nameGroup = (string)CB_groups.SelectedItem;
            _labels.Clear();

            if (CB_groups.SelectedItem != null)
            {                
                _telegaManager.OutputUsersInGroup(nameGroup);
            }

            LB_Users.Items.Refresh();
        }

        private void DelUserButt_Click(object sender, RoutedEventArgs e)
        {
            string username = (string)LB_Users.SelectedItem;
            string nameGroup = (string)CB_groups.SelectedItem;

            if (LB_Users.SelectedItem != null && CB_groups.SelectedItem != null && nameGroup != "Others")
            {
                _telegaManager.DeleteUserFromGroup(nameGroup, username);
                _telegaManager.AddUserInGroup("Others", username);
                _labels.RemoveAt(_labels.IndexOf(username));
            }
            else
            {
                MessageBox.Show("Are you stupid?");
            }

            LB_Users.Items.Refresh();
            CB_groups.Items.Refresh();
        }

        private void DelGroupButt_Click(object sender, RoutedEventArgs e)
        {
            string nameGroup = (string)CB_groups.SelectedItem;
            int index = CB_groups.SelectedIndex;
           
            
            if (CB_groups.SelectedItem != null && nameGroup != "Others")
            {
                _telegaManager.DeleteGroup(nameGroup);
                CB_groups.Items.RemoveAt(index);
                CB_GroupList.Items.Remove(nameGroup);
                CB_SelectGroup.Items.Remove(nameGroup);
                CB_groups.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Are you stupid?");
            }

            LB_Users.Items.Refresh();
            CB_groups.Items.Refresh();
        }        
           
        private void Button_AddTest_Click(object sender, RoutedEventArgs e)
        {
            if (TB_NameOfTest.Text != "")
            {
                string nameTest = TB_NameOfTest.Text;
                MyTests.AllTests.Add(new Test(nameTest));
                LB_AllTests.Items.Add(nameTest);
                TB_NameOfTest.Clear();
                Cb_SelectTest.Items.Add(nameTest);
            }
 
        }

        private void Button_RenameTest_Click(object sender, RoutedEventArgs e)
        {
            int testIndex = LB_AllTests.SelectedIndex;
            string newNameOfTest = TB_NameOfTest.Text;
            newNameOfTest = newNameOfTest.Trim();
            if(testIndex != -1 && newNameOfTest != "")
            {
                MyTests.AllTests[testIndex].NameTest = newNameOfTest;
                LB_AllTests.Items[testIndex] = newNameOfTest;
                TB_NameOfTest.Clear();
            }
        }

        private void LB_AllTests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LB_AllTests.SelectedItem != null)
            {
                Label_ViewQuest.Visibility = Visibility.Visible;
                CB_TypeQuestion.Visibility = Visibility.Visible;
                Label_InputQuest.Visibility = Visibility.Visible;
                TB_QuestionContent.Visibility = Visibility.Visible;
                Button_CreateQuest.Visibility = Visibility.Visible;
                Button_EditQuest.Visibility = Visibility.Visible;
                Button_DeleteQuest.Visibility = Visibility.Visible;
                Button_RenameTest.Visibility = Visibility.Visible;
                LB_QuestOfTest.Visibility = Visibility.Visible;
                string nameOfTest = (string)LB_AllTests.SelectedItem;
                TextBl_NameTest.Text = nameOfTest;
                TB_NameOfTest.Text = nameOfTest;
                LB_QuestOfTest.Items.Clear();
                for (int i = 0; i < MyTests.AllTests[LB_AllTests.SelectedIndex].Questions.Count; i++)
                {
                    LB_QuestOfTest.Items.Add(MyTests.AllTests[LB_AllTests.SelectedIndex].Questions[i].ContentOfQuestion);
                }
            }
            LB_QuestOfTest.Items.Refresh();
            CB_TypeQuestion.SelectedIndex = -1;
            CB_TypeQuestion.IsEnabled = false;
            OpenComponents(-1);
            TB_QuestionContent.Text = "";
        }


        private void CB_TypeQuestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CB_TypeQuestion.Items.Refresh();
        }

        private void Button_CreateQuest_Click(object sender, RoutedEventArgs e)
        {
            int index = CB_TypeQuestion.SelectedIndex;
            string newQuest = TB_QuestionContent.Text;
            if (CB_TypeQuestion.SelectedIndex > -1 && TB_QuestionContent.Text !="")
            {
                MyTests.AllTests[LB_AllTests.SelectedIndex].AddQuestion(newQuest, index);
                TB_QuestionContent.Clear();
                LB_QuestOfTest.Items.Add(newQuest);
            }
            LB_QuestOfTest.SelectedItem = 0;
            CB_TypeQuestion.SelectedIndex = -1;
        }
        private void ButtonCreateTest_Poll_Click(object sender, RoutedEventArgs e)
        {
            TabControl_Test.SelectedItem = CreateQuestTest;
        }

        private void Button_DeleteQuest_Click(object sender, RoutedEventArgs e)
        {
            if (CB_TypeQuestion.SelectedIndex > -1)
            {
                int testIndex = LB_AllTests.SelectedIndex;
                int questionIndex = LB_QuestOfTest.SelectedIndex;
                MyTests.AllTests[testIndex].DeleteQuestionByIndex(questionIndex);
                LB_QuestOfTest.Items.RemoveAt(LB_QuestOfTest.SelectedIndex);
                TB_QuestionContent.Text = "";
            }
        }

        private void OpenComponents(int index)
        {
            TB_CorrectAnswer.IsEnabled = false;
            switch (index)
            {
                case 0:
                    TB_Answer1.Visibility = Visibility.Visible;
                    TB_Answer2.Visibility = Visibility.Visible;
                    TB_Answer3.Visibility = Visibility.Visible;
                    TB_Answer4.Visibility = Visibility.Visible;
                    ChB_RightAns1.Visibility = Visibility.Visible;
                    ChB_RightAns2.Visibility = Visibility.Visible;
                    ChB_RightAns3.Visibility = Visibility.Visible;
                    ChB_RightAns4.Visibility = Visibility.Visible;
                    RB_RightAns1.Visibility = Visibility.Hidden;
                    RB_RightAns2.Visibility = Visibility.Hidden;
                    RB_RightAns3.Visibility = Visibility.Hidden;
                    RB_RightAns4.Visibility = Visibility.Hidden;
                    break;
                case 1:
                    TB_Answer1.Visibility = Visibility.Visible;
                    TB_Answer2.Visibility = Visibility.Visible;
                    TB_Answer3.Visibility = Visibility.Visible;
                    TB_Answer4.Visibility = Visibility.Visible;
                    RB_RightAns1.Visibility = Visibility.Visible;
                    RB_RightAns2.Visibility = Visibility.Visible;
                    RB_RightAns3.Visibility = Visibility.Visible;
                    RB_RightAns4.Visibility = Visibility.Visible;
                    ChB_RightAns1.Visibility = Visibility.Hidden;
                    ChB_RightAns2.Visibility = Visibility.Hidden;
                    ChB_RightAns3.Visibility = Visibility.Hidden;
                    ChB_RightAns4.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    TB_Answer1.Visibility = Visibility.Visible;
                    TB_Answer2.Visibility = Visibility.Visible;
                    TB_Answer3.Visibility = Visibility.Visible;
                    TB_Answer4.Visibility = Visibility.Visible;
                    break;
                case 3:
                    TB_Answer1.Visibility = Visibility.Hidden;
                    TB_Answer2.Visibility = Visibility.Hidden;
                    TB_Answer3.Visibility = Visibility.Hidden;
                    TB_Answer4.Visibility = Visibility.Hidden;
                    RB_RightAns1.Visibility = Visibility.Hidden;
                    RB_RightAns2.Visibility = Visibility.Hidden;
                    RB_RightAns3.Visibility = Visibility.Hidden;
                    RB_RightAns4.Visibility = Visibility.Hidden;
                    ChB_RightAns1.Visibility = Visibility.Hidden;
                    ChB_RightAns2.Visibility = Visibility.Hidden;
                    ChB_RightAns3.Visibility = Visibility.Hidden;
                    ChB_RightAns4.Visibility = Visibility.Hidden;
                    Button_AddAnswers.Visibility = Visibility.Hidden;
                    TB_CorrectAnswer.Visibility = Visibility.Hidden;
                    break;
                case 4:
                    TB_Answer1.Visibility = Visibility.Visible;
                    TB_Answer2.Visibility = Visibility.Visible;
                    TB_Answer3.Visibility = Visibility.Hidden;
                    TB_Answer4.Visibility = Visibility.Hidden;
                    RB_RightAns1.Visibility = Visibility.Visible;
                    RB_RightAns2.Visibility = Visibility.Visible;
                    RB_RightAns3.Visibility = Visibility.Hidden;
                    RB_RightAns4.Visibility = Visibility.Hidden;
                    ChB_RightAns1.Visibility = Visibility.Hidden;
                    ChB_RightAns2.Visibility = Visibility.Hidden;
                    ChB_RightAns3.Visibility = Visibility.Hidden;
                    ChB_RightAns4.Visibility = Visibility.Hidden;
                    TB_Answer1.IsEnabled = false;
                    TB_Answer2.IsEnabled = false;
                    break;
                case -1:
                    TB_Answer1.Visibility = Visibility.Hidden;
                    TB_Answer2.Visibility = Visibility.Hidden;
                    TB_Answer3.Visibility = Visibility.Hidden;
                    TB_Answer4.Visibility = Visibility.Hidden;
                    RB_RightAns1.Visibility = Visibility.Hidden;
                    RB_RightAns2.Visibility = Visibility.Hidden;
                    RB_RightAns3.Visibility = Visibility.Hidden;
                    RB_RightAns4.Visibility = Visibility.Hidden;
                    ChB_RightAns1.Visibility = Visibility.Hidden;
                    ChB_RightAns2.Visibility = Visibility.Hidden;
                    ChB_RightAns3.Visibility = Visibility.Hidden;
                    ChB_RightAns4.Visibility = Visibility.Hidden;
                    TB_Answer1.Text = "";
                    TB_Answer2.Text = "";
                    TB_Answer3.Text = "";
                    TB_Answer4.Text = "";
                    break;
            }
            TB_Answer1.IsEnabled = true;
            TB_Answer2.IsEnabled = true;
            TB_Answer3.IsEnabled = true;
            TB_Answer4.IsEnabled = true;
        }

        private void LB_QuestOfTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Button_AddAnswers.Visibility = Visibility.Visible;
            TB_CorrectAnswer.Visibility = Visibility.Visible;
            if (LB_QuestOfTest.SelectedIndex != -1 && LB_AllTests.SelectedIndex != -1)
            {
                int questionIndex = LB_QuestOfTest.SelectedIndex;
                int testIndex = LB_AllTests.SelectedIndex;
                int questionType = MyTests.AllTests[testIndex].Questions[questionIndex].TypeOfQuestion;
                TB_QuestionContent.Text = MyTests.AllTests[testIndex].Questions[questionIndex].ContentOfQuestion;
                if (questionType != 3)
                {
                    TB_Answer1.Text = MyTests.AllTests[testIndex].Questions[questionIndex].Answers[0];
                    TB_Answer2.Text = MyTests.AllTests[testIndex].Questions[questionIndex].Answers[1];
                    if (questionType != 4)
                    {
                        TB_Answer3.Text = MyTests.AllTests[testIndex].Questions[questionIndex].Answers[2];
                        TB_Answer4.Text = MyTests.AllTests[testIndex].Questions[questionIndex].Answers[3];
                    }
                    TB_CorrectAnswer.Text = MyTests.AllTests[testIndex].Questions[questionIndex].CorrectAnswer;
                }
                OpenComponents(questionType);
                if(questionType == 2)
                {
                    TB_CorrectAnswer.IsEnabled = true;
                }
                else
                {
                    TB_CorrectAnswer.IsEnabled = false;
                }
                CB_TypeQuestion.SelectedIndex = questionType;
                CB_TypeQuestion.IsEnabled = false;
                GetFlags(testIndex,questionIndex);
            }
        }

        private void TB_QuestionContent_TextChanged(object sender, TextChangedEventArgs e)
        {
            CB_TypeQuestion.SelectedIndex = -1;
            CB_TypeQuestion.IsEnabled = true;
            OpenComponents(-1);
        }

        private void Button_EditQuest_Click(object sender, RoutedEventArgs e)
        {
            string oldContent = (string)LB_QuestOfTest.SelectedItem;
            string newContent = TB_QuestionContent.Text;
            int indexQuestion = LB_QuestOfTest.SelectedIndex;
            int indexTest = LB_AllTests.SelectedIndex;

            if (TB_QuestionContent.Text!="" && LB_QuestOfTest.SelectedItem != null)
            {
                if (MyTests.AllTests[indexTest].Questions[indexQuestion].ContentOfQuestion == oldContent)
                {
                    MyTests.AllTests[indexTest].Questions[indexQuestion].ContentOfQuestion = newContent;
                    LB_QuestOfTest.Items[indexQuestion] =newContent;
                    TB_QuestionContent.Clear();
                }
            }
        }

        private void Button_AddAnswers_Click(object sender, RoutedEventArgs e)
        {
            int testIndex = LB_AllTests.SelectedIndex;
            int questionIndex = LB_QuestOfTest.SelectedIndex;
            int typeOfQestion = MyTests.AllTests[testIndex].Questions[questionIndex].TypeOfQuestion;
            string correctAnswer = CreateCorrectAnswerText(typeOfQestion);
            switch(typeOfQestion)
            {
                case 0:
                    {
                        MyTests.AllTests[testIndex].Questions[questionIndex].Answers[0] = TB_Answer1.Text;
                        MyTests.AllTests[testIndex].Questions[questionIndex].Answers[1] = TB_Answer2.Text;
                        MyTests.AllTests[testIndex].Questions[questionIndex].Answers[2] = TB_Answer3.Text;
                        MyTests.AllTests[testIndex].Questions[questionIndex].Answers[3] = TB_Answer4.Text;
                        MyTests.AllTests[testIndex].Questions[questionIndex].ChooseCorrect(correctAnswer);
                        break;
                    }
                case 1:
                    {
                        MyTests.AllTests[testIndex].Questions[questionIndex].Answers[0] = TB_Answer1.Text;
                        MyTests.AllTests[testIndex].Questions[questionIndex].Answers[1] = TB_Answer2.Text;
                        MyTests.AllTests[testIndex].Questions[questionIndex].Answers[2] = TB_Answer3.Text;
                        MyTests.AllTests[testIndex].Questions[questionIndex].Answers[3] = TB_Answer4.Text;
                        MyTests.AllTests[testIndex].Questions[questionIndex].ChooseCorrect(correctAnswer);
                        break;
                    }
                case 2:
                    {
                        MyTests.AllTests[testIndex].Questions[questionIndex].Answers[0] = TB_Answer1.Text;
                        MyTests.AllTests[testIndex].Questions[questionIndex].Answers[1] = TB_Answer2.Text;
                        MyTests.AllTests[testIndex].Questions[questionIndex].Answers[2] = TB_Answer3.Text;
                        MyTests.AllTests[testIndex].Questions[questionIndex].Answers[3] = TB_Answer4.Text;
                        MyTests.AllTests[testIndex].Questions[questionIndex].ChooseCorrect(correctAnswer);
                        break;
                    }
                case 4:
                    {
                        MyTests.AllTests[testIndex].Questions[questionIndex].Answers[0] = TB_Answer1.Text;
                        MyTests.AllTests[testIndex].Questions[questionIndex].Answers[1] = TB_Answer2.Text;
                        MyTests.AllTests[testIndex].Questions[questionIndex].ChooseCorrect(correctAnswer);
                        break;
                    }
            }
        }

        private string CreateCorrectAnswerText(int typeOfQuestion)
        {
            string correctAnswer="";
            switch(typeOfQuestion)
            {
                case 0:
                    {
                        if (ChB_RightAns1.IsChecked == true)
                        {
                            correctAnswer += "1";
                        }
                        if (ChB_RightAns2.IsChecked == true)
                        {
                            correctAnswer += "2";
                        }
                        if (ChB_RightAns3.IsChecked == true)
                        {
                            correctAnswer += "3";
                        }
                        if (ChB_RightAns4.IsChecked == true)
                        {
                            correctAnswer += "4";
                        }
                        break;
                    }
                case 1:
                    {
                        if (RB_RightAns1.IsChecked == true)
                        {
                            correctAnswer = TB_Answer1.Text;
                        }
                        else if (RB_RightAns2.IsChecked == true)
                        {
                            correctAnswer = TB_Answer2.Text;
                        }
                        else if (RB_RightAns3.IsChecked == true)
                        {
                            correctAnswer = TB_Answer3.Text;
                        }
                        else if (RB_RightAns4.IsChecked == true)
                        {
                            correctAnswer = TB_Answer4.Text;
                        }
                        break;
                    }
                case 2:
                    {
                        correctAnswer = TB_CorrectAnswer.Text;
                    }
                    break;
                case 4:
                    {
                        if (RB_RightAns1.IsChecked == true)
                        {
                            correctAnswer = TB_Answer1.Text;
                        }
                        else
                        {
                            correctAnswer = TB_Answer2.Text;
                        }
                        break;
                    }
            }
            if(correctAnswer == "")
            {
                correctAnswer = "Введите правильный ответ";
            }
            return correctAnswer;
        }

        private void Button_GoToMain_Click(object sender, RoutedEventArgs e)
        {
            TabControl_Test.SelectedItem = MainMenu;
        }

        private void ButtonStartTest_Click(object sender, RoutedEventArgs e)
        {
            TabControl_Test.SelectedItem = StartTest;
        }

        private void Button_GoToMain1_Click(object sender, RoutedEventArgs e)
        {
            TabControl_Test.SelectedItem = MainMenu;
        }

        private void Button_AddTest1_Click(object sender, RoutedEventArgs e)
        {
            TabControl_Test.SelectedItem = CreateQuestTest;
        }

        private void ButtonDelTest_Click(object sender, RoutedEventArgs e)
        {
            int testIndex = LB_AllTests.SelectedIndex;
            MyTests.AllTests.RemoveAt(testIndex);
            LB_AllTests.Items.RemoveAt(testIndex);
            Cb_SelectTest.Items.RemoveAt(testIndex);
            TB_NameOfTest.Text = "";
        }

        public void GetFlags(int testIndex, int questionIndex)
        {
            RB_RightAns1.IsChecked = false;
            RB_RightAns2.IsChecked = false;
            RB_RightAns3.IsChecked = false;
            RB_RightAns4.IsChecked = false;
            ChB_RightAns1.IsChecked=false;
            ChB_RightAns2.IsChecked=false;
            ChB_RightAns3.IsChecked=false;
            ChB_RightAns4.IsChecked=false;

            int typeOfQuestion = MyTests.AllTests[testIndex].Questions[questionIndex].TypeOfQuestion;
            string correctAnswer = "";
            correctAnswer = MyTests.AllTests[testIndex].Questions[questionIndex].CorrectAnswer;
            switch(typeOfQuestion)
            {
                case 0:
                    {
                        if (correctAnswer != null)
                        {
                            for (int i = 0; i < correctAnswer.Length; i++)
                            {
                                int c = correctAnswer[i];
                                if (c == 49)
                                {
                                    ChB_RightAns1.IsChecked = (true);
                                }
                                if (c == 50)
                                {
                                    ChB_RightAns2.IsChecked = true;
                                }
                                if (c == 51)
                                {
                                    ChB_RightAns3.IsChecked = true;
                                }
                                if (c == 52)
                                {
                                    ChB_RightAns4.IsChecked = true;
                                }
                            }
                        }
                        break;
                    }
                case 1:
                    {
                        if (TB_Answer1.Text == correctAnswer)
                        {
                            RB_RightAns1.IsChecked = true;
                        }
                        else if (TB_Answer2.Text == correctAnswer)
                        {
                            RB_RightAns2.IsChecked = true;
                        }
                        else if (TB_Answer3.Text == correctAnswer)
                        {
                            RB_RightAns3.IsChecked = true;
                        }
                        else if (TB_Answer4.Text == correctAnswer)
                        {
                            RB_RightAns4.IsChecked = true;
                        }
                        break;
                    }
                case 4:
                    {
                        if (TB_Answer1.Text == correctAnswer)
                        {
                            RB_RightAns1.IsChecked = true;
                        }
                        else if (TB_Answer2.Text == correctAnswer)
                        {
                            RB_RightAns2.IsChecked = true;
                        }
                        break;
                    }
                
            }
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            MyTests.Save(MyTests.AllTests);
        }

        private void Button_Load_Click(object sender, RoutedEventArgs e)
        {
            LB_AllTests.Items.Clear();
            LB_QuestOfTest.Items.Clear();
            MyTests.Load();
            for (int i = 0; i < MyTests.AllTests.Count; i++)
            {
                string nameTest = MyTests.AllTests[i].NameTest;
                LB_AllTests.Items.Add(nameTest);
                Cb_SelectTest.Items.Add(nameTest);
            }
        }

        private void Button_StartTest_Click(object sender, RoutedEventArgs e)
        {
            int index = Cb_SelectTest.SelectedIndex;
            string nameOfGroup = (string)CB_SelectGroup.SelectedItem;

            if (BaseOfUsers.GroupBase.ContainsKey(nameOfGroup))
            {
                foreach (var users in BaseOfUsers.NameBase)
                {
                    if (BaseOfUsers.GroupBase[nameOfGroup].Contains(users.Value))
                    {
                        _telegaManager.SendToUser(users.Key);

                        _telegaManager._isTesting = true;
                        _telegaManager._indexOfTest = index;
                    }
                }
            }
        }

        private void Button_StopTest_Click(object sender, RoutedEventArgs e)
        {
            MyTests.CreateTestReport(CB_SelectGroup.Text, MyTests.AllTests[Cb_SelectTest.SelectedIndex]);
            _telegaManager.ClearUserAnswers();

            _telegaManager._isTesting = false;
        }
    }
}