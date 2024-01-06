using HighSchoolProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Spectre.Console;

namespace HighSchoolProject.Logic
{
    internal class ViewStudents
    {
        HighSchoolContext context = new HighSchoolContext();

        // Method to view all students in the school (first name + last name)
        //Lets user choose how to group and order
        public void ViewAllStudents()
        {
            int answer2, answer3;

            do
            {
                Console.WriteLine("Vill du sortera eleverna efter:");
                Console.WriteLine("1. Förnamn\n2. Efternamn\n?");
                answer2 = HelpfulMethods.ReadInt();
                if (answer2 != 1 && answer2 != 2)
                {
                    HelpfulMethods.ClearAgain();
                }
            } while (answer2 != 1 && answer2 != 2);
            do
            {
                Console.WriteLine("Ska namnen vara i:\n1.Stigande ordning?\n2.fallande ordning?");
                answer3 = HelpfulMethods.ReadInt();
                if (answer3 != 1 && answer3 != 2)
                {
                    HelpfulMethods.ClearAgain();
                }
            } while (answer3 != 1 && answer3 != 2);

            var students = context.Students.OrderBy(b => b.FirstName);
            string orderStr = "";

            // Lists students by first names in ascendind order
            if (answer2 == 1 && answer3 == 1)
            {
                students = context.Students.OrderBy(b => b.FirstName);
                orderStr = "förnamn A-Ö";
            }

            //Lists students by first names in descending order
            if (answer2 == 1 && answer3 == 2)
            {
                students = context.Students.OrderByDescending(b => b.FirstName);
                orderStr = "förnamn Ö-A";
            }

            //Last names ascending order
            if (answer2 == 2 && answer3 == 1)
            {
                students = context.Students.OrderBy(b => b.LastName);
                orderStr = "efternamn A-Ö";
            }

            //Last names descending orders
            if (answer2 == 2 && answer3 == 2)
            {
                students = context.Students.OrderByDescending(b => b.LastName);
                orderStr = "efternamn Ö-A";
            }

            //Lists all students in chosen order by a foreach-loop
            Console.WriteLine("*** Alla elever på skolan ***");
            Console.WriteLine($"*** Sorterat: {orderStr} ***");
            foreach (var student in students)
            {
                Console.WriteLine(student.FirstName + " " + student.LastName);
            }
            Console.WriteLine("**************************\n");
            HelpfulMethods.PressKey();
        }


        //Method to show the students in a chosen class
        public void ViewStudInClass()
        {
            var classes = context.Classes.OrderBy(c => c.ClassId);
            int classint;

            //shows list of classes and asks which one to see the students of. 
            // classint is compared with ClassID
            Console.WriteLine("Klasser:");
            foreach (var x in classes)
            {
                Console.WriteLine(x.ClassId + ". " + x.ClassName);
            }
            do
            {
                {
                    Console.WriteLine("Vilken klass vill du se? (1-9)");

                    classint = HelpfulMethods.ReadInt();
                }
                if (classint < 1 || classint > 9)
                {
                    Console.WriteLine("Vänligen välj en klass mellan 1-9");
                    HelpfulMethods.PressKey();
                }
            } while (classint < 1 || classint > 9);

            //Collects the students of the class which relates to the class id
            var chosenClass = context.Students.Where(s => s.FkClassId.Equals(classint))
                    .Include(s => s.FkClass);

            //Collects the name of the chosen class
            Console.Clear();
            var classname = context.Classes.Where(s => s.ClassId == classint);
            foreach (var d in classname)
            {
                Console.WriteLine("Elever i klassen:" + d.ClassName);
            }

            //Lists all students in chosenClass using foreach-loop
            foreach (Student student in chosenClass)
            {
                Console.WriteLine("*" + student.FirstName + " " + student.LastName);
            }
            HelpfulMethods.PressKey();
        }

        public void StudentInfoFromID()
        {
            var table = new Table()
            {
                Title = new TableTitle("Elevens information", "bold green")
            };
            var table2 = new Table()
            {
                Title = new TableTitle("Slutförda kurser", "red")
            };
            var table3 = new Table()
            {
                Title = new TableTitle("Aktiva eller kommande kurser", "blue")
            };

            table.AddColumn("Namn");
            table.AddColumn(new TableColumn("Personnummer").Centered());
            table.AddColumn(new TableColumn("Kön").Centered());
            table.AddColumn(new TableColumn("Klass").Centered());

            table2.AddColumn("Kursnamn");
            table2.AddColumn(new TableColumn("Kursstart").Centered());
            table2.AddColumn(new TableColumn("Kursslut").Centered());
            table2.AddColumn(new TableColumn("Lärare").Centered());
            table2.AddColumn(new TableColumn("Betyg").Centered());

            table3.AddColumn("Kursnamn");
            table3.AddColumn(new TableColumn("Kursstart").Centered());
            table3.AddColumn(new TableColumn("Kursslut").Centered());
            table3.AddColumn(new TableColumn("Lärare").Centered());

            Console.WriteLine("Välj id på elev:");
            int idStud = HelpfulMethods.ReadInt();

            var chosenStud = context.Students.Where(s => s.StudentId == idStud)
                .Include(s => s.FkClass).ToList();
            if (chosenStud.Count == 0)
            {
                Console.WriteLine("Det finns ingen elev med det ID-numret");
            }
            else
            {
                var grades = context.Grades.Where(g => g.FkStudentId == idStud && g.Grade1.HasValue)
                                .Include(c => c.FkCourse)
                                .Include(p => p.FkPersonnel).ToList();

                var acticeCourses = context.Grades
                    .Include(c => c.FkCourse)
                    .Include(p=>p.FkPersonnel)
                    .Where(e => e.FkStudentId == idStud && e.FkCourse.EndDate > DateOnly.FromDateTime(DateTime.Now));

                foreach (var stud in chosenStud)
                {
                    table.AddRow(stud.FirstName + " " + stud.LastName, stud.PersonalNumber
                       ,stud.StudentGender, stud.FkClass.ClassName);
                }

                foreach (var g in grades)
                {
                    table2.AddRow(g.FkCourse.CourseName, g.FkCourse.StartDate.ToString(), g.FkCourse.EndDate.ToString(), 
                        g.FkPersonnel.FirstName + " " + g.FkPersonnel.LastName, g.Grade1.ToString());
                }


                //gör inte rätt med datumen
                foreach (var ac in acticeCourses)
                {
                    table3.AddRow(ac.FkCourse.CourseName, ac.FkCourse.StartDate.ToString()
                        , ac.FkCourse.EndDate.ToString(), ac.FkPersonnel.FirstName + " " + ac.FkPersonnel.LastName);
                }

                AnsiConsole.Write(table);
                Console.WriteLine();
                AnsiConsole.Write(table2);
                Console.WriteLine();
                AnsiConsole.Write(table3);
            }
            HelpfulMethods.PressKey();
        }
    }
}
