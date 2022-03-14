using ComfyLearn.Data;
using ComfyLearn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ComfyLearn.Helper;
using Microsoft.AspNetCore.Http;

namespace ComfyLearn.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            var user = User.GetLoggedInUserId<string>();
            if (user != null)
            {
                var count = _db.Korpa.Where(x => x.userId == user).ToList().Count();
                HttpContext.Session.SetInt32(SD.SD.ssKorpaCount, count);
            }

            if (User.IsInRole("Admin"))
                return RedirectToAction("Index", "Admin");
            else
                return RedirectToAction("Index", "User");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CourseList()
        {
            return PartialView("CourseList", await _db.Course.Where(x=>x.Aktivan==true).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetCourse(int Id)
        {
            var kurs = await _db.Course.Include(x=>x.kategorija)
                                       .Include(x=>x.korisnik)
                                       .Include(x=>x.jezik)
                                       .Where(x => x.Id == Id).FirstOrDefaultAsync();

            Korpa korpa = new Korpa()
            {
                course=kurs,
                courseId=Id
            };

            return PartialView("GetCourse",korpa);
        }

        [Authorize]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DodajUKorpu(Korpa korpa)
        {
            Korpa novaKorpa = new Korpa
            {
                userId = User.GetLoggedInUserId<string>(),
                courseId = korpa.courseId
            };

            if (ModelState.IsValid)
            {
                korpa.userId = User.GetLoggedInUserId<string>();

                Korpa korpaIzDb = await _db.Korpa
                                           .Where(x => x.userId == korpa.userId && x.courseId == korpa.courseId)
                                           .FirstOrDefaultAsync();

                if (korpaIzDb == null)
                {
                    await _db.Korpa.AddAsync(novaKorpa);
                    await _db.SaveChangesAsync();
                }
                
                var count = _db.Korpa.Where(x => x.userId == korpa.userId).ToList().Count();
                HttpContext.Session.SetInt32(SD.SD.ssKorpaCount, count);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
