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
    /// Логика взаимодействия для ToursPage.xaml
    /// </summary>
    public partial class ToursPage : Page
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

        ToursTableAdapter tours = new ToursTableAdapter();
        TourTypesTableAdapter types = new TourTypesTableAdapter();
        DestinationsTableAdapter destinations = new DestinationsTableAdapter();
        public ToursPage()
        {
            InitializeComponent();
            ToursDgr.ItemsSource = tours.GetData();
            TourTypeComboBox.ItemsSource = types.GetData();
            TourTypeComboBox.DisplayMemberPath = "TourTypeName";
            DestinationComboBox.ItemsSource = destinations.GetData();
            DestinationComboBox.DisplayMemberPath = "DestinationName";
        }
        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (TourNameTbx.Text != "" && !HasSpecialChars(TourNameTbx.Text) && !IsNumber(TourNameTbx.Text))
            {
                tours.InsertQuery(TourNameTbx.Text, TourTypeComboBox.SelectedIndex + 1, DestinationComboBox.SelectedIndex + 1);
            }
            else
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены и заполнены верным типом данных!");
            }
            ToursDgr.ItemsSource = tours.GetData();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            object id = (ToursDgr.SelectedItem as DataRowView).Row[0];
            if (TourNameTbx.Text != "" && !HasSpecialChars(TourNameTbx.Text) && !IsNumber(TourNameTbx.Text))
            {
                tours.UpdateQuery(TourNameTbx.Text, TourTypeComboBox.SelectedIndex + 1, DestinationComboBox.SelectedIndex + 1, Convert.ToInt32(id));
            }
            else
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены и заполнены верным типом данных!");
            }
            ToursDgr.ItemsSource = tours.GetData();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            object id = (ToursDgr.SelectedItem as DataRowView).Row[0];
            tours.DeleteQuery(Convert.ToInt32(id));
            ToursDgr.ItemsSource = tours.GetData();
        }

        private void ToursDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ToursDgr.SelectedItem as DataRowView != null)
            {
                object tourname = (ToursDgr.SelectedItem as DataRowView).Row[1];
                TourNameTbx.Text = tourname.ToString();
                object type = (ToursDgr.SelectedItem as DataRowView).Row[2];
                TourTypeComboBox.SelectedIndex = (int)type - 1;
                object destination = (ToursDgr.SelectedItem as DataRowView).Row[3];
                DestinationComboBox.SelectedIndex = (int)type - 1;
            }
        }
    }
}
