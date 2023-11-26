using Blanche.Domain.Products;
using tests.Fakers.Common;

namespace tests.Fakers.Products
{
    public class ProductFaker : EntityFaker<Product>
    {
        public ProductFaker(string locale = "nl") : base(locale)
        {
            CustomInstantiator(f => new Product(
                f.Commerce.ProductName(),
                f.Lorem.Paragraph(),
                f.Random.Int(1, 10),
                f.Random.Double(50, 200)
            ));
        }
    }
}

