namespace Inventario.DTOs
{
    public class AuthResponseDto
    {
       public string Token { get; set; } = null!;
       public string NombreUsuario { get; set; } = null!;
       public string Rol { get; set; } = null!;
    }
}
