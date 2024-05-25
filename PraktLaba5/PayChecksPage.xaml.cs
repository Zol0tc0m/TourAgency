using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using PraktLaba5.TravelAgency1DataSetTableAdapters;
using MessageBox = System.Windows.Forms.MessageBox;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

namespace PraktLaba5
{
    /// <summary>
    /// Логика взаимодействия для PayChecksPage.xaml
    /// </summary>
    public partial class PayChecksPage : Page
    {
        PaymentsTableAdapter payments = new PaymentsTableAdapter();
        public PayChecksPage()
        {
            InitializeComponent();
            PayChecksDgr.ItemsSource = payments.GetData();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            object id = (PayChecksDgr.SelectedItem as DataRowView).Row[0];
            payments.DeleteQuery(Convert.ToInt32(id));
            PayChecksDgr.ItemsSource = payments.GetData();
        }
        private void Unload_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовый документ (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.Unicode);
                try
                {
                    var export = payments.GetData().Rows;
                    for (int i = 0; i < export.Count; i++)
                    {
                        int id = (int)export[i][0];
                        int bid = (int)export[i][1];
                        string cost = (string)export[i][2];
                        string date = (string)export[i][3];
                        sw.Write("ID\tBooking ID\tCost\t\tDate\n");
                        sw.Write(id + "\t" + bid + "\t\t" + cost + "\t\t" + date + "\n");
                        sw.Write(" \r\n");
                    }
                    sw.Close();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }

            }
        }
    }
}