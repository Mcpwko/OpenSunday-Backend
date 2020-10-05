using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace opensunday_backend.Models
{
    public class Location
    {
        [Key]
        public long IdLocation { get; set; }
        public long Lat { get; set; }
        public long Long { get; set; }
        public string Address { get; set; }
        [ForeignKey("IdCity")]
        public long IdCity { get; set; }
        [ForeignKey("IdRegion")]
        public long IdRegion { get; set; }
    }
}
