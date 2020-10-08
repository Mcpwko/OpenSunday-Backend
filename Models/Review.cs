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
        public int Rate { get; set; }
        public string Comment { get; set; }
        
        [ForeignKey("UserSet")]
        public long IdUser { get; set; }
        public virtual User UserSet { get; set; }

        [ForeignKey("PlaceSet")]
        public long IdPlace { get; set; }
        public virtual Place PlaceSet { get; set; }
    }
}
