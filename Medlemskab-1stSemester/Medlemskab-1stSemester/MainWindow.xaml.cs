using Medlemskab_1stSemester;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
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
            if (!Admin.loggedIn) //Når vinduet åbnes for første gang
            {
                Admin.loggedIn = true;
                Arrays arrays = new Arrays();

                for(int i = 0; i < arrays.membersList.Length / 2; i++) //Der oprettes medlemmer i listen med navn og student/privat
                {
                    Member member = new Member(arrays.membersList[0, i], arrays.membersList[1, i]); //Et nyt objekt med navn og student/privat ved hjælp af en multi-dimensionel array
                    members.mlist.Add(member);
                    MemberList.Items.Add($"{i + 1}. {member.name} | {member.isStudent}");
                }

                for(int i = 0; i < arrays.activitiesList.Length; i++) ////Der oprettes kurser i listen med kurser
                {
                    Activity activity = new Activity(arrays.activitiesList[i]);
                    activities.alist.Add(activity);
                    ActivityList.Items.Add($"{i+1}. {activity.name}");
                }
                OpenLogin();
            }
        }

        private void OpenLogin()
        {
            LoginWin window = new LoginWin(AddLog);
            window.ShowDialog();
        }

        private void ChangeAdmin_Click(object sender, RoutedEventArgs e) //Skift administrator
        {
            OpenLogin();
        }

        ItemCollection members = new ItemCollection(); //Medlemmer
        ItemCollection activities = new ItemCollection(); //kurser

        /////////////////////////////////////
        ////////Håntering af kurser//////////
        /////////////////////////////////////

        private void AddActivity_Click(object sender, RoutedEventArgs e)
        {
            AddActivityWin window = new AddActivityWin(activities,ActivityList,AddLog);
            window.ShowDialog();
        }

        private void EditActivity_Click(object sender, RoutedEventArgs e)
        {
            if (ActivityList.SelectedIndex != -1)
            { //SelectedIndex er index på hvilken bruger man har valgt fra listen som integer hvor den første vil started med 0, hvis ingen er valgt returnerer -1
                EditActivityWin window = new EditActivityWin(activities, ActivityList, AddLog, ActivityList.SelectedIndex);
                window.ShowDialog();
            }
            else MessageBox.Show("Tryk på et kurs i listen for at redigere");
        }

        private void DeleteActivity_Click(object sender, RoutedEventArgs e)
        {
            if (ActivityList.SelectedIndex != -1) //SelectedIndex er index på hvilken bruger man har valgt fra listen, hvis ingen er valgt returnerer -1
            {
                int index = ActivityList.SelectedIndex;
                string name = activities.alist[index].name; //Få kursets navn
                if (MessageBox.Show($"Er du sikker på at du vil slette {name}?", "Slet Kurs", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    string temp;
                    int add = 1;
                    int counter = 0;
                    ActivityList.Items.RemoveAt(index); //Fjern brugeren fra listboxen
                    activities.alist.RemoveAt(index); //Også fra listen
                    for (int i = 0; i < activities.alist.Count(); i++) //Et loop der går igennem alle fra listen i listboxen og opdaterer rækkefølgen (første tal)
                    {
                        temp = ActivityList.Items[i].ToString(); //find nummer i som string som er temp
                        counter++;
                        if (counter >= 9) //hvis nummeret er større eller 9, vil der fjernes flere characters fra temp.Substring
                        {
                            counter = 0;
                            add++;
                        }
                        temp = i + 1 + temp.Substring(add); //temp er nu (i + 1) plus temp hvor antal characters ikke er talt med
                        ActivityList.Items[i] = temp; //Skift listen ud med temp
                    }
                    AddLog.Items.Add(Admin.name + ": " + name + " var slettet fra listen med kurser"); //Skriv en opdatering på informationsboxen
                }
            }
            else MessageBox.Show("Vælg et kurs ved at klikke på brugeren i listen for at slette"); //Hvis ingen er valgt i listen
        }

        /////////////////////////////////////
        ////////Håntering af medlemmer///////
        /////////////////////////////////////

        private void AddUser_Click(object sender, RoutedEventArgs e) //Opret en bruger
        {
            AddUserWin window = new AddUserWin(members,MemberList,AddLog); //Der laves et objekt fra AddUserWin med variabler, som bruges til når du skal åbne vinduet
            window.ShowDialog(); //Vinduet med oprettelse af bruger åbnes
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            if (MemberList.SelectedIndex != -1)
            { //SelectedIndex er index på hvilken bruger man har valgt fra listen som integer hvor den første vil started med 0, hvis ingen er valgt returnerer -1
                EditUserWin window = new EditUserWin(members, MemberList, AddLog, MemberList.SelectedIndex);
                window.ShowDialog();
            }
            else MessageBox.Show("Tryk på en medlem i listen for at redigere");
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e) //Admin vælger en bruger fra listen og trykker på "slet bruger"
        {
            if (MemberList.SelectedIndex != -1) //SelectedIndex er index på hvilken bruger man har valgt fra listen, hvis ingen er valgt returnerer -1
            {
                int index = MemberList.SelectedIndex;
                string name = members.mlist[index].name; //Få navnet på brugeren
                if (MessageBox.Show($"Er du sikker på at du vil slette {name}?", "Slet Medlem", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    string temp;
                    int add = 1;
                    int counter = 0;
                    MemberList.Items.RemoveAt(index); //Fjern brugeren fra listboxen
                    members.mlist.RemoveAt(index); //Også fra listen
                    for (int i = 0; i < members.mlist.Count(); i++) //Et loop der går igennem alle fra listen i listboxen og opdaterer rækkefølgen (første tal)
                    {
                        temp = MemberList.Items[i].ToString(); //find nummer i som string som er temp
                        counter++;
                        if (counter >= 9) //hvis nummeret er større eller 9, vil der fjernes flere characters fra temp.Substring
                        { 
                            counter = 0; 
                            add++; 
                        }
                        temp = i + 1 + temp.Substring(add); //temp er nu (i + 1) plus temp hvor antal characters ikke er talt med
                        MemberList.Items[i] = temp; //Skift listen ud med temp
                    }
                    AddLog.Items.Add(Admin.name + ": " + name + " var slettet fra medlemslisten"); //Skriv en opdatering på informationsboxen
                }
            }
            else MessageBox.Show("Vælg en bruger ved at klikke på brugeren i listen for at slette"); //Hvis ingen er valgt i listen
        }
    }
}