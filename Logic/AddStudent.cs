using HighSchoolProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighSchoolProject.Logic
{
    internal class AddStudent
    {
        //adds student to database (not as detailed as personnel)
        public void AddStudentToDB()
        {
            using HighSchoolContext context = new HighSchoolContext();

            Console.WriteLine("Här kan du lägga till en ny elev i databasen! Fyll i uppgiftera nedan:");
            Console.Write("\nFörnamn:");
            string firstName = Console.ReadLine();
            Console.Write("\nEfternamn:");
            string lastName = Console.ReadLine();
            Console.Write("\nPersonnummer (12 siffror utan bindestreck eller mellanslag):");
            string personalNumber = Console.ReadLine();

            if (personalNumber.Length != 12)
            {
                Console.WriteLine("Personnumret måste vara 12-siffrigt");
            }
            else
            {
                var stuClass = context.Classes.OrderBy(c => c.ClassId);
                int countClass = context.Classes.Count();

                Console.WriteLine("Välj bland följande klasser:\n");
                foreach (var classes in stuClass)
                {
                    Console.WriteLine(classes.ClassId + ". " + classes.ClassName);
                }
                Console.WriteLine("Vilken klass tillhör eleven? (1-"+countClass+"):");
                int fkClassID = HelpfulMethods.ReadInt();

                //Creates new student based in users variables
                // and saves it to the database
                context.Add(new Student(firstName, lastName, personalNumber, fkClassID));
                context.SaveChanges();
                Console.WriteLine("Eleven har blivit tillagd i databasen!");
            }
            HelpfulMethods.PressKey();
        }
    }
}
