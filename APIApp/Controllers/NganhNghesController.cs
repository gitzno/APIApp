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
    public class NganhNghesController : ControllerBase
    {
        private readonly CareerQuizDbContext _context;

        public NganhNghesController(CareerQuizDbContext context)
        {
            _context = context;
        }

        // GET: api/NganhNghes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NganhNghe>>> GetNganhNghes()
        {
            return await _context.NganhNghes.ToListAsync();
        }

        // GET: api/NganhNghes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NganhNghe>> GetNganhNghe(int id)
        {
            var nganhNghe = await _context.NganhNghes.FindAsync(id);

            if (nganhNghe == null)
            {
                return NotFound();
            }

            return nganhNghe;
        }

        // PUT: api/NganhNghes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNganhNghe(int id, NganhNghe nganhNghe)
        {
            if (id != nganhNghe.Id)
            {
                return BadRequest();
            }

            _context.Entry(nganhNghe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NganhNgheExists(id))
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

        // POST: api/NganhNghes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NganhNghe>> PostNganhNghe(NganhNghe nganhNghe)
        {
            _context.NganhNghes.Add(nganhNghe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNganhNghe", new { id = nganhNghe.Id }, nganhNghe);
        }

        // DELETE: api/NganhNghes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNganhNghe(int id)
        {
            var nganhNghe = await _context.NganhNghes.FindAsync(id);
            if (nganhNghe == null)
            {
                return NotFound();
            }

            _context.NganhNghes.Remove(nganhNghe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NganhNgheExists(int id)
        {
            return _context.NganhNghes.Any(e => e.Id == id);
        }
    }
}
