using HighSchoolProject.Models;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace HighSchoolProject.Logic
{
    internal class SectionsAndSalary
    {
        HighSchoolContext context = new HighSchoolContext();

        //lets user choose section and shows its personnel
        public void ViewSections()
        {
            int sectionCount;
            int chosenSection;
            
            //selects sections ordered by id
            var sections = context.Sections.OrderBy(p => p.SectionId).Distinct();
            sectionCount = context.Sections.Count();

            //shows sections in table and lets user choose by id
            Table table = new Table();
            table.AddColumn("Alla avdelningar");

            //while chooser doesn't pick valid id, they have to choose a new one
            do
            {    
                foreach (var s in sections)
                {
                    table.AddRow("(" + s.SectionId + ") " + s.SectionName);
                }
                AnsiConsole.Write(table);

                Console.WriteLine("Välj avdelning(1-" + sectionCount + ")");
                chosenSection = HelpfulMethods.ReadInt();
                Console.Clear();

                if (chosenSection < 1 || chosenSection > sectionCount)
                {
                    AnsiConsole.MarkupLine("Vänligen välj 1-" + sectionCount);
                }
            } while (chosenSection < 1 || chosenSection > sectionCount);

            //selects chosen section name
            var sectName = context.Sections.Where(s => s.SectionId == chosenSection);
            
            //selects personnel and some of their info from chosen section
            var pers = context.Personnel.Where(p => p.FkSectionId == chosenSection)
                .Include(r => r.FkRole)
                .Include(s => s.FkSection)
                .OrderBy(s=>s.FkSectionId);

            //creates new table to show personnel info of chosen section
            Table table2 = new Table();
            table2.AddColumn("Namn");
            table2.AddColumn(new TableColumn("Yrkesroll"));

            foreach (var p in pers)
            {
                table2.AddRow(p.FirstName+" "+p.LastName,p.FkRole.Role1);

            }
            foreach(var s in sectName)
            {
                Console.WriteLine("All personal på avdelning: "+s.SectionName);
            }

            AnsiConsole.Write(table2); //prints table
            HelpfulMethods.PressKey();
        }

        public void AverageSalary() //gör en tabell med spectre console!
        {
            //creates bar chart in spectre console
            BarChart bc = new BarChart();

            Console.WriteLine("Snittlöner per avdelning (kr/månad)");

            //counts average salary per section and adds them to bar chart
            var avgSalary = context.AverageSalaries.OrderBy(a => a.Avdelning);
            foreach (var a in avgSalary)
            {
                bc.AddItem(a.Avdelning, (double)a.Medellönen, Color.Yellow);
            }
            //prints chart
            AnsiConsole.Write(bc);
            HelpfulMethods.PressKey();
        }

        // pretty much same as above but shows sum of salary per month and section
        public void SalaryPerSection()
        {
            BarChart bc = new BarChart();

            Console.WriteLine("Totalt utbetald lön per avdelning (kr/månad)");

            var sumSalary = context.TotalSalaryPerSectionViews.OrderBy(a => a.Avdelning);
            foreach (var a in sumSalary)
            {
                bc.AddItem(a.Avdelning, (double)a.TotaltUtbetaladLönPerMånadKr, Color.Aqua);
            }
            AnsiConsole.Write(bc);
            HelpfulMethods.PressKey();
        }
    }
}
