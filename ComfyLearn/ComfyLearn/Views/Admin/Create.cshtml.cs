using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ComfyLearn.Data;
using ComfyLearn.Models;

namespace ComfyLearn.Views.Admin
{
    public class CreateModel : PageModel
    {
        private readonly ComfyLearn.Data.ApplicationDbContext _context;

        public CreateModel(ComfyLearn.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["jezikId"] = new SelectList(_context.Jezik, "Id", "Id");
        ViewData["kategorijaId"] = new SelectList(_context.Kategorija, "Id", "Naziv");
        ViewData["korisnikId"] = new SelectList(_context.User, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Course Course { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Course.Add(Course);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
