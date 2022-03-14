using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComfyLearn.Data;
using ComfyLearn.Models;
using ComfyLearn.Helper;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace ComfyLearn.Controllers
{
    public class SnizenjaController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SnizenjaController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var snizenja = await _db.Akcija.ToListAsync();

            return View(snizenja);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost,ActionName("Create")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreatePost(Akcija akcija)
        {

            Akcija a = new Akcija
            {
                Name = akcija.Name,
                Discount = akcija.Discount,
                IsActive = akcija.IsActive,
                MinAmount = akcija.MinAmount
            };

            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream() )
                    {
                        using (var ms1=new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    a.Picture = p1;
                }
                _db.Akcija.Add(a);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(akcija);
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var snizenje = await _db.Akcija.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (snizenje == null)
            {
                return NotFound();
            }

            return View(snizenje);
        }

        [HttpPost, ActionName("Edit")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> EditPost(int Id,Akcija akcija)
        {
            if (Id != akcija.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Akcija.Update(akcija);
                    var files = HttpContext.Request.Form.Files;
                    if (files.Count > 0)
                    {
                        byte[] p1 = null;
                        using(var fs1 = files[0].OpenReadStream())
                        {
                            using(var ms1=new MemoryStream())
                            {
                                fs1.CopyTo(ms1);
                                p1 = ms1.ToArray();
                            }
                        }
                        akcija.Picture = p1;
                    }

                    await _db.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!SnizenjeExists(akcija.Id))
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

            return View(akcija);
        }
        
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var snizenje = await _db.Akcija.FirstOrDefaultAsync(x => x.Id == Id);
            if (snizenje == null)
            {
                return NotFound();
            }
            return View(snizenje);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int? Id)
        {
            var snizenje = await _db.Akcija.FirstOrDefaultAsync(x => x.Id == Id);
            _db.Akcija.Remove(snizenje);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool SnizenjeExists(int id)
        {
            return _db.Podkategorija.Any(e => e.Id == id);
        }
    }
}
