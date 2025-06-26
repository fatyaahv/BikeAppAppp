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
    public class YetkiliServisApiController : ControllerBase
    {
        private readonly MotoDBContext _ctx;
        private readonly IMapper _map;
        private const int DefaultPageSize = 10;

        public YetkiliServisApiController(MotoDBContext ctx, IMapper map)
        {
            _ctx = ctx;
            _map = map;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<YetkiliServisDto>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = DefaultPageSize)
        {
            int total = await _ctx.YetkiliServis.CountAsync();

            var items = await _ctx.YetkiliServis
                                  .OrderBy(s => s.ServisAdi)
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();

            return Ok(new PagedResult<YetkiliServisDto>
            {
                Items = _map.Map<List<YetkiliServisDto>>(items),
                TotalItems = total,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<YetkiliServisDto>> GetById([FromRoute] int id)
        {
            var ent = await _ctx.YetkiliServis.FindAsync(id);
            if (ent == null) return NotFound();

            return Ok(_map.Map<YetkiliServisDto>(ent));
        }

        [HttpPost]
        public async Task<ActionResult<YetkiliServisDto>> Create([FromBody] YetkiliServisCreateDto createDto)
        {
            var entity = _map.Map<YetkiliServis>(createDto);
            _ctx.YetkiliServis.Add(entity);
            await _ctx.SaveChangesAsync();

            var dto = _map.Map<YetkiliServisDto>(entity);

            return CreatedAtAction(nameof(GetById), new { id = dto.ServisId }, dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] YetkiliServisUpdateDto updateDto)
        {
            if (id != updateDto.ServisId)
                return BadRequest("ID mismatch");

            var entity = await _ctx.YetkiliServis.FindAsync(id);
            if (entity == null) return NotFound();

            _map.Map(updateDto, entity);

            try
            {
                await _ctx.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_ctx.YetkiliServis.Any(e => e.ServisId == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var entity = await _ctx.YetkiliServis.FindAsync(id);
            if (entity == null) return NotFound();

            _ctx.YetkiliServis.Remove(entity);
            await _ctx.SaveChangesAsync();

            return NoContent();
        }
    }
}
