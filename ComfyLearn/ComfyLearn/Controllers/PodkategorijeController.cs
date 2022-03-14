using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComfyLearn.Data;
using ComfyLearn.Models;

namespace ComfyLearn.Controllers
{
    public class PodkategorijeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PodkategorijeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Podkategorije
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Podkategorija.Include(p => p.Kategorija);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Podkategorije/Create
        public IActionResult Create()
        {
            ViewData["KategorijaId"] = new SelectList(_context.Kategorija, "Id", "Naziv");
            return View();
        }

        // POST: Podkategorije/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,KategorijaId")] Podkategorija podkategorija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(podkategorija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategorijaId"] = new SelectList(_context.Kategorija, "Id", "Naziv", podkategorija.KategorijaId);
            return View(podkategorija);
        }

        // GET: Podkategorije/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podkategorija = await _context.Podkategorija.FindAsync(id);
            if (podkategorija == null)
            {
                return NotFound();
            }
            ViewData["KategorijaId"] = new SelectList(_context.Kategorija, "Id", "Naziv", podkategorija.KategorijaId);
            return View(podkategorija);
        }

        // POST: Podkategorije/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,KategorijaId")] Podkategorija podkategorija)
        {
            if (id != podkategorija.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(podkategorija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PodkategorijaExists(podkategorija.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategorijaId"] = new SelectList(_context.Kategorija, "Id", "Naziv", podkategorija.KategorijaId);
            return View(podkategorija);
        }

        // GET: Podkategorije/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podkategorija = await _context.Podkategorija
                .Include(p => p.Kategorija)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (podkategorija == null)
            {
                return NotFound();
            }

            return View(podkategorija);
        }

        // POST: Podkategorije/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var podkategorija = await _context.Podkategorija.FindAsync(id);
            _context.Podkategorija.Remove(podkategorija);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PodkategorijaExists(int id)
        {
            return _context.Podkategorija.Any(e => e.Id == id);
        }
    }
}
