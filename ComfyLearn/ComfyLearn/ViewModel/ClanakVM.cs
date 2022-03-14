using ComfyLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComfyLearn.ViewModel
{
    public class ClanakVM
    {
        public Clanak Clanak { get; set; }
        public List<ClanakKomentari> Komentari { get; set; }
        public ClanakKomentari NoviKomentar { get; set; }
    }
}
