using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public bool IsOpenSpecialDay { get; set;}
    public bool IsVerified { get; set; }
    public bool IsAdvertised { get; set; }
    [ForeignKey("IdLocation")]
    public long IdLocation { get; set; }
    [ForeignKey("IdType")]
    public long IdType { get; set; }
    [ForeignKey("IdCategory")]
    public long IdCategory { get; set; }
    public string Creator { get; set; }
}