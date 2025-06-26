using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BikeAppApp.Controllers;
using BikeAppApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace BikeAppApp.Tests.Controllers
{
    public class CalisanlarsControllerTests
    {
        private MotoDBContext BuildCtx()
        {
            var opts = new DbContextOptionsBuilder<MotoDBContext>()
                       .UseInMemoryDatabase($"CalisanDb_{Guid.NewGuid()}")
                       .Options;

            var ctx = new MotoDBContext(opts);

            // seed related tables
            ctx.Bayilers.Add(new Bayiler { BayiId = 1, BayiAdi = "Merkez" });

            ctx.Calisanlars.Add(new Calisanlar
            {
                CalisanId = 1,
                Isim = "Ali",
                Soyisim = "Yılmaz",
                Telefon = "123",
                Email = "ali@example.com",
                CalistigiYerId = 1,
                CalistigiYerTipi = "Bayi"
            });

            ctx.SaveChanges();
            return ctx;
        }

        [Fact]
        public async Task Index_ReturnsViewWithList()
        {
            var ctx = BuildCtx();
            var ctrl = new CalisanlarsController(ctx);

            var res = await ctrl.Index();
            var view = Assert.IsType<ViewResult>(res);
            var model = Assert.IsAssignableFrom<IEnumerable<Calisanlar>>(view.Model);

            Assert.Single(model);
        }

        [Fact]
        public async Task Details_InvalidId_NotFound()
        {
            var res = await new CalisanlarsController(BuildCtx()).Details(99);
            Assert.IsType<NotFoundResult>(res);
        }

        [Fact]
        public async Task Details_ValidId_ReturnsView()
        {
            var ctx = BuildCtx();
            var ctrl = new CalisanlarsController(ctx);

            var res = await ctrl.Details(1);
            var view = Assert.IsType<ViewResult>(res);
            var model = Assert.IsType<Calisanlar>(view.Model);

            Assert.Equal("Ali", model.Isim);
        }

        [Fact]
        public async Task DeleteConfirmed_RemovesEntity()
        {
            var ctx = BuildCtx();
            var ctrl = new CalisanlarsController(ctx);

            var res = await ctrl.DeleteConfirmed(1);

            Assert.IsType<RedirectToActionResult>(res);
            Assert.Empty(ctx.Calisanlars);
        }
    }
}
