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

namespace TelegramTestBot.UI
{
    public partial class MainWindow : Window
    {
        private TelegaBotManager _telegaManager;
        private const string _token = "5277457802:AAG5dI1aiAEQYGt08OVjn5snSkX1qbzkc7s";
        private List<string> _labels;
        private DispatcherTimer _timer;  //счетчик времени
        private List<Test> AllTests;

        public MainWindow()
        {
            _telegaManager = new TelegaBotManager(_token, OnMessage);
            _labels = new List<string>();
            InitializeComponent();

            LB_Users.ItemsSource = _labels;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += OnTimerTick;
            _timer.Start();
            AllTests = new List<Test>();

            //Label_ViewQuest.IsEnabled = false;
            //CB_TypeQuestion.IsEnabled = false;
            //RB_RightAns1.IsEnabled = false;
            //RB_RightAns2.IsEnabled = false;
            //RB_RightAns3.IsEnabled = false;
            //RB_RightAns4.IsEnabled = false;
            //TB_Answer1.IsEnabled = false;
            //TB_Answer2.IsEnabled = false;
            //TB_Answer3.IsEnabled = false;
            //TB_Answer4.IsEnabled = false;
            //ChB_RightAns1.IsEnabled = false;
            //ChB_RightAns2.IsEnabled = false;
            //ChB_RightAns3.IsEnabled = false;
            //ChB_RightAns4.IsEnabled = false;
            //Label_InputQuest.IsEnabled = false;
            //TB_QuestionContent.IsEnabled = false;
            //Button_CreateQuest.IsEnabled = false;
            //Button_EditQuest.IsEnabled = false;
            //Button_DeleteQuest.IsEnabled = false;
            //Button_RenameTest.IsEnabled = false;
            MainMenu.Visibility = Visibility.Visible;
            CreateQuestTest.Visibility = Visibility.Visible;
            TestItem.Visibility = Visibility.Visible;


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
        }

        private void ButtonReg_Click(object sender, RoutedEventArgs e)
        {
            _telegaManager.StartingButton();
        }

           
        private void Button_AddTest_Click(object sender, RoutedEventArgs e)
        {
            if (TB_NameOfTest.Text != "")
            {
                string nameTest = TB_NameOfTest.Text;
                AllTests.Add(new Test(nameTest));
                LB_AllTests.Items.Add(nameTest);
                TB_NameOfTest.Clear();
            }
 
        }

        private void LB_AllTests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //HideAllForTest();
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
                for (int i = 0; i < AllTests[LB_AllTests.SelectedIndex].Questions.Count; i++)
                {
                    LB_QuestOfTest.Items.Add(AllTests[LB_AllTests.SelectedIndex].Questions[i]._question_content);
                }
            }
            LB_QuestOfTest.Items.Refresh();
        }


        private void CB_TypeQuestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = CB_TypeQuestion.SelectedIndex;
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
                    break;
            }
            //CB_TypeQuestion.Items.Clear();
            CB_TypeQuestion.Items.Refresh();
        }
        private void Button_CreateQuest_Click(object sender, RoutedEventArgs e)
        {
            int index = CB_TypeQuestion.SelectedIndex;
            if (CB_TypeQuestion.SelectedIndex > -1)
            {
                string nameOfTest = (string)LB_AllTests.SelectedItem;
                string newQuest = TB_QuestionContent.Text;
                //LB_QuestOfTest.Items.Add(newQuest);
                AllTests[LB_AllTests.SelectedIndex].AddQuestion(newQuest, index);
                //LB_QuestOfTest.Items.Add(newQuest);
                TB_QuestionContent.Clear();
                CB_TypeQuestion.Items.Refresh();
            }
        }

        private void HideAllForTest()
        {
            Label_ViewQuest.Visibility = Visibility.Visible;
            CB_TypeQuestion.Visibility = Visibility.Visible;
            RB_RightAns1.Visibility = Visibility.Visible;
            RB_RightAns2.Visibility = Visibility.Visible;
            RB_RightAns3.Visibility = Visibility.Visible;
            RB_RightAns4.Visibility = Visibility.Visible;
            TB_Answer1.Visibility = Visibility.Visible;
            TB_Answer2.Visibility = Visibility.Visible;
            TB_Answer3.Visibility = Visibility.Visible;
            TB_Answer4.Visibility = Visibility.Visible;
            ChB_RightAns1.Visibility = Visibility.Visible;
            ChB_RightAns2.Visibility = Visibility.Visible;
            ChB_RightAns3.Visibility = Visibility.Visible;
            ChB_RightAns4.Visibility = Visibility.Visible;
            Label_InputQuest.Visibility = Visibility.Visible;
            TB_QuestionContent.Visibility = Visibility.Visible;
            Button_CreateQuest.Visibility = Visibility.Visible;
            Button_EditQuest.Visibility = Visibility.Visible;
            Button_DeleteQuest.Visibility = Visibility.Visible;
            Button_RenameTest.Visibility = Visibility.Visible;
            LB_QuestOfTest.Visibility = Visibility.Visible;
        }

        private void OpenComponents()
        {
            int index = CB_TypeQuestion.SelectedIndex;
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
                    break;
                case 2:
                    TB_Answer1.Visibility = Visibility.Visible;
                    TB_Answer2.Visibility = Visibility.Visible;
                    TB_Answer3.Visibility = Visibility.Visible;
                    TB_Answer4.Visibility = Visibility.Visible;
                    break;
                case 4:
                    TB_Answer1.Visibility = Visibility.Visible;
                    TB_Answer2.Visibility = Visibility.Visible;
                    RB_RightAns1.Visibility = Visibility.Visible;
                    RB_RightAns2.Visibility = Visibility.Visible;
                    break;
            }
        }

        

        private void ButtonCreateTest_Poll_Click(object sender, RoutedEventArgs e)
        {
            TabControl_Test.SelectedItem = CreateQuestTest;
        }

    }
}
