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
        public EditUserWin(ItemCollection users, ListBox listbox, ListBox textbox, int index) //En constructor med en list, to listbokse og index integer
        {
            InitializeComponent();
            this.index = index; //Alle bliver kendt som det samme, som det der føres i variablerne
            this.listbox = listbox;
            this.textbox = textbox;
            this.users = users;
            Member member = users.mlist[index]; //Laver en objekt med den valgte medlem
            EditUserName.Text = member.name; //udfylder navn i feltet, som skal ændres
            string student = member.isStudent; //Vi får enten student eller privat
            if (student == "Student") EditStudentCheck.IsChecked = true; //Checkboksen bliver slået til, hvis han/hun var student
        }

        private void EditUserConfirm_Click(object sender, RoutedEventArgs e)
        {
            string titleInput = EditUserName.Text; //Det der skrives i tekstboksen
            if (!string.IsNullOrEmpty(titleInput) && !titleInput.Any(x => Char.IsNumber(x))) //Første check hvis feltet ikke er tomt, andet check hvis der ikke er numre skrevet ned
            {
                Member member = users.mlist[index]; //Laver en objekt med den nuværende valgte medlem
                string student;
                string typeStudent;
                if (EditStudentCheck.IsChecked == true) //hvis checkboksen er markeret er det en student
                {
                    student = "Student";
                    typeStudent = "studerende";
                }
                else //Hvis ikke er det en privat
                {
                    student = "Privat";
                    typeStudent = student.ToLower();
                }
                string oldMember = (member.name + " | " + member.isStudent); //Det her bruges til informationsboksen som viser hvad der var før ændringen
                member.name = titleInput; //Opdateres med nyt navn
                member.isStudent = student; //Og student/privat
                string newMember = (member.name + " | " + member.isStudent); //Bruges også til informationsboksen, som viser den nye ændring
                listbox.Items[index] = (index + 1 + ". " + member.name + " | " + member.isStudent); //Listen opdateres med nyt navn og student/privat

                textbox.Items.Add($"{Admin.name}: {oldMember} var ændret til {newMember}");

                this.Close();
            }
            if (titleInput.Any(x => Char.IsNumber(x))) MessageBox.Show("Vær venligst ikke at bruge numre i navnefeltet"); //Hvis der er skrevet numre, vises dette
        }

        private void CancelUserEdit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
