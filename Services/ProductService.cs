using Inventario.Data;
using Inventario.DTOs;
using Inventario.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Services
{
     public class ProductService : IProductService
     {
         private readonly ApplicationDbContext _context;

         public ProductService(ApplicationDbContext context)
         {
            _context = context;
         }

         public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
         {
             return await _context.Productos
             .Select(p => new ProductResponseDto
             {
             Id = p.Id,
             Nombre = p.Nombre,
             Descripcion = p.Descripcion,
             Stock = p.Stock,
             Precio = p.Precio,
             FechaCreacion = p.FechaCreacion
             })
             .ToListAsync();
         }

         public async Task<ProductResponseDto?> GetByIdAsync(int id)
         {
             var p = await _context.Productos.FindAsync(id);
             if (p == null) return null;

             return new ProductResponseDto
             {
             Id = p.Id,
             Nombre = p.Nombre,
             Descripcion = p.Descripcion,
             Stock = p.Stock,
             Precio = p.Precio,
             FechaCreacion = p.FechaCreacion
             };
         }

         public async Task<ProductResponseDto> CreateAsync(ProductCreateDto dto)
         {
             var producto = new Producto{
                 Nombre = dto.Nombre,
                 Descripcion = dto.Descripcion,
                 Stock = dto.Stock,
                 Precio = dto.Precio,
                 FechaCreacion = DateTime.UtcNow
             };
                 _context.Productos.Add(producto);
                 await _context.SaveChangesAsync();

             return new ProductResponseDto{
                 Id = producto.Id,
                 Nombre = producto.Nombre,
                 Descripcion = producto.Descripcion,
                 Stock = producto.Stock,
                 Precio = producto.Precio,
                 FechaCreacion = producto.FechaCreacion
             };
         }

         public async Task<ProductResponseDto?> UpdateAsync(int id, ProductUpdateDto dto)
         {
             var producto = await _context.Productos.FindAsync(id);
             if (producto == null) return null;

             producto.Nombre = dto.Nombre;
             producto.Descripcion = dto.Descripcion;
             producto.Stock = dto.Stock;
             producto.Precio = dto.Precio;

             await _context.SaveChangesAsync();

             return new ProductResponseDto{
                 Id = producto.Id,
                 Nombre = producto.Nombre,
                 Descripcion = producto.Descripcion,
                 Stock = producto.Stock,
                 Precio = producto.Precio,
                 FechaCreacion = producto.FechaCreacion
             };
         }

         public async Task<bool> DeleteAsync(int id){
             var producto = await _context.Productos.FindAsync(id);
             if (producto == null) return false;

             _context.Productos.Remove(producto);
             await _context.SaveChangesAsync();
             return true;
         }
     }
}
