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
    public class BayiMotosikletsControllerTests
    {
        private MotoDBContext BuildCtx()
        {
            var opts = new DbContextOptionsBuilder<MotoDBContext>()
                       .UseInMemoryDatabase($"BayiMotoDb_{Guid.NewGuid()}")
                       .Options;

            var ctx = new MotoDBContext(opts);

            // seed related tables
            ctx.Bayilers.Add(new Bayiler { BayiId = 1, BayiAdi = "Merkez" });
            ctx.Motosikletlers.Add(new Motosikletler { MotosikletId = 1, Marka = "Honda" });

            ctx.BayiMotosiklets.Add(new BayiMotosiklet
            {
                BayiMotosikletId = 1,
                BayiId = 1,
                MotosikletId = 1
            });

            ctx.SaveChanges();
            return ctx;
        }

        [Fact]
        public async Task Index_ReturnsViewWithList()
        {
            var ctx = BuildCtx();
            var ctrl = new BayiMotosikletsController(ctx);

            var result = await ctrl.Index();

            var view = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<BayiMotosiklet>>(view.Model);
            Assert.Single(model);
        }

        [Fact]
        public async Task Details_NullId_NotFound()
        {
            var res = await new BayiMotosikletsController(BuildCtx()).Details(null);
            Assert.IsType<NotFoundResult>(res);
        }

        [Fact]
        public async Task Details_ValidId_ReturnsView()
        {
            var ctx = BuildCtx();
            var ctrl = new BayiMotosikletsController(ctx);

            var res = await ctrl.Details(1);

            var view = Assert.IsType<ViewResult>(res);
            var model = Assert.IsType<BayiMotosiklet>(view.Model);
            Assert.Equal(1, model.BayiMotosikletId);
        }

        [Fact]
        public async Task DeleteConfirmed_RemovesEntity()
        {
            var ctx = BuildCtx();
            var ctrl = new BayiMotosikletsController(ctx);

            var res = await ctrl.DeleteConfirmed(1);

            Assert.IsType<RedirectToActionResult>(res);
            Assert.Empty(ctx.BayiMotosiklets);
        }
    }
}
