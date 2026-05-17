using Inventario.DTOs;

namespace Inventario.Services
{
     public interface IAuthService
     {
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
     }
}
