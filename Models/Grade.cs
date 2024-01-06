using System;
using System.Collections.Generic;

namespace HighSchoolProject.Models;

public partial class Grade
{
    public int EnrollmentId { get; set; }

    public byte? Grade1 { get; set; }

    public DateOnly? GradeDateOfIssue { get; set; }

    public int FkStudentId { get; set; }

    public int FkPersonnelId { get; set; }

    public int? FkCourseId { get; set; }

    public virtual Course? FkCourse { get; set; }

    public virtual Personnel FkPersonnel { get; set; } = null!;

    public virtual Student FkStudent { get; set; } = null!;
}
