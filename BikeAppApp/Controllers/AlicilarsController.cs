using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BikeAppApp.Models;
using BikeAppApp.Helpers;

namespace BikeAppApp.Controllers
{
    public class AlicilarsController : Controller
    {
        private readonly MotoDBContext _context;
        private const int PageSize = 5;

        public AlicilarsController(MotoDBContext context)
        {
            _context = context;
        }

        // GET: Alicilars
        public async Task<IActionResult> Index(string searchString, int pageNumber = 1)
        {
            ViewData["CurrentFilter"] = searchString;

            if (_context.Alicilars == null)
            {
                return Problem("Entity set 'MotoDBContext.Alicilars'  is null.");
            }

            var query = _context.Alicilars.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                // Filtering by Isim (Name) or Email (adjust properties if needed)
                query = query.Where(a => a.Isim.Contains(searchString) || a.Email.Contains(searchString));
            }

            int totalItems = await query.CountAsync();

            var items = await query
                .OrderBy(a => a.Isim) // Sorting by Isim (Name)
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            var pagedResult = new PagedResult<Alicilar>
            {
                Items = items,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = PageSize
            };

            return View(pagedResult);
        }

        // The rest of your actions remain unchanged...

        // GET: Alicilars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Alicilars == null)
            {
                return NotFound();
            }

            var alicilar = await _context.Alicilars
                .FirstOrDefaultAsync(m => m.AliciId == id);
            if (alicilar == null)
            {
                return NotFound();
            }

            return View(alicilar);
        }

        // GET: Alicilars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alicilars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AliciId,Isim,Soyisim,Telefon,Email")] Alicilar alicilar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alicilar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alicilar);
        }

        // GET: Alicilars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Alicilars == null)
            {
                return NotFound();
            }

            var alicilar = await _context.Alicilars.FindAsync(id);
            if (alicilar == null)
            {
                return NotFound();
            }
            return View(alicilar);
        }

        // POST: Alicilars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AliciId,Isim,Soyisim,Telefon,Email")] Alicilar alicilar)
        {
            if (id != alicilar.AliciId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alicilar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlicilarExists(alicilar.AliciId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(alicilar);
        }

        // GET: Alicilars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Alicilars == null)
            {
                return NotFound();
            }

            var alicilar = await _context.Alicilars
                .FirstOrDefaultAsync(m => m.AliciId == id);
            if (alicilar == null)
            {
                return NotFound();
            }

            return View(alicilar);
        }

        // POST: Alicilars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Alicilars == null)
            {
                return Problem("Entity set 'MotoDBContext.Alicilars'  is null.");
            }
            var alicilar = await _context.Alicilars.FindAsync(id);
            if (alicilar != null)
            {
                _context.Alicilars.Remove(alicilar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlicilarExists(int id)
        {
            return (_context.Alicilars?.Any(e => e.AliciId == id)).GetValueOrDefault();
        }
    }
}
