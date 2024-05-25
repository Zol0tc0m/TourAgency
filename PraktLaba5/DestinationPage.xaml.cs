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
    /// Логика взаимодействия для DestinationPage.xaml
    /// </summary>
    public partial class DestinationPage : Page
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

        DestinationsTableAdapter destinations = new DestinationsTableAdapter();
        public DestinationPage()
        {
            InitializeComponent();
            DescriptionDgr.ItemsSource = destinations.GetData();
        }
        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (DestinationNameTbx.Text != "" && DestinationDescTbx.Text != "" && !HasSpecialChars(DestinationNameTbx.Text) && !HasSpecialChars(DestinationDescTbx.Text))
            {
                destinations.InsertQuery(DestinationNameTbx.Text, DestinationDescTbx.Text);
            }
            else
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены верным типом данных!");
            }
            DescriptionDgr.ItemsSource = destinations.GetData();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            object id = (DescriptionDgr.SelectedItem as DataRowView).Row[0];
            if (DestinationNameTbx.Text != "" && DestinationDescTbx.Text != "" && !HasSpecialChars(DestinationNameTbx.Text) && !HasSpecialChars(DestinationDescTbx.Text))
            {
                destinations.UpdateQuery(DestinationNameTbx.Text, DestinationDescTbx.Text, Convert.ToInt32(id));
            }
            else
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены верным типом данных!");
            }
            DescriptionDgr.ItemsSource = destinations.GetData();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            object id = (DescriptionDgr.SelectedItem as DataRowView).Row[0];
            destinations.DeleteQuery(Convert.ToInt32(id));
            DescriptionDgr.ItemsSource = destinations.GetData();
        }

        private void DescriptionDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DescriptionDgr.SelectedItem != null)
            {
                if (DescriptionDgr.SelectedItem as DataRowView != null)
                {
                    object name = (DescriptionDgr.SelectedItem as DataRowView).Row[1];
                    DestinationNameTbx.Text = name.ToString();
                    object desc = (DescriptionDgr.SelectedItem as DataRowView).Row[2];
                    DestinationDescTbx.Text = desc.ToString();
                }
            }
        }
        private void Import_Click(object sender, RoutedEventArgs e)
        {
            List<DestinationsModel> forImport = ImportClass.Deserialize<List<DestinationsModel>>();
            foreach (var item in forImport)
            {
                destinations.InsertQuery(item.DestinationName, item.DestinationDescription);
            }
            DescriptionDgr.ItemsSource = null;
            DescriptionDgr.ItemsSource = destinations.GetData();
        }
    }
}
