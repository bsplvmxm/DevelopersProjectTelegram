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

        public MainWindow()
        {
            _telegaManager = new TelegaBotManager(_token, OnMessage);
            _labels = new List<string>();
            
            InitializeComponent();

            LB_Users.ItemsSource = _labels;
            CB_groups.Items.Add("Others");
            
            LabelError.Visibility = Visibility.Hidden;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += OnTimerTick;
            _timer.Start();
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
            LabelError.Visibility = Visibility.Hidden;

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
                LabelError.Visibility = Visibility.Visible;
                LabelError.Content = "Ты что дурачок?";
            }

            LB_Users.Items.Refresh();
            TB_Name.Clear();           
        }

        private void TestButOut_Click(object sender, RoutedEventArgs e)
        {
            LabelError.Visibility = Visibility.Hidden;

            string userName = (string)LB_Users.SelectedItem;
            string groupName = TB_GroupName.Text;

            if (LB_Users.SelectedItem != null && groupName != "" && CB_groups.Items.Contains(groupName))
            {
                _telegaManager.AddUserInGroup(groupName, userName);
                _labels.RemoveAt(_labels.IndexOf(userName));
            }
            else
            {
                LabelError.Visibility = Visibility.Visible;
                LabelError.Content = "Ты что дурачок?";
            }

            LB_Users.Items.Refresh();
            TB_GroupName.Clear();
        }

        private void AddGroupButt_Click(object sender, RoutedEventArgs e)
        {
            LabelError.Visibility = Visibility.Hidden;

            string groupName = TB_GroupName.Text;

            if (groupName != "")
            {
                _telegaManager.CreateGroup(groupName);
                CB_groups.Items.Add(groupName);
            }
            else
            {
                LabelError.Visibility = Visibility.Visible;
                LabelError.Content = "Ты что дурачок?";
            }

            LB_Users.Items.Refresh();
            CB_groups.Items.Refresh();
            TB_GroupName.Clear();
        }

        private void CB_groups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LabelError.Visibility = Visibility.Hidden;

            string nameGroup = (string)CB_groups.SelectedItem;
            _labels.Clear();

            if (CB_groups.SelectedItem != null)
            {                
                _telegaManager.OutputUsersInGroup(nameGroup);
            }

            LB_Users.Items.Refresh();
        }

        private void DelButt_Click(object sender, RoutedEventArgs e)
        {
            LabelError.Visibility = Visibility.Hidden;

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
                LabelError.Visibility = Visibility.Visible;
                LabelError.Content = "Ты что дурачок?";
            }

            LB_Users.Items.Refresh();
            CB_groups.Items.Refresh();
        }

        private void DelGroupButt_Click(object sender, RoutedEventArgs e)
        {
            LabelError.Visibility = Visibility.Hidden;

            string nameGroup = (string)CB_groups.SelectedItem;
            int index = CB_groups.SelectedIndex;
           
            
            if (CB_groups.SelectedItem != null && nameGroup != "Others")
            {
                _telegaManager.DeleteGroup(nameGroup);
                CB_groups.Items.RemoveAt(index);
                CB_groups.SelectedIndex = 0;
            }
            else
            {
                LabelError.Visibility = Visibility.Visible;
                LabelError.Content = "Ты что дурачок?";
            }

            LB_Users.Items.Refresh();
            CB_groups.Items.Refresh();
        }
    }
}
