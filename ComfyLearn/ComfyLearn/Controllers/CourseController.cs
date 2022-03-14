using ComfyLearn.Data;
using ComfyLearn.Models;
using ComfyLearn.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using ComfyLearn.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ComfyLearn.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private int PageSize = 4;
        public CourseController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _db = context;
            _userManager = userManager;
        }
        [HttpPost]
        public IActionResult Add(AddCourseVM model) //upsert course
        {
            var userId = User.GetLoggedInUserId<string>();
            if (model.Id == 0)
            {
                Course newCourse = new Course()
                {
                    jezikId = 1,
                    kategorijaId = model.kategorijaId,
                    korisnikId = userId,
                    BrojLekcija = model.BrojLekcija,
                    Certifikat = model.Certifikat,
                    Cijena = model.Cijena,
                    DatumKreiranja = DateTime.Now,
                    Naziv = model.Naziv,
                    Opis = model.Opis,
                    Trajanje = model.Trajanje
                };
                _db.Course.Add(newCourse);
                TempData["CourseAdded"] = $"Course added successfully!";
            }
            else
            {
                var updateCourse = new Course()
                {
                    Id = model.Id,
                    jezikId = 1,
                    kategorijaId = model.kategorijaId,
                    korisnikId = userId,
                    BrojLekcija = model.BrojLekcija,
                    Certifikat = model.Certifikat,
                    Cijena = model.Cijena,
                    DatumKreiranja = DateTime.Now.ToLocalTime(),
                    Naziv = model.Naziv,
                    Opis = model.Opis,
                    Trajanje = model.Trajanje
                };

                _db.Course.Update(updateCourse);
            }
            _db.SaveChanges();
            return RedirectToAction("Dashboard", "Admin");

        }

        [HttpPost]
        public IActionResult AddMaterial(AddCourseVM model) //upsert course
        {
            var userId = User.GetLoggedInUserId<string>();
            if (model.materijal.Id == 0)
            {
                MaterialKurs mt = new MaterialKurs()
                {
                    courseId = model.Id,
                    Naslov = model.materijal.Naslov,
                    Opis = model.materijal.Opis,
                    VideoPath = model.materijal.VideoPath
                };
                _db.MaterialKurs.Add(mt);
                TempData["CourseAdded"] = $"Material added successfully!";
            }
            else
            {
                var updateMaterial = new MaterialKurs()
                {
                    Id = model.materijal.Id,
                    courseId = model.Id,
                    Naslov = model.materijal.Naslov,
                    Opis = model.materijal.Opis,
                    VideoPath = model.materijal.VideoPath
                };

                _db.MaterialKurs.Update(updateMaterial);
            }
            _db.SaveChanges();
            return RedirectToAction("Dashboard", "Admin");

        }

        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index(int productPage=1)
        {
            KurseviListaVM model = new KurseviListaVM
            {
                Kursevi = new List<Course>()
            };


            model.Kursevi = await _db.Course.ToListAsync();

            var count = model.Kursevi.Count;
            model.Kursevi = model.Kursevi.OrderByDescending(x => x.Id)
                                        .Skip((productPage - 1) * PageSize)
                                        .Take(PageSize).ToList();
            model.pagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = count,
                urlParam = "/Course/Index?productPage=:"
            };

            return View(model);
        }

        public async Task<IActionResult> LockUnlock(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var kurs = await _db.Course.FindAsync(Id);
            if (kurs == null)
            {
                return NotFound();
            }
            kurs.Aktivan = !kurs.Aktivan;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        public IActionResult DeleteMaterial(int id) //upsert course
        {
            var del = _db.MaterialKurs.Find(id);
            var kursId = 0;
            if (del != null)
            {
                kursId = del.courseId;
                _db.MaterialKurs.Remove(del);
                _db.SaveChanges();
                TempData["succMsg"] = "Successfully deleted!";
            }
            else
            {
                TempData["errorMsg"] = "Error!";
            }
            return RedirectToAction("AddCourse", "Admin", new { id = kursId }); 
        }
    }
}
