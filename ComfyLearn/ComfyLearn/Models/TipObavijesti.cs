﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComfyLearn.Models
{
    public class TipObavijesti
    {
        public int Id { get; set; }
        [Required]
        public string Naziv { get; set; }
    }
}
