using Medlemskab_1stSemester;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    /// Interaction logic for AddUserWin.xaml
    /// </summary>
    public partial class AddUserWin : Window
    {
        ItemCollection users;
        ListBox listbox;
        ListBox textbox;
        public AddUserWin(ItemCollection users, ListBox listbox, ListBox textbox)
        {
            this.users = users;
            this.listbox = listbox;
            this.textbox = textbox;
            InitializeComponent();
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            string titleInput = UserText.Text;
            if (!string.IsNullOrEmpty(UserText.Text) && !titleInput.Any(x => Char.IsNumber(x)))
            {
                Member member = new Member(titleInput, 15);
                users.mlist.Add(member);
                int index = users.mlist.Count();

                listbox.Items.Add(index + ". " + member.name);
                textbox.Items.Add(UserText.Text + " var oprettet som medlem");
                this.Close();
            }
            if(titleInput.Any(x => Char.IsNumber(x))) MessageBox.Show("Vær venligst ikke at bruge numre i teksten");
        }

        private void CancelUser_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}