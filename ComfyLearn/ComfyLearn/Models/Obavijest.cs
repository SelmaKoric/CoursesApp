using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComfyLearn.Models
{
    public class Obavijest
    {
        public int Id { get; set; }
        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
        public string DatumObavijesti { get; set; }
        public int TipObavijestiId { get; set; }
        public TipObavijesti TipObavijesti { get; set; }

        public string userId { get; set; }
        public User user { get; set; }
    }
}
