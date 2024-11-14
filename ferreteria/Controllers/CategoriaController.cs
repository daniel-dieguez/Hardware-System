using System.Security.Cryptography;
using ferreteria.context;
using ferreteria.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ferreteria.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriaController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<CategoriaController> _logger;
    
    public CategoriaController(ApplicationDbContext context, ILogger<CategoriaController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("/categoria/viewAll")]
    public async Task<ActionResult<IEnumerable<Categorias>>> GetCategorias()
    {
        var response = new Dictionary<String, Object>();
        _logger.LogInformation("It´s make a consult by categories");
        response.Add("message","You would make a consult");
        var categorias = await _context.Categoria.ToListAsync();
        if (categorias == null!)
        {
            return NotFound();
        }

        return Ok(categorias);
    }

    [HttpPost("categoria/newProduct")]
    public async Task<IActionResult> newCategory(Categorias categorias)
    {
        try
        {
            
            categorias.id_categoria = GenerateRandomLong(); //create a UUID with data long
            await _context.Categoria.AddAsync(categorias);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("you can create a new category");
            return CreatedAtAction(nameof(GetCategorias), new { id = categorias.id_categoria }, categorias);  
        }
        catch (Exception e)
        {
            _logger.LogInformation(e,"try again bro");
            return StatusCode(500, "Error to create a new category");

        }
        
    }
    
    private long GenerateRandomLong()
    {
        byte[] buffer = new byte[8];
        RandomNumberGenerator.Fill(buffer);
        return BitConverter.ToInt64(buffer, 0) & long.MaxValue; // Aseguramos que sea positivo
    }
}