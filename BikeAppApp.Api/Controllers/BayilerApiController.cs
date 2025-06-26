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
    public class BayilerApiController : ControllerBase
    {
        private readonly MotoDBContext _ctx;
        private readonly IMapper _map;
        private const int DefaultPageSize = 10;

        public BayilerApiController(MotoDBContext ctx, IMapper map)
        {
            _ctx = ctx;
            _map = map;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<BayilerDto>>> GetAll(
            [FromQuery] string? search = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = DefaultPageSize)
        {
            var q = _ctx.Bayilers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(b => b.BayiAdi.Contains(search));

            int total = await q.CountAsync();

            var items = await q.OrderBy(b => b.BayiAdi)
                               .Skip((pageNumber - 1) * pageSize)
                               .Take(pageSize)
                               .ToListAsync();

            return Ok(new PagedResult<BayilerDto>
            {
                Items = _map.Map<List<BayilerDto>>(items),
                TotalItems = total,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BayilerDto>> GetById([FromRoute] int id)
        {
            var ent = await _ctx.Bayilers.FindAsync(id);
            if (ent == null) return NotFound();

            return Ok(_map.Map<BayilerDto>(ent));
        }

        [HttpPost]
        public async Task<ActionResult<BayilerDto>> Create([FromBody] BayilerCreateDto createDto)
        {
            var entity = _map.Map<Bayiler>(createDto);
            _ctx.Bayilers.Add(entity);
            await _ctx.SaveChangesAsync();

            var dto = _map.Map<BayilerDto>(entity);

            return CreatedAtAction(nameof(GetById), new { id = dto.BayiId }, dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BayilerUpdateDto updateDto)
        {
            if (id != updateDto.BayiId)
                return BadRequest("ID mismatch");

            var entity = await _ctx.Bayilers.FindAsync(id);
            if (entity == null) return NotFound();

            _map.Map(updateDto, entity);

            try
            {
                await _ctx.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_ctx.Bayilers.Any(e => e.BayiId == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var entity = await _ctx.Bayilers.FindAsync(id);
            if (entity == null) return NotFound();

            _ctx.Bayilers.Remove(entity);
            await _ctx.SaveChangesAsync();

            return NoContent();
        }
    }
}
