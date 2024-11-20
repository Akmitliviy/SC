using System;
using System.Collections.Generic;

namespace ScaffoldEF.Migrations;

public partial class qr_session
{
    public int qr_session_id { get; set; }

    public string qr_code { get; set; } = null!;

    public DateTime expiration_time { get; set; }

    public int course_id { get; set; }

    public virtual course course { get; set; } = null!;
}
