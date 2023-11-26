using Blanche.Domain.Common;
using Blanche.Domain.Customers;
using Bogus; 

namespace tests.Fakers.Customer
{
    public class AddressFaker : Faker<Address>
    {
        public AddressFaker(string locale = "nl") : base(locale)
        {
            CustomInstantiator(f => Address.Builder()
            .WithStreet(f.Address.StreetName())
            .WithNumber(f.Random.Number(0, 999).ToString())
            .WithPostalCode(f.Address.ZipCode())
            .WithCity(f.Address.CityPrefix())
            .WithCountry(f.Address.Country())
            .Build());

        }
    }

}
