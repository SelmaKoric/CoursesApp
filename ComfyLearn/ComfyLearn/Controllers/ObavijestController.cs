using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComfyLearn.Data;
using ComfyLearn.Models;
using Serilog;

namespace ComfyLearn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObavijestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ObavijestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetObavijesti")]
        public async Task<ActionResult<IEnumerable<Obavijest>>> GetObavijesti()
        {
            var applicationDbContext = _context.Obavijest.Include(o => o.TipObavijesti).Include(o => o.user);
            return await applicationDbContext.ToListAsync();
        }

        [HttpGet("GetObavijest")]
        public async Task<ActionResult<Obavijest>> GetObavijest(int id)
        {
            var obavijest = await _context.Obavijest
                .Include(o => o.TipObavijesti)
                .Include(o => o.user)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (obavijest == null)
            {
                return NotFound();
            }

            return obavijest;
        }

        [HttpPut("PutObavijest")]
        public async Task<IActionResult> PutObavijest(int Id, Obavijest obavijest)
        {
            if(Id != obavijest.Id)
            {
                return BadRequest();
            }

            _context.Entry(obavijest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!ObavijestExists(Id))
                {
                    return NotFound();
                }
                else
                {

                }
            }

            return NoContent();
        }

        [HttpPost]
        [Route("PostObavijest")]
        public async Task<ActionResult<Obavijest>> PostObavijest(Obavijest obavijest)
        {
            _context.Obavijest.Add(obavijest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetObavijest", new { Id = obavijest.Id }, obavijest);
        }

       [HttpDelete("DeleteObavijest")]
        public async Task<IActionResult> DeleteObavijest(int id)
        {
            var obavijest = await _context.Obavijest
                .Include(o => o.TipObavijesti)
                .Include(o => o.user)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (obavijest == null)
            {
                return NotFound();
            }

            _context.Obavijest.Remove(obavijest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ObavijestExists(int id)
        {
            return _context.Obavijest.Any(e => e.Id == id);
        }
    }
}
