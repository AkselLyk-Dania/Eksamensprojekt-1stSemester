using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Medlemskab_1stSemester
{
    public class Activity //kurser
    {
        public string name;
        public List<Member> list = new List<Member>();

        public Activity(string name, List<Member> list)
        {
            this.name = name;
            this.list = list;
        }
    }

    public class Member //Medlem
    {
        public string isStudent;
        public string name;
        public bool isActive;

        public Member(string name, string isStudent)
        {
            this.name = name;
            this.isStudent = isStudent;
        }
    }

}
