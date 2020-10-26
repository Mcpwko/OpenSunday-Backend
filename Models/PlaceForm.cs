using opensunday_backend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Type = opensunday_backend.Models.Type;

public class PlaceForm
{
    public string name { get; set; }
    public string description { get; set; }
    public string email { get; set; }
    public string website { get; set; }
    public string phoneNumber { get; set; }
    public string address { get; set; }
    public int zip { get; set; }
    public string city { get; set; }
    public bool isOpenSunday { get; set; }
    public bool isOpenSpecialDay { get; set; }
    public bool isVerified { get; set; }
    public bool isAdvertised { get; set; }
    public float lat { get; set; }
    public float Long {get;set;}

    public long idRegion { get; set; }
    
    public long idCategory { get; set; }

    public long idType { get; set; }
    

}