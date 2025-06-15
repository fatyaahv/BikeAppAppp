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
    public class BayiMotosikletsController : Controller
    {
        private readonly MotoDBContext _context;

        public BayiMotosikletsController(MotoDBContext context)
        {
            _context = context;
        }

        // GET: BayiMotosiklets
        public async Task<IActionResult> Index()
        {
            var motoDBContext = _context.BayiMotosiklets.Include(b => b.Bayi).Include(b => b.Motosiklet);
            return View(await motoDBContext.ToListAsync());
        }

        // GET: BayiMotosiklets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BayiMotosiklets == null)
            {
                return NotFound();
            }

            var bayiMotosiklet = await _context.BayiMotosiklets
                .Include(b => b.Bayi)
                .Include(b => b.Motosiklet)
                .FirstOrDefaultAsync(m => m.BayiMotosikletId == id);
            if (bayiMotosiklet == null)
            {
                return NotFound();
            }

            return View(bayiMotosiklet);
        }

        // GET: BayiMotosiklets/Create
        public IActionResult Create()
        {
            ViewData["BayiId"] = new SelectList(_context.Bayilers, "BayiId", "BayiId");
            ViewData["MotosikletId"] = new SelectList(_context.Motosikletlers, "MotosikletId", "MotosikletId");
            return View();
        }

        // POST: BayiMotosiklets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BayiMotosikletId,BayiId,MotosikletId")] BayiMotosiklet bayiMotosiklet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bayiMotosiklet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BayiId"] = new SelectList(_context.Bayilers, "BayiId", "BayiId", bayiMotosiklet.BayiId);
            ViewData["MotosikletId"] = new SelectList(_context.Motosikletlers, "MotosikletId", "MotosikletId", bayiMotosiklet.MotosikletId);
            return View(bayiMotosiklet);
        }

        // GET: BayiMotosiklets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BayiMotosiklets == null)
            {
                return NotFound();
            }

            var bayiMotosiklet = await _context.BayiMotosiklets.FindAsync(id);
            if (bayiMotosiklet == null)
            {
                return NotFound();
            }
            ViewData["BayiId"] = new SelectList(_context.Bayilers, "BayiId", "BayiId", bayiMotosiklet.BayiId);
            ViewData["MotosikletId"] = new SelectList(_context.Motosikletlers, "MotosikletId", "MotosikletId", bayiMotosiklet.MotosikletId);
            return View(bayiMotosiklet);
        }

        // POST: BayiMotosiklets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BayiMotosikletId,BayiId,MotosikletId")] BayiMotosiklet bayiMotosiklet)
        {
            if (id != bayiMotosiklet.BayiMotosikletId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bayiMotosiklet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BayiMotosikletExists(bayiMotosiklet.BayiMotosikletId))
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
            ViewData["BayiId"] = new SelectList(_context.Bayilers, "BayiId", "BayiId", bayiMotosiklet.BayiId);
            ViewData["MotosikletId"] = new SelectList(_context.Motosikletlers, "MotosikletId", "MotosikletId", bayiMotosiklet.MotosikletId);
            return View(bayiMotosiklet);
        }

        // GET: BayiMotosiklets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BayiMotosiklets == null)
            {
                return NotFound();
            }

            var bayiMotosiklet = await _context.BayiMotosiklets
                .Include(b => b.Bayi)
                .Include(b => b.Motosiklet)
                .FirstOrDefaultAsync(m => m.BayiMotosikletId == id);
            if (bayiMotosiklet == null)
            {
                return NotFound();
            }

            return View(bayiMotosiklet);
        }

        // POST: BayiMotosiklets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BayiMotosiklets == null)
            {
                return Problem("Entity set 'MotoDBContext.BayiMotosiklets'  is null.");
            }
            var bayiMotosiklet = await _context.BayiMotosiklets.FindAsync(id);
            if (bayiMotosiklet != null)
            {
                _context.BayiMotosiklets.Remove(bayiMotosiklet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BayiMotosikletExists(int id)
        {
          return (_context.BayiMotosiklets?.Any(e => e.BayiMotosikletId == id)).GetValueOrDefault();
        }
    }
}
