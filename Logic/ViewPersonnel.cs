using HighSchoolProject.Models;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighSchoolProject.Logic
{
    internal class ViewPersonnel
    {
        HighSchoolContext context = new HighSchoolContext();

        // Method that shows all personnel and some of their information in a table
        public void ViewAllPersonnel()
        {
            //new table
            var table = new Table()
            {
                Title = new TableTitle("All skolpersonal", "blue")
            };

            //adds columns to table
            table.AddColumn("Namn");
            table.AddColumn(new TableColumn("Yrkesroll").Centered());
            table.AddColumn(new TableColumn("Avdelning").Centered());

            //finds all personell, their, roles, and sections. Ordered by roleID.
            var personnel1 = context.Personnel
                .Include(f => f.FkRole)
                .Include(s=>s.FkSection)
                .OrderBy(r => r.FkRoleId);

            //foreach-loop that adds each row of information to the table
            foreach (var p in personnel1)
            {
                table.AddRow(p.FirstName +" "+ p.LastName, p.FkRole.Role1, p.FkSection.SectionName);
            }
            //prints table with spectre console
            AnsiConsole.Write(table);
            HelpfulMethods.PressKey();
        }
    }
}
