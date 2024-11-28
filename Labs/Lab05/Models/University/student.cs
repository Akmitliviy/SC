using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab05SC.Models.University
{
    [Table("students", Schema = "public")]
    public partial class student
    {
        [Key]
        [Required]
        public Guid student_id { get; set; }

        [Required]
        public string first_name { get; set; }

        [Required]
        public string last_name { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public DateTime birth_date { get; set; }

        [Required]
        public string phone_number { get; set; }

        [Required]
        public Guid group_id { get; set; }

        public group group1 { get; set; }

        public ICollection<enrollment> enrollments { get; set; }
    }
}