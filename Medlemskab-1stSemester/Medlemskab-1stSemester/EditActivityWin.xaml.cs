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
    /// Interaction logic for EditActivityWin.xaml
    /// </summary>
    public partial class EditActivityWin : Window
    {
        ListBox listbox;
        ListBox textbox;
        ItemCollection activities;
        public int index;
        public EditActivityWin(ItemCollection activities, ListBox listbox, ListBox textbox, int index)
        {
            InitializeComponent();
            this.index = index;
            this.listbox = listbox;
            this.textbox = textbox;
            this.activities = activities;

            //Tekstboksen bliver automatisk fyldt ud med navnet fra den nuværende kurs
            ActivityInput.Text = activities.alist[index].name;
        }

        private void EditActivityConfirm_Click(object sender, RoutedEventArgs e)
        {
            //Det nye der er skrivet i tekstboksen
            string titleInput = ActivityInput.Text;

            //Første check hvis feltet ikke er tomt, andet check hvis der ikke er numre skrevet ned
            if (!string.IsNullOrEmpty(titleInput) && !titleInput.Any(x => Char.IsNumber(x)))
            {
                //Ny objekt af kurs fra listen med den givende index
                Activity activity = activities.alist[index];

                //Bruges til informationsboksen
                string oldActivity = activity.name;

                //Udskifter navn til den nye
                activity.name = titleInput;

                //Kurs bliver ændret i listboksen
                //Hvis kursen har make 5 tilmeldte, vil den være (fuld)
                if (activity.list.Count >= 5) listbox.Items[index] = $"{index + 1}. {activity.name} (fuld)";
                else listbox.Items[index] = $"{index + 1}. {activity.name}";

                //Udfylder informationsboksen
                textbox.Items.Add($"{Admin.name}: {oldActivity} var ændret til {activity.name}");
                this.Close();
            }
            //Hvis der er skrevet numre, vises dette
            if (titleInput.Any(x => Char.IsNumber(x))) MessageBox.Show("Vær venligst ikke at bruge numre i navnefeltet");
        }

        private void CancelActivityEdit_Click(object sender, RoutedEventArgs e) //Annuller
        {
            this.Close();
        }
    }
}
