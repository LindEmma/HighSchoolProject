using System;
using System.Collections.Generic;

namespace HighSchoolProject.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public string? Description { get; set; }

    public int? FkPersonnelId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual Personnel? FkPersonnel { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
