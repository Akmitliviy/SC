using System;
using System.Collections.Generic;

namespace WebAPI.Migrations;

public partial class group
{
    public Guid group_id { get; set; }

    public string name { get; set; } = null!;

    public virtual ICollection<enrollment> enrollments { get; set; } = new List<enrollment>();

    public virtual ICollection<student> students { get; set; } = new List<student>();
}
