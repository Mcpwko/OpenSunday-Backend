﻿using System.ComponentModel.DataAnnotations;
namespace opensunday_backend.Models
{
    public class City
    {
        [Key]
        public long IdCity { get; set; }
        public string Name { get; set; }

    }
}
