using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ComfyLearn.Models
{
    public class KupljenaKorpa
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string KorisnikId { get; set; }
        [ForeignKey("KorisnikId")]
        public User Korisnik { get; set; }
        [Required]
        public DateTime DatumKupovine { get; set; }
        [Required]
        [DisplayFormat(DataFormatString ="{0:C}")]
        [Display(Name ="Ukupan iznos")]
        public double UkupanIznos { get; set; }

        [Display(Name ="Naziv kupona")]
        public string NazivKupona { get; set; }
        public double KuponDiscount { get; set; }
        public string TransakcijaId { get; set; }

    }
}
