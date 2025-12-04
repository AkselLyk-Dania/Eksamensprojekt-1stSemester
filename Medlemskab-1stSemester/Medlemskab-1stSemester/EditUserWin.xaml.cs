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
        public EditUserWin(ListBox listbox)
        {
            InitializeComponent();
            this.listbox = listbox;
        }

        private void EditUserConfirm_Click(object sender, RoutedEventArgs e)
        {
            string id = EditUserID.Text;
            string name = EditUserText.Text;

            listbox.Items.Insert(0, "name");

            this.Close();

        }

        private void CancelUserEdit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
