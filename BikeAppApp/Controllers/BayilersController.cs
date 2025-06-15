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
    public class BayilersController : Controller
    {
        private readonly MotoDBContext _context;

        public BayilersController(MotoDBContext context)
        {
            _context = context;
        }

        // GET: Bayilers
        public async Task<IActionResult> Index()
        {
              return _context.Bayilers != null ? 
                          View(await _context.Bayilers.ToListAsync()) :
                          Problem("Entity set 'MotoDBContext.Bayilers'  is null.");
        }

        // GET: Bayilers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bayilers == null)
            {
                return NotFound();
            }

            var bayiler = await _context.Bayilers
                .FirstOrDefaultAsync(m => m.BayiId == id);
            if (bayiler == null)
            {
                return NotFound();
            }

            return View(bayiler);
        }

        // GET: Bayilers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bayilers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BayiId,BayiAdi,Adres")] Bayiler bayiler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bayiler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bayiler);
        }

        // GET: Bayilers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bayilers == null)
            {
                return NotFound();
            }

            var bayiler = await _context.Bayilers.FindAsync(id);
            if (bayiler == null)
            {
                return NotFound();
            }
            return View(bayiler);
        }

        // POST: Bayilers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BayiId,BayiAdi,Adres")] Bayiler bayiler)
        {
            if (id != bayiler.BayiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bayiler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BayilerExists(bayiler.BayiId))
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
            return View(bayiler);
        }

        // GET: Bayilers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bayilers == null)
            {
                return NotFound();
            }

            var bayiler = await _context.Bayilers
                .FirstOrDefaultAsync(m => m.BayiId == id);
            if (bayiler == null)
            {
                return NotFound();
            }

            return View(bayiler);
        }

        // POST: Bayilers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bayilers == null)
            {
                return Problem("Entity set 'MotoDBContext.Bayilers'  is null.");
            }
            var bayiler = await _context.Bayilers.FindAsync(id);
            if (bayiler != null)
            {
                _context.Bayilers.Remove(bayiler);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BayilerExists(int id)
        {
          return (_context.Bayilers?.Any(e => e.BayiId == id)).GetValueOrDefault();
        }
    }
}
