﻿using System;
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
        public float Lat { get; set; }
        public float Long { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Place> Place { get; set; }

        [ForeignKey("CitySet")]
        public long IdCity { get; set; }
        public virtual City CitySet { get; set; }
        

        [ForeignKey("RegionSet")]
        public long IdRegion { get; set; }
        public virtual Region RegionSet { get; set; }
        
    }
}
