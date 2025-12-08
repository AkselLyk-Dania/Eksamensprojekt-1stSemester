using Medlemskab_1stSemester;
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
    /// Interaction logic for EditUserWin.xaml
    /// </summary>
    public partial class EditUserWin : Window
    {
        ListBox listbox;
        ListBox textbox;
        ItemCollection users;
        public int index;

        //En constructor med en list, to listbokse og index integer
        public EditUserWin(ItemCollection users, ListBox listbox, ListBox textbox, int index)
        {
            InitializeComponent();

            //Alle bliver kendt som det samme, som det der føres i variablerne
            this.index = index;
            this.listbox = listbox;
            this.textbox = textbox;
            this.users = users;

            //Laver en objekt med den valgte medlem
            Member member = users.mlist[index];

            //udfylder navn i feltet, som skal ændres
            EditUserName.Text = member.name;

            //Der fås enten student eller privat
            string student = member.isStudent;

            //Checkboksen bliver slået til, hvis han/hun var student
            if (student == "Student") EditStudentCheck.IsChecked = true;
        }

        private void EditUserConfirm_Click(object sender, RoutedEventArgs e)
        {
            //Det der skrives i tekstboksen
            string titleInput = EditUserName.Text;

            //Første check hvis feltet ikke er tomt, andet check hvis der ikke er numre skrevet ned
            if (!string.IsNullOrEmpty(titleInput) && !titleInput.Any(x => Char.IsNumber(x)))
            {
                //Laver en objekt med den nuværende valgte medlem
                Member member = users.mlist[index];
                string student;
                string typeStudent;

                //hvis checkboksen er markeret er det en student
                if (EditStudentCheck.IsChecked == true)
                {
                    student = "Student";
                    typeStudent = "studerende";
                }

                //Hvis ikke er det en privat
                else
                {
                    student = "Privat";
                    typeStudent = student.ToLower();
                }

                //Det her bruges til informationsboksen som viser hvad der var før ændringen
                string oldMember = member.name + " | " + member.isStudent;

                //Opdateres med nyt navn
                member.name = titleInput;

                //Og student/privat
                member.isStudent = student;

                //Bruges også til informationsboksen, som viser den nye ændring
                string newMember = member.name + " | " + member.isStudent;

                //Listen opdateres med nyt navn og student/privat
                listbox.Items[index] = index + 1 + ". " + member.name + " | " + member.isStudent;

                //Tekst til informationsboksen
                textbox.Items.Add($"{Admin.name}: {oldMember} var ændret til {newMember}");

                this.Close();
            }

            //Hvis der er skrevet numre, vises dette
            if (titleInput.Any(x => Char.IsNumber(x))) MessageBox.Show("Vær venligst ikke at bruge numre i navnefeltet");
        }

        private void CancelUserEdit_Click(object sender, RoutedEventArgs e) //Annuller
        {
            this.Close();
        }

        
    }
}
