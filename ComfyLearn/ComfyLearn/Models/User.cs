using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ComfyLearn.Models
{
    public class User : IdentityUser
    {
        private string slikaDefault = "/img/profilePic.png";

        public static ClaimsIdentity Identity { get; internal set; }
        public int BrojKartice { get; set; }
        public string AdresaUlice { get; set; }
        public string Name { get; set; }
        public string Grad { get; set; }
        public string Država { get; set; }
        public string PoštanskiBroj { get; set; }
        public string Slika { get { return slikaDefault; } set { slikaDefault = value; }}
    }
}