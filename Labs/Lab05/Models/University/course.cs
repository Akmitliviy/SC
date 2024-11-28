using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab05SC.Models.University
{
    [Table("courses", Schema = "public")]
    public partial class course
    {
        [Key]
        [Required]
        public Guid course_id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string description { get; set; }

        public ICollection<enrollment> enrollments { get; set; }

        public ICollection<qr_session> qr_sessions { get; set; }

        public ICollection<specialty_course> specialty_courses { get; set; }
    }
}