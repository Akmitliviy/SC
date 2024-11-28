using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab05SC.Models.University
{
    [Table("qr_sessions", Schema = "public")]
    public partial class qr_session
    {
        [Key]
        [Required]
        public Guid qr_session_id { get; set; }

        [Required]
        public string qr_code { get; set; }

        [Required]
        public DateTime expiration_date { get; set; }

        [Required]
        public Guid course_id { get; set; }

        public course course { get; set; }
    }
}