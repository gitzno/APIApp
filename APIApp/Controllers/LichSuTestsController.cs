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
    public class LichSuTestsController : ControllerBase
    {
        private readonly CareerQuizDbContext _context;

        public LichSuTestsController(CareerQuizDbContext context)
        {
            _context = context;
        }

        // GET: api/LichSuTests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LichSuTest>>> GetLichSuTests()
        {
            return await _context.LichSuTests.ToListAsync();
        }

        // GET: api/LichSuTests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LichSuTest>> GetLichSuTest(int id)
        {
            var lichSuTest = await _context.LichSuTests.FindAsync(id);

            if (lichSuTest == null)
            {
                return NotFound();
            }

            return lichSuTest;
        }

        // PUT: api/LichSuTests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLichSuTest(int id, LichSuTest lichSuTest)
        {
            if (id != lichSuTest.Id)
            {
                return BadRequest();
            }

            _context.Entry(lichSuTest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LichSuTestExists(id))
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

        // POST: api/LichSuTests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LichSuTest>> PostLichSuTest(LichSuTest lichSuTest)
        {
            _context.LichSuTests.Add(lichSuTest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLichSuTest", new { id = lichSuTest.Id }, lichSuTest);
        }

        // DELETE: api/LichSuTests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLichSuTest(int id)
        {
            var lichSuTest = await _context.LichSuTests.FindAsync(id);
            if (lichSuTest == null)
            {
                return NotFound();
            }

            _context.LichSuTests.Remove(lichSuTest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LichSuTestExists(int id)
        {
            return _context.LichSuTests.Any(e => e.Id == id);
        }
    }
}
