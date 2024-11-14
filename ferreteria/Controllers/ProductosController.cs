using ferreteria.context;
using ferreteria.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ferreteria.Controllers;

[Controller]
[Route("[controller]")]
public class ProductosController : Controller
{
    
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ProductosController> _logger;
    
    public ProductosController( ApplicationDbContext context, ILogger<ProductosController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("/productos/view")]
    public async Task<ActionResult<IEnumerable<Productos>>> viewProductos()
    {
        var response = new Dictionary<String, Object>();
        try
        {
            _logger.LogInformation("you make a consult at productos");
            response.Add("message","consultation at products");
            var productosConsult = await _context.Productos
                .Include(p => p.Categorias) // Esto debe incluir la relación
                .ToListAsync();
            return Ok(productosConsult);
        }
        catch (Exception e)
        { 
            _logger.LogError(e, "An error occurred while fetching products");
            return StatusCode(500, "Error: try again");
        }
    }
    
}