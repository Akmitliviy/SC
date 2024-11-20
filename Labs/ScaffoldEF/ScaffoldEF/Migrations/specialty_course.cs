using System;
using System.Collections.Generic;

namespace ScaffoldEF.Migrations;

public partial class specialty_course
{
    public int specialty_course_id { get; set; }

    public int specialty_id { get; set; }

    public int course_id { get; set; }

    public virtual course course { get; set; } = null!;

    public virtual specialty specialty { get; set; } = null!;
}
