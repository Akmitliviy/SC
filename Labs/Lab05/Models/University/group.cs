using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab05SC.Models.University
{
    [Table("groups", Schema = "public")]
    public partial class group
    {
        [Key]
        [Required]
        public Guid group_id { get; set; }

        [Required]
        public string name { get; set; }

        public ICollection<student> students { get; set; }

        public ICollection<enrollment> enrollments { get; set; }
    }
}