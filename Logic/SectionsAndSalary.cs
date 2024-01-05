using HighSchoolProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace HighSchoolProject.Logic
{
    internal class SectionsAndSalary
    {
        HighSchoolContext context = new HighSchoolContext();
        public void ViewSections()
        {
            var sections = context.Sections.OrderBy(p=>p.SectionId).Distinct();

            Console.WriteLine("Alla avdelningar:\n");
            foreach (var s in sections)
            {
                Console.WriteLine($"({s.SectionId}) {s.SectionName}");
            }
            Console.WriteLine("Välj avdelning");
            int chosenSection = HelpfulMethods.ReadInt();
            Console.Clear();

            

            var pers = context.Personnel.Where(p => p.FkSectionId == chosenSection).Include(r=>r.FkRole).Include(s=>s.FkSection);
            Console.WriteLine($"Personal som jobbar i avdelningen:");
            foreach(var s in pers)
            {
                
            }

            foreach(var p in pers)
            {
                Console.WriteLine(p.FirstName +" "+ p.LastName + " "+p.FkRole.Role1);
                HelpfulMethods.PressKey();
            }

        }

        public void AverageSalary() //gör en tabell med spectre console!
        {
            Console.WriteLine("Snittlöner per avdelning (kr/månad)");

            var avgSalary = context.AverageSalaries.OrderBy(a => a.Avdelning);
            foreach(var a in avgSalary)
            {
                Console.WriteLine($"Avdelning: {a.Avdelning}\nSnittlön: {Math.Round(a.Medellönen)}kr/mån");
            }
            HelpfulMethods.PressKey();

        }

        public void SalaryPerSection()//gör en tabell med spectre console!
        {
            Console.WriteLine("Totalt utbetald lön per avdelning (kr/månad)");

            var sumSalary = context.TotalSalaryPerSectionViews.OrderBy(a => a.Avdelning);
            foreach (var a in sumSalary)
            {
                Console.WriteLine($"Avdelning: {a.Avdelning}\nUtbetalad lön: {Math.Round(a.TotaltUtbetaladLönPerMånadKr)}kr/mån");
            }
            HelpfulMethods.PressKey();
        }
    }
}
