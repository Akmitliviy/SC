using System;
using System.Collections.Generic;

namespace ScaffoldEF.Migrations;

public partial class student
{
    public int student_id { get; set; }

    public string first_name { get; set; } = null!;

    public string last_name { get; set; } = null!;

    public string email { get; set; } = null!;

    public int? group_id { get; set; }

    public virtual ICollection<enrollment> enrollments { get; set; } = new List<enrollment>();

    public virtual group? group { get; set; }
}
