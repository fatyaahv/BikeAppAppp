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
    public class BayiMotosikletApiController : ControllerBase
    {
        private readonly MotoDBContext _ctx;
        private readonly IMapper _map;
        private const int DefaultPageSize = 10;

        public BayiMotosikletApiController(MotoDBContext ctx, IMapper map)
        {
            _ctx = ctx;
            _map = map;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<BayiMotosikletDto>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = DefaultPageSize)
        {
            int total = await _ctx.BayiMotosiklets.CountAsync();

            var items = await _ctx.BayiMotosiklets
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();

            return Ok(new PagedResult<BayiMotosikletDto>
            {
                Items = _map.Map<List<BayiMotosikletDto>>(items),
                TotalItems = total,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BayiMotosikletDto>> GetById([FromRoute] int id)
        {
            var ent = await _ctx.BayiMotosiklets.FindAsync(id);
            if (ent == null) return NotFound();

            return Ok(_map.Map<BayiMotosikletDto>(ent));
        }

        [HttpPost]
        public async Task<ActionResult<BayiMotosikletDto>> Create([FromBody] BayiMotosikletCreateDto createDto)
        {
            var entity = _map.Map<BayiMotosiklet>(createDto);
            _ctx.BayiMotosiklets.Add(entity);
            await _ctx.SaveChangesAsync();

            var dto = _map.Map<BayiMotosikletDto>(entity);

            return CreatedAtAction(nameof(GetById), new { id = dto.BayiMotosikletId }, dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BayiMotosikletUpdateDto updateDto)
        {
            if (id != updateDto.BayiMotosikletId)
                return BadRequest("ID mismatch");

            var entity = await _ctx.BayiMotosiklets.FindAsync(id);
            if (entity == null) return NotFound();

            _map.Map(updateDto, entity);

            try
            {
                await _ctx.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_ctx.BayiMotosiklets.Any(e => e.BayiMotosikletId == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var entity = await _ctx.BayiMotosiklets.FindAsync(id);
            if (entity == null) return NotFound();

            _ctx.BayiMotosiklets.Remove(entity);
            await _ctx.SaveChangesAsync();

            return NoContent();
        }
    }
}
