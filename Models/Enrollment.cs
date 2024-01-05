using System;
using System.Collections.Generic;

namespace HighSchoolProject.Models;

public partial class Enrollment
{
    public int EnrollmentId { get; set; }

    public int FkStudentId { get; set; }

    public int FkCourseId { get; set; }

    public virtual Course FkCourse { get; set; } = null!;

    public virtual Student FkStudent { get; set; } = null!;
}
