using Medlemskab_1stSemester;
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
            if(!Admin.loggedIn)
            {
                LoginWin window = new LoginWin(AddLog);
                window.ShowDialog();
                this.Close();
            }
            AddLog.Items.Clear();
            AddLog.Items.Add("Du er logget ind som " + Admin.username + ". " + "Hej " + Admin.name + "!");
        }

        private void ChangeAdmin_Click(object sender, RoutedEventArgs e)
        {
            LoginWin window = new LoginWin(AddLog);
            window.ShowDialog();
        }

        ItemCollection members = new ItemCollection(); //Medlemmer
        ItemCollection activities = new ItemCollection(); //kurser


        /////////////////////////////////////
        ////////Håntering af medlemmer///////
        /////////////////////////////////////

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

        private void DeleteUser_Click(object sender, RoutedEventArgs e) //Admin vælger en bruger fra listen og trykker på "slet bruger"
        {
            if (MemberList.SelectedIndex != -1) //SelectedIndex er index på hvilken bruger man har valgt fra listen, hvis ingen er valgt returnerer -1
            {
                string name = members.mlist[MemberList.SelectedIndex].name; //Få navnet på brugeren
                if (MessageBox.Show($"Er du sikker på at du vil slette {name}?", "Slet Medlem", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    string temp;
                    MemberList.Items.RemoveAt(MemberList.SelectedIndex); //Fjern brugeren fra listboxen
                    members.mlist.RemoveAt(MemberList.SelectedIndex + 1); //Også fra listen
                    for (int i = 0; i < members.mlist.Count(); i++) //Et loop der går igennem alle fra listen i listboxen og opdaterer rækkefølgen (første tal)
                    {
                        temp = MemberList.Items[i].ToString(); //find nummer i som string som er temp
                        temp = i + 1 + temp.Substring(1); //temp er nu (i + 1) plus temp hvor første bogstav ikke er talt med
                        MemberList.Items[i] = temp; //Skift listen ud med temp
                    }
                    AddLog.Items.Add(name + " var slettet fra medlemslisten"); //Skriv en opdatering på informationsboxen
                }
            }
            else MessageBox.Show("Vælg en bruger ved at klikke på brugeren i listen for at slette"); //Hvis ingen er valgt i listen
        }
    }
}