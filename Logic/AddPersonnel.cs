using HighSchoolProject.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Spectre.Console;

namespace HighSchoolProject.Logic
{
    internal class AddPersonnel
    {
        //adds new personnel to database
        public void AddPersonnelToDB()
        {
            using HighSchoolContext context = new HighSchoolContext();
            int role, roleCount;

            //user enters first name and last name
            Console.WriteLine("Här kan du lägga till ny personal i databasen! Fyll i uppgiftera nedan:");
            Console.Write("\nFörnamn:");
            string firstName = Console.ReadLine();
            Console.Write("\nEfternamn:");
            string lastName = Console.ReadLine();

            // Shows all titles to let user choose correctly from their ID
            do
            {
                Console.WriteLine("\nYrkesroller att välja bland:");
                var roles = context.Roles.OrderBy(r => r.RoleId); //selects roles ordered by id
                foreach (var r in roles)
                {
                    Console.WriteLine(r.RoleId + ". " + r.Role1);
                }

                roleCount = context.Roles.Count();  //counts amount of roles in db
                Console.Write("\nVilken yrkesroll har personen? (1-" + roleCount + ":");
                role = HelpfulMethods.ReadInt();

                if (role < 1 || role > roleCount)
                {
                    Console.Clear();
                    Console.WriteLine("Vänligen välj 1-" + roleCount);
                }
            } while (role < 1 || role > roleCount);

            //asks for the personnels salary
            Console.Write("Skriv in personalens månadslön i kronor:");
            decimal salary = HelpfulMethods.ReadDecimal();

            // asks user if the information is correct or not
            string answer = "";
            while (answer.ToLower() != "j" || answer.ToLower() != "n")
            {
                Console.WriteLine("Stämmer uppgifterna nedan? (j/n)");

                Console.WriteLine($"Namn:{firstName} {lastName}\n Yrkesroll: {role}\nMånadslön: {salary}");
                answer = Console.ReadLine();

                //if user is not happy with the input, the personnel is not added to db
                if (answer == "n" || answer == "N")
                {
                    Console.WriteLine("Personalen läggs inte in i databasen, testa gärna igen!");
                }

                //if user answer J, new personnel is added to db
                //along with todays date as employment date
                else if (answer == "j" || answer == "J")
                {

                    DateOnly date = DateOnly.FromDateTime(DateTime.Now);

                    context.Add(new Personnel(firstName, lastName, role, date, salary));
                    context.SaveChanges();
                    Console.WriteLine("Personalen har blivit tillagd i databasen!");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Vänligen svara j eller n");
                }
            }
            HelpfulMethods.PressKey();
        }
    }
}
