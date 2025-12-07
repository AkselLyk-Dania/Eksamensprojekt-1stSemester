using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medlemskab_1stSemester
{

    public class Arrays //Foruddefineret arrays som fyldes ud når programmet starter
    {
        private static class Members
        {
            //Array med medlemmer læses fra en txt fil inde i Files, lavet til privat (kan kun bruges i Arrays klassen) og static (kan åbnes som en værktøjskasse)
            public static string[] members = System.IO.File.ReadAllLines(@"Files/Members.txt");
        }

        //Array med tilgængelige admins som læses fra en txt fil inde i Files
        public string[] adminsList = System.IO.File.ReadAllLines(@"Files/Admins.txt");

        //Array med kurser som læses fra en txt fil inde i Files
        public string[] activitiesList = System.IO.File.ReadAllLines(@"Files/Activities.txt");

        //Multidimensional array, som indeholder medlemsnavn og student/privat
        public string[,] membersList =
        {
            { 
                Members.members[0],
                Members.members[1],
                Members.members[2],
                Members.members[3], 
                Members.members[4],
                Members.members[5],
                Members.members[6],
                Members.members[7],
                Members.members[8],
                Members.members[9]
            },

            {
                "Privat",
                "Student",
                "Privat",
                "Student",
                "Student",
                "Student",
                "Student",
                "Student",
                "Privat",
                "Privat"
            }
        };
    }
}
