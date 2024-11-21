using System.Collections;
using System.Security.Cryptography;
using ferreteria.context;
using ferreteria.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ferreteria.Controllers;

[ApiController]
[Route("[controller]")]
public class MovimientosController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<MovimientosController> _logger;
    
    public MovimientosController( ApplicationDbContext context, ILogger<MovimientosController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    [HttpGet("/movimientos/viewAll")]
    public async Task<ActionResult<IEnumerable<DetallesMovimientos>>> viewAllMovimientos()
    {
        var response = new Dictionary<String, Object>();
        try
        {
            _logger.LogInformation("You make a consult at movimientosDetalles");
            response.Add("mensaje", "consult at detallesMovimientos");
            var movimientos = await _context.detallesMov
                .Include(p => p.Productos)
                .ToListAsync();
            return Ok(movimientos);

        }
        catch (Exception e)
        {
            _logger.LogError(e,"Not found data");
            return StatusCode(500, "try again");
        }
    }
    
    [HttpPost("/movimientos/newMovimiento")]
    public async Task<IActionResult> newMovimiento(DetallesMovimientos movimientos)
    {
        try
        {
            movimientos.id_movimientos = GenerateRandomLong();
            //verificacion de id producto
            var productoExistente = await _context.Productos.FindAsync(movimientos.id_producto);
            if (productoExistente == null)
            {
                return NotFound(new { message = "El producto especificado no existe." });
            }

            //asignar el producto relacionado
            movimientos.Productos = null;
            await _context.detallesMov.AddAsync(movimientos);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(viewAllMovimientos), new { id = movimientos.id_movimientos }, movimientos);

        }
        catch (Exception e)
        {

            _logger.LogError(e, "error to create a new movimiento");
            return StatusCode(500, "try again to create movimiento");
        }
        
    }  
    //Http post
    private long GenerateRandomLong()
    {
        byte[] buffer = new byte[8];
        RandomNumberGenerator.Fill(buffer);
        return BitConverter.ToInt64(buffer, 0) & long.MaxValue; // Aseguramos que sea positivo
    }
    
    
    
    
}