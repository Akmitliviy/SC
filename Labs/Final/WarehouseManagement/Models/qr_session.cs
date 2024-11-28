using System;
using System.Collections.Generic;

namespace WarehouseManagement.Migrations;

public partial class qr_session
{
    public Guid qr_session_id { get; set; }

    public string qr_code { get; set; } = null!;

    public DateOnly expiration_date { get; set; }

    public Guid course_id { get; set; }

    public virtual course course { get; set; } = null!;
}
