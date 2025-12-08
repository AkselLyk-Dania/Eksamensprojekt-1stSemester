using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Medlemskab_1stSemester
{

    public class Item //Denne bliver brugt til at lave en midlertidlig liste over medlemmer der er ledige i tilmeldingssiden
    {
        public string name;
        public Item(string name)
        {
            this.name = name;
        }
    }

    public class Activity : Item //For kurser, som nedarves af Item
    {
        public List<Member> list = new List<Member>(); //Hvert kurs har sin egen liste, hvor medlemmer bliver tilmeldt

        public Activity(string name, List<Member> list) : base(name) //Constructor
        {
            this.name = name;
            this.list = list;
        }
    }

    public class Member : Item //For Medlemmer, som nedarves af Item
    {
        public string isStudent; //Student eller privat
        public bool isActive; //Hvis medlem er tilmeldt eller ikke

        public Member(string name, string isStudent, bool isActive) : base(name)
        {
            this.name = name;
            this.isStudent = isStudent;
            this.isActive = isActive;
        }
    }

}
