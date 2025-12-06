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
            this.index = index; //Alle bliver kendt som det samme, som det der føres i variablerne
            this.listbox = listbox;
            this.textbox = textbox;
            this.activities = activities;
            Activity activity = activities.alist[index]; //Laver en objekt med den valgte medlem
            ActivityInput.Text = activity.name;
        }

        private void EditActivityConfirm_Click(object sender, RoutedEventArgs e)
        {
            string titleInput = ActivityInput.Text; //Det der skrives i tekstboksen
            if (!string.IsNullOrEmpty(titleInput) && !titleInput.Any(x => Char.IsNumber(x))) //Første check hvis feltet ikke er tomt, andet check hvis der ikke er numre skrevet ned
            {
                Activity activity = activities.alist[index];
                string oldActivity = activity.name;
                activity.name = titleInput;
                listbox.Items[index] = index + 1 + ". " + activity.name;
                textbox.Items.Add($"{Admin.name}: {oldActivity} var ændret til {activity.name}");
                this.Close();
            }
            if (titleInput.Any(x => Char.IsNumber(x))) MessageBox.Show("Vær venligst ikke at bruge numre i navnefeltet"); //Hvis der er skrevet numre, vises dette
        }

        private void CancelActivityEdit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
