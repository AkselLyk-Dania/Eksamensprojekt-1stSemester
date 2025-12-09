using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Medlemskab_1stSemester
{
    /// <summary>
    /// Interaction logic for EditRegistrationWin.xaml
    /// </summary>
    public partial class EditRegistrationWin : Window
    {
        ItemCollection activities;
        ItemCollection members;
        ItemCollection listedMembers = new ItemCollection(); //Midlertidlig liste med ledige medlemmer
        ListBox listbox;
        ListBox textbox;
        public int index;
        public EditRegistrationWin(ItemCollection activities, ItemCollection members, ListBox listbox, ListBox textbox, int index)
        {
            this.activities = activities;
            this.members = members;
            this.listbox = listbox;
            this.textbox = textbox;
            this.index = index;
            InitializeComponent();

            //Titlen ændres til den kurs man har valgt at administrere
            //Hvis der er mere end 5 medlemmer tilmeldt vil der stå (fuld)
            if(activities.alist[index].list.Count >= 5) ActivityText.Text = $"Tilmelding for {activities.alist[index].name} (Fuld)";
            else ActivityText.Text = $"Tilmelding for {activities.alist[index].name}";

            //Begge lister renses og derefter opdateret
            AvailableMembers.Items.Clear();
            OccupiedMembers.Items.Clear();

            //Listen med ledige medlemmer bliver udfyldt
            int counter = 1;
            for (int i = 0; i < members.mlist.Count; i++)
            {
                //Kun medlemmer der er ledige dvs isActive is false bliver tilføjet
                if (members.mlist[i].isActive == false)
                {
                    //En objekt bliver lavet af medlems navn, som bliver tilføjet til en liste
                    Item item = new Item(members.mlist[i].name);
                    listedMembers.ilist.Add(item);

                    //Bliver også tilføjet til listboksen med ledige medlemmer
                    AvailableMembers.Items.Add($"{counter}. {item.name}");
                    counter++;
                }
            }

            //Tilmeldte medlemmer bliver tilføjet til listboksen
            for(int i = 0; i < activities.alist[index].list.Count; i++)
            {
                OccupiedMembers.Items.Add($"{i + 1}. {activities.alist[index].list[i].name}");
            }

        }

        private void Assign_Click(object sender, RoutedEventArgs e) //Tilmeld
        {
            //index på den valgte ledige medlem
            int selectedIndex = AvailableMembers.SelectedIndex;

            //Hvor mange der allerede er tilmeldt
            int assignedMembers = activities.alist[index].list.Count;

            //Hvis der er valgt en medlem og der er mindre end 5 medlemmer tilmeldt
            if (selectedIndex != -1 && assignedMembers < 5)
            {

                //Finder navn i listen
                string selectedName = listedMembers.ilist[selectedIndex].name;

                //Hvis navn er ens med navn i listen af all medlemmer
                for (int i = 0; i < members.mlist.Count; i++)
                {
                    if (members.mlist[i].name == selectedName)
                    {
                        activities.alist[index].list.Add(members.mlist[i]); //Bliver tilføjet til tilmeldingslisten
                        OccupiedMembers.Items.Add($"{OccupiedMembers.Items.Count + 1}. {members.mlist[i].name}"); //Skrevet på tekstboksen a tilmeldte
                        textbox.Items.Add($"{Admin.name}: {selectedName} var tilmeldt til {activities.alist[index].name}"); //Bliver skrevet i informationsboksen
                        members.mlist[i].isActive = true; //Denne medlem er nu tilmeldt


                        //Listen i hovedmenuen bliver opdateret, for at vise at den er (fuld) hvis der er 5 personer tilmeldt
                        if (activities.alist[index].list.Count >= 5)
                        {
                            ActivityText.Text = $"Tilmelding for {activities.alist[index].name} (Fuld)";
                            listbox.Items.Clear();
                            for (int l = 0; l < activities.alist.Count(); l++)
                            {
                                //Bliver tilføjet til listboksen
                                listbox.Items.Add($"{l + 1}. {activities.alist[l].name}");
                                //Hvis kursen som er tilføjet har maks antal tilmeldinger, vil der vises (fuld)
                                if (activities.alist[l].list.Count >= 5) listbox.Items[l] = listbox.Items[l] + " (Fuld)";
                            }
                        }

                       break; //Forsøger at gå ud a loopen
                    }
                }

                //Listen af ledige bliver nu opdateret
                if(activities.alist[index].list.Count >= 5) ActivityText.Text = $"Tilmelding for {activities.alist[index].name} (Fuld)";
                AvailableMembers.Items.Clear();
                listedMembers.ilist.Clear();
                int counter = 1;
                for (int i = 0; i < members.mlist.Count; i++)
                {
                    if (members.mlist[i].isActive == false)
                    {
                        Item item = new Item(members.mlist[i].name);
                        listedMembers.ilist.Add(item);
                        AvailableMembers.Items.Add($"{counter}. {item.name}");
                        counter++;
                    }
                }

            }
            if (assignedMembers >= 5) MessageBox.Show("Denne kurs er fyldt op med maks 5 medlemmer");
            else if(selectedIndex == -1) MessageBox.Show("Tryk venligst på en person i medlemslisten for at tilmelde");
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (OccupiedMembers.SelectedIndex != -1)
            {
                int selectedIndex = OccupiedMembers.SelectedIndex;
                string deletedName = activities.alist[index].list[selectedIndex].name;

                //Medlem bliver fjernet fra kursens liste
                activities.alist[index].list.RemoveAt(selectedIndex);
                textbox.Items.Add($"{Admin.name}: {deletedName} var afmeldt fra {activities.alist[index].name}");

                //Find medlem for at sætte til false
                for (int i = 0; i < members.mlist.Count; i++)
                {
                    if (members.mlist[i].name == deletedName) members.mlist[i].isActive = false;
                }

                //Medlem bliver fjernet fra listboksen
                OccupiedMembers.Items.RemoveAt(selectedIndex);

                //Listen med ledige medlemmer bliver opdateret
                AvailableMembers.Items.Clear();
                int counter = 1;
                for (int i = 0; i < members.mlist.Count; i++)
                {
                    if (members.mlist[i].isActive == false) //Bliver tilføjet til listen hvis ikke aktiv eller tilmeldt
                    {
                        Item item = new Item(members.mlist[i].name);
                        listedMembers.ilist.Add(item);
                        AvailableMembers.Items.Add($"{counter}. {members.mlist[i].name}");
                        counter++;
                    }
                }

                //Opdater listen med tilmeldte
                OccupiedMembers.Items.Clear();
                for (int i = 0; i < activities.alist[index].list.Count; i++)
                {
                    OccupiedMembers.Items.Add($"{i + 1}. {activities.alist[index].list[i].name}");
                }

                //Opdater listen i hovedmenuen hvis (fuld) skal fjernes
                listbox.Items.Clear();
                for (int l = 0; l < activities.alist.Count(); l++)
                {
                    //Bliver tilføjet til listboksen
                    listbox.Items.Add($"{l + 1}. {activities.alist[l].name}");
                    //Hvis kursen som er tilføjet har maks antal tilmeldinger, vil der vises (fuld)
                    if (activities.alist[l].list.Count >= 5) listbox.Items[l] = listbox.Items[l] + " (Fuld)";
                }

                ActivityText.Text = $"Tilmelding for {activities.alist[index].name}";
            }
            else MessageBox.Show("Tryk venligst på en person i tildmelingslisten for at afmelde");
        }

        private void GoBack_Click(object sender, RoutedEventArgs e) //Tilbage
        {
            RegistrationWin window = new RegistrationWin(activities, members, listbox, textbox);
            this.Close();
            window.ShowDialog();
        }

        //Tooltips er oprettet som (?), hvor så snart musen rammer den, viser den information
        //Denne funktion kører, når musen er indenfor teksten
        private void AvailableMemberInfo_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Help; //Musen bliver ændret til et ? ikon
        }

        //Denne funktion kører, når musen er udenfor teksten
        private void AvailableMemberInfo_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null; //Går tilbage til default
        }

        private void AssignedMemberInfo_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Help;
        }

        private void AssignedMemberInfo_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
        }
    }
}
