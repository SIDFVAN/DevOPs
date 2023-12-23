using Blanche.Domain.Products;
using Blanche.Shared.Products;
using Riok.Mapperly.Abstractions;

namespace Blanche.Mappers.Products
{
    [Mapper]
    public static partial class ProductMapper
    {
        public static partial ProductDto ToProductDto(Product product);

        public static partial Product ToProduct(ProductDto productDto);
    }
}
