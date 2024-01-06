using HighSchoolProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

            bool check = context.Personnel.Where(p => p.PersonnelId == teacherID).IsNullOrEmpty();

            if (check)
            {
                Console.WriteLine("Ogiltigt lärarID");
                HelpfulMethods.PressKey();
            }
            else if (check == false)
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
                    var active = context.Courses.Where(d => d.StartDate < dt && d.FkPersonnelId == teacherID);

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

                        var stud = context.Grades.Where(e => e.FkCourseId == courseId)
                            .Include(s => s.FkStudent);

                        Console.WriteLine("Elever i kursen:");
                        foreach (var students in stud)
                        {
                            Console.WriteLine($"({students.FkStudentId}) {students.FkStudent.FirstName} {students.FkStudent.LastName} Betyg: {students.Grade1}");
                        }

                        Console.WriteLine("Vilken elev vill du sätta betyg på?");
                        int chosenStud = HelpfulMethods.ReadInt();

                        Console.WriteLine("Ange betyget: (1-5)");

                        byte gradeNum;
                        do
                        {
                            while (byte.TryParse(Console.ReadLine(), out gradeNum) == false)
                            {
                                Console.WriteLine("Skriv ett heltal");
                            }

                            if (gradeNum < 1 || gradeNum > 5)
                            {
                                Console.WriteLine("Vänligen välj ett betyg mellan 1-5");
                            }

                        } while (gradeNum < 1 || gradeNum > 5);

                        string ans = "";

                        do
                        {
                            Console.WriteLine("Vill du lägga till följande betyg i databasen? (j/n)");

                            var checkStudent = context.Grades.Where(g => g.FkCourseId == courseId && g.FkStudentId == chosenStud);

                            foreach (var cs in checkStudent)
                            {
                                Console.WriteLine($"Kurs: {cs.FkCourse.CourseName}\nElev: {cs.FkStudent.FirstName} {cs.FkStudent.LastName}\nBetyg: {gradeNum}");
                            }
                            ans = Console.ReadLine();
                            if (ans.ToLower() == "j")
                            {

                                var upDateStud = context.Grades.Where(c => c.FkCourseId == courseId && c.FkStudentId == chosenStud).First();

                                upDateStud.Grade1 = gradeNum;
                                upDateStud.GradeDateOfIssue = DateOnly.FromDateTime(DateTime.Now);
                                context.SaveChanges();
                                Console.WriteLine("Betyget är nu inlagt i databasen!");
                            }
                            else if (ans.ToLower() == "n")
                            {
                                Console.WriteLine("Betyget läggs inte in i databasen, ha en trevlig dag!");
                            }
                            else
                            {
                                HelpfulMethods.ClearAgain();
                            }
                        } while (ans.ToLower() != "n" && ans.ToLower() != "j");
                    }
                }
                HelpfulMethods.PressKey();
            }
        }
    }
}
