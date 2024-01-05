﻿using HighSchoolProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighSchoolProject.Logic
{
    internal class AddStudent
    {
        public void AddStudentToDB()
        {
            using HighSchoolContext context = new HighSchoolContext();

            Console.WriteLine("Här kan du lägga till en ny elev i databasen! Fyll i uppgiftera nedan:");
            Console.WriteLine("Förnamn:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Efternamn:");
            string lastName = Console.ReadLine();
            Console.WriteLine("Personnummer (12 siffror utan bindestreck eller mellanslag):");
            string personalNumber = Console.ReadLine();

            var stuClass = context.Classes.OrderBy(c => c.ClassId);
            foreach (var classes in stuClass)
            {
                Console.WriteLine(classes.ClassId + ". " + classes.ClassName);
            }
            Console.WriteLine("Vilken klass tillhör eleven? (1-9):");
            int fkClassID = HelpfulMethods.ReadInt();

            //Creates new student based in users variables
            // and saves it to the database
            context.Add(new Student(firstName, lastName, personalNumber, fkClassID));
            context.SaveChanges();
            Console.WriteLine("Eleven har blivit tillagd i databasen!");
            HelpfulMethods.PressKey();
        }
    }
}