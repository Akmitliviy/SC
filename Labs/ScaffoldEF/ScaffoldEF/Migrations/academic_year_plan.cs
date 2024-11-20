using System;
using System.Collections.Generic;

namespace ScaffoldEF.Migrations;

public partial class academic_year_plan
{
    public int year_plan_id { get; set; }

    public int academic_year { get; set; }

    public virtual ICollection<course> courses { get; set; } = new List<course>();
}
