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
    /// Interaction logic for AddActivityWin.xaml
    /// </summary>
    public partial class AddActivityWin : Window
    {
        ItemCollection activities;
        ListBox listbox;
        ListBox textbox;
        public AddActivityWin(ItemCollection activities, ListBox listbox, ListBox textbox)
        {
            //Alle variabler bliver lavet ens med objekterne
            this.activities = activities;
            this.listbox = listbox;
            this.textbox = textbox;
            InitializeComponent();
        }

        private void AddActivity_Click(object sender, RoutedEventArgs e) //Opret kursus
        {
            //Det der skrives i tekstboksen
            string titleInput = ActivityText.Text;

            //Første check er hvis tekstboksen ikke er tom, andet er hvis numre er brugt
            if (!string.IsNullOrEmpty(titleInput) && !titleInput.Any(x => Char.IsNumber(x)))
            {
                //Kursus bliver oprettet med navn der er skrevet og med en tom liste
                List<Member> list = new List<Member>();
                Activity activity = new Activity(titleInput, list);
                activities.alist.Add(activity);

                //En metode bliver brugt, som tæller alle kurser i listen
                int index = Admin.GetListTotal(activities, false);

                //Kursus bliver tilføjet til listboksen
                listbox.Items.Add($"{index}. {activity.name}");

                //Bliver også tilføjet til informationsboksen
                textbox.Items.Add($"{Admin.name}: {titleInput} var oprettet som nyt kursus");
                this.Close();
            }

            //Hvis der er skrevet numre, vises dette
            if (titleInput.Any(x => Char.IsNumber(x))) MessageBox.Show("Vær venligst ikke at bruge numre i navnefeltet");
        }

        private void CancelActivity_Click(object sender, RoutedEventArgs e) //Annuller
        {
            this.Close();
        }
    }
}
