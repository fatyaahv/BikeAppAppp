using AutoMapper;
using BikeAppApp.Helpers;
using BikeAppApp.Models;
using BikeAppApp.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeAppApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalisanlarApiController : ControllerBase
    {
        private readonly MotoDBContext _ctx;
        private readonly IMapper _map;
        private const int DefaultPageSize = 10;

        public CalisanlarApiController(MotoDBContext ctx, IMapper map)
        {
            _ctx = ctx;
            _map = map;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<CalisanlarDto>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = DefaultPageSize)
        {
            int total = await _ctx.Calisanlars.CountAsync();

            var items = await _ctx.Calisanlars
                                  .OrderBy(c => c.Isim)
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();

            return Ok(new PagedResult<CalisanlarDto>
            {
                Items = _map.Map<List<CalisanlarDto>>(items),
                TotalItems = total,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CalisanlarDto>> GetById([FromRoute] int id)
        {
            var ent = await _ctx.Calisanlars.FindAsync(id);
            if (ent == null) return NotFound();

            return Ok(_map.Map<CalisanlarDto>(ent));
        }

        [HttpPost]
        public async Task<ActionResult<CalisanlarDto>> Create([FromBody] CalisanlarCreateDto createDto)
        {
            var entity = _map.Map<Calisanlar>(createDto);
            _ctx.Calisanlars.Add(entity);
            await _ctx.SaveChangesAsync();

            var dto = _map.Map<CalisanlarDto>(entity);

            return CreatedAtAction(nameof(GetById), new { id = dto.CalisanId }, dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CalisanlarUpdateDto updateDto)
        {
            if (id != updateDto.CalisanId)
                return BadRequest("ID mismatch");

            var entity = await _ctx.Calisanlars.FindAsync(id);
            if (entity == null) return NotFound();

            _map.Map(updateDto, entity);

            try
            {
                await _ctx.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_ctx.Calisanlars.Any(e => e.CalisanId == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var entity = await _ctx.Calisanlars.FindAsync(id);
            if (entity == null) return NotFound();

            _ctx.Calisanlars.Remove(entity);
            await _ctx.SaveChangesAsync();

            return NoContent();
        }
    }
}
