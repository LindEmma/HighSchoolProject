using HighSchoolProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

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
                Console.WriteLine("Elever is klassen:" + d.ClassName);
            }

            //Lists all students in chosenClass using foreach-loop
            foreach (Student student in chosenClass)
            {
                Console.WriteLine("*" + student.FirstName + " " + student.LastName);
            }
            HelpfulMethods.PressKey();
        }

        public void StoredProcedures()
        {
            Console.WriteLine("Välj id på elev:");
            int idStud = HelpfulMethods.ReadInt();

          

            var chosenStud = context.Students
                .FromSql($"EXECUTE dbo.ShowStudentInfo @StudentID ={idStud}")
                .ToList();

          

            foreach(var stud in chosenStud)
            {
                Console.WriteLine(stud.FirstName,stud.LastName);
            }
        }
    }
}
