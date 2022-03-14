using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ComfyLearn.Models
{
    public class Course
    {
        private bool _myVal = true;
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public int Trajanje { get; set; }
        public int BrojLekcija { get; set; }
        public bool Certifikat { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string korisnikId { get; set; }
        public string SlikaKursa { get; set; }
        public string PreviewVideo { get; set; }
        public User korisnik { get; set; }
        public int kategorijaId { get; set; }
        public Kategorija kategorija { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public int jezikId { get; set; }
        public Jezik jezik { get; set; }
        public float Cijena { get; set; }
        public bool Aktivan
        {
            get
            {
                return _myVal;
            }
            set
            {
                _myVal = value;
            }
        }
    }
}
