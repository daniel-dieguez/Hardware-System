using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ferreteria.models;

[Table("Categoria")]
public class Categorias
{
 [Key]
 [Column ("id_categoria")]
 public long id_categoria { get; set; } 
 [Column("categoria_producto")]
 public string categoria_producto { get; set; }

 [JsonIgnore] public List<Productos> ProductosList { get; set; } = new List<Productos>();
}