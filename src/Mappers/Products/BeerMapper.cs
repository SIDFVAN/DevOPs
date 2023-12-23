using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blanche.Domain.Products;
using Blanche.Shared.Products;
using Riok.Mapperly.Abstractions;

namespace Blanche.Mappers.Products
{
    [Mapper]
    public static partial class BeerMapper
    {
        public static partial BeerDto ToBeerDto(Beer beer);
        public static partial Beer ToBeer(BeerDto beerDto);
    }
}
