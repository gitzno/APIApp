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
    public class DaoTaoNganhNghesController : ControllerBase
    {
        private readonly CareerQuizDbContext _context;

        public DaoTaoNganhNghesController(CareerQuizDbContext context)
        {
            _context = context;
        }

        // GET: api/DaoTaoNganhNghes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DaoTaoNganhNghe>>> GetDaoTaoNganhNghes()
        {
            return await _context.DaoTaoNganhNghes.ToListAsync();
        }

        // GET: api/DaoTaoNganhNghes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DaoTaoNganhNghe>> GetDaoTaoNganhNghe(int id)
        {
            var daoTaoNganhNghe = await _context.DaoTaoNganhNghes.FindAsync(id);

            if (daoTaoNganhNghe == null)
            {
                return NotFound();
            }

            return daoTaoNganhNghe;
        }

        // PUT: api/DaoTaoNganhNghes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDaoTaoNganhNghe(int id, DaoTaoNganhNghe daoTaoNganhNghe)
        {
            if (id != daoTaoNganhNghe.NganhNgheId)
            {
                return BadRequest();
            }

            _context.Entry(daoTaoNganhNghe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DaoTaoNganhNgheExists(id))
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

        // POST: api/DaoTaoNganhNghes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DaoTaoNganhNghe>> PostDaoTaoNganhNghe(DaoTaoNganhNghe daoTaoNganhNghe)
        {
            _context.DaoTaoNganhNghes.Add(daoTaoNganhNghe);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DaoTaoNganhNgheExists(daoTaoNganhNghe.NganhNgheId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDaoTaoNganhNghe", new { id = daoTaoNganhNghe.NganhNgheId }, daoTaoNganhNghe);
        }

        // DELETE: api/DaoTaoNganhNghes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDaoTaoNganhNghe(int id)
        {
            var daoTaoNganhNghe = await _context.DaoTaoNganhNghes.FindAsync(id);
            if (daoTaoNganhNghe == null)
            {
                return NotFound();
            }

            _context.DaoTaoNganhNghes.Remove(daoTaoNganhNghe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DaoTaoNganhNgheExists(int id)
        {
            return _context.DaoTaoNganhNghes.Any(e => e.NganhNgheId == id);
        }
    }
}
