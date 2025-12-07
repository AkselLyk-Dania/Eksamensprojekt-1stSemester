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
            this.activities = activities;
            this.listbox = listbox;
            this.textbox = textbox;
            InitializeComponent();
        }

        private void AddActivity_Click(object sender, RoutedEventArgs e)
        {
            string titleInput = ActivityText.Text; //Det der skrives i tekstboksen
            if (!string.IsNullOrEmpty(titleInput) && !titleInput.Any(x => Char.IsNumber(x)))
            {
                List<Member> list = new List<Member>();
                Activity activity = new Activity(titleInput, list);
                activities.alist.Add(activity);
                int index = Admin.GetListTotal(activities, false);
                listbox.Items.Add($"{index}. {activity.name}");
                textbox.Items.Add($"{Admin.name}: {titleInput} var oprettet som nyt kurs"); //Bliver tilføjet til informationsboksen
                this.Close();
            }
            if (titleInput.Any(x => Char.IsNumber(x))) MessageBox.Show("Vær venligst ikke at bruge numre i navnefeltet"); //Hvis der er skrevet numre, vises dette
        }

        private void CancelActivity_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
