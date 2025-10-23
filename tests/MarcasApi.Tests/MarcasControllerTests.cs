using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;  // ? ESTE FALTA
using Microsoft.EntityFrameworkCore;
using MarcasApi.Controllers;
using MarcasApi.Data;
using MarcasApi.Models;
using Xunit;

namespace MarcasApi.Tests;

public class MarcasControllerTests
{
    // Helper to create an in-memory context seeded with data (matching OnModelCreating seed)
    private AppDbContext CreateInMemoryContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        var context = new AppDbContext(options);
        // Ensure created and seed manually because InMemory doesn't run OnModelCreating seed automatically for some cases
        if (!context.MarcasAutos.Any())
        {
            context.MarcasAutos.AddRange(
                new MarcaAuto { Id = 1, Nombre = "Toyota" },
                new MarcaAuto { Id = 2, Nombre = "Ford" },
                new MarcaAuto { Id = 3, Nombre = "Chevrolet" }
            );
            context.SaveChanges();
        }
        return context;
    }

    [Fact]
    public async Task GetAll_ReturnsAllSeededMarcas()
    {
        // Arrange
        using var context = CreateInMemoryContext(nameof(GetAll_ReturnsAllSeededMarcas));
        var controller = new MarcasAutosController(context);

        // Act
        var result = await controller.GetAll();

        // Assert
        var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result.Result);
        var items = Assert.IsAssignableFrom<IEnumerable<MarcaAuto>>(okResult.Value);
        Assert.Equal(3, items.Count());
    }

    [Fact]
    public async Task GetAll_ReturnsEmpty_WhenNoMarcas()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: nameof(GetAll_ReturnsEmpty_WhenNoMarcas))
            .Options;
        using var context = new AppDbContext(options);
        // ensure empty
        context.MarcasAutos.RemoveRange(context.MarcasAutos);
        context.SaveChanges();

        var controller = new MarcasAutosController(context);

        // Act
        var result = await controller.GetAll();

        // Assert
        var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result.Result);
        var items = Assert.IsAssignableFrom<IEnumerable<MarcaAuto>>(okResult.Value);
        Assert.Empty(items);
    }
}