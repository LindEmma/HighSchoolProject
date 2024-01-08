using HighSchoolProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Spectre.Console;

namespace HighSchoolProject.Logic
{
    public class ViewGrades
    {
        HighSchoolContext context = new HighSchoolContext();

        //Shows the average grades
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

                //Asks which course the user wants to see average grade of
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
                var grades = context.Grades.Where(s => s.FkCourseId == choice && s.Grade1.HasValue).Average(s => s.Grade1);
                var gradeRounded = Math.Round((double)grades, 2);
                Console.WriteLine("Snittbetyget i ämnet är: " + gradeRounded);
            }
            HelpfulMethods.PressKey();
        }

        //Shows all grades and their information in a table
        public void ShowAllGrades() //spectre console tabell?
        {
            Table table = new Table()
            {
                Title = new TableTitle("Alla satta betyg")
            };
            table.AddColumn("Datum för betygsättning");
            table.AddColumn(new TableColumn("Betyg"));
            table.AddColumn(new TableColumn("Elev"));
            table.AddColumn(new TableColumn("Kurs"));
            table.AddColumn(new TableColumn("Lärare"));

            //Selects grades, personnel, students and course where there is a set grade
            //ordered by date
            var grades = context.Grades
                .Include(s => s.FkPersonnel)
                .Include(s => s.FkStudent)
                .Include(c => c.FkCourse)
                .OrderByDescending(d => d.GradeDateOfIssue)
                .Where(g => g.Grade1.HasValue);

            //if there is no set grades the program tells the user so
            if (grades.IsNullOrEmpty())
            {
                Console.WriteLine("Det finns inga satta betyg");
            }
            //else it is added to the rows in the table
            else
            {
                foreach (var grade in grades)
                {
                    table.AddRow(grade.GradeDateOfIssue.ToString(), grade.Grade1.ToString(), grade.FkStudent.FirstName + " " + grade.FkStudent.LastName, grade.FkCourse.CourseName, grade.FkPersonnel.FirstName + " " + grade.FkPersonnel.LastName);
                }
                AnsiConsole.Write(table); //prints table   
            }
            HelpfulMethods.PressKey();
        }
    }
}