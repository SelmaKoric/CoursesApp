using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComfyLearn.ViewModel
{
    public class CourseListVM
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public int Trajanje { get; set; }
        public int BrojLekcija { get; set; }
        public bool Certifikat { get; set; }
        public string kategorija { get; set; }
        public string DatumKreiranja { get; set; }
        public string jezik { get; set; }
        public float Cijena { get; set; }
    }
}
