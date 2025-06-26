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
    public class BakimGecmisiApiController : ControllerBase
    {
        private readonly MotoDBContext _context;

        public BakimGecmisiApiController(MotoDBContext context)
        {
            _context = context;
        }

        // GET: api/BakimGecmisiApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BakimGecmisi>>> GetAll()
        {
            return await _context.BakimGecmisis.ToListAsync();
        }

        // GET: api/BakimGecmisiApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BakimGecmisi>> GetById(int id)
        {
            var item = await _context.BakimGecmisis.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        // POST: api/BakimGecmisiApi
        [HttpPost]
        public async Task<ActionResult<BakimGecmisi>> Create(BakimGecmisi bakim)
        {
            _context.BakimGecmisis.Add(bakim);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = bakim.BakimId }, bakim);
        }

        // PUT: api/BakimGecmisiApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BakimGecmisi bakim)
        {
            if (id != bakim.BakimId) return BadRequest();

            _context.Entry(bakim).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.BakimGecmisis.Any(e => e.BakimId == id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // DELETE: api/BakimGecmisiApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bakim = await _context.BakimGecmisis.FindAsync(id);
            if (bakim == null) return NotFound();

            _context.BakimGecmisis.Remove(bakim);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
