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
    public class MucLuongsController : ControllerBase
    {
        private readonly CareerQuizDbContext _context;

        public MucLuongsController(CareerQuizDbContext context)
        {
            _context = context;
        }

        // GET: api/MucLuongs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MucLuong>>> GetMucLuongs()
        {
            return await _context.MucLuongs.ToListAsync();
        }

        // GET: api/MucLuongs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MucLuong>> GetMucLuong(int id)
        {
            var mucLuong = await _context.MucLuongs.FindAsync(id);

            if (mucLuong == null)
            {
                return NotFound();
            }

            return mucLuong;
        }

        // PUT: api/MucLuongs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMucLuong(int id, MucLuong mucLuong)
        {
            if (id != mucLuong.Id)
            {
                return BadRequest();
            }

            _context.Entry(mucLuong).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MucLuongExists(id))
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

        // POST: api/MucLuongs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MucLuong>> PostMucLuong(MucLuong mucLuong)
        {
            _context.MucLuongs.Add(mucLuong);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMucLuong", new { id = mucLuong.Id }, mucLuong);
        }

        // DELETE: api/MucLuongs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMucLuong(int id)
        {
            var mucLuong = await _context.MucLuongs.FindAsync(id);
            if (mucLuong == null)
            {
                return NotFound();
            }

            _context.MucLuongs.Remove(mucLuong);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MucLuongExists(int id)
        {
            return _context.MucLuongs.Any(e => e.Id == id);
        }
    }
}
