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
using System.Windows.Shapes;


namespace PraktLaba5
{
    /// <summary>
    /// Логика взаимодействия для AgentWindow.xaml
    /// </summary>
    public partial class AgentWindow : Window
    {

        public AgentWindow()
        {
            InitializeComponent();
        }

        private void Booking_Click(object sender, RoutedEventArgs e)
        {
            AgentFrame.Content = new BookingPayPage();
        }

        private void PayCheks_Click(object sender, RoutedEventArgs e)
        {
            AgentFrame.Content = new PayChecksPage();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
