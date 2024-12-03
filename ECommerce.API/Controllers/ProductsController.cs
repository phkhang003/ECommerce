using Microsoft.AspNetCore.Mvc;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;
using ECommerce.Application.Models;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IValidator<CreateProductDto> _createProductValidator;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(
            IProductService productService,
            IValidator<CreateProductDto> createProductValidator,
            ILogger<ProductsController> logger)
        {
            _productService = productService;
            _createProductValidator = createProductValidator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<ProductDto>>> GetProducts([FromQuery] ProductParameters parameters)
        {
            var products = await _productService.GetProductsAsync(parameters);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(string id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto productDto)
        {
            var validationResult = await _createProductValidator.ValidateAsync(productDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var product = await _productService.CreateProductAsync(productDto);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> UpdateProduct(string id, [FromBody] UpdateProductDto productDto)
        {
            try
            {
                var updatedProduct = await _productService.UpdateProductAsync(id, productDto);
                if (updatedProduct == null)
                {
                    return NotFound($"Product with ID {id} not found");
                }
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product with ID {ProductId}", id);
                return StatusCode(500, "An error occurred while updating the product");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
} 