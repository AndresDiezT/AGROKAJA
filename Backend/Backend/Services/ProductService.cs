using Backend.Data;
using Backend.DTOs.ProductDTOs;
using Backend.Interfaces;
using Backend.Models;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class ProductService : IProductService
    {
        private readonly BackendDbContext _context;
        public ProductService(BackendDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }
        public async Task<Product> GetProductByIdAsync(string codeProduct)
        {
            return await _context.Products.FindAsync(codeProduct);
        }
    public async Task<Product> CreateProductAsync(CreateProductDto createProductDto)
    {
        var productExists = await _context.Products
            .AnyAsync(p => p.NameProduct == createProductDto.NameProduct);
        if (productExists)
        {
            throw new InvalidOperationException($"El producto '{createProductDto.NameProduct}' ya existe.");
        }

        var productId = GenerateProductId(createProductDto.NameProduct);

        var product = new Product
        {
            CodeProduct = productId,
            NameProduct = createProductDto.NameProduct,
            DescriptionProduct = createProductDto.Description,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }
    //Revisar Codigo 
    private string GenerateProductId(string name)
    {
        var words = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var idParts = words.Take(3).Select(w => w.Substring(0, Math.Min(3, w.Length)).ToUpper());
        return string.Join("", idParts);
    }
    public async Task<Product> UpdateProductAsync(string CodeProduct, CreateProductDto updateProductDto)
        {
            var product = await _context.Products.FindAsync(CodeProduct);
            if (product == null)
            {
                throw new KeyNotFoundException($"El producto con el ID {CodeProduct} no existe.");
            }
            var productExists = await _context.Products
                .AnyAsync(p => p.NameProduct == updateProductDto.NameProduct && p.CodeProduct != CodeProduct);
            if (productExists)
            {
                throw new InvalidOperationException($"El producto '{updateProductDto.NameProduct}' ya existe.");
            }
            product.NameProduct = updateProductDto.NameProduct;
            product.DescriptionProduct = updateProductDto.Description;
            product.UpdatedAt = DateTime.UtcNow;
            product.IdSubCategory = updateProductDto.IdSubCategory;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<bool> DeactivateProductAsync(string codeProduct)
        {
            var product = await _context.Products.FindAsync(codeProduct);
            if (product == null) return false;
            product.IsActive = false;
            product.DeactivatedAt = DateTime.UtcNow;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ActivateProductAsync(string codeProduct)
        {
            var product = await _context.Products.FindAsync(codeProduct);
            if (product == null) return false;
            product.IsActive = true;
            product.DeactivatedAt = null;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
