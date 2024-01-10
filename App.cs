using HighSchoolProject.Logic;

namespace HighSchoolProject
{
    internal class App
    {
        bool RunApp { get; set; }
        bool StayInSmallMenu { get; set; }
        ViewPersonnel VP { get; set; }
        AddPersonnel AP { get; set; }
        ViewStudents VS { get; set; }
        AddStudent AS { get; set; }
        ViewSchoolClasses VSC { get; set; }
        ViewCourses VC { get; set; }
        ViewGrades VG { get; set; }
        AddGrade AG { get; set; }
        SectionsAndSalary SAS { get; set; }
        public App()
        {
            RunApp = true;
            VP = new ViewPersonnel();
            AP = new AddPersonnel();
            VS = new ViewStudents();
            AS = new AddStudent();
            VSC = new ViewSchoolClasses();
            VC = new ViewCourses();
            VG = new ViewGrades();
            AG = new AddGrade();
            SAS = new SectionsAndSalary();
        }

        //takes the user back to the start menu
        public void BackToStartMenu()
        {
            StayInSmallMenu = false;
        }

        //quits app
        public void Quit()
        {
            RunApp = false;
        }

        //the whole program in method Run(), has a switch case with the start menu and nested 
        // switch cases with smaller menus sorted by what user wants to see/add
        public void Run()
        {
            while (RunApp == true)
            {
                StayInSmallMenu = true;
                Console.Clear();
                PrintedMenus.Header();
                PrintedMenus.StartMenu(); //start menu
                int answer = HelpfulMethods.ReadInt();
                Console.Clear();
                PrintedMenus.Header();

                while (StayInSmallMenu)
                {
                    switch (answer)
                    {
                        //switch case menu (perAns) with choices for personnel
                        case 1:
                            Console.Clear();
                            PrintedMenus.Header();
                            PrintedMenus.PersonnelMenu();
                            int perAns = HelpfulMethods.ReadInt();
                            Console.Clear();
                            PrintedMenus.Header();
                            switch (perAns)
                            {
                                case 1:
                                    VP.ViewAllPersonnel(); //shows all personnel and their information
                                    break;
                                case 2:
                                    AP.AddPersonnelToDB(); //adds a personnel to the database
                                    break;
                                case 3:
                                    BackToStartMenu(); // goes back to the start menu
                                    break;
                                default:
                                    HelpfulMethods.ClearAgain();
                                    break;
                            }
                            break;

                            // switch case menu for choice students
                        case 2:
                            Console.Clear();
                            PrintedMenus.Header();
                            PrintedMenus.StudentMenu();
                            int studAns = HelpfulMethods.ReadInt();
                            Console.Clear();
                            PrintedMenus.Header();
                            switch (studAns)
                            {
                                case 1:
                                    VS.ViewAllStudents(); // shows all students in different orders
                                    break;
                                case 2:
                                    VS.StudentInfoFromID(); //lets user pick a student ID and shows student connected to it
                                    break;
                                case 3:
                                    AS.AddStudentToDB(); // adds student to database
                                    break;
                                case 4:
                                    BackToStartMenu(); //goes back to start menu
                                    break;
                                default:
                                    HelpfulMethods.ClearAgain();
                                    break;
                            }
                            break;

                        case 3:
                            VSC.ViewSchoolClass(); //shows all school classes and its students
                            BackToStartMenu();
                            break;

                            //switch case menu for courses
                        case 4:
                            Console.Clear();
                            PrintedMenus.Header();
                            PrintedMenus.CoursesMenu();
                            int couAns = HelpfulMethods.ReadInt();
                            Console.Clear();
                            PrintedMenus.Header();
                            switch (couAns)
                            {
                                case 1:
                                    VC.ViewAllCourses(); //shows all courses
                                    break;
                                case 2:
                                    VC.ViewActiveCourses(); //shows only active courses
                                    break;
                                case 3:
                                    BackToStartMenu(); // backs to start menu
                                    break;
                                default:
                                    HelpfulMethods.ClearAgain();
                                    break;
                            }
                            break;

                            //switch case menu for set grades
                        case 5:
                            Console.Clear();
                            PrintedMenus.Header();
                            PrintedMenus.GradesMenu();
                            int graAns = HelpfulMethods.ReadInt();
                            Console.Clear();
                            PrintedMenus.Header();

                            switch (graAns)
                            {
                                case 1:
                                    VG.AverageGrade(); // shows average grade in specific course
                                    break;
                                case 2:
                                    VG.ShowAllGrades(); // shows all set grades with info
                                    break;
                                case 3:
                                    AG.AddGradeToDB(); // adds a grade to db if user is a teacher
                                    break;
                                case 4:
                                    BackToStartMenu();// backs to start menu
                                    break;
                                default:
                                    HelpfulMethods.ClearAgain();
                                    break;
                            }
                            break;

                            // switch menu for the sections at school
                        case 6:
                            Console.Clear();
                            PrintedMenus.Header();
                            PrintedMenus.SectionsMenu();
                            int secAns = HelpfulMethods.ReadInt();
                            Console.Clear();
                            PrintedMenus.Header();

                            switch (secAns)
                            {
                                case 1:
                                    SAS.ViewSections(); // views all sections and their personnel
                                    break;
                                case 2:
                                    SAS.AverageSalary(); //shows average salary per section with bar chart
                                    break;
                                case 3:
                                    SAS.SalaryPerSection(); // shows sum of salary paid per month with bar chart
                                    break;
                                case 4:
                                    BackToStartMenu();// backs to start menu
                                    break;
                                default:
                                    HelpfulMethods.ClearAgain();
                                    break;
                            }
                            break;

                            //quits program with a message
                        case 7:
                            Console.WriteLine("Tack för att du använde High School DB!");
                            BackToStartMenu();
                            Quit();
                            break;
                        default:
                            HelpfulMethods.ClearAgain();
                            break;
                    }

                }
            }
        }
    }
}
