using HighSchoolProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace HighSchoolProject.Logic
{
    internal class ViewCourses
    {
        HighSchoolContext context = new HighSchoolContext();

        public void ViewAllCourses()
        {
            Table table = new Table()
            {
                Title = new TableTitle("Alla kurser i databasen")
            };

            table.AddColumn("KursID");
            table.AddColumn(new TableColumn("Kursnamn"));
            table.AddColumn(new TableColumn("Lärare"));
            table.AddColumn(new TableColumn("Startdatum"));
            table.AddColumn(new TableColumn("Slutdatum"));

            var courses = context.Courses.OrderByDescending(c => c.StartDate)
                .Include(p=>p.FkPersonnel);

            foreach(var c in courses)
            {
                table.AddRow(c.CourseId.ToString(), c.CourseName, c.FkPersonnel.FirstName + c.FkPersonnel.LastName, c.StartDate.ToString(), c.EndDate.ToString());
            }
            AnsiConsole.Write(table);
            HelpfulMethods.PressKey();
        }

        public void ViewActiveCourses()
        {
            DateOnly dt = DateOnly.FromDateTime(DateTime.Now);
            var active = context.Courses.Where(d => d.StartDate < dt && d.EndDate>dt);
            Console.WriteLine("Alla aktiva kurser:");

            Table table = new Table()
            {
                Title = new TableTitle("Alla pågående kurser")
            };
            table.AddColumn("KursID");
            table.AddColumn(new TableColumn("Kursnamn"));
            table.AddColumn(new TableColumn("Lärare"));
            table.AddColumn(new TableColumn("Startdatum"));
            table.AddColumn(new TableColumn("Slutdatum"));

            foreach (var a in active)
            {
                table.AddRow(a.CourseId.ToString(),a.CourseName,a.FkPersonnel.FirstName+" "+a.FkPersonnel.LastName,a.StartDate.ToString(),a.EndDate.ToString());
            }
            AnsiConsole.Write(table);
            HelpfulMethods.PressKey();
        }
    }
}
