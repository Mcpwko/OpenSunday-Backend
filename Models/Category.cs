using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using OpenSundayApi.Controllers;
using System.ComponentModel.DataAnnotations.Schema;

namespace opensunday_backend.Models
{
    public class Category
    {
        [Key]
        public long IdCategory { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Place> PlaceSet { get; set; }

        [ForeignKey("TypeSet")]
        public long IdType { get; set; }
        public virtual Type TypeSet { get; set; }
    }
}
