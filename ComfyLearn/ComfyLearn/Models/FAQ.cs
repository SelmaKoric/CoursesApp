using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComfyLearn.Models
{
    public class FAQ
    {
        public int Id { get; set; }
        public string Pitanje { get; set; }
        public string Odgovor { get; set; }
        public string korisnikId { get; set; }
        public User korisnik { get; set; }
    }
}
