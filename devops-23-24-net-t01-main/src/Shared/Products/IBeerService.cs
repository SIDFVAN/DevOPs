using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blanche.Shared.Products
{
    public interface IBeerService
    {
        Task<BeerDto> GetByIdAsync(Guid id);
        Task<IEnumerable<BeerDto>> GetAllAsync();
        Task<Guid> CreateAsync(BeerDto beerDto);
        Task UpdateAsync(BeerDto beerDto);
        Task DeleteAsync(Guid id);
    }
}
