using Blanche.Domain.Reservations;
using Bogus;
using tests.Fakers.Products;

namespace tests.Fakers.Reservations
{
    public class ReservationItemFaker : Faker<ReservationItem>
    {
        public ReservationItemFaker(string locale = "nl") : base(locale)
        {
            CustomInstantiator(f => new ReservationItem(
                new ProductFaker(locale),
                f.Random.Int(1, 5)
            ));
        }
    }
}

