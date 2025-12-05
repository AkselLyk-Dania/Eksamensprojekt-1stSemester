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
        public AddUserWin(ItemCollection users, ListBox listbox, ListBox textbox) //For at oprette fra et andet vindue skal man helst sende lister og tekstboke videre til ne constructor
        {
            this.users = users;
            this.listbox = listbox;
            this.textbox = textbox;
            InitializeComponent();
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            string titleInput = UserText.Text; //Det der skrives i tekstboksen
            if (!string.IsNullOrEmpty(titleInput) && !titleInput.Any(x => Char.IsNumber(x))) //Første check hvis feltet ikke er tomt, andet check hvis der ikke er numre skrevet ned
            {
                string student;
                string typeStudent;
                if (StudentCheck.IsChecked == true) //Hvis checkboksen er markeret, er det en student
                {
                    student = "Student";
                    typeStudent = "studerende";
                }
                else //Hvis ikke er det en privat
                {
                    student = "Privat";
                    typeStudent = student.ToLower();
                }
                Member member = new Member(titleInput, student); //laves et nyt Member objekt med navn og student/privat
                users.mlist.Add(member); //Bliver tilføjet til listen
                int index = users.mlist.Count(); //medlemsnummer

                listbox.Items.Add(index + ". " + member.name + " | " + member.isStudent); //Bliver tilføjet til medlemsboksen
                textbox.Items.Add($"{Admin.name}: {UserText.Text} var oprettet som {typeStudent} medlem"); //Bliver tilføjet til informationsboksen
                this.Close();
            }
            if(titleInput.Any(x => Char.IsNumber(x))) MessageBox.Show("Vær venligst ikke at bruge numre i navnefeltet"); //Hvis der er skrevet numre, vises dette
        }

        private void CancelUser_Click(object sender, RoutedEventArgs e) //Hvis man trykker på "annuller"
        {
            this.Close();
        }
    }
}