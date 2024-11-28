using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab05SC.Models.University
{
    [Table("academic_year_plans", Schema = "public")]
    public partial class academic_year_plan
    {
        [Key]
        [Required]
        public Guid year_plan_id { get; set; }

        [Required]
        public int academic_year { get; set; }
    }
}