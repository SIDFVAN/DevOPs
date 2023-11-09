using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blanche.Shared.Products;
using Blanche.Domain.Products;
//using Blanche.Persistence;
//hier ga je eigenlijk objecten naar de databank schrijven of uit de databank halen
namespace Blanche.Server.Services.Products;

public class ProductService : IProductService
{
    public Task<int> CreateAsync(ProductDto productDTO)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int productId)
    {
        throw new NotImplementedException();
    }

    public Task EditAsync(ProductDto productDTO)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductDto>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<ProductDto> GetById(int productId)
    {
        throw new NotImplementedException();
    }
}
