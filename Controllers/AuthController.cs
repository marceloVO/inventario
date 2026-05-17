using Inventario.DTOs;
using Inventario.Models;
using Inventario.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventario.Data;

namespace Inventario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ApplicationDbContext _context;

        public AuthController(IAuthService authService, ApplicationDbContext context)
        {
            _authService = authService;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos de login inválidos.");

            var result = await _authService.LoginAsync(dto);
            if (result == null)
                return Unauthorized("Usuario o contraseña incorrectos.");

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginDto dto)
        {
            // Validación básica de datos
            if (string.IsNullOrWhiteSpace(dto.NombreUsuario) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Nombre de usuario y contraseña son obligatorios.");

            // Verifica si el usuario ya existe
            if (await _context.Usuarios.AnyAsync(u => u.NombreUsuario == dto.NombreUsuario))
                return BadRequest("El usuario ya existe.");

            // Crea el hash de la contraseña
            var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // Crea el nuevo usuario
            var user = new Usuario
            {
                NombreUsuario = dto.NombreUsuario,
                PasswordHash = hash,
                Rol = "User"
            };

            // Guarda el usuario en la base de datos
            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Usuario creado correctamente" });
        }
    }
}
