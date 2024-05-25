using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Логика взаимодействия для DirWindow.xaml
    /// </summary>
    public partial class DirWindow : Window
    {
        public DirWindow()
        {
            InitializeComponent();
        }
        private void EmpRole_Click(object sender, RoutedEventArgs e)
        {
            DirFrame.Content = new RolePage();
        }

        private void Employees_Click(object sender, RoutedEventArgs e)
        {
            DirFrame.Content = new EmployeesPage();
        }

        private void LoginData_Click(object sender, RoutedEventArgs e)
        {
            DirFrame.Content = new LoginDataPage();
        }

        private void Guides_Click(object sender, RoutedEventArgs e)
        {
            DirFrame.Content = new GuidesPage();
        }
        private void Services_Click(object sender, RoutedEventArgs e)
        {
            DirFrame.Content = new ServicesPage();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
