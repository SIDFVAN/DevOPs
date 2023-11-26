using Microsoft.AspNetCore.Mvc;
using Blanche.Shared.Products;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using Blanche.Domain.Products;
using System.Data;
using Blanche.Shared.Reservations;

namespace Blanche.Server.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductController : Controller
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[SwaggerOperation("Returns a list of products available for renting during an event.")]
        [HttpGet, AllowAnonymous]
        public async Task<IEnumerable<ProductDto>> GetAllAsync()
		{
			return await _productService.GetAllAsync();
		}

        [SwaggerOperation("Returns a specific product available for renting during an event.")]
        [HttpGet("{id}"), AllowAnonymous]
		public async Task<ProductDto> GetByIdAsync(Guid productId)
        {
			return await _productService.GetByIdAsync(productId);
		}

        [SwaggerOperation("Creates a new product for renting.")]
        [HttpPost]	
		public async Task<IActionResult> Create(ProductDto productDto)
		{
            // authentication required
            var productId = await _productService.CreateAsync(productDto);
            return CreatedAtAction(nameof(Create), productId);
        }

        [SwaggerOperation("Edites an existing product in the catalog.")]
        [HttpPut("{productId}")]
        public async Task<IActionResult> Edit(ProductDto productDto)
        {
            // authentication required
            await _productService.EditAsync(productDto);
            return NoContent();
        }

        [SwaggerOperation("Updates the quantity in stock of a product.")]
        [HttpPut]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ProductDto> EditQuantityInStockAsync(ProductDto productDto)
        {
            var product = await _productService.EditQuantityInStockAsync(productDto);
            return product!;
        }

        [SwaggerOperation("Deletes an existing product.")]
        [HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid productId)
		{
            // authentication required
            await _productService.DeleteAsync(productId);
            return NoContent();
        }
	}
}
