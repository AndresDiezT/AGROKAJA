using Backend.DTOs.ProductDTOs;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        // Primer Controller
        [HttpGet("products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }
        // Segundo Controller .. REVISAR CODIGO
        [HttpGet("products/{codeProduct}")]
        public async Task<IActionResult> GetProductById(string codeProduct)
        {
            var product = await _productService.GetProductByIdAsync(codeProduct);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        // Tercer Controller
        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            try
            {
                var product = await _productService.CreateProductAsync(createProductDto);
                return CreatedAtAction(nameof(GetProductById), new { codeProduct = product.CodeProduct }, product);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // Cuarto Controller
        [HttpPut("products/{codeProduct}")]
        public async Task<IActionResult> UpdateProduct(string codeProduct, [FromBody] CreateProductDto updateProductDto)
        {
            try
            {
                var updatedProduct = await _productService.UpdateProductAsync(codeProduct, updateProductDto);
                return Ok(updatedProduct);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // Quinto Controller
        [HttpPut("products/{codeProduct}/deactivate")]
        public async Task<IActionResult> DeactivateProduct(string codeProduct)
        {
            var result = await _productService.DeactivateProductAsync(codeProduct);
            if (!result)
            {
                return NoContent();
            }
            return NotFound($"El producto con el {codeProduct} no Existe");
        }
        // Sexto Controller
        [HttpPost("products/{codeProduct}/activate")]
        public async Task<IActionResult> ActivateProduct(string codeProduct)
        {
            var result = await _productService.ActivateProductAsync(codeProduct);
            if (!result)
            {
                return NoContent();
            }
            return NotFound($"El producto con el {codeProduct} no Existe");
        }
    }
}
