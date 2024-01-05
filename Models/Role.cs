using System;
using System.Collections.Generic;

namespace HighSchoolProject.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string Role1 { get; set; } = null!;

    public virtual ICollection<Personnel> Personnel { get; set; } = new List<Personnel>();
}
