using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighSchoolProject
{
    public static class PrintedMenus
    {
        public static void StartMenu()
        {
            Console.WriteLine("Välkommen till skoldatabasen \n**** High School App ****\n");
            Console.WriteLine("Välj ett alternativ:");
            Console.WriteLine("(1) Personal");
            Console.WriteLine("(2) Elever");
            Console.WriteLine("(3) Klasser");
            Console.WriteLine("(4) Kurser");
            Console.WriteLine("(5) Betyg");
            Console.WriteLine("(6) Avdelningar och löner");
            Console.WriteLine("(7) Stäng av programmet");
        }

        public static void PersonnelMenu()
        {
            Console.WriteLine("(1) Visa all personal");
            Console.WriteLine("(2) Lägg till personal i databasen");
        }

        public static void StudentMenu()
        {
            Console.WriteLine("(1) Visa alla elever");
            Console.WriteLine("(2) Sök på elev utefter ID-nummer");
            Console.WriteLine("(3) Lägg till ny elev i databasen");
        }


        public static void CoursesMenu()
        {
            Console.WriteLine("(1) Visa alla kurser");
            Console.WriteLine("(2) Visa alla aktuella kurser");
        }

        public static void GradesMenu()
        {
            Console.WriteLine("(1) Visa snittbetyg");
            Console.WriteLine("(2) Visa alla satta betyg");
            Console.WriteLine("(3) Lägg till nytt betyg i databasen");
        }

        public static void SectionsMenu()
        {
            Console.WriteLine("(1) Visa översikt: avdelningar");
            Console.WriteLine("(2) Visa snittlön per avdelning");
            Console.WriteLine("(3) Visa totalt utbetald lön per månad och avdelning");
        }
    }
}
