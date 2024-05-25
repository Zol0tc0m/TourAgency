using System;
using System.Collections.Generic;
using System.Data;
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
using PraktLaba5.TravelAgency1DataSetTableAdapters;

namespace PraktLaba5
{
    /// <summary>
    /// Логика взаимодействия для ServicesPage.xaml
    /// </summary>
    public partial class ServicesPage : Page
    {

        private bool HasSpecialChars(string yourString)
        {
            return yourString.Any(ch => !char.IsLetterOrDigit(ch));
        }

        bool IsNumber(string value)
        {
            try
            {
                int number = int.Parse(value);
                return true;
            }
            catch { return false; }
        }

        GServicesTableAdapter services = new GServicesTableAdapter();
        public ServicesPage()
        {
            InitializeComponent();
            ServiceDgr.ItemsSource = services.GetData();
        }
        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (ServiceNameTbx.Text != "" && ServiceDescTbx.Text != "" && !HasSpecialChars(ServiceNameTbx.Text) && !HasSpecialChars(ServiceDescTbx.Text))
            {
                services.InsertQuery(ServiceNameTbx.Text, ServiceDescTbx.Text);
            }
            else
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены верным типом данных!");
            }
            ServiceDgr.ItemsSource = services.GetData();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            object id = (ServiceDgr.SelectedItem as DataRowView).Row[0];
            if (ServiceNameTbx.Text != "" && ServiceDescTbx.Text !="" && !HasSpecialChars(ServiceNameTbx.Text) && !HasSpecialChars(ServiceDescTbx.Text))
            {
                services.UpdateQuery(ServiceNameTbx.Text, ServiceDescTbx.Text, Convert.ToInt32(id));
            }
            else
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены верным типом данных!");
            }
            ServiceDgr.ItemsSource = services.GetData();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            object id = (ServiceDgr.SelectedItem as DataRowView).Row[0];
            services.DeleteQuery(Convert.ToInt32(id));
            ServiceDgr.ItemsSource = services.GetData();
        }

        private void ServiceDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ServiceDgr.SelectedItem != null)
            {
                if (ServiceDgr.SelectedItem as DataRowView != null)
                {
                    object name = (ServiceDgr.SelectedItem as DataRowView).Row[1];
                    ServiceNameTbx.Text = name.ToString();
                    object desc = (ServiceDgr.SelectedItem as DataRowView).Row[2];
                    ServiceDescTbx.Text = desc.ToString();
                }
            }
        }
    }
}
