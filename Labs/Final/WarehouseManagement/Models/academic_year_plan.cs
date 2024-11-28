using System;
using System.Collections.Generic;

namespace WarehouseManagement.Migrations;

public partial class academic_year_plan
{
    public Guid year_plan_id { get; set; }

    public int academic_year { get; set; }
}
