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
    public class BayilerApiController : ControllerBase
    {
        private readonly MotoDBContext _context;

        public BayilerApiController(MotoDBContext context)
        {
            _context = context;
        }

        // GET: api/BayilerApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bayiler>>> GetAll()
        {
            return await _context.Bayilers.ToListAsync();
        }

        // GET: api/BayilerApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bayiler>> GetById(int id)
        {
            var item = await _context.Bayilers.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        // POST: api/BayilerApi
        [HttpPost]
        public async Task<ActionResult<Bayiler>> Create(Bayiler bayi)
        {
            _context.Bayilers.Add(bayi);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = bayi.BayiId }, bayi);
        }

        // PUT: api/BayilerApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Bayiler bayi)
        {
            if (id != bayi.BayiId) return BadRequest();

            _context.Entry(bayi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Bayilers.Any(e => e.BayiId == id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // DELETE: api/BayilerApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Bayilers.FindAsync(id);
            if (item == null) return NotFound();

            _context.Bayilers.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
