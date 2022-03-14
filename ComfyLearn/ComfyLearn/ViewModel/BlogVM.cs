using ComfyLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComfyLearn.ViewModel
{
    public class BlogVM
    {
        public List<Kategorija> Kategorije { get; set; }
        public List<Clanak> Clanci { get; set; }
        public PagingInfo pagingInfo { get; set; }
        public Clanak Clanak { get; set; }

    }
}
