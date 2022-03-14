using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComfyLearn.Models
{
    public class Akcija
    {
        public int Id { get; set; }
        [DisplayName("Naziv")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Sniženje")]
        public double Discount { get; set; }
        [DisplayName("Minimalni broj kurseva za primjenu popusta")]
        public int MinAmount { get; set; }
        [DisplayName("Slika")]
        public byte[] Picture { get; set; }
        [DisplayName("Aktivan")]
        public bool IsActive { get; set; }
    }
}
