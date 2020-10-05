using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace opensunday_backend.Models
{
    public class Region
    {
        [Key]
        public long IdRegion { get; set; }
        public string Name { get; set; }

    }
}
