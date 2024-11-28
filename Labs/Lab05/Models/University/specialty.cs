using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab05SC.Models.University
{
    [Table("specialties", Schema = "public")]
    public partial class specialty
    {
        [Key]
        [Required]
        public Guid specialty_id { get; set; }

        [Required]
        public string name { get; set; }

        public ICollection<specialty_course> specialty_courses { get; set; }
    }
}