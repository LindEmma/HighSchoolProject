using HighSchoolProject.Logic;

namespace HighSchoolProject
{
    internal class App
    {
        bool RunApp { get; set; }
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
        public void Quit()
        {
            RunApp = false;
        }

        public void Run()
        {
            while (RunApp == true)
            {
                Console.Clear();
                PrintedMenus.Header();
                PrintedMenus.StartMenu();
                int answer = HelpfulMethods.ReadInt();
                Console.Clear();
                PrintedMenus.Header();

                switch (answer)
                {
                    case 1:
                        PrintedMenus.PersonnelMenu();
                        int perAns = HelpfulMethods.ReadInt();
                        Console.Clear();
                        switch (perAns)
                        {
                            case 1:
                                VP.ViewAllPersonnel(); //klar
                                break;
                            case 2:
                                AP.AddPersonnelToDB(); //klar
                                break;
                            case 3:
                                break;
                            default:
                                HelpfulMethods.ClearAgain();
                                break;
                        }
                        break;

                    case 2:
                        PrintedMenus.StudentMenu();
                        int studAns = HelpfulMethods.ReadInt();
                        Console.Clear();
                        switch (studAns)
                        {
                            case 1:
                                VS.ViewAllStudents(); //klar
                                break;
                            case 2:
                                VS.StudentInfoFromID();
                                break;
                            case 3:
                                AS.AddStudentToDB(); //klar
                                break;
                            case 4:
                                break;
                            default:
                                HelpfulMethods.ClearAgain();
                                break;
                        }
                        break;

                        case 3:
                        VSC.ViewSchoolClass(); //tomt
                        break;

                    case 4:
                        Console.Clear();
                        PrintedMenus.CoursesMenu();
                        int couAns = HelpfulMethods.ReadInt();
                        Console.Clear();
                        switch (couAns)
                        {
                            case 1:
                                VC.ViewAllCourses(); //klar
                                break;
                            case 2:
                                VC.ViewActiveCourses(); //klar?
                                break;
                            case 3:
                                break;
                            default:
                                HelpfulMethods.ClearAgain();
                                break;
                        }
                        break;

                    case 5:
                        Console.Clear();
                        PrintedMenus.GradesMenu();
                        int graAns = HelpfulMethods.ReadInt();
                        Console.Clear();

                        switch (graAns)
                        {
                            case 1:
                                VG.AverageGrade(); // klar
                                break;
                            case 2:
                                VG.ShowAllGrades(); // klar
                                break;
                            case 3:
                                AG.AddGradeToDB(); //Klar
                                break;
                            case 4:
                                break;
                            default:
                                HelpfulMethods.ClearAgain();
                                break;
                        }
                        break;

                    case 6:
                        Console.Clear();
                        PrintedMenus.SectionsMenu();
                        int secAns = HelpfulMethods.ReadInt();
                        Console.Clear();

                        switch (secAns)
                        {
                            case 1:
                                SAS.ViewSections(); // klar
                                break;
                            case 2:
                                SAS.AverageSalary(); //klar
                                break;
                            case 3:
                                SAS.SalaryPerSection(); //klar
                                break;
                            case 4:
                                //logga ut till startmenyn
                                break;
                            default:
                                HelpfulMethods.ClearAgain();
                                break;
                        }
                        break;

                    case 7:
                        Console.WriteLine("Tack för att du använde High School App!");
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
