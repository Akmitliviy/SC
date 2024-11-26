using System;
using System.Collections.Generic;

namespace WebAPI.Migrations;

public partial class enrollment
{
    public Guid enrollment_id { get; set; }

    public Guid student_id { get; set; }

    public Guid course_id { get; set; }

    public Guid group_id { get; set; }

    public virtual course course { get; set; } = null!;

    public virtual group group { get; set; } = null!;

    public virtual student student { get; set; } = null!;
}
