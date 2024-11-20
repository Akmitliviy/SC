using System;
using System.Collections.Generic;

namespace ScaffoldEF.Migrations;

public partial class group
{
    public int group_id { get; set; }

    public string name { get; set; } = null!;

    public virtual ICollection<enrollment> enrollments { get; set; } = new List<enrollment>();

    public virtual ICollection<student> students { get; set; } = new List<student>();
}
