using HighSchoolProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighSchoolProject.Logic
{
    internal class ViewPersonnel
    {
        HighSchoolContext context = new HighSchoolContext();

        // Method that shows all personnel or teachers only
        public void ViewAllPersonnel()
        {
            string choice = "";
            do
            {
                Console.WriteLine("Vill du visa:\n1.All personal?\n2.Alla lärare?");
                choice = Console.ReadLine();
                if (choice != "1" && choice != "2")
                {
                    HelpfulMethods.ClearAgain();
                }
            } while (choice != "1" && choice != "2");
            Console.Clear();

            //Shows list of all personnel (last name and first name)
            if (choice == "1")
            {
                var personnel1 = context.Personnel.OrderBy(p => p.FirstName)
                    .Include(s => s.FkRole);
                Console.WriteLine("***All personal på skolan***");
                foreach (var personnel in personnel1)
                {
                    Console.WriteLine(personnel.FirstName + " " + personnel.LastName);
                }
            }

            // Shows list of all personnel that are teachers
            if (choice == "2")
            {
                //Collects personnel where RoleID is 3( = teachers)
                //and lists them with foreach-loop
                var teacher = context.Personnel.Where(s => s.FkRoleId == 3);
                Console.WriteLine("***Alla lärare på skolan***");
                foreach (var personnel in teacher)
                {
                    Console.WriteLine(personnel.FirstName + " " + personnel.LastName);
                }
            }
            Console.WriteLine("**************************\n");
            HelpfulMethods.PressKey();
        }
    }
}
