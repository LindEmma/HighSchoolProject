using System;
using System.Collections.Generic;

namespace HighSchoolProject.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string PersonalNumber { get; set; } = null!;

    public int FkClassId { get; set; }

    public string? StudentGender { get; set; }

    public virtual Class FkClass { get; set; } = null!;

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public Student(string firstName, string lastName, string personalNumber, int fkClassID)
    {
        FirstName = firstName;
        LastName = lastName;
        PersonalNumber = personalNumber;
        FkClassId = fkClassID;
    }
    public Student(string firstName)
    {
        FirstName = firstName;
    }
}
