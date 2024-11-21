using ferreteria.models;
using Microsoft.EntityFrameworkCore;

namespace ferreteria.context;

public class ApplicationDbContext : DbContext
{
    
    public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options) : base(options) { }
    
    
    public DbSet<Categorias> Categoria { get; set; } = null!;
    public DbSet<Productos> Productos { get; set; } = null!;
    public DbSet<DetallesMovimientos> detallesMov { get; set; } = null!;
    public DbSet<Carrito> CarritosList { get; set; } = null!;
    
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //relationship between products and categorias (one to many)
        modelBuilder.Entity<Productos>()
            .HasOne(p => p.Categorias) 
            .WithMany(a => a.ProductosList)
            .HasForeignKey(p => p.id_categoria);

        //relationship between detalles and producto (one to many)
        modelBuilder.Entity<DetallesMovimientos>()
            .HasOne(p => p.Productos)
            .WithMany(d => d.DetallesMovimientosList)
            .HasForeignKey(p => p.id_producto); //importante este es el key ancla
        
        //relationShip between carrito and product (one to many)
        modelBuilder.Entity<Carrito>()
            .HasOne(p => p.Productos)
            .WithMany(c => c.CarritosList)
            .HasForeignKey(a => a.id_producto);

    }

}