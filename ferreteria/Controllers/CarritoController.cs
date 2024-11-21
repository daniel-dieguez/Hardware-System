using System.Collections;
using System.Security.Cryptography;
using ferreteria.context;
using ferreteria.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ferreteria.Controllers;

[ApiController]
[Route("[controller]")]
public class CarritoController : Controller
{

    private readonly ApplicationDbContext _context;
    private readonly ILogger<CarritoController> _logger;
    public CarritoController( ApplicationDbContext context, ILogger<CarritoController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("/carrito/viewAll")]
    public async Task<ActionResult<IEnumerable<Carrito>>> viewCarrito()
    {
        var response = new Dictionary<String, Object>();
        try
        {
            
            _logger.LogInformation("Se a realizado la consulta con exito");
            response.Add("mensaje", "Consulta en");
            var carritoView = await _context.Productos
                .Include(p => p.Categorias) /// realmente puedo escoger categoria o algo mas
                .ToListAsync();
            return Ok(carritoView);
        }
        catch (Exception e)
        {

            _logger.LogError("Error in the consult");
            return StatusCode(500, "try again to call carrito");
        }
    }

    [HttpPost("/carrito/newCarrito")]
    public async Task<IActionResult> newCarrito(Carrito carrito)
    {
        var response = new Dictionary<String, Object>();
        try
        {
            carrito.id_carrito = GenerateRandomLong();

            if (carrito == null)
            {
                return NotFound(new { message = "El carrito no fue encontrado" });
            }

            _logger.LogInformation("se creo un nuevo carrito");
            carrito.Productos = null;
            await _context.CarritosList.AddAsync(carrito);
            await _context.SaveChangesAsync();
            response.Add("mensaje","nuevo carrito");
            return CreatedAtAction(nameof(viewCarrito), new { id = carrito.id_carrito }, carrito);
            


        }
        catch (Exception e)
        {
            _logger.LogError("Error to create new carrito");
            return StatusCode(500, "try againt to create new carrito");
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