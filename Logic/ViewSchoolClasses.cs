using HighSchoolProject.Models;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace HighSchoolProject.Logic
{
    internal class ViewSchoolClasses
    {
        HighSchoolContext context = new HighSchoolContext();
        public void ViewSchoolClass()
        {
            Console.WriteLine("Alla skolklasser:");

            var sc = context.Classes.OrderBy(c => c.ClassId).Distinct();
            int classID;
            do
            {
                foreach (var s in sc)
                {
                    Console.WriteLine($"({s.ClassId}) {s.ClassName}");
                }
                Console.WriteLine("\nVälj id-numret för en klass för att se dess elever");
                classID = HelpfulMethods.ReadInt();


                if (classID < 1 || classID > 9)
                {
                    Console.WriteLine("Vänligen välj en klass mellan 1-9");
                    HelpfulMethods.PressKey();
                }
            } while (classID < 1 || classID > 9);

            //Collects the students of the class which relates to the class id
            var chosenClass = context.Students.Where(s => s.FkClassId.Equals(classID))
                    .Include(s => s.FkClass);

            //Collects the name of the chosen class
            Console.Clear();
            var classname = context.Classes.Where(s => s.ClassId == classID);

            Table table = new Table();
            
            foreach (var d in classname)
            {
                table.AddColumn("Elever i klassen: " + d.ClassName);
            }
            
            //Lists all students in chosenClass using foreach-loop
            foreach (Student student in chosenClass)
            {
                table.AddRow(student.FirstName + " " + student.LastName);
            }
            AnsiConsole.Write(table);
            HelpfulMethods.PressKey();
        }
    }
}
