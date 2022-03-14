    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComfyLearn.Models
{
    public class Clanak
    {
        private bool aktivan = true;
        public int Id { get; set; }
        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
        public string korisnikId { get; set; }
        public User korisnik { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public byte[] Slika { get; set; }
        public int KategorijaId { get; set; }
        public Kategorija Kategorija { get; set; }
        public virtual List<ClanakLike> ClanakLikes { get; set; }
        public int Likes => ClanakLikes.Count(x => x.Like == true);
        public int Dislikes => ClanakLikes.Count(x => x.Like == false);
        public bool Aktivan
        {
            get { return aktivan; }

            set { aktivan = value; }
           
        }
    }
}
