using Microsoft.AspNetCore.Mvc;
using Blanche.Shared.Products;

namespace Blanche.Server.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductController : Controller
	{
		private readonly IProductService _productService;
		private readonly List<ProductDto> _products = new()
		{
			new ProductDto {Id = Guid.NewGuid(), Description = "Voor de sfeer en gezelligheid", MinimumUnits=1, Name="Vuurschaal", Quantity = 5, ImageUrl = "images/products/img_vuurschaal" },
			new ProductDto {Id = Guid.NewGuid(), Description = "Om een lekker stukje vlees of vis te bakken", MinimumUnits=1, Name="Barbecue", Quantity = 5, ImageUrl = "images/products/img_barbecue" },
			new ProductDto {Id = Guid.NewGuid(), Description = "Handige en stevige tafels voor al uw feesten", MinimumUnits=1, Name="tafel", Quantity = 5, ImageUrl = "images/products/img_tafel" },
			new ProductDto {Id = Guid.NewGuid(), Description = "Voor extra sfeer en gezelligheid 's avonds", MinimumUnits=1, Name="Verlichting", Quantity = 5, ImageUrl = "images/products/img_verlichting" },
			new ProductDto {Id = Guid.NewGuid(), Description = "Niet genoeg glazen, we hebben er!", MinimumUnits=6, Name="Glazen tripel", Quantity = 36, ImageUrl = "images/products/img_glazen" },
		};

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

        [HttpGet]
        public IActionResult Index()
		{
			return View();
		}

		[HttpGet("{id}")]
		public IActionResult Details(int id)
		{
			return View();
		}

		[HttpGet("List")]
		public ActionResult List()
		{
			return Ok(_products);
		}

		[HttpPost]
		public ActionResult Create(ProductDto productDTO)
		{
			// authentication required
			return View(productDTO);
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			// authentication required
			return View();
		}
	}
}
