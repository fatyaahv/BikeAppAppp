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
    public class BakimGecmisiApiController : ControllerBase
    {
        private readonly MotoDBContext _ctx;
        private readonly IMapper _map;
        private const int DefaultPageSize = 10;

        public BakimGecmisiApiController(MotoDBContext ctx, IMapper map)
        {
            _ctx = ctx;
            _map = map;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<BakimGecmisiDto>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = DefaultPageSize)
        {
            var query = _ctx.BakimGecmisis.AsQueryable();

            int total = await query.CountAsync();

            var items = await query
                .OrderByDescending(b => b.BakimTarihi)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new PagedResult<BakimGecmisiDto>
            {
                Items = _map.Map<List<BakimGecmisiDto>>(items),
                TotalItems = total,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BakimGecmisiDto>> GetById([FromRoute] int id)
        {
            var ent = await _ctx.BakimGecmisis.FindAsync(id);
            if (ent == null) return NotFound();

            return Ok(_map.Map<BakimGecmisiDto>(ent));
        }

        [HttpPost]
        public async Task<ActionResult<BakimGecmisiDto>> Create([FromBody] BakimGecmisiCreateDto createDto)
        {
            var entity = _map.Map<BakimGecmisi>(createDto);
            _ctx.BakimGecmisis.Add(entity);
            await _ctx.SaveChangesAsync();

            var dto = _map.Map<BakimGecmisiDto>(entity);

            return CreatedAtAction(nameof(GetById), new { id = dto.BakimGecmisiId }, dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BakimGecmisiUpdateDto updateDto)
        {
            if (id != updateDto.BakimGecmisiId)
                return BadRequest("ID mismatch");

            var entity = await _ctx.BakimGecmisis.FindAsync(id);
            if (entity == null) return NotFound();

            _map.Map(updateDto, entity);

            try
            {
                await _ctx.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_ctx.BakimGecmisis.Any(e => e.BakimId == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var entity = await _ctx.BakimGecmisis.FindAsync(id);
            if (entity == null) return NotFound();

            _ctx.BakimGecmisis.Remove(entity);
            await _ctx.SaveChangesAsync();

            return NoContent();
        }
    }
}
