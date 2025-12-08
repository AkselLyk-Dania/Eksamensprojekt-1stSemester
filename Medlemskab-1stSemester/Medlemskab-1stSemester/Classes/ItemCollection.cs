using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medlemskab_1stSemester
{
    public class ItemCollection //Klasse til at lave objetker med lister
    {
        public List<Item> ilist = new List<Item>(); //For Item
        public List<Member> mlist = new List<Member>(); //For Member
        public List<Activity> alist = new List<Activity>(); //For Kurser
    }
}
