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
    public class DetailsModel : PageModel
    {
        private readonly ComfyLearn.Data.ApplicationDbContext _context;

        public DetailsModel(ComfyLearn.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Course Course { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course = await _context.Course
                .Include(c => c.jezik)
                .Include(c => c.kategorija)
                .Include(c => c.korisnik).FirstOrDefaultAsync(m => m.Id == id);

            if (Course == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
