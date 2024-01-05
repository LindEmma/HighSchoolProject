using HighSchoolProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighSchoolProject.Logic
{
    internal class AddGrade
    {
        HighSchoolContext context = new HighSchoolContext();
        public void AddGradeToDB()
        {
            Console.WriteLine("Här kan du sätta betyg");
            Console.WriteLine("Ange ditt unika lärarID:");
            int teacherID = HelpfulMethods.ReadInt();

            bool check = context.Personnel.Where(p=>p.PersonnelId==teacherID).IsNullOrEmpty();
            
            if (check)
            {
                Console.WriteLine("Ogiltigt lärarID");
                HelpfulMethods.PressKey();
            }
            else if (check ==false)
            {
                var c = context.Personnel.Where(p => p.PersonnelId == teacherID && p.FkRoleId == 3);

                if (c.IsNullOrEmpty())
                {
                    Console.WriteLine();
                }
                else
                {
                    foreach (var k in c)
                    {
                        Console.WriteLine("Hej " + k.FirstName + "!");
                    }

                    Console.WriteLine("Dina kurser:");
                    DateOnly dt = DateOnly.FromDateTime(DateTime.Now);
                    var active = context.Courses.Where(d => d.StartDate < dt && d.FkPersonnelId==teacherID);

                    if (active.IsNullOrEmpty())
                    {
                        Console.WriteLine("Du har inga kurser att sätta betyg i");
                    }
                    else
                    {
                        foreach (var courses in active)
                        {
                            Console.WriteLine($"KursID: {courses.CourseId}\nKursdatum: {courses.StartDate} - {courses.EndDate}\n" +
                                $"Kursnamn: {courses.CourseName}");
                        }
                        Console.WriteLine("Vilken kurs vill du sätta betyg i?");
                        int courseId = HelpfulMethods.ReadInt();

                        var stud = context.Enrollments.Where(e => e.FkCourseId == courseId);

                        Console.WriteLine("Elever i kursen:");
                        foreach (var students in stud)
                        {
                            Console.WriteLine($"{students.FkStudent.FirstName} {students.FkStudent.LastName}");
                        }

                        //fyll i enrollments till alla kurser
                    }
                }
                HelpfulMethods.PressKey();
            }
        }
    }
}
