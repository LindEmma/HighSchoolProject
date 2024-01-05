using System;
using System.Collections.Generic;

namespace HighSchoolProject.Models;

public partial class TotalSalaryPerSectionView
{
    public string Avdelning { get; set; } = null!;

    public decimal TotaltUtbetaladLönPerMånadKr { get; set; }
}
