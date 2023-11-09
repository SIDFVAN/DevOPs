using Blanche.Shared.Customers;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests.Fakers.Customer
{
    public class AddressDtoFaker : Faker<AddressDto>
    {
        public AddressDtoFaker(string locale = "nl") : base(locale)
        {
            CustomInstantiator(f => AddressDto.Builder()
            .WithStreet(f.Address.StreetName())
            .WithHouseNumber(f.Random.Number(1, 999).ToString())
            .WithPostalCode(f.Address.ZipCode())
            .WithCity(f.Address.City())
            .WithCountry(f.Address.Country())
            .Build());

        }
    }
}
