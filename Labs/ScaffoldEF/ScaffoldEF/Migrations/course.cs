using System;
using System.Collections.Generic;

namespace ScaffoldEF.Migrations;

public partial class course
{
    public int course_id { get; set; }

    public string name { get; set; } = null!;

    public string description { get; set; } = null!;

    public int? AcademicYearPlanyear_plan_id { get; set; }

    public virtual academic_year_plan? AcademicYearPlanyear_plan { get; set; }

    public virtual ICollection<enrollment> enrollments { get; set; } = new List<enrollment>();

    public virtual ICollection<qr_session> qr_sessions { get; set; } = new List<qr_session>();

    public virtual ICollection<specialty_course> specialty_courses { get; set; } = new List<specialty_course>();
}
