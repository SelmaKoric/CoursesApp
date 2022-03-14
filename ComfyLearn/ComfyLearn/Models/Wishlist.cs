using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComfyLearn.Models
{
    public class Wishlist
    {
        public int Id { get; set; }
        public int courseId { get; set; }
        public Course course { get; set; }
        public string userId { get; set; }
        public User user { get; set; }
        public DateTime DatumDodavanja { get; set; }
    }
}
