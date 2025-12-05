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
        public LoginWin(ListBox textbox) //Textboksen bruges kun når man skifter administrator
        {
            InitializeComponent();
            this.textbox = textbox;
            Arrays arrays = new Arrays();
            for (int i = 0; i < arrays.adminsList.Length; i++)
            { //Der oprettes automatisk admins fra Arrays.cs
                AdminList.Items.Add(arrays.adminsList[i]);
            }

        }

        private void TryLogin_Click(object sender, RoutedEventArgs e)
        {
            if (AdminList.SelectedIndex != -1) //Hvis brugeren har valgt en administrator
            {
                Arrays arrays = new Arrays();
                Admin.name = arrays.adminsList[AdminList.SelectedIndex]; //Admins navn er nu den, som man har valgt
                textbox.Items.Add("Du er logget ind som " + Admin.name + ". " + "Hej " + Admin.name + "!"); //Der oprettes information i hovedtekstboksen
                this.Close();
            }
            else MessageBox.Show("Tryk venligst på en administrator fra listen og tryk derefter på login"); //Hvis brugeren ikke har valgt en administrator
        }
    }
}
