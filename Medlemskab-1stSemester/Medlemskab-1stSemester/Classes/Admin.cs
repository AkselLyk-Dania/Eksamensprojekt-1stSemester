using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Medlemskab_1stSemester
{
    public static class Admin //Admins eller brugerens oplysninger. Klassen kan ikke instantieres men i stedet finde eller ændre klassens variabler ved at bruge klassen selv
    {
        public static string name = "Daniel"; //Navn, Daniel er Default
        public static bool loggedIn = false; //Hvis brugeren åbner programmet først gang, åbner loginsiden og sætter denne bool til true


        public static int GetListTotal(ItemCollection item, bool isMember) //En metode, der finder hvor mange der er i en liste (enten mlist eller alist)
        {
            if (isMember) return item.mlist.Count();
            else return item.alist.Count();
        }

    }

}
