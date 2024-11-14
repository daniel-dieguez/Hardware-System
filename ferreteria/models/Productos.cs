using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ferreteria.models;


[Table("Producto")]
public class Productos
{
    [Key]
    [Column("id_producto")]
    public long id_producto { get; set; }
    [Column("nombre_producto")]
    public  string nombre_producto { get; set; }
    [Column("descripcion_producto")]
    public string descripcion_producto { get; set; }
    [Column("precio_producto")]
    public int precio_producto { get; set; }
    
    [Column("id_categoria")]
    public long id_categoria { get; set; }
    [Column("foto_producto")]
    public byte[] foto_producto { get; set; }
    [Column("activo")]
    public bool Activo { get; set; }
    
    public Categorias? Categorias { get; set; }
    
}