using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blanche.Shared.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>?> GetAllAsync();
        Task<ProductDto> GetByIdAsync(Guid productId);
        Task<ProductResult.Saved?> CreateAsync(ProductDto productDTO);
        Task<ProductResult.Saved?> CreateWithoutImageAsync(ProductDto productDto);
        Task<ProductResult.Saved?> EditAsync(ProductDto productDTO);
        Task<ProductDto?> EditQuantityInStockAsync(ProductDto productDto);
        Task DeleteAsync(Guid productId);
    }
}
