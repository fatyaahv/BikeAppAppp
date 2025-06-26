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
    public class BayiMotosikletApiController : ControllerBase
    {
        private readonly MotoDBContext _context;

        public BayiMotosikletApiController(MotoDBContext context)
        {
            _context = context;
        }

        // GET: api/BayiMotosikletApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BayiMotosiklet>>> GetAll()
        {
            return await _context.BayiMotosiklets.ToListAsync();
        }

        // GET: api/BayiMotosikletApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BayiMotosiklet>> GetById(int id)
        {
            var item = await _context.BayiMotosiklets.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        // POST: api/BayiMotosikletApi
        [HttpPost]
        public async Task<ActionResult<BayiMotosiklet>> Create(BayiMotosiklet bayiMotosiklet)
        {
            _context.BayiMotosiklets.Add(bayiMotosiklet);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = bayiMotosiklet.BayiMotosikletId }, bayiMotosiklet);
        }

        // PUT: api/BayiMotosikletApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BayiMotosiklet bayiMotosiklet)
        {
            if (id != bayiMotosiklet.BayiMotosikletId) return BadRequest();

            _context.Entry(bayiMotosiklet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.BayiMotosiklets.Any(e => e.BayiMotosikletId == id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // DELETE: api/BayiMotosikletApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.BayiMotosiklets.FindAsync(id);
            if (item == null) return NotFound();

            _context.BayiMotosiklets.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
