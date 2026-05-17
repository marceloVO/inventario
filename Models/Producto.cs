using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventario.Models
{
    public class Producto
    {
       [Key]
       public int Id { get; set; }

       [Required, MaxLength(100)]
       public string Nombre { get; set; } = null!;

       [MaxLength(255)]
       public string? Descripcion { get; set; }

       [Required]
       public int Stock { get; set; } = 0;

       [Required, Column(TypeName = "decimal(18,2)")]
       public decimal Precio { get; set; }

       [Required]
       public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
     }
}
