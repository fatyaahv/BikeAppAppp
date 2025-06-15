using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BikeAppApp.Models;

namespace BikeAppApp.Controllers
{
    public class CalisanlarsController : Controller
    {
        private readonly MotoDBContext _context;

        public CalisanlarsController(MotoDBContext context)
        {
            _context = context;
        }

        // GET: Calisanlars
        public async Task<IActionResult> Index()
        {
            var motoDBContext = _context.Calisanlars.Include(c => c.CalistigiYer).Include(c => c.CalistigiYerNavigation);
            return View(await motoDBContext.ToListAsync());
        }

        // GET: Calisanlars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Calisanlars == null)
            {
                return NotFound();
            }

            var calisanlar = await _context.Calisanlars
                .Include(c => c.CalistigiYer)
                .Include(c => c.CalistigiYerNavigation)
                .FirstOrDefaultAsync(m => m.CalisanId == id);
            if (calisanlar == null)
            {
                return NotFound();
            }

            return View(calisanlar);
        }

        // GET: Calisanlars/Create
        public IActionResult Create()
        {
            ViewData["CalistigiYerId"] = new SelectList(_context.Bayilers, "BayiId", "BayiId");
            ViewData["CalistigiYerId"] = new SelectList(_context.YetkiliServis, "ServisId", "ServisId");
            return View();
        }

        // POST: Calisanlars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CalisanId,Isim,Soyisim,Telefon,Email,CalistigiYerId,CalistigiYerTipi")] Calisanlar calisanlar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calisanlar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CalistigiYerId"] = new SelectList(_context.Bayilers, "BayiId", "BayiId", calisanlar.CalistigiYerId);
            ViewData["CalistigiYerId"] = new SelectList(_context.YetkiliServis, "ServisId", "ServisId", calisanlar.CalistigiYerId);
            return View(calisanlar);
        }

        // GET: Calisanlars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Calisanlars == null)
            {
                return NotFound();
            }

            var calisanlar = await _context.Calisanlars.FindAsync(id);
            if (calisanlar == null)
            {
                return NotFound();
            }
            ViewData["CalistigiYerId"] = new SelectList(_context.Bayilers, "BayiId", "BayiId", calisanlar.CalistigiYerId);
            ViewData["CalistigiYerId"] = new SelectList(_context.YetkiliServis, "ServisId", "ServisId", calisanlar.CalistigiYerId);
            return View(calisanlar);
        }

        // POST: Calisanlars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CalisanId,Isim,Soyisim,Telefon,Email,CalistigiYerId,CalistigiYerTipi")] Calisanlar calisanlar)
        {
            if (id != calisanlar.CalisanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calisanlar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalisanlarExists(calisanlar.CalisanId))
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
            ViewData["CalistigiYerId"] = new SelectList(_context.Bayilers, "BayiId", "BayiId", calisanlar.CalistigiYerId);
            ViewData["CalistigiYerId"] = new SelectList(_context.YetkiliServis, "ServisId", "ServisId", calisanlar.CalistigiYerId);
            return View(calisanlar);
        }

        // GET: Calisanlars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Calisanlars == null)
            {
                return NotFound();
            }

            var calisanlar = await _context.Calisanlars
                .Include(c => c.CalistigiYer)
                .Include(c => c.CalistigiYerNavigation)
                .FirstOrDefaultAsync(m => m.CalisanId == id);
            if (calisanlar == null)
            {
                return NotFound();
            }

            return View(calisanlar);
        }

        // POST: Calisanlars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Calisanlars == null)
            {
                return Problem("Entity set 'MotoDBContext.Calisanlars'  is null.");
            }
            var calisanlar = await _context.Calisanlars.FindAsync(id);
            if (calisanlar != null)
            {
                _context.Calisanlars.Remove(calisanlar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalisanlarExists(int id)
        {
          return (_context.Calisanlars?.Any(e => e.CalisanId == id)).GetValueOrDefault();
        }
    }
}
