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
    public class YetkiliServiApiController : ControllerBase
    {
        private readonly MotoDBContext _context;

        public YetkiliServiApiController(MotoDBContext context)
        {
            _context = context;
        }

        // GET: api/YetkiliServiApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<YetkiliServis>>> GetAll()
        {
            return await _context.YetkiliServis.ToListAsync();
        }

        // GET: api/YetkiliServiApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<YetkiliServis>> GetById(int id)
        {
            var servis = await _context.YetkiliServis.FindAsync(id);
            if (servis == null) return NotFound();
            return servis;
        }

        // POST: api/YetkiliServiApi
        [HttpPost]
        public async Task<ActionResult<YetkiliServis>> Create(YetkiliServis servis)
        {
            _context.YetkiliServis.Add(servis);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = servis.ServisId }, servis);
        }

        // PUT: api/YetkiliServiApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, YetkiliServis servis)
        {
            if (id != servis.ServisId) return BadRequest();

            _context.Entry(servis).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.YetkiliServis.Any(e => e.ServisId == id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // DELETE: api/YetkiliServiApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var servis = await _context.YetkiliServis.FindAsync(id);
            if (servis == null) return NotFound();

            _context.YetkiliServis.Remove(servis);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
