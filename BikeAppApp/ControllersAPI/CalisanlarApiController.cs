using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BikeAppApp.Models;

namespace BikeAppApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalisanlarApiController : ControllerBase
    {
        private readonly MotoDBContext _context;

        public CalisanlarApiController(MotoDBContext context)
        {
            _context = context;
        }

        // GET: api/CalisanlarApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calisanlar>>> GetAll()
        {
            return await _context.Calisanlars.ToListAsync();
        }

        // GET: api/CalisanlarApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Calisanlar>> GetById(int id)
        {
            var calisan = await _context.Calisanlars.FindAsync(id);
            if (calisan == null) return NotFound();
            return calisan;
        }

        // POST: api/CalisanlarApi
        [HttpPost]
        public async Task<ActionResult<Calisanlar>> Create(Calisanlar calisan)
        {
            _context.Calisanlars.Add(calisan);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = calisan.CalisanId }, calisan);
        }

        // PUT: api/CalisanlarApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Calisanlar calisan)
        {
            if (id != calisan.CalisanId) return BadRequest();

            _context.Entry(calisan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Calisanlars.Any(e => e.CalisanId == id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // DELETE: api/CalisanlarApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var calisan = await _context.Calisanlars.FindAsync(id);
            if (calisan == null) return NotFound();

            _context.Calisanlars.Remove(calisan);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
