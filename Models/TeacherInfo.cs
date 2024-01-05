using System;
using System.Collections.Generic;

namespace HighSchoolProject.Models;

public partial class TeacherInfo
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public decimal? SalaryKrPerMonth { get; set; }

    public string Role { get; set; } = null!;

    public string SectionName { get; set; } = null!;
}
