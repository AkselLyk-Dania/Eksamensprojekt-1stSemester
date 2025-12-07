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
        ListBox listbox;
        public RegistrationWin(ItemCollection activities, ListBox listbox)
        {
            this.listbox = listbox;
            this.activities = activities;
            InitializeComponent();
            ActivityList.Items.Clear();

            for(int i = 0; i < listbox.Items.Count; i++)
            {
                ActivityList.Items.Add(listbox.Items[i]);
            }

        }

        private void ToRegistration_Click(object sender, RoutedEventArgs e)
        {
            if (ActivityList.SelectedIndex != -1)
            {

            }
            else MessageBox.Show("Tryk på et kurs i listen for at fortsætte");
        }

        private void CancelRegistration_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
