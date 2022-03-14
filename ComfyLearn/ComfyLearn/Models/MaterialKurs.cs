using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComfyLearn.Models
{
    public class MaterialKurs
    {
        public int Id { get; set; }
        public string VideoPath { get; set; }
        public string Naslov { get; set; }
        public string Opis { get; set; }
        public int courseId { get; set; }
        public Course course { get; set; }

    }
}
