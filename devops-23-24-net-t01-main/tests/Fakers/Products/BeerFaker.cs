using Blanche.Domain.Formulas;
using Blanche.Domain.Products;
using tests.Fakers.Common;

namespace tests.Fakers.Products
{
    public class BeerFaker : EntityFaker<Beer>
    {
        public BeerFaker(string locale = "nl") : base(locale)
        {

        }
    }
}