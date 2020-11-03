using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace opensunday_backend.Models
{
    public class Report
    {
        [Key]
        public long IdReport { get; set; }
        public string Comment { get; set; }
        public bool IsForDelete { get; set; }
        public bool IsForEdit { get; set; }
        public DateTime ReportDate { get; set; }
        public bool Status { get; set; }

        [ForeignKey("UserSet")]
        public long IdUser { get; set; }
        public virtual User UserSet { get; set; }

        [ForeignKey("PlaceSet")]
        public long IdPlace { get; set; }
        public virtual Place PlaceSet { get; set; }

    }
}
