using Blanche.Domain.Reservations;
using Blanche.Shared.Formulas;
using Blanche.Shared.Reservations;
using Bogus;
using tests.Fakers.Customer;

namespace tests.Fakers.Reservations
{
    public class ReservationDtoFaker : Faker<ReservationDto>
    {
        public static readonly List<ReservationItemDto> items = new();
        public ReservationDtoFaker(string locale = "nl") : base(locale)
        {
            CustomInstantiator(f => new ReservationDto()
            {
                StartDate = f.Date.Recent(0, DateTime.Today),
                EndDate = f.Date.Soon(2, DateTime.Today),
                TotalPrice = f.Random.Double(0, 300),
                SubTotalPrice = f.Random.Double(0, 300),
                State = ReservationState.OFFER_SENT,
                NumberOfPersons = f.Random.Int(40, 50),
                Customer = new CustomerDtoFaker(locale),
                Formula = new FormulaDto.Index(),
                Items = items
            });
        }
    }
}
