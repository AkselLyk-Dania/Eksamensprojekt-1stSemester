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
    /// Interaction logic for RegistrationWin.xaml
    /// </summary>
    public partial class RegistrationWin : Window
    {
        ItemCollection activities;
        ItemCollection members;
        ListBox listbox;
        ListBox textbox;
        public RegistrationWin(ItemCollection activities, ItemCollection members, ListBox listbox, ListBox textbox)
        {
            this.listbox = listbox;
            this.textbox = textbox;
            this.activities = activities;
            this.members = members;
            InitializeComponent();

            //Listen af aktiviteter bliver opdateret
            ActivityList.Items.Clear();
            for(int i = 0; i < listbox.Items.Count; i++)
            {
                ActivityList.Items.Add(listbox.Items[i]);
            }

        }

        private void ToRegistration_Click(object sender, RoutedEventArgs e)
        {
            //Hvis der er en kurs der er valgt
            if (ActivityList.SelectedIndex != -1)
            {
                //Nyt vindue åbnes med administrering af den valgte kurs
                EditRegistrationWin window = new EditRegistrationWin(activities, members, listbox, textbox, ActivityList.SelectedIndex);
                this.Close();
                window.ShowDialog();
            }
            else MessageBox.Show("Tryk på et kurs i listen for at fortsætte");
        }

        private void CancelRegistration_Click(object sender, RoutedEventArgs e) //Annuller
        {
            this.Close();
        }
    }
}
