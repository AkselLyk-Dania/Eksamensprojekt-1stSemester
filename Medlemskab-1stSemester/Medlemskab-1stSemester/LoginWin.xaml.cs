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

        //Textboksen bruges kun når man skifter administrator
        public LoginWin(ListBox textbox)
        {
            InitializeComponent();
            this.textbox = textbox;
            Arrays arrays = new Arrays();

            //Der oprettes automatisk admins fra Arrays.cs
            for (int i = 0; i < arrays.adminsList.Length; i++)
            {
                AdminList.Items.Add(arrays.adminsList[i]);
            }

        }

        private void TryLogin_Click(object sender, RoutedEventArgs e)
        {
            //Hvis brugeren har valgt en administrator
            if (AdminList.SelectedIndex != -1)
            {
                Arrays arrays = new Arrays();

                //Admins navn er nu den, som man har valgt
                Admin.name = arrays.adminsList[AdminList.SelectedIndex];

                //Der oprettes information i hovedtekstboksen
                textbox.Items.Add($"Du er logget ind som {Admin.name}. Hej {Admin.name}!");
                this.Close();
            }
            //Hvis brugeren ikke har valgt en administrator
            else MessageBox.Show("Tryk venligst på en administrator fra listen og tryk derefter på login");
        }
    }
}
