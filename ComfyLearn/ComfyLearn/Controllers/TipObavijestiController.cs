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
    public class TipObavijestiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipObavijestiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetTipObavijesti")]
        public async Task<ActionResult<IEnumerable<TipObavijesti>>> GetTipObavijesti()
        {
            var applicationDbContext = _context.TipObavijesti.ToListAsync();
            return await applicationDbContext;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<TipObavijesti>> GetTipObavijesti(int id)
        {
            var tip = await _context.TipObavijesti
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tip == null)
            {
                return NotFound();
            }

            return tip;
        }

        [HttpPut("{Id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PutTipObavijesti(int Id, TipObavijesti tip)
        {
            if (Id != tip.Id)
            {
                return BadRequest();
            }

            _context.Entry(tip).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!TipObavijestExists(Id))
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
        public async Task<ActionResult<TipObavijesti>> PostTipObavijesti(TipObavijesti tip)
        {
            _context.TipObavijesti.Add(tip);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipObavijesti", new { Id = tip.Id }, tip);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteTipObavijesti(int id)
        {
            var tip = await _context.TipObavijesti
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tip == null)
            {
                return NotFound();
            }

            _context.TipObavijesti.Remove(tip);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipObavijestExists(int id)
        {
            return _context.TipObavijesti.Any(e => e.Id == id);
        }
    }
}
