using opensunday_backend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Type = opensunday_backend.Models.Type;

public class Place
{
    [Key]
    public long IdPlace { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime CreateAt { get; set; }
    public bool IsOpenSunday { get; set; }
    public bool IsOpenSpecialDay { get; set; }
    public bool IsVerified { get; set; }
    public string Creator { get; set; }

    public virtual ICollection<Review> ReviewSet { get; set; }
    public virtual ICollection<Report> ReportSet { get; set; }

    [ForeignKey("LocationSet")]
    public long IdLocation { get; set; }
    public virtual Location LocationSet { get; set; }
    

    [ForeignKey("CategorySet")]
    public long IdCategory { get; set; }
    public virtual Category CategorySet { get; set; }
    

    [ForeignKey("TypeSet")]
    public long IdType { get; set; }
    public virtual Type TypeSet { get; set; }
    

}