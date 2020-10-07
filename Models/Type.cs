using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using OpenSundayApi.Controllers;

namespace opensunday_backend.Models
{
    public class Type
    {
        [Key]
        public long IdType { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Place> PlaceSet { get; set; }
    }
}
