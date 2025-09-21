using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIApp.Models;

namespace APIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CauHoisController : ControllerBase
    {
        private readonly CareerQuizDbContext _context;

        public CauHoisController(CareerQuizDbContext context)
        {
            _context = context;
        }

        // GET: api/CauHois
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CauHoi>>> GetCauHois()
        {
            return await _context.CauHois.ToListAsync();
        }

        // GET: api/CauHois/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CauHoi>> GetCauHoi(int id)
        {
            var cauHoi = await _context.CauHois.FindAsync(id);

            if (cauHoi == null)
            {
                return NotFound();
            }

            return cauHoi;
        }

        // PUT: api/CauHois/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCauHoi(int id, CauHoi cauHoi)
        {
            if (id != cauHoi.Id)
            {
                return BadRequest();
            }

            _context.Entry(cauHoi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CauHoiExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CauHois
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CauHoi>> PostCauHoi(CauHoi cauHoi)
        {
            _context.CauHois.Add(cauHoi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCauHoi", new { id = cauHoi.Id }, cauHoi);
        }

        // DELETE: api/CauHois/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCauHoi(int id)
        {
            var cauHoi = await _context.CauHois.FindAsync(id);
            if (cauHoi == null)
            {
                return NotFound();
            }

            _context.CauHois.Remove(cauHoi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CauHoiExists(int id)
        {
            return _context.CauHois.Any(e => e.Id == id);
        }
    }
}
