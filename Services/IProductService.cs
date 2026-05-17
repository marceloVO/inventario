using Inventario.DTOs;

namespace Inventario.Services
{
     public interface IProductService
     {
         Task<IEnumerable<ProductResponseDto>> GetAllAsync();
         Task<ProductResponseDto?> GetByIdAsync(int id);
         Task<ProductResponseDto> CreateAsync(ProductCreateDto dto);
         Task<ProductResponseDto?> UpdateAsync(int id, ProductUpdateDto dto);
         Task<bool> DeleteAsync(int id);
     }
}
