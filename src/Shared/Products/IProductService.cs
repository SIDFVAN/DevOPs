using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blanche.Shared.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAll();
        Task<ProductDto> GetById(int productId);
        Task<int> CreateAsync(ProductDto productDTO);
        Task EditAsync(ProductDto productDTO);
        Task DeleteAsync(int productId);

    }
}
