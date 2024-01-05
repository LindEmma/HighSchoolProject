using System;
using System.Collections.Generic;

namespace HighSchoolProject.Models;

public partial class AverageSalary
{
    public decimal Medellönen { get; set; }

    public string Avdelning { get; set; } = null!;
}
