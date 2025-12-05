using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medlemskab_1stSemester
{
    public class Arrays //Foruddefineret arrays som fyldes ud når programmet starter
    {
        public string[,] membersList = //Multi-dimensionel array med medlemmer og alder som oprettes til start
         {
            { "Hans Holm", "Simon Kjærsgaard", "Gitte Lund", "Simone Jensen", "Troels Dahl", "Kaj Hedelund Vestergaard", "Andrea Lundbæk", "Sebastian Thomsen", "Emil Thorsted", "Hans Henrik Dynesen" },
            { "Privat", "Student", "Privat", "Student", "Student", "Student", "Student", "Student", "Privat", "Privat" }
        };

       public string[] adminsList = //Array med tilgængelige admins
        {
          "Erik",
          "Daniel",
          "Marie"
        };

        public string[] activitiesList = //Tilgængelige kurser hvor første er optaget
         {
          "Datamatiker (Optaget)",
          "IT-Supporter",
          "SoftwareUdvikler",
          "Web Designer",
          "E-Handel"
        };
    }
}
