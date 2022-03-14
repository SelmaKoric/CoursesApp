using ComfyLearn.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ComfyLearn.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var snizenja = await _db.Akcija.Where(x => x.IsActive == true && x.Picture != null).ToListAsync();

            return View(snizenja);
        }
        public async Task<IActionResult> Prikaz()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var korisnici = await _db.User.Where(x=>x.Id!=claim.Value).ToListAsync();

            return View(korisnici);
        }

        public async Task<IActionResult> Lock(string Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var korisnik = await _db.User.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (korisnik == null)
            {
                return NotFound();
            }
            korisnik.LockoutEnd = DateTime.Now.AddYears(100);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Prikaz));
        }

        public async Task<IActionResult> UnLock(string Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var korisnik = await _db.User.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (korisnik == null)
            {
                return NotFound();
            }
            korisnik.LockoutEnd = DateTime.Now;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Prikaz));
        }

    }
}
