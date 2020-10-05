using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace opensunday_backend.Models
{
    public class UserType
    {
        [Key]
        public long IdUserType { get; set; }
        public string Name { get; set; }
    }
}
