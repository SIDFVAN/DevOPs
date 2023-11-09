using Blanche.Domain.Reservations;
using tests.Fakers.Common;
using tests.Fakers.Formulas;

namespace tests.Fakers.Reservations
{
    public class ReservationFaker : EntityFaker<Reservation>
    {
        public static readonly List<ReservationItem> items = new();

        public ReservationFaker(string locale = "nl") : base(locale)
        {
            CustomInstantiator(f => new Reservation(
                f.Date.Recent(0, DateTime.Today),
                f.Date.Soon(2, DateTime.Today),
                f.Random.Double(0, 300),
                f.Random.Bool(),
                f.Random.Int(40, 50),
                new CustomerFaker(locale),
                new FormulaFaker(locale),
                items
            ));
        }
    }
}

