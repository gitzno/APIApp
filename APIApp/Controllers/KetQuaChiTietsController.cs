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
    public class KetQuaChiTietsController : ControllerBase
    {
        private readonly CareerQuizDbContext _context;

        public KetQuaChiTietsController(CareerQuizDbContext context)
        {
            _context = context;
        }

        // GET: api/KetQuaChiTiets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KetQuaChiTiet>>> GetKetQuaChiTiets()
        {
            return await _context.KetQuaChiTiets.ToListAsync();
        }

        // GET: api/KetQuaChiTiets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KetQuaChiTiet>> GetKetQuaChiTiet(int id)
        {
            var ketQuaChiTiet = await _context.KetQuaChiTiets.FindAsync(id);

            if (ketQuaChiTiet == null)
            {
                return NotFound();
            }

            return ketQuaChiTiet;
        }

        // PUT: api/KetQuaChiTiets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKetQuaChiTiet(int id, KetQuaChiTiet ketQuaChiTiet)
        {
            if (id != ketQuaChiTiet.Id)
            {
                return BadRequest();
            }

            _context.Entry(ketQuaChiTiet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KetQuaChiTietExists(id))
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

        // POST: api/KetQuaChiTiets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KetQuaChiTiet>> PostKetQuaChiTiet(KetQuaChiTiet ketQuaChiTiet)
        {
            _context.KetQuaChiTiets.Add(ketQuaChiTiet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKetQuaChiTiet", new { id = ketQuaChiTiet.Id }, ketQuaChiTiet);
        }

        // DELETE: api/KetQuaChiTiets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKetQuaChiTiet(int id)
        {
            var ketQuaChiTiet = await _context.KetQuaChiTiets.FindAsync(id);
            if (ketQuaChiTiet == null)
            {
                return NotFound();
            }

            _context.KetQuaChiTiets.Remove(ketQuaChiTiet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KetQuaChiTietExists(int id)
        {
            return _context.KetQuaChiTiets.Any(e => e.Id == id);
        }
    }
}
