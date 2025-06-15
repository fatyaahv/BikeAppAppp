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
    public class BayiAlicisController : Controller
    {
        private readonly MotoDBContext _context;

        public BayiAlicisController(MotoDBContext context)
        {
            _context = context;
        }

        // GET: BayiAlicis
        public async Task<IActionResult> Index()
        {
            var motoDBContext = _context.BayiAlicis.Include(b => b.Alici).Include(b => b.Bayi).Include(b => b.Motosiklet);
            return View(await motoDBContext.ToListAsync());
        }

        // GET: BayiAlicis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BayiAlicis == null)
            {
                return NotFound();
            }

            var bayiAlici = await _context.BayiAlicis
                .Include(b => b.Alici)
                .Include(b => b.Bayi)
                .Include(b => b.Motosiklet)
                .FirstOrDefaultAsync(m => m.BayiAliciId == id);
            if (bayiAlici == null)
            {
                return NotFound();
            }

            return View(bayiAlici);
        }

        // GET: BayiAlicis/Create
        public IActionResult Create()
        {
            ViewData["AliciId"] = new SelectList(_context.Alicilars, "AliciId", "AliciId");
            ViewData["BayiId"] = new SelectList(_context.Bayilers, "BayiId", "BayiId");
            ViewData["MotosikletId"] = new SelectList(_context.Motosikletlers, "MotosikletId", "MotosikletId");
            return View();
        }

        // POST: BayiAlicis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BayiAliciId,BayiId,AliciId,MotosikletId,SatisTarihi")] BayiAlici bayiAlici)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bayiAlici);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AliciId"] = new SelectList(_context.Alicilars, "AliciId", "AliciId", bayiAlici.AliciId);
            ViewData["BayiId"] = new SelectList(_context.Bayilers, "BayiId", "BayiId", bayiAlici.BayiId);
            ViewData["MotosikletId"] = new SelectList(_context.Motosikletlers, "MotosikletId", "MotosikletId", bayiAlici.MotosikletId);
            return View(bayiAlici);
        }

        // GET: BayiAlicis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BayiAlicis == null)
            {
                return NotFound();
            }

            var bayiAlici = await _context.BayiAlicis.FindAsync(id);
            if (bayiAlici == null)
            {
                return NotFound();
            }
            ViewData["AliciId"] = new SelectList(_context.Alicilars, "AliciId", "AliciId", bayiAlici.AliciId);
            ViewData["BayiId"] = new SelectList(_context.Bayilers, "BayiId", "BayiId", bayiAlici.BayiId);
            ViewData["MotosikletId"] = new SelectList(_context.Motosikletlers, "MotosikletId", "MotosikletId", bayiAlici.MotosikletId);
            return View(bayiAlici);
        }

        // POST: BayiAlicis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BayiAliciId,BayiId,AliciId,MotosikletId,SatisTarihi")] BayiAlici bayiAlici)
        {
            if (id != bayiAlici.BayiAliciId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bayiAlici);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BayiAliciExists(bayiAlici.BayiAliciId))
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
            ViewData["AliciId"] = new SelectList(_context.Alicilars, "AliciId", "AliciId", bayiAlici.AliciId);
            ViewData["BayiId"] = new SelectList(_context.Bayilers, "BayiId", "BayiId", bayiAlici.BayiId);
            ViewData["MotosikletId"] = new SelectList(_context.Motosikletlers, "MotosikletId", "MotosikletId", bayiAlici.MotosikletId);
            return View(bayiAlici);
        }

        // GET: BayiAlicis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BayiAlicis == null)
            {
                return NotFound();
            }

            var bayiAlici = await _context.BayiAlicis
                .Include(b => b.Alici)
                .Include(b => b.Bayi)
                .Include(b => b.Motosiklet)
                .FirstOrDefaultAsync(m => m.BayiAliciId == id);
            if (bayiAlici == null)
            {
                return NotFound();
            }

            return View(bayiAlici);
        }

        // POST: BayiAlicis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BayiAlicis == null)
            {
                return Problem("Entity set 'MotoDBContext.BayiAlicis'  is null.");
            }
            var bayiAlici = await _context.BayiAlicis.FindAsync(id);
            if (bayiAlici != null)
            {
                _context.BayiAlicis.Remove(bayiAlici);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BayiAliciExists(int id)
        {
          return (_context.BayiAlicis?.Any(e => e.BayiAliciId == id)).GetValueOrDefault();
        }
    }
}
