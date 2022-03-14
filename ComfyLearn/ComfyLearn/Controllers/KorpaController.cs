using ComfyLearn.Data;
using ComfyLearn.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComfyLearn.Helper;
using ComfyLearn.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Stripe;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;

namespace ComfyLearn.Controllers
{
    public class KorpaController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<KorpaController> _logger;

        public KorpaController(ApplicationDbContext db, ILogger<KorpaController> logger)
        {
            _db = db;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var KorpaDetalji = new KorpaVM()
            {
                KupljenaKorpa=new Models.KupljenaKorpa()
            };
            KorpaDetalji.KupljenaKorpa.UkupanIznos = 0;

            var user = User.GetLoggedInUserId<string>();

            var korpa = _db.Korpa.Where(x => x.userId == user);
            if (korpa != null)
            {
                KorpaDetalji.KorpaStavke = korpa.ToList();
            }

            foreach(var stavka in KorpaDetalji.KorpaStavke)
            {
                stavka.course = await _db.Course.FirstOrDefaultAsync(x => x.Id == stavka.courseId);
                KorpaDetalji.KupljenaKorpa.UkupanIznos = KorpaDetalji.KupljenaKorpa.UkupanIznos + (stavka.course.Cijena);
            }

            return PartialView(KorpaDetalji);
        }

        public async Task<IActionResult> UkloniKurs(int Id)
        {
            var korpa = _db.Korpa.Where(x => x.Id == Id).FirstOrDefault();
            _db.Korpa.Remove(korpa);
            await _db.SaveChangesAsync();

            var count = _db.Korpa.Where(x => x.userId == korpa.userId).ToList().Count();
            HttpContext.Session.SetInt32(SD.SD.ssKorpaCount, count);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("PayNow")]
        public async Task<IActionResult> PayNow(string stripeToken)
        {
            var user = User.GetLoggedInUserId<string>();
            var korpa = await _db.Korpa.Include(x=>x.course).Where(x => x.userId == user).ToListAsync();
            float ukupanIznos = 0;

            foreach (var stavka in korpa)
            {
                ukupanIznos += stavka.course.Cijena;
            }

            var kupovina = new KupljenaKorpa
            {
                KorisnikId = user,
                DatumKupovine = DateTime.Now,
                UkupanIznos = ukupanIznos
            };
          
            _db.KupljenaKorpa.Add(kupovina);
            await _db.SaveChangesAsync();

            var options = new ChargeCreateOptions
            {
                Amount = Convert.ToInt32(kupovina.UkupanIznos * 100),
                Currency = "usd",
                Description = "Korpa Id " + kupovina.Id,
                Source = stripeToken
            };

            var service = new ChargeService();
            Charge charge = service.Create(options);

            if (charge.BalanceTransactionId == null)
            {
                _logger.LogError("Payment error!");
            }
            else
            {
                kupovina.TransakcijaId = charge.BalanceTransactionId;
            }
            if (charge.Status.ToLower() == "succeeded")
            {
                _logger.LogInformation("Payment succeeded!");
            }

            _db.Korpa.RemoveRange(korpa);
            HttpContext.Session.SetInt32(SD.SD.ssKorpaCount, 0);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
