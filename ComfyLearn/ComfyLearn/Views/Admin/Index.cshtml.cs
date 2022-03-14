using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ComfyLearn.Data;
using ComfyLearn.Models;

namespace ComfyLearn.Views.Admin
{
    public class IndexModel : PageModel
    {
        private readonly ComfyLearn.Data.ApplicationDbContext _context;

        public IndexModel(ComfyLearn.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Course> Course { get;set; }

        public async Task OnGetAsync()
        {
            Course = await _context.Course
                .Include(c => c.jezik)
                .Include(c => c.kategorija)
                .Include(c => c.korisnik).ToListAsync();
        }
    }
}
