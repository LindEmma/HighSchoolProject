using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighSchoolProject
{
    // printed menus and header (text only)
    public static class PrintedMenus
    {
        public static void Header()
        {
            Console.WriteLine("╦ ╦┬┌─┐┬ ┬  ╔═╗┌─┐┬ ┬┌─┐┌─┐┬  \r\n╠═╣││ ┬├─┤  ╚═╗│  ├─┤│ ││ ││  \r\n╩ ╩┴└─┘┴ ┴  ╚═╝└─┘┴ ┴└─┘└─┘┴─┘\r\n" +
                "        ╔╦╗╔╗                 \r\n         ║║╠╩╗                \r\n        ═╩╝╚═╝\n");
        }
        public static void StartMenu()
        {
            Console.WriteLine("Välkommen till skoldatabasen!\n");
            Console.WriteLine("Vad vill du visa?");
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
            Console.WriteLine("Vad vill du göra?\n");
            Console.WriteLine("(1) Visa all personal");
            Console.WriteLine("(2) Lägg till personal i databasen");
            Console.WriteLine("(3) Gå tillbaka till startmenyn");
        }

        public static void StudentMenu()
        {
            Console.WriteLine("Vad vill du göra?\n");
            Console.WriteLine("(1) Visa alla elever");
            Console.WriteLine("(2) Sök på elev utefter ID-nummer");
            Console.WriteLine("(3) Lägg till ny elev i databasen");
            Console.WriteLine("(4) Gå tillbaka till startmenyn");
        }


        public static void CoursesMenu()
        {
            Console.WriteLine("Vad vill du visa?\n");
            Console.WriteLine("(1) Alla kurser");
            Console.WriteLine("(2) Bara aktuella kurser");
            Console.WriteLine("(3) Gå tillbaka till startmenyn");
        }

        public static void GradesMenu()
        {
            Console.WriteLine("Vad vill du göra?\n");
            Console.WriteLine("(1) Visa snittbetyg");
            Console.WriteLine("(2) Visa alla satta betyg");
            Console.WriteLine("(3) Lägg till nytt betyg i databasen");
            Console.WriteLine("(4) Gå tillbaka till startmenyn");
        }

        public static void SectionsMenu()
        {
            Console.WriteLine("Vad vill du visa?\n");
            Console.WriteLine("(1) Översikt: avdelningar");
            Console.WriteLine("(2) Snittlön per avdelning");
            Console.WriteLine("(3) Totalt utbetald lön per månad och avdelning");
            Console.WriteLine("(4) Gå tillbaka till startmenyn");
        }
    }
}
