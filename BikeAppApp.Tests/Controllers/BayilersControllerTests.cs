using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BikeAppApp.Controllers;
using BikeAppApp.Models;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace BikeAppApp.Tests.Controllers
{
    public class BayilersControllerTests
    {
        private MotoDBContext BuildContext()
        {
            var opts = new DbContextOptionsBuilder<MotoDBContext>()
                       .UseInMemoryDatabase($"BayilerTestDb_{Guid.NewGuid()}")
                       .Options;

            var ctx = new MotoDBContext(opts);
            ctx.Bayilers.Add(new Bayiler { BayiId = 1, BayiAdi = "Merkez", Adres = "Ankara" });
            ctx.SaveChanges();
            return ctx;
        }

        [Fact]
        public async Task Index_ReturnsViewWithBayilerList()
        {
            var ctx = BuildContext();
            var ctrl = new BayilersController(ctx);

            var result = await ctrl.Index();

            var view = Assert.IsType<ViewResult>(result);
            var list = Assert.IsAssignableFrom<IEnumerable<Bayiler>>(view.ViewData.Model);
            Assert.Single(list);
        }


        [Fact]
        public async Task Details_InvalidId_ReturnsNotFound()
        {
            var result = await new BayilersController(BuildContext()).Details(99);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ValidId_ReturnsView()
        {
            var ctx = BuildContext();
            var ctrl = new BayilersController(ctx);

            var result = await ctrl.Details(1);

            var view = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Bayiler>(view.Model);
            Assert.Equal("Merkez", model.BayiAdi);
        }

        [Fact]
        public async Task DeleteConfirmed_RemovesBayiler()
        {
            var ctx = BuildContext();
            var ctrl = new BayilersController(ctx);

            var result = await ctrl.DeleteConfirmed(1);

            Assert.IsType<RedirectToActionResult>(result);
            Assert.Empty(ctx.Bayilers);
        }
    }
}
