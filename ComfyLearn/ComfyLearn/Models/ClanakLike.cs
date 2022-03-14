using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ComfyLearn.Models
{
    public class ClanakLike
    {
        public int Id { get; set; }
        public virtual Clanak Clanak { get; set; }
        public int ClanakId { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public string IpAddress { get; set; }
        public bool Like { get; set; }
        public DateTime Datum { get; set; }
    }
}
