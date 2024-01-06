using HighSchoolProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighSchoolProject.Logic
{
    internal class ViewSchoolClasses
    {
        HighSchoolContext context = new HighSchoolContext();
        public void ViewSchoolClass()
        {
            Console.WriteLine("Alla skolklasser:");

            var sc = context.Classes.OrderBy(c=>c.ClassId).Distinct();

            foreach(var s in sc)
            {
                Console.WriteLine($"{s.ClassName}");
            }
            HelpfulMethods.PressKey();

        }
    }
}
