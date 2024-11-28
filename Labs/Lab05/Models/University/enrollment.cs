using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab05SC.Models.University
{
    [Table("enrollments", Schema = "public")]
    public partial class enrollment
    {
        [Key]
        [Required]
        public Guid enrollment_id { get; set; }

        [Required]
        public Guid student_id { get; set; }

        public student student { get; set; }

        [Required]
        public Guid course_id { get; set; }

        public course course { get; set; }

        [Required]
        public Guid group_id { get; set; }

        public group group1 { get; set; }
    }
}