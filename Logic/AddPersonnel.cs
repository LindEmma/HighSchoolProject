using HighSchoolProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighSchoolProject.Logic
{
    internal class AddPersonnel
    {
        public void AddPersonnelToDB()
        {
            using HighSchoolContext context = new HighSchoolContext();
            int role, pCount;

            Console.WriteLine("Här kan du lägga till ny personal i databasen! Fyll i uppgiftera nedan:");
            Console.WriteLine("Förnamn:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Efternamn:");
            string lastName = Console.ReadLine();

            // Shows all titles to let user choose correctly from their ID
            Console.WriteLine("Vilken titel har personalen?");
            var roles = context.Roles.OrderBy(r=>r.Role1); //???????
            foreach (var r in roles)
            {
                Console.WriteLine(r.RoleId + ". " + r.Role1);
            }
            do
            {
                role = HelpfulMethods.ReadInt();
                pCount = context.Courses.Count();
                Console.WriteLine("Vilken titel har personen? (1-" + pCount + ":");

                if (role < 1 || role > pCount)
                {
                    Console.Clear();
                    Console.WriteLine("Vänligen välj 1-" + pCount);
                }
            } while (role < 1 || role > pCount);

            Console.WriteLine("Skriv in personalens månadslön i kronor:");
            decimal salary = HelpfulMethods.ReadDecimal();
            string answer = "";

            while(answer.ToLower()!="j"||answer.ToLower()!="n")
            {
                Console.WriteLine("Stämmer uppgifterna nedan? (j/n)");

                Console.WriteLine($"Namn:{firstName} {lastName}\n Yrkestitel: {role}\nMånadslön: {salary}");
                answer = Console.ReadLine();

                if (answer == "n" || answer == "N")
                {
                    Console.WriteLine("Personalen läggs inte in i databasen, testa gärna igen!");
                }
                else if (answer == "j" || answer == "J")//Creates new personnel, saves it to the database
                {

                    DateOnly date = DateOnly.FromDateTime(DateTime.Now);

                    context.Add(new Personnel(firstName, lastName, role, date, salary));
                    context.SaveChanges();
                    Console.WriteLine("Personalen har blivit tillagd i databasen!");
                }
                else
                {
                    HelpfulMethods.ClearAgain();
                }

                HelpfulMethods.PressKey();
            }
        }
    }
}
