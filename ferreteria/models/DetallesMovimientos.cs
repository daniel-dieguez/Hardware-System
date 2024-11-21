using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ferreteria.models;

[Table("Movimientos")]
public class DetallesMovimientos
{
    [Key]
    [Column("id_movimiento")]
    public long  id_movimientos { get; set; }
    
    [Column("id_producto")]
    public long id_producto { get; set; }
    [Column("tipo_movimiento")]
    public int tipo_movimiento { get; set; }
    [Column("cantidad")]
    public int cantidad { get; set; }
    [Column("fecha_movimiento")]
    public DateOnly fecha_movimiento { get; set; }
    
    [JsonIgnore]
    public Productos ? Productos { get; set; } 
}