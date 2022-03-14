using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComfyLearn.Models
{
    public class Podkategorija
    {
        public int Id { get; set; }
        [Required]
        public string Naziv { get; set; }

        public Kategorija Kategorija { get; set; }
        [Display(Name ="Kategorija")]
        public int KategorijaId { get; set; }
    }
}
