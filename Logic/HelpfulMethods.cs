using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighSchoolProject.Logic
{
    public static class HelpfulMethods
    {
        public static void PressKey()
        {
            Console.WriteLine("\nTryck på valfri knapp för att gå tillbaka till menyn");
            Console.ReadKey();
        }

        public static int ReadInt()
        {
            int intNum;
            while (int.TryParse(Console.ReadLine(), out intNum) == false)
            {
                Console.WriteLine("Skriv ett heltal");
            }
            return intNum;
        }
        public static decimal ReadDecimal()
        {
            decimal decNum;
            while (decimal.TryParse(Console.ReadLine(), out decNum) == false)
            {
                Console.WriteLine("Skriv ett hel- eller decimaltal");
            }
            return decNum;
        }

        public static void ClearAgain()
        {
            Console.Clear();
            Console.WriteLine("\nVänligen välj ett alternativ");
        }
    }
}
