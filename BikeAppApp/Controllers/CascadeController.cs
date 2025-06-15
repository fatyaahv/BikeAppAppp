using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BikeAppApp.Models;

namespace BikeAppApp.Controllers
{
    public class CascadeController : Controller
    {
        private readonly MotoDBContext _context;

        public CascadeController(MotoDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ccList = await _context.Motosikletlers.Select(m => m.CC).Distinct().ToListAsync();
            var model = new Cascade
            {
                CCList = ccList.Select(cc => new SelectListItem { Value = cc.ToString(), Text = cc.ToString() })
            };

            model.MotorList = new SelectList(Enumerable.Empty<SelectListItem>());

            return View(model);
        }

        public async Task<JsonResult> GetMotorsByCC(int cc)
        {
            var motors = await _context.Motosikletlers
                .Where(m => m.CC == cc)
                .Select(m => new { m.Marka, m.Model })
                .ToListAsync();

            return Json(motors);
        }
    }
}
