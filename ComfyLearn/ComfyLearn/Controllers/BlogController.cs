using ComfyLearn.Data;
using ComfyLearn.Helper;
using ComfyLearn.Models;
using ComfyLearn.Services;
using ComfyLearn.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ComfyLearn.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<BlogController> _logger;
        private readonly int PageSize = 3;

        public BlogController(ApplicationDbContext db, IEmailSender emailSender, ILogger<BlogController> logger)
        {
            _db = db;
            _emailSender = emailSender;
            _logger = logger;
        }
        public async Task<IActionResult> Index(int productPage = 1)
        {
            BlogVM model = new BlogVM
            {
                Kategorije = await _db.Kategorija.ToListAsync(),
                Clanci = await _db.Clanak.OrderByDescending(x => x.DatumKreiranja).ToListAsync()
            };
            foreach (var item in model.Clanci)
            {
                if (item.Sadrzaj.Length > 100)
                {
                    item.Sadrzaj = Regex.Replace(item.Sadrzaj, "<.*?>", String.Empty).Substring(0, 150) + "...";
                }
            }

            var count = model.Clanci.Count;
            model.Clanci = model.Clanci.OrderByDescending(x => x.Id)
                                        .Skip((productPage - 1) * PageSize)
                                        .Take(PageSize).ToList();

            model.pagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = (int)Math.Ceiling(count / (double)PageSize),
                urlParam = "/Blog/Index?productPage=:"
            };


            return View(model);
        }

        //[Authorize(Roles ="User")]
        public async Task<IActionResult> Create()
        {
            BlogVM model = new BlogVM
            {
                Kategorije = await _db.Kategorija.ToListAsync(),
                Clanci = await _db.Clanak.OrderByDescending(x => x.DatumKreiranja).ToListAsync()
            };
            ViewData["KategorijaId"] = new SelectList(_db.Kategorija, "Id", "Naziv");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Clanak clanak)
        {
            Clanak noviClanak = new Clanak
            {
                Naslov = clanak.Naslov,
                Sadrzaj = clanak.Sadrzaj,
                DatumKreiranja = DateTime.Now,
                korisnikId = User.GetLoggedInUserId<string>(),
                Aktivan = true,
                KategorijaId = clanak.KategorijaId
            };

            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    noviClanak.Slika = p1;
                }
                _db.Clanak.Add(noviClanak);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View("/Blog/Create");
        }
        public async Task<IActionResult> GetClanak(int kategorijaId)
        {
            var clanak = await _db.Clanak.Include(x => x.Kategorija).OrderByDescending(x => x.DatumKreiranja).Where(x => x.KategorijaId == kategorijaId).ToListAsync();
            foreach (var item in clanak)
            {
                if (item.Sadrzaj.Length > 100)
                {
                    item.Sadrzaj = Regex.Replace(item.Sadrzaj, "<.*?>", String.Empty).Substring(0, 150) + "...";
                }
            }

            return View("ListaClanaka", clanak);
        }

        public async Task<IActionResult> ReadMore(int Id)
        {
            ClanakVM model = new ClanakVM
            {
                Clanak = await _db.Clanak
                .Include(x => x.Kategorija)
                .Include(x => x.korisnik)
                .Include(x => x.ClanakLikes)
                .FirstOrDefaultAsync(x => x.Id == Id),
                Komentari = await _db.BlogKomentari.Include(x => x.korisnik).Where(x => x.ClanakId == Id).ToListAsync(),
                NoviKomentar = new ClanakKomentari()
            };
            var claimId = (ClaimsIdentity)User.Identity;
            var claim = claimId.FindFirst(ClaimTypes.NameIdentifier);

            if (model.Clanak == null)
            {
                return NotFound();
            }
            var user = await _db.User.Where(x => x.Id == claim.Value).FirstOrDefaultAsync();
            model.NoviKomentar.korisnik = user;
            model.NoviKomentar.ClanakId = Id;

            return PartialView(model);
        }

        public async Task<IActionResult> LockUnlock(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var clanak = await _db.Clanak.FirstOrDefaultAsync(x => x.Id == Id);
            if (clanak == null)
            {
                return NotFound();
            }
            clanak.Aktivan = !clanak.Aktivan;
            await _db.SaveChangesAsync();

            return Redirect("/Blog/ReadMore/" + Id);
        }

        public async Task<IActionResult> GetComment(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var kom = await _db.BlogKomentari.Include(x => x.korisnik).Include(x => x.Clanak).FirstOrDefaultAsync(x => x.Id == Id);
            if (kom == null)
            {
                return NotFound();
            }
            return PartialView(kom);
        }

        public async Task<IActionResult> GetCommentPost(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var kom = await _db.BlogKomentari.Include(x => x.korisnik).Include(x => x.Clanak).FirstOrDefaultAsync(x => x.Id == Id);
            if (kom == null)
            {
                return NotFound();
            }
            var clanak = await _db.BlogKomentari.Include(x => x.Clanak).Where(x => x.Id == Id).Select(x => x.ClanakId).FirstOrDefaultAsync();
            _db.Remove(kom);
            await _db.SaveChangesAsync();

            return Redirect("/Blog/GetComments?Id=" + clanak);
        }

        public async void LockAndSendEmail(int? Id)
        {
            var dislikes = _db.ClanakLike.Where(x => x.ClanakId == Id && x.Like == false).ToList();
            if (dislikes.Count() >= 3)
            {
                try
                {
                    await _emailSender.SendEmailAsync("koricselma999@gmail.com",
                        "ComfyLearn - Clanak dislike warning" + Id.ToString(), 
                        "Clanak ima preko 15 dislikes! Zatvara se.");
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
            Clanak clanak = _db.Clanak.Include(x=>x.ClanakLikes).Where(x => x.Id == Id).FirstOrDefault();
            clanak.Aktivan = false;
          
            _db.SaveChanges();
        }

        [HttpGet]
        public async Task<IActionResult> GetComments(int Id)
        {
            List<ClanakKomentari> komentari = new List<ClanakKomentari>();

            komentari = await _db.BlogKomentari.Include(x => x.korisnik)
                                               .Include(x => x.Clanak)
                                               .Where(x => x.ClanakId == Id)
                                               .ToListAsync();

            return PartialView("Comments",komentari);
        }

       
        public async Task<IActionResult> PostComment(ClanakVM vm)
        {
            var kom = new ClanakKomentari
            {
                korisnikId=User.GetLoggedInUserId<string>(),
                ClanakId=vm.NoviKomentar.ClanakId,
                Komentar=vm.NoviKomentar.Komentar,
                DatumKreiranja=DateTime.Now
            };

            _db.BlogKomentari.Add(kom);
            await _db.SaveChangesAsync();

            return Redirect("/Blog/GetComments?Id=" + kom.ClanakId);
        }
    }
}
