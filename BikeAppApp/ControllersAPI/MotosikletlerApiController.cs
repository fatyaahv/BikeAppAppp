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
    public class MotosikletlerApiController : ControllerBase
    {
        private readonly MotoDBContext _context;

        public MotosikletlerApiController(MotoDBContext context)
        {
            _context = context;
        }

        // GET: api/MotosikletlerApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Motosikletler>>> GetAll()
        {
            return await _context.Motosikletlers.ToListAsync();
        }

        // GET: api/MotosikletlerApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Motosikletler>> GetById(int id)
        {
            var moto = await _context.Motosikletlers.FindAsync(id);
            if (moto == null) return NotFound();
            return moto;
        }

        // POST: api/MotosikletlerApi
        [HttpPost]
        public async Task<ActionResult<Motosikletler>> Create(Motosikletler moto)
        {
            _context.Motosikletlers.Add(moto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = moto.MotosikletId }, moto);
        }

        // PUT: api/MotosikletlerApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Motosikletler moto)
        {
            if (id != moto.MotosikletId) return BadRequest();

            _context.Entry(moto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Motosikletlers.Any(e => e.MotosikletId == id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // DELETE: api/MotosikletlerApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var moto = await _context.Motosikletlers.FindAsync(id);
            if (moto == null) return NotFound();

            _context.Motosikletlers.Remove(moto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
