using ComfyLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComfyLearn.ViewModel
{
    public class KurseviListaVM
    {
        public List<Course> Kursevi { get; set; }
        public PagingInfo pagingInfo { get; set; }
    }
}
