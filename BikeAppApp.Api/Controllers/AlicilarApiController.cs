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
    public class AlicilarApiController : ControllerBase
    {
        private readonly MotoDBContext _ctx;
        private readonly IMapper _map;
        private const int DefaultPageSize = 10;

        public AlicilarApiController(MotoDBContext ctx, IMapper map)
        {
            _ctx = ctx;
            _map = map;
        }

        // GET api/alicilar?search=ali&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<PagedResult<AlicilarDto>>> GetAll(
            string? search = null,
            int pageNumber = 1,
            int pageSize = DefaultPageSize)
        {
            var query = _ctx.Alicilars.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(a =>
                         a.Isim.Contains(search) ||
                         a.Soyisim.Contains(search) ||
                         a.Email.Contains(search));

            int totalItems = await query.CountAsync();

            var items = await query
                .OrderBy(a => a.Isim)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var dtoPage = new PagedResult<AlicilarDto>
            {
                Items = _map.Map<List<AlicilarDto>>(items),
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Ok(dtoPage);
        }

        // GET api/alicilar/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AlicilarDto>> GetById(int id)
        {
            var ent = await _ctx.Alicilars.FindAsync(id);
            if (ent == null) return NotFound();

            return Ok(_map.Map<AlicilarDto>(ent));
        }

        // POST api/alicilar
        [HttpPost]
        public async Task<ActionResult<AlicilarDto>> Create([FromBody] AlicilarCreateDto dto)
        {
            var entity = _map.Map<Alicilar>(dto);

            _ctx.Alicilars.Add(entity);
            await _ctx.SaveChangesAsync();

            var resultDto = _map.Map<AlicilarDto>(entity);

            // Return 201 Created with route to new resource
            return CreatedAtAction(nameof(GetById), new { id = entity.AliciId }, resultDto);
        }

        // PUT api/alicilar/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] AlicilarUpdateDto dto)
        {
            if (id != dto.AliciId)
                return BadRequest("ID mismatch");

            var entity = await _ctx.Alicilars.FindAsync(id);
            if (entity == null)
                return NotFound();

            // Map updated fields from DTO into entity
            _map.Map(dto, entity);

            _ctx.Alicilars.Update(entity);
            await _ctx.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/alicilar/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _ctx.Alicilars.FindAsync(id);
            if (entity == null) return NotFound();

            _ctx.Alicilars.Remove(entity);
            await _ctx.SaveChangesAsync();

            return NoContent();
        }
    }
}
