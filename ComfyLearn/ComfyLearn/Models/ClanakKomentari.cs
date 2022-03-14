using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComfyLearn.Models
{
    public class ClanakKomentari
    {
        public int Id { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public string Komentar { get; set; }
        public int ClanakId { get; set; }
        public Clanak Clanak { get; set; }
        public string korisnikId { get; set; }
        public User korisnik { get; set; }
       
    }
}
