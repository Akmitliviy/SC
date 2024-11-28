using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab05SC.Models.University
{
    [Table("specialty_courses", Schema = "public")]
    public partial class specialty_course
    {
        [Key]
        [Required]
        public Guid specialty_course_id { get; set; }

        [Required]
        public Guid specialty_id { get; set; }

        public specialty specialty { get; set; }

        [Required]
        public Guid course_id { get; set; }

        public course course { get; set; }
    }
}