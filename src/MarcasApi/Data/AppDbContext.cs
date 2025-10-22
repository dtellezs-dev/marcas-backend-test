using Microsoft.EntityFrameworkCore;
using MarcasApi.Models;

namespace MarcasApi.Data;

/// <summary>
/// DbContext para la aplicación. Configurado para PostgreSQL pero compatible con InMemory provider para pruebas.
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<MarcaAuto> MarcasAutos { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed data: al menos tres marcas de autos
        modelBuilder.Entity<MarcaAuto>().HasData(
            new MarcaAuto { Id = 1, Nombre = "Toyota" },
            new MarcaAuto { Id = 2, Nombre = "Ford" },
            new MarcaAuto { Id = 3, Nombre = "Chevrolet" }
        );
    }
}
