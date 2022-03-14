using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ComfyLearn.Models
{
    public class Korpa
    {
        public int Id { get; set; }
        public string userId { get; set; }
        [NotMapped]
        [ForeignKey("userId")]
        public User user { get; set; }
        public int courseId { get; set; }
        public Course course { get; set; }
        
    }
}
