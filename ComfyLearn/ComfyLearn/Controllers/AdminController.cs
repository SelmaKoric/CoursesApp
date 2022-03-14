using ComfyLearn.Data;
using ComfyLearn.Helper;
using ComfyLearn.Models;
using ComfyLearn.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ComfyLearn.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return RedirectToAction("Index","Course",1);
        }
        public IActionResult AddCourse(int? Id, int? materialid)
        {
            var VM = new AddCourseVM();
            var matModel = new MaterialKurs();
            if (Id != null)
            {
                 var s = _context.Course.Find(Id);
                if(materialid != null)
                {
                    var material = _context.MaterialKurs.Find(materialid);
                    matModel = new MaterialKurs()
                    {
                        Id = material.Id,
                        courseId = material.courseId,
                        Naslov = material.Naslov,
                        Opis = material.Opis,
                        VideoPath = material.VideoPath
                    };
                    VM = new AddCourseVM()
                    {
                        Id = matModel.courseId,
                        materijal = matModel,
                        materijalEmbed = matModel.VideoPath.Replace("watch?v=","embed/").Substring(0, matModel.VideoPath.IndexOf('&')-2)
                    };
                    ViewBag.editMaterijal = "edit";
                    return View("EditCourse", VM);
                }
                 VM = new AddCourseVM()
                {
                    Id = s.Id,
                    BrojLekcija = s.BrojLekcija,
                    Certifikat = s.Certifikat,
                    Cijena = s.Cijena,
                    DatumKreiranja = s.DatumKreiranja,
                    jezik = s.jezik,
                    Naziv = s.Naziv,
                    Opis = s.Opis,
                    Trajanje = s.Trajanje,
                    kategorija = s.kategorija,
                    materijali = _context.MaterialKurs.Where(x => x.courseId == s.Id).ToList(),
                     materijal = matModel,
                kategorije = _context.Kategorija.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Naziv,
                        Selected=x.Id==s.kategorijaId
                    }).ToList()
                };
            return View(VM);
            }
            VM = new AddCourseVM()
            {
                kategorije = _context.Kategorija.Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Naziv
                }).ToList()
            };
            return View(VM);
        }
        public IActionResult Dashboard()
        {
            if (TempData["CourseAdded"] != null)
            {

            ViewBag.Message = TempData["CourseAdded"];
            }if(TempData["CourseDeleted"]!=null)
                ViewBag.Message = TempData["CourseDeleted"];

            var userId = User.GetLoggedInUserId<string>();

            List<CourseListVM> data = _context.Course.Where(x=>x.korisnikId== userId).Select(s => new CourseListVM
            {
                Id=s.Id,
                BrojLekcija=s.BrojLekcija,
                Certifikat=s.Certifikat,
                Cijena=s.Cijena,
                DatumKreiranja=s.DatumKreiranja.ToString("dd.MM.yyyy"),
                jezik=s.jezik.Naziv,
                Naziv=s.Naziv,
                Opis=s.Opis,
                Trajanje=s.Trajanje,
                kategorija=s.kategorija.Naziv
            }).ToList();
            return View(data);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var course = _context.Course.Find(id);
            _context.Course.Remove(course);
            _context.SaveChanges();
            TempData["CourseDeleted"] = $"Course {course.Naziv} deleted successfully.";
            return RedirectToAction("Dashboard");
        }

    }
}
