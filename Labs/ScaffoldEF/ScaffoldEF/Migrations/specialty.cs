﻿using System;
using System.Collections.Generic;

namespace ScaffoldEF.Migrations;

public partial class specialty
{
    public int specialty_id { get; set; }

    public string name { get; set; } = null!;

    public virtual ICollection<specialty_course> specialty_courses { get; set; } = new List<specialty_course>();
}