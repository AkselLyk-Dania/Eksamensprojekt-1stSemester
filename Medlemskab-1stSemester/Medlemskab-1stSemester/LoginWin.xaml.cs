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
using System.Windows.Shapes;

namespace Medlemskab_1stSemester
{
    /// <summary>
    /// Interaction logic for LoginWin.xaml
    /// </summary>
    public partial class LoginWin : Window
    {
        ListBox textbox;
        public LoginWin(ListBox textbox)
        {
            InitializeComponent();
            this.textbox = textbox;
            Arrays arrays = new Arrays();
            for (int i = 0; i < arrays.adminsList.Length / 2; i++)
            {
                AdminList.Items.Add(arrays.adminsList[0, i]);
            }

        }

        private void TryLogin_Click(object sender, RoutedEventArgs e)
        {
            if (AdminList.SelectedIndex != -1)
            {
                Arrays arrays = new Arrays();
                Admin.username = arrays.adminsList[0, AdminList.SelectedIndex];
                Admin.name = arrays.adminsList[1, AdminList.SelectedIndex];
                if (!Admin.loggedIn)
                {
                    Admin.loggedIn = true;
                    MainWindow window = new MainWindow();
                    this.Close();
                    window.ShowDialog();
                }
                else
                {
                    textbox.Items.Add("Du er logget ind som " + Admin.username + ". " + "Hej " + Admin.name + "!");
                    this.Close();
                }
            }
            else MessageBox.Show("Tryk venligst på en administrator fra listen og tryk derefter på login");
        }
    }
}
