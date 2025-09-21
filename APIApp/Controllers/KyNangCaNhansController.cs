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
    public class KyNangCaNhansController : ControllerBase
    {
        private readonly CareerQuizDbContext _context;

        public KyNangCaNhansController(CareerQuizDbContext context)
        {
            _context = context;
        }

        // GET: api/KyNangCaNhans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KyNangCaNhan>>> GetKyNangCaNhans()
        {
            return await _context.KyNangCaNhans.ToListAsync();
        }

        // GET: api/KyNangCaNhans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KyNangCaNhan>> GetKyNangCaNhan(int id)
        {
            var kyNangCaNhan = await _context.KyNangCaNhans.FindAsync(id);

            if (kyNangCaNhan == null)
            {
                return NotFound();
            }

            return kyNangCaNhan;
        }

        // PUT: api/KyNangCaNhans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKyNangCaNhan(int id, KyNangCaNhan kyNangCaNhan)
        {
            if (id != kyNangCaNhan.Id)
            {
                return BadRequest();
            }

            _context.Entry(kyNangCaNhan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KyNangCaNhanExists(id))
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

        // POST: api/KyNangCaNhans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KyNangCaNhan>> PostKyNangCaNhan(KyNangCaNhan kyNangCaNhan)
        {
            _context.KyNangCaNhans.Add(kyNangCaNhan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKyNangCaNhan", new { id = kyNangCaNhan.Id }, kyNangCaNhan);
        }

        // DELETE: api/KyNangCaNhans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKyNangCaNhan(int id)
        {
            var kyNangCaNhan = await _context.KyNangCaNhans.FindAsync(id);
            if (kyNangCaNhan == null)
            {
                return NotFound();
            }

            _context.KyNangCaNhans.Remove(kyNangCaNhan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KyNangCaNhanExists(int id)
        {
            return _context.KyNangCaNhans.Any(e => e.Id == id);
        }
    }
}
