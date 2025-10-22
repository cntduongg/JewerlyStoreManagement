using Microsoft.AspNetCore.Mvc;
using ProductsApi.Models.Dtos;
using ProductsApi.Service.IService;

namespace ProductsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productService;

        public ProductsController(IProductsService productService)
        {
            _productService = productService;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllAsync();
                return Ok(new { success = true, data = products });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error retrieving products",
                    error = ex.Message
                });
            }
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound(new { success = false, message = $"Product with ID {id} not found" });
                }
                return Ok(new { success = true, data = product });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error retrieving product",
                    error = ex.Message
                });
            }
        }

        // GET: api/Products/code/ABC123
        [HttpGet("code/{productCode}")]
        public async Task<IActionResult> GetProductByCode(string productCode)
        {
            try
            {
                var product = await _productService.GetByProductCodeAsync(productCode);
                if (product == null)
                {
                    return NotFound(new { success = false, message = $"Product with code '{productCode}' not found" });
                }
                return Ok(new { success = true, data = product });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error retrieving product",
                    error = ex.Message
                });
            }
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { success = false, message = "Invalid data", errors = ModelState });
                }

                var product = await _productService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId },
                    new { success = true, message = "Product created successfully", data = product });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error creating product",
                    error = ex.Message
                });
            }
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto updateDto)
        {
            try
            {
                if (id != updateDto.ProductId)
                {
                    return BadRequest(new { success = false, message = "Product ID mismatch" });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { success = false, message = "Invalid data", errors = ModelState });
                }

                var product = await _productService.UpdateAsync(updateDto);
                return Ok(new { success = true, message = "Product updated successfully", data = product });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error updating product",
                    error = ex.Message
                });
            }
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var result = await _productService.DeleteAsync(id);
                if (result)
                {
                    return Ok(new { success = true, message = "Product deleted successfully" });
                }
                return NotFound(new { success = false, message = $"Product with ID {id} not found" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error deleting product",
                    error = ex.Message
                });
            }
        }
    }
}