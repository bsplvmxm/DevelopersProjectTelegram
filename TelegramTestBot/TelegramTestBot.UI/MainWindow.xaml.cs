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

namespace TelegramTestBot.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            TextBox textBox = new TextBox() { Width = 606, Height = 40 };
            Button button = new Button() { Content = "Удалить", Height = 40, Width = 90 };
            ListBoxForAdd.Items.Add(textBox);
            ListBoxForAdd.Items.Add(button);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListBoxForAdd.Items.Clear();
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
