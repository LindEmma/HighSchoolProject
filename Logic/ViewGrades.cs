using HighSchoolProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HighSchoolProject.Logic
{
    public class ViewGrades
    {
        HighSchoolContext context = new HighSchoolContext();

        //Shows the set grades
        public void AverageGrade()
        {
                int courseCount;
                int choice;

                //shows list of subjects grouped by their id
                do
                {
                    Console.WriteLine("Alla ämnen:");
                    var courses = context.Courses.OrderBy(s => s.CourseId);
                    courseCount = context.Courses.Count(); //stores the amount of subjects in subjectCount

                    foreach (var course in courses)
                    {
                        Console.WriteLine(course.CourseId + ". " + course.CourseName);
                    }
                    Console.WriteLine("Vilket ämne vill du se snittbetyget i? 1-" + courseCount);
                    choice = HelpfulMethods.ReadInt();

                    if (choice < 1 || choice > courseCount)
                    {
                        Console.Clear();
                        Console.WriteLine("Siffran du angav matchar inget ämne på listan, prova igen.\n");
                    }
                } while (choice < 1 || choice > courseCount);

                // checks if the chosen subject has any set grades
                var check = context.Grades.Where(s => s.FkCourseId == choice).IsNullOrEmpty();

                //if check is null nothing more happens, else average grade for chosen subject is shown 
                if (check == true)
                {
                    Console.WriteLine("Det finns inga satta betyg i ämnet");
                }
                else
                {
                    //grades returns a double, calculated with Average() from the chosen grade
                    var grades = context.Grades.Where(s => s.FkCourseId == choice&&s.Grade1.HasValue).Average(s => s.Grade1);
                    var gradeRounded = Math.Round((double)grades, 2);
                    Console.WriteLine("Snittbetyget i ämnet är: " + gradeRounded);
                }
            HelpfulMethods.PressKey();
        }

        public void ShowAllGrades() //spectre console tabell?
        {
            Console.WriteLine($"Alla satta betyg:");

            var grades = context.Grades.Include(s => s.FkPersonnel).Include(s => s.FkStudent).Include(c => c.FkCourse).OrderByDescending(d => d.GradeDateOfIssue);

            foreach (var grade in grades)
            {
                Console.WriteLine($"{grade.GradeDateOfIssue} - {grade.Grade1} - {grade.FkStudent.FirstName} {grade.FkStudent.LastName} - {grade.FkCourse.CourseName} - {grade.FkPersonnel.FirstName} {grade.FkPersonnel.LastName}");
            }
            HelpfulMethods.PressKey();
        }
    }
}
