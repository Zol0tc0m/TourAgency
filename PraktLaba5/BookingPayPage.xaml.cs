using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
    /// Логика взаимодействия для BookingPayPage.xaml
    /// </summary>
 
    public partial class BookingPayPage : Page
    {
        BookingsTableAdapter bookings = new BookingsTableAdapter();
        PaymentsTableAdapter payments = new PaymentsTableAdapter();

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
        public BookingPayPage()
        {
            InitializeComponent();
            BookingDgr.ItemsSource = bookings.GetData();
        }

        private void Pay_Click(object sender, RoutedEventArgs e)
        {
            object id = (BookingDgr.SelectedItem as DataRowView).Row[0];
            DateTime time = DateTime.Now;
            if (PriceTbx.Text != "" && !HasSpecialChars(PriceTbx.Text) && IsNumber(PriceTbx.Text))
            {
                payments.InsertQuery(Convert.ToInt32(id), PriceTbx.Text, Convert.ToString(time));
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены верным типом данных!");
            }
        }
    }
}
