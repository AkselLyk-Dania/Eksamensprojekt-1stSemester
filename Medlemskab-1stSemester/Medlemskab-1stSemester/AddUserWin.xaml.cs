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

        //Variabler fro objektet bliver genkendt som det samme, som klasserne der er oprette for oven
        public AddUserWin(ItemCollection users, ListBox listbox, ListBox textbox)
        {
            this.users = users;
            this.listbox = listbox;
            this.textbox = textbox;
            InitializeComponent();
        }

        private void AddUser_Click(object sender, RoutedEventArgs e) //Opret
        {
            //Det der skrives i tekstboksen
            string titleInput = UserText.Text;

            //Første check hvis feltet ikke er tomt, andet check hvis der ikke er numre skrevet ned
            if (!string.IsNullOrEmpty(titleInput) && !titleInput.Any(x => Char.IsNumber(x)))
            {

                string student = "Privat";
                string typeStudent = student.ToLower();

                //Hvis checkboksen er markeret, er det en student
                if (StudentCheck.IsChecked == true)
                {
                    student = "Student";
                    typeStudent = "studerende";
                }

                //laves et nyt Member objekt med navn, student/privat og status på tilmelding som ikke er lavet endnu
                Member member = new Member(titleInput,student,false);

                //Bliver tilføjet til listen
                users.mlist.Add(member);
                int index = Admin.GetListTotal(users,true); //Find hvor mange medlemmer

                //Bliver tilføjet til listboksen
                listbox.Items.Add(index + ". " + member.name + " | " + member.isStudent);

                //Og til informationsboksen
                textbox.Items.Add($"{Admin.name}: {UserText.Text} var oprettet som {typeStudent} medlem");
                this.Close();
            }
            //Hvis der er skrevet numre, vises dette
            if (titleInput.Any(x => Char.IsNumber(x))) MessageBox.Show("Vær venligst ikke at bruge numre i navnefeltet");
        }

        private void CancelUser_Click(object sender, RoutedEventArgs e) //Annuller
        {
            this.Close();
        }
    }
}