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
    /// Логика взаимодействия для TourTypePage.xaml
    /// </summary>
    public partial class TourTypePage : Page
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


        TourTypesTableAdapter type = new TourTypesTableAdapter();
        public TourTypePage()
        {
            InitializeComponent();
            TourTypeDgr.ItemsSource = type.GetData();
        }
        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (TypeNameTbx.Text != "" && !HasSpecialChars(TypeNameTbx.Text) && !IsNumber(TypeNameTbx.Text))
            {
                type.InsertQuery(TypeNameTbx.Text);
            }
            else
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены верным типом данных!");
            }
            TourTypeDgr.ItemsSource = type.GetData();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            object id = (TourTypeDgr.SelectedItem as DataRowView).Row[0];
            if (TypeNameTbx.Text != "" && !HasSpecialChars(TypeNameTbx.Text) && !IsNumber(TypeNameTbx.Text))
            {
                type.UpdateQuery(TypeNameTbx.Text, Convert.ToInt32(id));
            }
            else
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены верным типом данных!");
            }
            TourTypeDgr.ItemsSource = type.GetData();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            object id = (TourTypeDgr.SelectedItem as DataRowView).Row[0];
            type.DeleteQuery(Convert.ToInt32(id));
            TourTypeDgr.ItemsSource = type.GetData();
        }

        private void TourTypeDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TourTypeDgr.SelectedItem != null)
            {
                if (TourTypeDgr.SelectedItem as DataRowView != null)
                {
                    object type = (TourTypeDgr.SelectedItem as DataRowView).Row[1];
                    TypeNameTbx.Text = type.ToString();
                }
            }
        }
    }
}
