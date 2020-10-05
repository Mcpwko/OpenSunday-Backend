using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace opensunday_backend.Models
{
    public class User
    {
        [Key]
        public long IdUser { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; }
        public string IdAuth0 { get; set; }
        [ForeignKey("IdUserType")]
        public int IdUserType { get; set; }

    }
}
