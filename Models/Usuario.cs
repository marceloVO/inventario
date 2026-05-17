using System.ComponentModel.DataAnnotations;

namespace Inventario.Models
{
    public class Usuario
    {
      [Key]
      public int Id { get; set; }

      [Required, MaxLength(50)]
      public string NombreUsuario { get; set; } = null!;

      [Required]
      public string PasswordHash { get; set; } = null!;

      [Required, MaxLength(20)]
      public string Rol { get; set; } = "User";
    }
}
