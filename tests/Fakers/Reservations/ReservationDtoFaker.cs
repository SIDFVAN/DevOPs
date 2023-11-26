using Blanche.Shared.Formulas;
using Blanche.Shared.Invoice;
using Blanche.Shared.Products;
using Blanche.Shared.Reservations;
using Bogus;
using tests.Fakers.Customer;

namespace tests.Fakers.Reservations
{
    public class ReservationDtoFaker : Faker<ReservationDto>
    {
        public static readonly List<ProductDto> items = new();
        public ReservationDtoFaker(string locale = "nl") : base(locale)
        {
            CustomInstantiator(f => new ReservationDto()
            {
                StartDate = f.Date.Recent(0, DateTime.Today),
                EndDate = f.Date.Soon(2, DateTime.Today),
                TotalPrice = f.Random.Double(0, 300),
                SubTotalPrice = f.Random.Double(0, 300),
                IsConfirmed = f.Random.Bool(),
                NumberOfPersons = f.Random.Int(40, 50),
                Customer = new CustomerDtoFaker(locale),
                Invoice = new InvoiceDto(),
                Formula = new FormulaDto.Index(),
                Items = items
            });
        }
    }
}
