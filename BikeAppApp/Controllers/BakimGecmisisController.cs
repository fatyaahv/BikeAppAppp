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
    public class BakimGecmisisController : Controller
    {
        private readonly MotoDBContext _context;

        public BakimGecmisisController(MotoDBContext context)
        {
            _context = context;
        }

        // GET: BakimGecmisis
        public async Task<IActionResult> Index()
        {
            var motoDBContext = _context.BakimGecmisis.Include(b => b.Motosiklet).Include(b => b.Servis);
            return View(await motoDBContext.ToListAsync());
        }

        // GET: BakimGecmisis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BakimGecmisis == null)
            {
                return NotFound();
            }

            var bakimGecmisi = await _context.BakimGecmisis
                .Include(b => b.Motosiklet)
                .Include(b => b.Servis)
                .FirstOrDefaultAsync(m => m.BakimId == id);
            if (bakimGecmisi == null)
            {
                return NotFound();
            }

            return View(bakimGecmisi);
        }

        // GET: BakimGecmisis/Create
        public IActionResult Create()
        {
            ViewData["MotosikletId"] = new SelectList(_context.Motosikletlers, "MotosikletId", "MotosikletId");
            ViewData["ServisId"] = new SelectList(_context.YetkiliServis, "ServisId", "ServisId");
            return View();
        }

        // POST: BakimGecmisis/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BakimId,MotosikletId,ServisId,BakimTarihi,YapilanIslemler")] BakimGecmisi bakimGecmisi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bakimGecmisi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MotosikletId"] = new SelectList(_context.Motosikletlers, "MotosikletId", "MotosikletId", bakimGecmisi.MotosikletId);
            ViewData["ServisId"] = new SelectList(_context.YetkiliServis, "ServisId", "ServisId", bakimGecmisi.ServisId);
            return View(bakimGecmisi);
        }

        // GET: BakimGecmisis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BakimGecmisis == null)
            {
                return NotFound();
            }

            var bakimGecmisi = await _context.BakimGecmisis.FindAsync(id);
            if (bakimGecmisi == null)
            {
                return NotFound();
            }
            ViewData["MotosikletId"] = new SelectList(_context.Motosikletlers, "MotosikletId", "MotosikletId", bakimGecmisi.MotosikletId);
            ViewData["ServisId"] = new SelectList(_context.YetkiliServis, "ServisId", "ServisId", bakimGecmisi.ServisId);
            return View(bakimGecmisi);
        }

        // POST: BakimGecmisis/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BakimId,MotosikletId,ServisId,BakimTarihi,YapilanIslemler")] BakimGecmisi bakimGecmisi)
        {
            if (id != bakimGecmisi.BakimId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bakimGecmisi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BakimGecmisiExists(bakimGecmisi.BakimId))
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
            ViewData["MotosikletId"] = new SelectList(_context.Motosikletlers, "MotosikletId", "MotosikletId", bakimGecmisi.MotosikletId);
            ViewData["ServisId"] = new SelectList(_context.YetkiliServis, "ServisId", "ServisId", bakimGecmisi.ServisId);
            return View(bakimGecmisi);
        }

        // GET: BakimGecmisis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BakimGecmisis == null)
            {
                return NotFound();
            }

            var bakimGecmisi = await _context.BakimGecmisis
                .Include(b => b.Motosiklet)
                .Include(b => b.Servis)
                .FirstOrDefaultAsync(m => m.BakimId == id);
            if (bakimGecmisi == null)
            {
                return NotFound();
            }

            return View(bakimGecmisi);
        }

        // POST: BakimGecmisis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BakimGecmisis == null)
            {
                return Problem("Entity set 'MotoDBContext.BakimGecmisis'  is null.");
            }
            var bakimGecmisi = await _context.BakimGecmisis.FindAsync(id);
            if (bakimGecmisi != null)
            {
                _context.BakimGecmisis.Remove(bakimGecmisi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BakimGecmisiExists(int id)
        {
            return (_context.BakimGecmisis?.Any(e => e.BakimId == id)).GetValueOrDefault();
        }
    }
}
