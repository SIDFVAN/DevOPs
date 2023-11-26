using Blanche.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tests.Fakers.Common;

namespace tests.Fakers.Products
{
    public class BeerFaker : EntityFaker<Beer>
    {
        public BeerFaker(string locale = "nl") : base(locale)
    {
        CustomInstantiator(f => new Beer(
            f.Commerce.ProductName(),
            f.Lorem.Paragraph(),
            f.Random.Double(40, 100)
        ));
    }
}
}
