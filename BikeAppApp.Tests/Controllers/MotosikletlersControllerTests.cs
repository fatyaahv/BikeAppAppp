using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BikeAppApp.Controllers;
using BikeAppApp.Models;
using BikeAppApp.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace BikeAppApp.Tests.Controllers
{
    public class MotosikletlersControllerTests
    {
        // ----------  In-memory context helper  ----------
        private MotoDBContext GetContext()
        {
            var opts = new DbContextOptionsBuilder<MotoDBContext>()
                       .UseInMemoryDatabase($"MotoTestDb_{Guid.NewGuid()}")   // unique db per test
                       .Options;

            var ctx = new MotoDBContext(opts);

            // seed once per test
            ctx.Motosikletlers.AddRange(
                new Motosikletler { MotosikletId = 1, Marka = "Yamaha", Model = "R15", CC = 155 },
                new Motosikletler { MotosikletId = 2, Marka = "Honda", Model = "CBR250", CC = 250 }
            );
            ctx.SaveChanges();
            return ctx;
        }

        // ----------  Tests  ----------
        [Fact]
        public async Task Index_NoFilter_ReturnsPagedResult()
        {
            var ctx = GetContext();
            var controller = new MotosikletlersController(ctx);

            var result = await controller.Index(null, 1);

            var view = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<PagedResult<Motosikletler>>(view.Model);

            Assert.Equal(2, model.TotalItems);
            Assert.Equal(2, model.Items.Count);
        }

        [Fact]
        public async Task Index_WithFilter_ReturnsFilteredList()
        {
            var ctx = GetContext();
            var controller = new MotosikletlersController(ctx);

            var result = await controller.Index("Yamaha", 1);

            var view = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<PagedResult<Motosikletler>>(view.Model);

            Assert.Single(model.Items);
            Assert.Equal("Yamaha", model.Items.First().Marka);
        }

        [Fact]
        public async Task Details_InvalidId_ReturnsNotFound()
        {
            var ctx = GetContext();
            var controller = new MotosikletlersController(ctx);

            var result = await controller.Details(99);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ValidId_ReturnsViewWithEntity()
        {
            var ctx = GetContext();
            var controller = new MotosikletlersController(ctx);

            var result = await controller.Details(1);

            var view = Assert.IsType<ViewResult>(result);
            var moto = Assert.IsType<Motosikletler>(view.Model);

            Assert.Equal(1, moto.MotosikletId);
        }

        [Fact]
        public async Task DeleteConfirmed_RemovesEntity_AndRedirects()
        {
            var ctx = GetContext();
            var controller = new MotosikletlersController(ctx);

            var result = await controller.DeleteConfirmed(1);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
            Assert.Single(ctx.Motosikletlers);         // originally 2, now 1
        }
    }
}
