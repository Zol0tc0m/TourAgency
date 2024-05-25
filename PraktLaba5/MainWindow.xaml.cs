using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LoginDataTableAdapter adapter = new LoginDataTableAdapter();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var allLogins = adapter.GetData().Rows;

            for (int i = 0; i < allLogins.Count; i++)
            {
                if (allLogins[i][1].ToString() == LoginTbx.Text &&
                    allLogins[i][2].ToString() == PasswordTbx.Password)
                {
                    int roleID = (int)allLogins[i][3];

                    switch (roleID)
                    {
                        case 1:
                            DirWindow dirWindow = new DirWindow();
                            dirWindow.Show();
                            Close();
                            break;
                        case 2:
                            ManagerWindow managerWindow = new ManagerWindow();
                            managerWindow.Show();
                            Close();
                            break;
                        case 3:
                            AgentWindow agentWindow = new AgentWindow();
                            agentWindow.Show();
                            Close();
                            break;
                    }
                }

            }
        }
    }
}
