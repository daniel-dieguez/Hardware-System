using ferreteria.models;
using Microsoft.EntityFrameworkCore;

namespace ferreteria.context;

public class ApplicationDbContext : DbContext
{
    
    public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options) : base(options) { }
    
    
    public DbSet<Categorias> Categoria { get; set; } = null!;
    public DbSet<Productos> Productos { get; set; } = null!;
    
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //relationship between products and categorias
        modelBuilder.Entity<Productos>()
            .HasOne(p => p.Categorias)
            .WithMany(a => a.ProductosList)
            .HasForeignKey(p => p.id_categoria);

    }

}