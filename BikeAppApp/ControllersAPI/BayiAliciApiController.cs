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
    public class BayiAliciApiController : ControllerBase
    {
        private readonly MotoDBContext _context;

        public BayiAliciApiController(MotoDBContext context)
        {
            _context = context;
        }

        // GET: api/BayiAliciApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BayiAlici>>> GetAll()
        {
            return await _context.BayiAlicis.ToListAsync();
        }

        // GET: api/BayiAliciApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BayiAlici>> GetById(int id)
        {
            var item = await _context.BayiAlicis.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        // POST: api/BayiAliciApi
        [HttpPost]
        public async Task<ActionResult<BayiAlici>> Create(BayiAlici bayiAlici)
        {
            _context.BayiAlicis.Add(bayiAlici);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = bayiAlici.BayiAliciId }, bayiAlici);
        }

        // PUT: api/BayiAliciApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BayiAlici bayiAlici)
        {
            if (id != bayiAlici.BayiAliciId) return BadRequest();

            _context.Entry(bayiAlici).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.BayiAlicis.Any(e => e.BayiAliciId == id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // DELETE: api/BayiAliciApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.BayiAlicis.FindAsync(id);
            if (item == null) return NotFound();

            _context.BayiAlicis.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
