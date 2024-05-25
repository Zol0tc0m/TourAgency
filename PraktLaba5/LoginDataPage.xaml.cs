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
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PraktLaba5.TravelAgency1DataSetTableAdapters;

namespace PraktLaba5
{
    /// <summary>
    /// Логика взаимодействия для LoginDataPage.xaml
    /// </summary>
    public partial class LoginDataPage : Page
    {
        public int limit;
        private bool HasSpecialChars(string yourString)
        {
            return yourString.Any(ch => !char.IsLetterOrDigit(ch));
        }

        LoginDataTableAdapter logdat = new LoginDataTableAdapter();
        EmployeesTableAdapter employees = new EmployeesTableAdapter();
        EmpRoleTableAdapter role = new EmpRoleTableAdapter();
        public LoginDataPage()
        {
            InitializeComponent();
            LoginDataDgr.ItemsSource = logdat.GetData();
            EmployeesComboBox.ItemsSource = employees.GetData();
            EmployeesComboBox.DisplayMemberPath = "ID_Employee";
            RoleComboBox.ItemsSource = role.GetData();
            RoleComboBox.DisplayMemberPath = "RoleName";

            var allLogins = logdat.GetData().Rows;
            for (int i = 0; i < allLogins.Count; i++)
            {
                limit = i;
            }
        }
        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (LoginTbx.Text != "" && PasswordTbx.Text != "" && !HasSpecialChars(LoginTbx.Text))
            {

                logdat.InsertQuery(EmployeesComboBox.SelectedIndex + 1, LoginTbx.Text, PasswordTbx.Text, RoleComboBox.SelectedIndex + 1);

            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены и заполнены верным типом данных!");
            }
            LoginDataDgr.ItemsSource = logdat.GetData();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

            object id = 0;
            if (LoginDataDgr.SelectedItem as DataRowView != null)
            {
                id = (LoginDataDgr.SelectedItem as DataRowView).Row[0];
            }
            else
            {
                MessageBox.Show("За выбранным ID не прикреплён ни один сотрудник!");
            }
            var allLogins = logdat.GetData().Rows;
            if (LoginTbx.Text != "" && PasswordTbx.Text != "")
            {
                if (EmployeesComboBox.SelectedIndex > limit + 1)
                {
                    logdat.UpdateQuery(EmployeesComboBox.SelectedIndex + 1, LoginTbx.Text, PasswordTbx.Text, RoleComboBox.SelectedIndex + 1, Convert.ToInt32(id));
                }
                else
                {
                    MessageBox.Show("На одного сотрудника только одни данные авторизации!");
                }
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены и заполнены верным типом данных!");
            }
            LoginDataDgr.ItemsSource = logdat.GetData();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            object id = (LoginDataDgr.SelectedItem as DataRowView).Row[0];
            logdat.DeleteQuery(Convert.ToInt32(id));
            LoginDataDgr.ItemsSource = logdat.GetData();
        }
        private void LoginDataDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LoginDataDgr.SelectedItem as DataRowView != null)
            {
                object employee = (LoginDataDgr.SelectedItem as DataRowView).Row[0];
                EmployeesComboBox.SelectedIndex = (int)employee - 1;
                object login = (LoginDataDgr.SelectedItem as DataRowView).Row[1];
                LoginTbx.Text = login.ToString();
                object password = (LoginDataDgr.SelectedItem as DataRowView).Row[2];
                PasswordTbx.Text = password.ToString();
                object role = (LoginDataDgr.SelectedItem as DataRowView).Row[3];
                RoleComboBox.SelectedIndex = (int)role - 1;
            }
        }
    }
}
