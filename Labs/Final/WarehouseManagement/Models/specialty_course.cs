using System;
using System.Collections.Generic;

namespace WarehouseManagement.Migrations;

public partial class specialty_course
{
    public Guid specialty_course_id { get; set; }

    public Guid specialty_id { get; set; }

    public Guid course_id { get; set; }

    public virtual course course { get; set; } = null!;

    public virtual specialty specialty { get; set; } = null!;
}
