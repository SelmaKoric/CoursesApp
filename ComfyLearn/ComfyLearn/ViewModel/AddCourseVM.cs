using ComfyLearn.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComfyLearn.ViewModel
{
    public class AddCourseVM
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public int Trajanje { get; set; }
        public int BrojLekcija { get; set; }
        public bool Certifikat { get; set; }
        public string korisnikId { get; set; }
        public User korisnik { get; set; }
        public int kategorijaId { get; set; }
        public Kategorija kategorija { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public int jezikId { get; set; }
        public Jezik jezik { get; set; }
        public float Cijena { get; set; }
        public List<SelectListItem> kategorije { get; set; }
        public List<MaterialKurs> materijali { get; set; }
        public MaterialKurs materijal { get; set; }
        public string materijalEmbed { get; set; }
    }
}
