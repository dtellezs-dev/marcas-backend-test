using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarcasApi.Data;
using MarcasApi.Models;

namespace MarcasApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MarcasAutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public MarcasAutosController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene todas las marcas de autos en la base de datos.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MarcaAuto>>> GetAll()
    {
        var marcas = await _context.MarcasAutos.AsNoTracking().ToListAsync();
        return Ok(marcas);
    }
}
