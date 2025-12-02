using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medlemskab_1stSemester
{
    public class Item
    {
        public string iD;
        public string name;

        public Item(string navn, string iD)
        {
            this.name = navn;
            this.iD = iD;
        }
    }
}
