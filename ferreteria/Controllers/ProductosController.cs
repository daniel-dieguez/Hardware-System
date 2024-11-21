using System.Runtime.InteropServices.JavaScript;
using System.Security.Cryptography;
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
    


    [HttpPost("/productos/newProduct")]
    public async Task<IActionResult> newProduct( Productos productos)
    {
        var response = new Dictionary<String, Object>();
        try
        {
            //id generate
            productos.id_producto = GenerateRandomLong();

            var categoriaExist = await _context.Categoria.AnyAsync(c => c.id_categoria == productos.id_categoria);

            if (!categoriaExist)
            {
                return NotFound("not found this id categoria");
            }
            
            
            await _context.Productos.AddAsync(productos);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("you should create new product");
            response.Add("message","you can consult");
            return CreatedAtAction(nameof(viewProductos), new {id = productos.id_producto}, productos);

        }
        catch (Exception e)
        {
            _logger.LogInformation(e,"Error to create a new product");
            return StatusCode(500, "Error");
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