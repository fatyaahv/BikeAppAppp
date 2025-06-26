using BikeAppApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BikeAppApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlicilarApiController : ControllerBase
    {
        private readonly MotoDBContext _context;

        public AlicilarApiController(MotoDBContext context)
        {
            _context = context;
        }

        // GET: api/alicilar
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alicilar>>> GetAlicilars()
        {
            return await _context.Alicilars.ToListAsync();
        }

        // GET: api/alicilar/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alicilar>> GetAlicilar(int id)
        {
            var alicilar = await _context.Alicilars.FindAsync(id);

            if (alicilar == null)
            {
                return NotFound();
            }

            return alicilar;
        }

        // POST: api/alicilar
        [HttpPost]
        public async Task<ActionResult<Alicilar>> PostAlicilar(Alicilar alicilar)
        {
            _context.Alicilars.Add(alicilar);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAlicilar), new { id = alicilar.AliciId }, alicilar);
        }

        // PUT: api/alicilar/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlicilar(int id, Alicilar alicilar)
        {
            if (id != alicilar.AliciId)
            {
                return BadRequest();
            }

            _context.Entry(alicilar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Alicilars.Any(e => e.AliciId == id))
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

        // DELETE: api/alicilar/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlicilar(int id)
        {
            var alicilar = await _context.Alicilars.FindAsync(id);
            if (alicilar == null)
            {
                return NotFound();
            }

            _context.Alicilars.Remove(alicilar);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
