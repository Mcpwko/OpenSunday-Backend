using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace opensunday_backend.Models
{
    public class User
    {
        [Key]
        public long IdUser { get; set; }
        public string Email { get; set; }
        public string Pseudo { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; }
        public string IdAuth0 { get; set; }

        
        public virtual ICollection<Review> ReviewSet { get; set; }
        public virtual ICollection<Report> ReportSet { get; set; }

        [ForeignKey("UserTypeSet")]
        public long IdUserType { get; set; }
        public virtual UserType UserTypeSet { get; set; }
        


    }
}
