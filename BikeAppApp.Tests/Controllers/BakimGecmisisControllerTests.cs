using Xunit;
using Microsoft.EntityFrameworkCore;
using BikeAppApp.Controllers;
using BikeAppApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace BikeAppApp.Tests.Controllers
{
    public class BakimGecmisisControllerTests
    {
        private MotoDBContext GetDbContext()
        {
            // 🎯 NEW: unique database name per call
            var options = new DbContextOptionsBuilder<MotoDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new MotoDBContext(options);

            // Seed YetkiliServis
            context.YetkiliServis.AddRange(
                new YetkiliServis { ServisId = 1, ServisAdi = "Service A" },
                new YetkiliServis { ServisId = 2, ServisAdi = "Service B" });

            // Seed Motosikletler
            context.Motosikletlers.AddRange(
                new Motosikletler { MotosikletId = 1, Model = "Model A" },
                new Motosikletler { MotosikletId = 2, Model = "Model B" });

            // Seed one BakimGecmisi row
            context.BakimGecmisis.Add(
                new BakimGecmisi
                {
                    BakimId = 1,
                    MotosikletId = 1,
                    ServisId = 1,
                    BakimTarihi = DateTime.Today,
                    YapilanIslemler = "Oil change"
                });

            context.SaveChanges();
            return context;
        }


        [Fact]
        public async Task Index_ReturnsViewWithBakimGecmisiList()
        {
            // Arrange
            var context = GetDbContext();
            var controller = new BakimGecmisisController(context);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<BakimGecmisi>>(viewResult.Model);
            Assert.NotEmpty(model);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            var context = GetDbContext();
            var controller = new BakimGecmisisController(context);

            var result = await controller.Details(null);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WhenIdIsValid()
        {
            var context = GetDbContext();
            var controller = new BakimGecmisisController(context);

            var result = await controller.Details(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<BakimGecmisi>(viewResult.Model);
            Assert.Equal(1, model.BakimId);
        }

        [Fact]
        public async Task DeleteConfirmed_RemovesItem()
        {
            var context = GetDbContext();
            var controller = new BakimGecmisisController(context);

            var result = await controller.DeleteConfirmed(1);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            Assert.False(context.BakimGecmisis.Any(b => b.BakimId == 1));
        }
    }
}
