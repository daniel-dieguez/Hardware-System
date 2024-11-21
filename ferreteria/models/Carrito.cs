using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ferreteria.models;

[Table("Carrito")]
public class Carrito
{
    [Key]
    [Column("id_carrito")]
    public long id_carrito{ get; set;}
    [Column("id_producto")]
    public long id_producto{ get; set;}
    [Column("cantidad")]
    public int cantidad{ get; set;}
    [Column("fecha_agregado")]
    public DateOnly fecha_agregado{ get; set;}
    
    [JsonIgnore]
    public Productos ? Productos { get; set; } 
    

}