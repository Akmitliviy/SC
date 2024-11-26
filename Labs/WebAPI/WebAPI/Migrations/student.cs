using System;
using System.Collections.Generic;

namespace WebAPI.Migrations;

public partial class student
{
    public Guid student_id { get; set; }

    public string first_name { get; set; } = null!;

    public string last_name { get; set; } = null!;

    public string email { get; set; } = null!;

    public DateOnly birth_date { get; set; }

    public string phone_number { get; set; } = null!;

    public Guid group_id { get; set; }

    public virtual ICollection<enrollment> enrollments { get; set; } = new List<enrollment>();

    public virtual group group { get; set; } = null!;
}
