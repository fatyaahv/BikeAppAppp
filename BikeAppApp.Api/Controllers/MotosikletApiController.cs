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
    public class MotosikletApiController : ControllerBase
    {
        private readonly MotoDBContext _ctx;
        private readonly IMapper _map;
        private const int DefaultPageSize = 10;

        public MotosikletApiController(MotoDBContext ctx, IMapper map)
        {
            _ctx = ctx;
            _map = map;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<MotosikletDto>>> GetAll(
            [FromQuery] string? search = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = DefaultPageSize)
        {
            var query = _ctx.Motosikletlers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(m => m.Marka.Contains(search) || m.Model.Contains(search));

            int totalItems = await query.CountAsync();

            var items = await query
                .OrderBy(m => m.Marka)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var dtoPage = new PagedResult<MotosikletDto>
            {
                Items = _map.Map<List<MotosikletDto>>(items),
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Ok(dtoPage);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MotosikletDto>> GetById([FromRoute] int id)
        {
            var moto = await _ctx.Motosikletlers.FindAsync(id);
            if (moto == null) return NotFound();

            return Ok(_map.Map<MotosikletDto>(moto));
        }

        [HttpPost]
        public async Task<ActionResult<MotosikletDto>> Create([FromBody] MotosikletCreateDto createDto)
        {
            var entity = _map.Map<Motosikletler>(createDto);
            _ctx.Motosikletlers.Add(entity);
            await _ctx.SaveChangesAsync();

            var dto = _map.Map<MotosikletDto>(entity);

            return CreatedAtAction(nameof(GetById), new { id = dto.MotosikletId }, dto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] MotosikletUpdateDto updateDto)
        {
            if (id != updateDto.MotosikletId)
                return BadRequest("ID mismatch");

            var entity = await _ctx.Motosikletlers.FindAsync(id);
            if (entity == null) return NotFound();

            _map.Map(updateDto, entity);

            try
            {
                await _ctx.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_ctx.Motosikletlers.Any(e => e.MotosikletId == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var entity = await _ctx.Motosikletlers.FindAsync(id);
            if (entity == null) return NotFound();

            _ctx.Motosikletlers.Remove(entity);
            await _ctx.SaveChangesAsync();

            return NoContent();
        }
    }
}
