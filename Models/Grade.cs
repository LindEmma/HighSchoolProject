using System;
using System.Collections.Generic;

namespace HighSchoolProject.Models;

public partial class Grade
{
    public int GradeId { get; set; }

    public byte Grade1 { get; set; }

    public DateOnly DateOfIssue { get; set; }

    public int FkStudentId { get; set; }

    public int FkPersonnelId { get; set; }

    public int FkCourseId { get; set; }

    public virtual Course FkCourse { get; set; } = null!;

    public virtual Personnel FkPersonnel { get; set; } = null!;

    public virtual Student FkStudent { get; set; } = null!;
}
