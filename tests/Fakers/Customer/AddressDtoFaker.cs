using Blanche.Shared.Customers;
using Bogus;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests.Fakers.Customer
{
    public class AddressDtoFaker : Faker<AddressDto>
    {
        public AddressDtoFaker(string locale = "nl") : base(locale)
        {
            CustomInstantiator(f => new AddressDto()
            {
                Street = f.Address.StreetName(),
                Number = f.Random.Number(1, 999).ToString(),
                PostalCode = f.Address.ZipCode(),
                City = f.Address.City(),
                Country = f.Address.Country()
            });
        }
    }
}