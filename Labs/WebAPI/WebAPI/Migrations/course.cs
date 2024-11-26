using System;
using System.Collections.Generic;

namespace WebAPI.Migrations;

public partial class course
{
    public Guid course_id { get; set; }

    public string name { get; set; } = null!;

    public string description { get; set; } = null!;

    public virtual ICollection<enrollment> enrollments { get; set; } = new List<enrollment>();

    public virtual ICollection<qr_session> qr_sessions { get; set; } = new List<qr_session>();

    public virtual ICollection<specialty_course> specialty_courses { get; set; } = new List<specialty_course>();
}
