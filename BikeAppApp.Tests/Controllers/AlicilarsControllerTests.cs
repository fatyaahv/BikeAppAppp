using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using BikeAppApp.Controllers;
using BikeAppApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeAppApp.Helpers;

namespace BikeAppApp.Tests.Controllers
{
    public class AlicilarsControllerTests
    {
        private readonly List<Alicilar> _fakeAlicilar = new List<Alicilar>
        {
            new Alicilar { AliciId = 1, Isim = "Ali", Soyisim = "Veli", Telefon = "123", Email = "ali@example.com" },
            new Alicilar { AliciId = 2, Isim = "Ayşe", Soyisim = "Yılmaz", Telefon = "456", Email = "ayse@example.com" }
        };
        private MotoDBContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<MotoDBContext>()
                // Each call gets a fresh, isolated in-memory store
                .UseInMemoryDatabase(databaseName: $"TestAlicilarDb_{Guid.NewGuid()}")
                .Options;

            var ctx = new MotoDBContext(options);
            ctx.Alicilars.AddRange(_fakeAlicilar);   // always seed
            ctx.SaveChanges();
            return ctx;
        }

      /*  private MotoDBContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<MotoDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            var context = new MotoDBContext(options);
            if (!context.Alicilars.Any())
            {
                context.Alicilars.AddRange(_fakeAlicilar);
                context.SaveChanges();
            }
            return context;
        }*/
        [Fact]
        public async Task Index_ReturnsView_WithPagedResult()
        {
            // Arrange
            var context = GetDbContext();
            var controller = new AlicilarsController(context);

            // Act
            var result = await controller.Index(null, 1);   // note pageNumber param

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<PagedResult<Alicilar>>(viewResult.Model);

            Assert.Equal(2, model.TotalItems);          // overall count
            Assert.Equal(2, model.Items.Count);         // items on page 1
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            var controller = new AlicilarsController(GetDbContext());

            var result = await controller.Details(null);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsView_WhenValidId()
        {
            var controller = new AlicilarsController(GetDbContext());

            var result = await controller.Details(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Alicilar>(viewResult.Model);
            Assert.Equal(1, model.AliciId);
        }
    }
}
