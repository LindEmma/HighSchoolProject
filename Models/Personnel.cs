using System;
using System.Collections.Generic;

namespace HighSchoolProject.Models;

public partial class Personnel
{
    public int PersonnelId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int FkRoleId { get; set; }

    public DateOnly? EmploymentDate { get; set; }

    public int? FkSectionId { get; set; }

    public decimal? SalaryKrPerMonth { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual Role FkRole { get; set; } = null!;

    public virtual Section? FkSection { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public Personnel()
    {

    }
    public Personnel(string firstName, string lastName, int fkRoleId, DateOnly dt, decimal salary)
    {
        FirstName = firstName;
        LastName = lastName;
        FkRoleId = fkRoleId;
        EmploymentDate = dt;
        SalaryKrPerMonth = salary;
    }
}
