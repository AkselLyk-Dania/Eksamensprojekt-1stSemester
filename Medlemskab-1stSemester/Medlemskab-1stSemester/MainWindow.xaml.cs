using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Medlemskab_1stSemester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            OpenMainWin();
        }

        private void OpenMainWin()
        {

        }

        ItemCollection members = new ItemCollection(); //Medlemmer
        ItemCollection activities = new ItemCollection(); //kurser

        private void Opret_Bruger_Click(object sender, RoutedEventArgs e)
        {
            AddUserWin window = new AddUserWin(members,MemberList,AddLog);
            window.ShowDialog();
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            if (MemberList.SelectedIndex != -1)
            {
                EditUserWin window = new EditUserWin(MemberList);
                window.ShowDialog();
            }
            else MessageBox.Show("Tryk på en bruger i listen for at redigere");
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (MemberList.SelectedIndex != -1)
            {
                MemberList.Items.RemoveAt(MemberList.SelectedIndex);
            }
            else MessageBox.Show("Tryk på en bruger i listen for at slette");
        }
    }
}
