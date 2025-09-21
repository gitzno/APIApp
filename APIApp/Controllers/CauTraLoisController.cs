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
    public class CauTraLoisController : ControllerBase
    {
        private readonly CareerQuizDbContext _context;

        public CauTraLoisController(CareerQuizDbContext context)
        {
            _context = context;
        }

        // GET: api/CauTraLois
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CauTraLoi>>> GetCauTraLois()
        {
            return await _context.CauTraLois.ToListAsync();
        }

        // GET: api/CauTraLois/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CauTraLoi>> GetCauTraLoi(int id)
        {
            var cauTraLoi = await _context.CauTraLois.FindAsync(id);

            if (cauTraLoi == null)
            {
                return NotFound();
            }

            return cauTraLoi;
        }

        // PUT: api/CauTraLois/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCauTraLoi(int id, CauTraLoi cauTraLoi)
        {
            if (id != cauTraLoi.Id)
            {
                return BadRequest();
            }

            _context.Entry(cauTraLoi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CauTraLoiExists(id))
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

        // POST: api/CauTraLois
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CauTraLoi>> PostCauTraLoi(CauTraLoi cauTraLoi)
        {
            _context.CauTraLois.Add(cauTraLoi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCauTraLoi", new { id = cauTraLoi.Id }, cauTraLoi);
        }

        // DELETE: api/CauTraLois/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCauTraLoi(int id)
        {
            var cauTraLoi = await _context.CauTraLois.FindAsync(id);
            if (cauTraLoi == null)
            {
                return NotFound();
            }

            _context.CauTraLois.Remove(cauTraLoi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CauTraLoiExists(int id)
        {
            return _context.CauTraLois.Any(e => e.Id == id);
        }
    }
}
