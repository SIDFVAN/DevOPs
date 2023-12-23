using Blanche.Domain.Products;
using Blanche.Domain.Reservations;
using tests.Fakers.Common;
using tests.Fakers.Formulas;
using tests.Fakers.Products;

namespace tests.Fakers.Reservations
{
    public class ReservationFaker : EntityFaker<Reservation>
    {
        public static readonly IEnumerable<ReservationItem> items = new List<ReservationItem>();

        public ReservationFaker(string locale = "nl") : base(locale)
        {
            CustomInstantiator(f => new Reservation(
                f.Date.Recent(0, DateTime.Today),
                f.Date.Soon(2, DateTime.Today),
                f.Random.Double(0, 300),
                ReservationState.OFFER_SENT,
                f.Random.Int(40, 50),
                new CustomerFaker(locale),
                new FormulaFaker(locale), 
                new BeerFaker(locale),
                items
            ));
        }
    }
}

