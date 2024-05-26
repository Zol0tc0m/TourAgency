using System;
using System.Collections;
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
    /// Логика взаимодействия для EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
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

        EmployeesTableAdapter employees = new EmployeesTableAdapter();
        EmpRoleTableAdapter role = new EmpRoleTableAdapter();
        public EmployeesPage()
        {
            InitializeComponent();
            EmployeesDgr.ItemsSource = employees.GetData();
            RoleComboBox.ItemsSource = role.GetData();
            RoleComboBox.DisplayMemberPath = "RoleName";
        }
        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (SurnameTbx.Text != "" && FirstNameTbx.Text != "" && PhoneTbx.Text != "" && EmailTbx.Text != "" && !HasSpecialChars(SurnameTbx.Text) && !HasSpecialChars(FirstNameTbx.Text) && !HasSpecialChars(MiddleNameTbx.Text) && !HasSpecialChars(PhoneTbx.Text))
            {
                employees.InsertQuery(SurnameTbx.Text, FirstNameTbx.Text, MiddleNameTbx.Text, PhoneTbx.Text, EmailTbx.Text, RoleComboBox.SelectedIndex + 1);
            }
            else
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены и заполнены верным типом данных!");
            }
            EmployeesDgr.ItemsSource = employees.GetData();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            object id = (EmployeesDgr.SelectedItem as DataRowView).Row[0];
            if (SurnameTbx.Text != "" && FirstNameTbx.Text != "" && PhoneTbx.Text != "" && EmailTbx.Text != "" && !HasSpecialChars(SurnameTbx.Text) && !HasSpecialChars(FirstNameTbx.Text) && !HasSpecialChars(MiddleNameTbx.Text)  && !HasSpecialChars(PhoneTbx.Text))
            {
                employees.UpdateQuery(SurnameTbx.Text, FirstNameTbx.Text, MiddleNameTbx.Text, PhoneTbx.Text, EmailTbx.Text, RoleComboBox.SelectedIndex + 1, Convert.ToInt32(id));
            }
            else
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены и заполнены верным типом данных!");
            }
            EmployeesDgr.ItemsSource = employees.GetData();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            object id = (EmployeesDgr.SelectedItem as DataRowView).Row[0];
            employees.DeleteQuery(Convert.ToInt32(id));
            EmployeesDgr.ItemsSource = employees.GetData();
        }

        private void EmployeesDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmployeesDgr.SelectedItem as DataRowView != null)
            {
                object surname = (EmployeesDgr.SelectedItem as DataRowView).Row[1];
                SurnameTbx.Text = surname.ToString();
                object firstname = (EmployeesDgr.SelectedItem as DataRowView).Row[2];
                FirstNameTbx.Text = firstname.ToString();
                object middlename = (EmployeesDgr.SelectedItem as DataRowView).Row[3];
                MiddleNameTbx.Text = middlename.ToString();
                object phone = (EmployeesDgr.SelectedItem as DataRowView).Row[4];
                PhoneTbx.Text = phone.ToString();
                object email = (EmployeesDgr.SelectedItem as DataRowView).Row[5];
                EmailTbx.Text = email.ToString();
                object role = (EmployeesDgr.SelectedItem as DataRowView).Row[6];
                RoleComboBox.SelectedIndex = (int)role - 1;
            }
        }
    }
}
