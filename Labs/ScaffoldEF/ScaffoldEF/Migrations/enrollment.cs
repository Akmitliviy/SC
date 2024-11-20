using System;
using System.Collections.Generic;

namespace ScaffoldEF.Migrations;

public partial class enrollment
{
    public int enrollment_id { get; set; }

    public int student_id { get; set; }

    public int course_id { get; set; }

    public int group_id { get; set; }

    public virtual course course { get; set; } = null!;

    public virtual group group { get; set; } = null!;

    public virtual student student { get; set; } = null!;
}
