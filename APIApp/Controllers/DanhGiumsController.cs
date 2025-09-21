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
    public class DanhGiumsController : ControllerBase
    {
        private readonly CareerQuizDbContext _context;

        public DanhGiumsController(CareerQuizDbContext context)
        {
            _context = context;
        }

        // GET: api/DanhGiums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DanhGium>>> GetDanhGia()
        {
            return await _context.DanhGia.ToListAsync();
        }

        // GET: api/DanhGiums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DanhGium>> GetDanhGium(int id)
        {
            var danhGium = await _context.DanhGia.FindAsync(id);

            if (danhGium == null)
            {
                return NotFound();
            }

            return danhGium;
        }

        // PUT: api/DanhGiums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDanhGium(int id, DanhGium danhGium)
        {
            if (id != danhGium.Id)
            {
                return BadRequest();
            }

            _context.Entry(danhGium).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DanhGiumExists(id))
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

        // POST: api/DanhGiums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DanhGium>> PostDanhGium(DanhGium danhGium)
        {
            _context.DanhGia.Add(danhGium);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDanhGium", new { id = danhGium.Id }, danhGium);
        }

        // DELETE: api/DanhGiums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDanhGium(int id)
        {
            var danhGium = await _context.DanhGia.FindAsync(id);
            if (danhGium == null)
            {
                return NotFound();
            }

            _context.DanhGia.Remove(danhGium);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DanhGiumExists(int id)
        {
            return _context.DanhGia.Any(e => e.Id == id);
        }
    }
}
