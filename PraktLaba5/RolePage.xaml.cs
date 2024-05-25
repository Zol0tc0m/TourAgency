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
    /// Логика взаимодействия для RolePage.xaml
    /// </summary>
    /// 

    public partial class RolePage : Page
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

        EmpRoleTableAdapter role = new EmpRoleTableAdapter();
        public RolePage()
        {
            InitializeComponent();
            RolesDgr.ItemsSource = role.GetData();
        }
        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (AddTbx.Text != "" && !IsNumber(AddTbx.Text) && !HasSpecialChars(AddTbx.Text))
            {
                role.InsertQuery(AddTbx.Text);
            }
            else
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены верным типом данных!");
            }
            RolesDgr.ItemsSource = role.GetData();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            object id = (RolesDgr.SelectedItem as DataRowView).Row[0];
            if (AddTbx.Text != "" && !IsNumber(AddTbx.Text) && !HasSpecialChars(AddTbx.Text))
            {
                role.UpdateQuery(AddTbx.Text, Convert.ToInt32(id));
            }
            else
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены верным типом данных!");
            }
            RolesDgr.ItemsSource = role.GetData();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            object id = (RolesDgr.SelectedItem as DataRowView).Row[0];
            role.DeleteQuery(Convert.ToInt32(id));
            RolesDgr.ItemsSource = role.GetData();
        }

        private void RolesDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RolesDgr.SelectedItem != null)
            {
                object cell = (RolesDgr.SelectedItem as DataRowView).Row[1];
                AddTbx.Text = cell.ToString();
            }
        }
    }
}