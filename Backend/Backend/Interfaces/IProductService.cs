using Backend.DTOs.ProductDTOs;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(string codeProduct);
        Task<Product> CreateProductAsync(CreateProductDto createProductDto);
        Task<Product> UpdateProductAsync(string codeProduct, CreateProductDto updateProductDto);
        Task<bool> DeactivateProductAsync(string codeProduct);
        Task<bool> ActivateProductAsync(string codeProduct);
    }
}
