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
    /// Логика взаимодействия для BookingsPage.xaml
    /// </summary>
    public partial class BookingsPage : Page
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

        ClientsTableAdapter clients = new ClientsTableAdapter();
        ToursTableAdapter tours = new ToursTableAdapter();
        GuidesTableAdapter guides = new GuidesTableAdapter();
        EmployeesTableAdapter employees = new EmployeesTableAdapter();
        BookingsTableAdapter bookings = new BookingsTableAdapter();
        public BookingsPage()
        {
            InitializeComponent();
            ClientComboBox.ItemsSource = clients.GetData();
            ClientComboBox.DisplayMemberPath = "Surname";
            TourComboBox.ItemsSource = tours.GetData();
            TourComboBox.DisplayMemberPath = "TourName";
            GuideComboBox.ItemsSource = guides.GetData();
            GuideComboBox.DisplayMemberPath = "Surname";
            EmployeeComboBox.ItemsSource = employees.GetData();
            EmployeeComboBox.DisplayMemberPath = "Surname";
            BookingsDgr.ItemsSource = bookings.GetData();
        }
        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (BookingDateTbx.Text != "")
            {
                bookings.InsertQuery(ClientComboBox.SelectedIndex + 1, TourComboBox.SelectedIndex + 1, GuideComboBox.SelectedIndex + 1, EmployeeComboBox.SelectedIndex + 1, BookingDateTbx.Text);
            }
            else
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены и заполнены верным типом данных!");
            }
            BookingsDgr.ItemsSource = bookings.GetData();
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            object id = (BookingsDgr.SelectedItem as DataRowView).Row[0];
            if (BookingDateTbx.Text != "")
            {
                bookings.UpdateQuery(ClientComboBox.SelectedIndex + 1, TourComboBox.SelectedIndex + 1, GuideComboBox.SelectedIndex + 1, EmployeeComboBox.SelectedIndex + 1, BookingDateTbx.Text, Convert.ToInt32(id));
            }
            else
            {
                MessageBox.Show("Все текстовые поля должны быть заполнены и заполнены верным типом данных!");
            }
            BookingsDgr.ItemsSource = bookings.GetData();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            object id = (BookingsDgr.SelectedItem as DataRowView).Row[0];
            bookings.DeleteQuery(Convert.ToInt32(id));
            BookingsDgr.ItemsSource = bookings.GetData();
        }
        private void BookingsDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BookingsDgr.SelectedItem as DataRowView != null)
            {
                object clients = (BookingsDgr.SelectedItem as DataRowView).Row[1];
                ClientComboBox.SelectedIndex = (int)clients - 1;
                object tours = (BookingsDgr.SelectedItem as DataRowView).Row[2];
                TourComboBox.SelectedIndex = (int)tours - 1;
                object guides = (BookingsDgr.SelectedItem as DataRowView).Row[3];
                GuideComboBox.SelectedIndex = (int)guides - 1;
                object employees = (BookingsDgr.SelectedItem as DataRowView).Row[4];
                EmployeeComboBox.SelectedIndex = (int)employees - 1;
                object date = (BookingsDgr.SelectedItem as DataRowView).Row[5];
                BookingDateTbx.Text = date.ToString();
            }
        }
    }
}
