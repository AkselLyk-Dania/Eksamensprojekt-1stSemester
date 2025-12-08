using Medlemskab_1stSemester;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
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
            if (!Admin.loggedIn) //Når vinduet åbnes for første gang, bruges til at tilføjet medlemmer, kurser og tilmeldinger
            {
                Admin.loggedIn = true;
                Arrays arrays = new Arrays(); //Nyt objekt fra Arrays klassen

                for(int i = 0; i < arrays.membersList.Length / 2; i++) //Der oprettes medlemmer i listen med navn og student/privat
                {
                    //Et nyt objekt med navn og student/privat ved hjælp af en multi-dimensionel array
                    Member member = new Member(arrays.membersList[0, i], arrays.membersList[1, i],false);

                    //Bliver tilføjet til listen
                    members.mlist.Add(member);

                    //Bliver tilføjet til listboksen med medlemmer
                    MemberList.Items.Add($"{i + 1}. {member.name} | {member.isStudent}");
                }

                //Der oprettes kurser i listen med kurser
                for (int i = 0; i < arrays.activitiesList.Length; i++)
                {
                    //Ny tilmeldingsliste til hvert kurs
                    List<Member> list = new List<Member>();
                    Activity activity = new Activity(arrays.activitiesList[i],list);
                    activities.alist.Add(activity);

                    //Bliver også skrevet i listboksen med kurser
                    ActivityList.Items.Add($"{i + 1}. {activity.name}");

                    //Den første kurs i listen bliver fyldt ud med tilmeldinger
                    if (i == 0)
                    {
                        for(int l = 0; l < 5; l++) //For loop der vil gå igennem de første 5 ledige medlemmer
                        {
                            activity.list.Add(members.mlist[l]); //Bliver tilføjet til tilmeldingslisten
                            members.mlist[l].isActive = true; //Medlem er nu tilmeldt
                        }
                        ActivityList.Items[i] = ActivityList.Items[i] + " (Fuld)";
                    }

                }
                OpenLogin();
            }
        }

        private void OpenLogin() //Funktion der åbner loginsiden
        {
            LoginWin window = new LoginWin(AddLog); //Nyt objekt af vindue + AddLog some bruges til information
            window.ShowDialog(); //Vis vinduet
        }

        private void ChangeAdmin_Click(object sender, RoutedEventArgs e) //Skift administrator
        {
            OpenLogin();
        }

        private void Registration_Click(object sender, RoutedEventArgs e) //Gå til tilmelding
        {
            if(Admin.GetListTotal(activities,false) >= 5) //Der skal mindste være 5 kurser oprettet, før man kan begynde at tilmelde
            {
                //Nyt objekt af tilmeldingsvindue + listen med kurser, medlemmer, listbox med kurser og information
                RegistrationWin window = new RegistrationWin(activities, members, ActivityList, AddLog);
                window.ShowDialog();
            }
            else MessageBox.Show("Der skal være mindst 5 oprettede kurser før du kan fortsætte");
        }

        ItemCollection members = new ItemCollection(); //Medlemmer
        ItemCollection activities = new ItemCollection(); //kurser

        /////////////////////////////////////
        ////////Håntering af kurser//////////
        /////////////////////////////////////

        private void AddActivity_Click(object sender, RoutedEventArgs e) //Opret kurs
        {
            AddActivityWin window = new AddActivityWin(activities,ActivityList,AddLog);
            window.ShowDialog();
        }

        private void EditActivity_Click(object sender, RoutedEventArgs e) //Rediger kurs
        {
            //SelectedIndex er index på hvilken bruger man har valgt fra listen som integer hvor den første vil started med 0, hvis ingen er valgt returnerer -1
            if (ActivityList.SelectedIndex != -1)
            {
                EditActivityWin window = new EditActivityWin(activities, ActivityList, AddLog, ActivityList.SelectedIndex);
                window.ShowDialog();
            }
            else MessageBox.Show("Tryk venligst på et kurs i listen for at redigere");
        }

        private void DeleteActivity_Click(object sender, RoutedEventArgs e) //Slet kurs
        {
            int index = ActivityList.SelectedIndex;

            //Hvis SelectedIndex er andet end -1 og hvis der er stadig er tilmeldte i kursen
            if (index != -1 && activities.alist[index].list.Count < 1)
            {
                //Få kursets navn
                string name = activities.alist[index].name;

                //Et "er du sikker" vindue, hvor man enten trykker på yes eller no
                if (MessageBox.Show($"Er du sikker på at du vil slette {name}?", "Slet Kurs", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    //Fjern brugeren fra listboxen
                    activities.alist.RemoveAt(index);

                    //Et loop der opdaterer listboksen med aktiviteter
                    ActivityList.Items.Clear();
                    for(int i = 0; i < activities.alist.Count(); i++)
                    {
                        //Bliver tilføjet til listboksen
                        ActivityList.Items.Add($"{i + 1}. {activities.alist[i].name}");

                        //Hvis kursen som er tilføjet har maks antal tilmeldinger, vil der vises (fuld)
                        if (activities.alist[i].list.Count >= 5) ActivityList.Items[i] = ActivityList.Items[i] + " (Fuld)";

                    }

                    //Skriv en opdatering på informationsboxen
                    AddLog.Items.Add(Admin.name + ": " + name + " var slettet fra listen med kurser");
                }
            }
            //Hvis der er tilmeldte til kursen du prøver at slette skal du første afmelde alle fra den før du gør det
            if (activities.alist[index].list.Count > 0) MessageBox.Show("Vær venligst at afmelde alle tilmeldte til denne kurs, før du sletter den");

            //Hvis ingen er valgt i listen
            else if (index == -1) MessageBox.Show("Vælg et kurs ved at klikke på brugeren i listen for at slette");
        }

        /////////////////////////////////////
        ////////Håntering af medlemmer///////
        /////////////////////////////////////

        private void AddUser_Click(object sender, RoutedEventArgs e) //Opret en bruger
        {
            //Der laves et objekt fra AddUserWin med variabler, som bruges til når du skal åbne vinduet
            AddUserWin window = new AddUserWin(members,MemberList,AddLog);

            //Vinduet med oprettelse af bruger åbnes
            window.ShowDialog();
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            //SelectedIndex er index på hvilken bruger man har valgt fra listen som integer hvor den første vil started med 0, hvis ingen er valgt returnerer -1
            if (MemberList.SelectedIndex != -1)
            {
                EditUserWin window = new EditUserWin(members, MemberList, AddLog, MemberList.SelectedIndex);
                window.ShowDialog();
            }
            else MessageBox.Show("Tryk på en medlem i listen for at redigere");
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e) //Admin vælger en bruger fra listen og trykker på "slet bruger"
        {
            //Find den valgte medlem som integer
            int index = MemberList.SelectedIndex;

            //Find status på om medlem er tilmeldt eller ikke
            //Først definer active og lave en check fordi den kan ramme en null værdi hvis index is -1, safety first
            bool active = false;
            if(index != -1) active = members.mlist[index].isActive;

            //SelectedIndex er index på hvilken bruger man har valgt fra listen, hvis ingen er valgt returnerer -1
            if (index != -1 && !active)
            {
                //Få navnet på brugeren
                string name = members.mlist[index].name;

                //Et "er du sikker" vindue, hvor man enten trykker på yes eller no
                if (MessageBox.Show($"Er du sikker på at du vil slette {name}?", "Slet Medlem", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    //Slet medlem fra listen
                    members.mlist.RemoveAt(index);

                    //Listboksen med medlemmer bliver opdateret
                    MemberList.Items.Clear();
                    for (int i = 0; i < members.mlist.Count(); i++)
                    {
                        MemberList.Items.Add($"{i + 1}. {members.mlist[i].name} | {members.mlist[i].isStudent}");
                    }

                    //Skriv en opdatering på informationsboxen
                    AddLog.Items.Add(Admin.name + ": " + name + " var slettet fra medlemslisten");
                }
            }
            //Hvis medlem er tilmeldt, skal du først afmelde før du sletter
            if (active) MessageBox.Show("Vær venligst at afmelde personen fra den tilmeldte kurs, før du sletter");

            //Hvis ingen er valgt i listen
            else if (index == -1) MessageBox.Show("Vælg en bruger ved at klikke på personen i listen for at slette");
        }
    }
}