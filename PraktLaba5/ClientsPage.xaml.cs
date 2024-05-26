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
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
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

        ClientsTableAdapter clients = new ClientsTableAdapter();
        public ClientsPage()
        {
            InitializeComponent();
            ClientsDgr.ItemsSource = clients.GetData();
        }
        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (SurnameTbx.Text != "" && FirstNameTbx.Text != "" && PhoneTbx.Text != "" && EmailTbx.Text != "" && !HasSpecialChars(SurnameTbx.Text) && !HasSpecialChars(FirstNameTbx.Text) && !HasSpecialChars(MiddleNameTbx.Text) && !HasSpecialChars(PhoneTbx.Text))
            {
                clients.InsertQuery(SurnameTbx.Text, FirstNameTbx.Text, MiddleNameTbx.Text, PhoneTbx.Text, EmailTbx.Text);
            }
            else
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены и заполнены верным типом данных!");
            }
            ClientsDgr.ItemsSource = clients.GetData();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            object id = (ClientsDgr.SelectedItem as DataRowView).Row[0];
            if (SurnameTbx.Text != "" && FirstNameTbx.Text != "" && PhoneTbx.Text != "" && EmailTbx.Text != "" && !HasSpecialChars(SurnameTbx.Text) && !HasSpecialChars(FirstNameTbx.Text) && !HasSpecialChars(MiddleNameTbx.Text) && !HasSpecialChars(PhoneTbx.Text))
            {
                clients.UpdateQuery(SurnameTbx.Text, FirstNameTbx.Text, MiddleNameTbx.Text, PhoneTbx.Text, EmailTbx.Text, Convert.ToInt32(id));
            }
            else
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены и заполнены верным типом данных!");
            }
            ClientsDgr.ItemsSource = clients.GetData();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            object id = (ClientsDgr.SelectedItem as DataRowView).Row[0];
            clients.DeleteQuery(Convert.ToInt32(id));
            ClientsDgr.ItemsSource = clients.GetData();
        }

        private void ClientsDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientsDgr.SelectedItem as DataRowView != null)
            {
                object surname = (ClientsDgr.SelectedItem as DataRowView).Row[1];
                SurnameTbx.Text = surname.ToString();
                object firstname = (ClientsDgr.SelectedItem as DataRowView).Row[2];
                FirstNameTbx.Text = firstname.ToString();
                object middlename = (ClientsDgr.SelectedItem as DataRowView).Row[3];
                MiddleNameTbx.Text = middlename.ToString();
                object phone = (ClientsDgr.SelectedItem as DataRowView).Row[4];
                PhoneTbx.Text = phone.ToString();
                object email = (ClientsDgr.SelectedItem as DataRowView).Row[5];
                EmailTbx.Text = email.ToString();
            }
        }
        private void Import_Click(object sender, RoutedEventArgs e)
        {
            List<ClientsModel> forImport = ImportClass.Deserialize<List<ClientsModel>>();
            foreach (var item in forImport)
            {
                clients.InsertQuery(item.Surname, item.FirstName, item.MiddleName, item.Phone, item.Email);
            }
            ClientsDgr.ItemsSource = null;
            ClientsDgr.ItemsSource = clients.GetData();
        }
    }
}
