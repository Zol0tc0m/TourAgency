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
using PraktLaba5.TravelAgency1DataSetTableAdapters;

namespace PraktLaba5
{
    /// <summary>
    /// Логика взаимодействия для ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent();
        }
        private void Tours_Click(object sender, RoutedEventArgs e)
        {
            ManagerFrame.Content = new ToursPage();
        }

        private void TourType_Click(object sender, RoutedEventArgs e)
        {
            ManagerFrame.Content = new TourTypePage();
        }

        private void Destination_Click(object sender, RoutedEventArgs e)
        {
            ManagerFrame.Content = new DestinationPage();
        }

        private void Clients_Click(object sender, RoutedEventArgs e)
        {
            ManagerFrame.Content = new ClientsPage();
        }

        private void Bookings_Click(object sender, RoutedEventArgs e)
        {
            ManagerFrame.Content = new BookingsPage();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
