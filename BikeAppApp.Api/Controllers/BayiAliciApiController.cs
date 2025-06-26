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
    public class BayiAliciApiController : ControllerBase
    {
        private readonly MotoDBContext _ctx;
        private readonly IMapper _map;
        private const int DefaultPageSize = 10;

        public BayiAliciApiController(MotoDBContext ctx, IMapper map)
        {
            _ctx = ctx;
            _map = map;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<BayiAliciDto>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = DefaultPageSize)
        {
            int total = await _ctx.BayiAlicis.CountAsync();

            var items = await _ctx.BayiAlicis
                                  .OrderByDescending(b => b.SatisTarihi)
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();

            return Ok(new PagedResult<BayiAliciDto>
            {
                Items = _map.Map<List<BayiAliciDto>>(items),
                TotalItems = total,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BayiAliciDto>> GetById([FromRoute] int id)
        {
            var ent = await _ctx.BayiAlicis.FindAsync(id);
            if (ent == null) return NotFound();

            return Ok(_map.Map<BayiAliciDto>(ent));
        }

       /* [HttpPost]
        public async Task<ActionResult<BayiAliciDto>> Create([FromBody] BayiAliciCreateDto createDto)
        {
            var entity = _map.Map<BayiAlici>(createDto);
            _ctx.BayiAlicis.Add(entity);
            await _ctx.SaveChangesAsync();

            var dto = _map.Map<BayiAliciDto>(entity);

            return CreatedAtAction(nameof(GetById), new { id = dto.BayiAliciId }, dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BayiAliciUpdateDto updateDto)
        {
            if (id != updateDto.BayiAliciId)
                return BadRequest("ID mismatch");

            var entity = await _ctx.BayiAlicis.FindAsync(id);
            if (entity == null) return NotFound();

            _map.Map(updateDto, entity);

            try
            {
                await _ctx.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_ctx.BayiAlicis.Any(e => e.BayiAliciId == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }*/

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var entity = await _ctx.BayiAlicis.FindAsync(id);
            if (entity == null) return NotFound();

            _ctx.BayiAlicis.Remove(entity);
            await _ctx.SaveChangesAsync();

            return NoContent();
        }
    }
}
