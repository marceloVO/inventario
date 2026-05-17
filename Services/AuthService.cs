using Inventario.Data;
using Inventario.DTOs;
using Inventario.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace Inventario.Services
{
     public class AuthService : IAuthService
     {
         private readonly ApplicationDbContext _context;
         private readonly IConfiguration _config;

         public AuthService(ApplicationDbContext context, IConfiguration config)
         {
             _context = context;
             _config = config;
         }

         public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
         {
             var user = await _context.Usuarios
             .FirstOrDefaultAsync(u => u.NombreUsuario == loginDto.NombreUsuario);

             if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
             throw new UnauthorizedAccessException("Usuario o contraseña incorrectos.");

             var token = GenerateJwtToken(user);

             return new AuthResponseDto
             {
                 Token = token,
                 NombreUsuario = user.NombreUsuario,
                 Rol = user.Rol
             };
         }

         private string GenerateJwtToken(Usuario user)
         {
             var jwtSettings = _config.GetSection("Jwt");
             var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
             var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

             var claims = new[]
             {
                 new Claim(ClaimTypes.Name, user.NombreUsuario),
                 new Claim(ClaimTypes.Role, user.Rol)
             };

             var token = new JwtSecurityToken(
                 issuer: jwtSettings["Issuer"],
                 audience: jwtSettings["Audience"],
                 claims: claims,
                 expires: DateTime.UtcNow.AddHours(2),
                 signingCredentials: creds
             );

             return new JwtSecurityTokenHandler().WriteToken(token);
         }
     }
}
