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
    /// Логика взаимодействия для GuidesPage.xaml
    /// </summary>
    public partial class GuidesPage : Page
    {

        bool IsNumber(string value)
        {
            try
            {
                int number = int.Parse(value);
                return true;
            }
            catch { return false; }
        }

        private bool HasSpecialChars(string yourString)
        {
            return yourString.Any(ch => !char.IsLetterOrDigit(ch));
        }

        GuidesTableAdapter guides = new GuidesTableAdapter();
        GServicesTableAdapter gservices = new GServicesTableAdapter();
        public GuidesPage()
        {
            InitializeComponent();
            GuidesDgr.ItemsSource = guides.GetData();
            ServiceComboBox.ItemsSource = gservices.GetData();
            ServiceComboBox.DisplayMemberPath = "ServiceName";
        }
        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (SurnameTbx.Text != "" && FirstNameTbx.Text != "" && PhoneTbx.Text != "" && EmailTbx.Text != "" && !HasSpecialChars(SurnameTbx.Text) && !HasSpecialChars(FirstNameTbx.Text) && !HasSpecialChars(PhoneTbx.Text))
            {
                guides.InsertQuery(SurnameTbx.Text, FirstNameTbx.Text, MiddleNameTbx.Text, PhoneTbx.Text, EmailTbx.Text, ServiceComboBox.SelectedIndex + 1);
            }
            else
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены и заполнены верным типом данных!");
            }
            GuidesDgr.ItemsSource = guides.GetData();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            object id = (GuidesDgr.SelectedItem as DataRowView).Row[0];
            if (SurnameTbx.Text != "" && FirstNameTbx.Text != "" && PhoneTbx.Text != "" && EmailTbx.Text != "" && !HasSpecialChars(SurnameTbx.Text) && !HasSpecialChars(FirstNameTbx.Text) && !HasSpecialChars(PhoneTbx.Text))
            {
                guides.UpdateQuery(SurnameTbx.Text, FirstNameTbx.Text, MiddleNameTbx.Text, PhoneTbx.Text, EmailTbx.Text, ServiceComboBox.SelectedIndex + 1, Convert.ToInt32(id));
            }
            else
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены и заполнены верным типом данных!");
            }
            GuidesDgr.ItemsSource = guides.GetData();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            object id = (GuidesDgr.SelectedItem as DataRowView).Row[0];
            guides.DeleteQuery(Convert.ToInt32(id));
            GuidesDgr.ItemsSource = guides.GetData();
        }

        private void GuidesDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GuidesDgr.SelectedItem as DataRowView != null)
            {
                object surname = (GuidesDgr.SelectedItem as DataRowView).Row[1];
                SurnameTbx.Text = surname.ToString();
                object firstname = (GuidesDgr.SelectedItem as DataRowView).Row[2];
                FirstNameTbx.Text = firstname.ToString();
                object middlename = (GuidesDgr.SelectedItem as DataRowView).Row[3];
                MiddleNameTbx.Text = middlename.ToString();
                object phone = (GuidesDgr.SelectedItem as DataRowView).Row[4];
                PhoneTbx.Text = phone.ToString();
                object email = (GuidesDgr.SelectedItem as DataRowView).Row[5];
                EmailTbx.Text = email.ToString();
                object service = (GuidesDgr.SelectedItem as DataRowView).Row[6];
                ServiceComboBox.SelectedIndex = (int)service - 1;
            }
        }
    }
}
