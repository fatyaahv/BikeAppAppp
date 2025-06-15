using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BikeAppApp.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace BikeAppApp.Controllers
{
    public class MotosikletlersController : Controller
    {
        private readonly MotoDBContext _context;

        public MotosikletlersController(MotoDBContext context)
        {
            _context = context;
        }

        // GET: Motosikletlers
        public async Task<IActionResult> Index(string searchString)
        {
            var motosikletler = from m in _context.Motosikletlers select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                motosikletler = motosikletler.Where(s => s.Marka.Contains(searchString) || s.Model.Contains(searchString));
            }

            return View(await motosikletler.ToListAsync());
        }

        // GET: Motosikletlers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Motosikletlers == null)
            {
                return NotFound();
            }

            var motosikletler = await _context.Motosikletlers
                .FirstOrDefaultAsync(m => m.MotosikletId == id);
            if (motosikletler == null)
            {
                return NotFound();
            }

            return View(motosikletler);
        }

        // GET: Motosikletlers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Motosikletlers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MotosikletId,Marka,Model,CC,FotoğrafYolu")] Motosikletler motosikletler, IFormFile Fotoğraf)
        {
            if (ModelState.IsValid)
            {
                if (Fotoğraf != null && Fotoğraf.Length > 0)
                {
                    // Fotoğrafın kaydedileceği yol
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", Fotoğraf.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Fotoğraf.CopyToAsync(stream);
                    }

                    // Fotoğraf yolu modelde saklanabilir
                    motosikletler.FotoğrafYolu = "/images/" + Fotoğraf.FileName;
                }

                _context.Add(motosikletler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(motosikletler);
        }

        // GET: Motosikletlers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Motosikletlers == null)
            {
                return NotFound();
            }

            var motosikletler = await _context.Motosikletlers.FindAsync(id);
            if (motosikletler == null)
            {
                return NotFound();
            }
            return View(motosikletler);
        }

        // POST: Motosikletlers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MotosikletId,Marka,Model,FotoğrafYolu,CC")] Motosikletler motosikletler, IFormFile Fotoğraf)
        {
            if (id != motosikletler.MotosikletId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Fotoğraf != null && Fotoğraf.Length > 0)
                    {
                        // Eski fotoğrafı sil (isteğe bağlı)
                        if (!string.IsNullOrEmpty(motosikletler.FotoğrafYolu))
                        {
                            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", motosikletler.FotoğrafYolu.TrimStart('/'));
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }
                        }

                        // Yeni fotoğrafın kaydedileceği yol
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", Fotoğraf.FileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await Fotoğraf.CopyToAsync(stream);
                        }

                        // Fotoğraf yolu modelde saklanabilir
                        motosikletler.FotoğrafYolu = "/images/" + Fotoğraf.FileName;
                    }

                    _context.Update(motosikletler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MotosikletlerExists(motosikletler.MotosikletId))
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
            return View(motosikletler);
        }

        // GET: Motosikletlers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Motosikletlers == null)
            {
                return NotFound();
            }

            var motosikletler = await _context.Motosikletlers
                .FirstOrDefaultAsync(m => m.MotosikletId == id);
            if (motosikletler == null)
            {
                return NotFound();
            }

            return View(motosikletler);
        }

        // POST: Motosikletlers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Motosikletlers == null)
            {
                return Problem("Entity set 'MotoDBContext.Motosikletlers'  is null.");
            }
            var motosikletler = await _context.Motosikletlers.FindAsync(id);
            if (motosikletler != null)
            {
                if (!string.IsNullOrEmpty(motosikletler.FotoğrafYolu))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", motosikletler.FotoğrafYolu.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
                _context.Motosikletlers.Remove(motosikletler);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MotosikletlerExists(int id)
        {
            return (_context.Motosikletlers?.Any(e => e.MotosikletId == id)).GetValueOrDefault();
        }

        // Yeni Action: GetMotorsByCC
        public async Task<IActionResult> GetMotorsByCC(int cc, string marka, string sortBy)
        {
            var motors = _context.Motosikletlers.AsQueryable();

            if (cc > 0)
            {
                motors = motors.Where(m => m.CC == cc);
            }

            if (!string.IsNullOrEmpty(marka))
            {
                motors = motors.Where(m => m.Marka.Contains(marka));
            }

            switch (sortBy)
            {
                case "model":
                    motors = motors.OrderBy(m => m.Model);
                    break;
                case "marka":
                    motors = motors.OrderBy(m => m.Marka);
                    break;
                default:
                    motors = motors.OrderBy(m => m.MotosikletId);
                    break;
            }

            var result = await motors
                .Select(m => new { m.MotosikletId, m.Marka, m.Model })
                .ToListAsync();

            return Json(result);
        }

    }
}