using ComfyLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComfyLearn.ViewModel
{
    public class KorpaVM
    {
        public List<Korpa> KorpaStavke { get; set; }
        public KupljenaKorpa KupljenaKorpa { get; set; }
    }
}
