using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace opensunday_backend.Models
{
    public class Review
    {
        [Key]
        public long IdReview { get; set; }
        public int Text { get; set; }
        public string Comment { get; set; }
        [ForeignKey("IdUser")]
        public long IdUser { get; set; }
        [ForeignKey("IdPlace")]
        public long IdPlace { get; set; }
    }
}
