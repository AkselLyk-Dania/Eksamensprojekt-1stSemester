using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Medlemskab_1stSemester
{
    public class Activity //kurser, som kun bruger navn
    {
        public string name;

        public Activity(string name)
        {
            this.name = name;
        }
    }

    public class Member : Activity //Brugere, som både bruger navn og alder og nedarver kurser
    {
        public string isStudent;
        
        public Member(string name, string isStudent) : base(name)
        {
            this.name = name;
            this.isStudent = isStudent;
        }
    }

}
