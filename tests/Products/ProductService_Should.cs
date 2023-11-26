using Blanche.Server.Persistence;
using Blanche.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Blanche.Shared.Reservations;
using Moq;
using System.Text.Json;
using tests.Fakers.Products;
using Blanche.Domain.Products;
using Blanche.Shared.Products;
using Blanche.Mappers.Products;
using Blanche.Server.Services.Products;
using Shouldly;

namespace tests.Products
{
    public class ProductService_Should
    {
        private readonly ITestOutputHelper _output;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly BlancheDbContext _dbContext = default!;

        public ProductService_Should(ITestOutputHelper output)
        {
            _output = output;
            _unitOfWork = new UnitOfWork(_dbContext);
        }

        [Fact]
        public async Task CreateProductAsync_should_add_product_when_not_duplicate()
        {
            // Arrange
            Product product = new ProductFaker();
            _output.WriteLine(JsonSerializer.Serialize(product, new JsonSerializerOptions { WriteIndented = true }));
            ProductDto productDto = ProductMapper.ToProductDto(product);
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Products.Add(product));
            //ProductService productService = new ProductService(mockUnitOfWork.Object);

            // Act
            //var result = await productService.CreateAsync(productDto);
            //_output.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true }));

            // Assert
            //result.ShouldBeOfType<ProductDto>();
            //result.ShouldNotBeNull();
        }
    }
}
