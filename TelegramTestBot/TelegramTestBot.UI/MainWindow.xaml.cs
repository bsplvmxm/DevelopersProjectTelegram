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
        //private DispatcherTimer _timer;  //счетчик времени

        public MainWindow()
        {
            _telegaManager = new TelegaBotManager(_token, OnMessage);
            _labels = new List<string>();
            InitializeComponent();

            //_timer = new DispatcherTimer();
            //_timer.Interval = TimeSpan.FromSeconds(1);
            //_timer.Tick += OnTimerTick;
            //_timer.Start();
        }

        public void OnMessage(string s)
        {
            _labels.Add(s);
        }
        private void Window_Initialized(object sender, EventArgs e)
        {

        }

        //private void OnTimerTick(object sender, EventArgs e)
        //{

        //}
    }
}
