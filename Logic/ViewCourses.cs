using HighSchoolProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighSchoolProject.Logic
{
    internal class ViewCourses
    {
        HighSchoolContext context = new HighSchoolContext();

        public void ViewAllCourses()
        {
            Console.WriteLine("Alla kurser i databasen:");
            var courses = context.Courses.OrderByDescending(c => c.StartDate).Include(p=>p.FkPersonnel);

            foreach(var c in courses)
            {
                Console.WriteLine($"Start:{c.StartDate}\nSlut: \n{c.EndDate}\nKursnamn: {c.CourseName}\n Lärare: {c.FkPersonnel.FirstName} {c.FkPersonnel.LastName}");
            }
            HelpfulMethods.PressKey();
        }

        public void ViewActiveCourses()
        {
            DateOnly dt = DateOnly.FromDateTime(DateTime.Now);
            var active = context.Courses.Where(d => d.StartDate < dt && d.EndDate>dt);
            Console.WriteLine("Alla aktiva kurser:");

            foreach(var a in active)
            {
                Console.WriteLine($"Startdatum: {a.StartDate}\nSlutdatum: {a.EndDate}\n Kursnamn: {a.CourseName}");
            }
            HelpfulMethods.PressKey();
        }
    }
}
