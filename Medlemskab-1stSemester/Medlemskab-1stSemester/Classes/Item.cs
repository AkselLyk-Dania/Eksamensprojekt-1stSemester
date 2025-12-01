using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medlemskab_1stSemester
{
    public class Item
    {
        public int iD;
        public string name;

        public Item(string navn, int iD)
        {
            this.name = navn;
            this.iD = iD;
        }
    }
}
