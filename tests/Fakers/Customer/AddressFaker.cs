using Blanche.Domain.Common;
using Blanche.Domain.Customers;
using Bogus; 

namespace tests.Fakers.Customer
{
    public class AddressFaker : Faker<Address>
    {
        public AddressFaker(string locale = "nl") : base(locale)
        {
            CustomInstantiator(f => new Address()
            {
                Street = f.Address.StreetName(),
                Number = f.Random.Number(0, 999).ToString(),
                PostalCode = f.Address.ZipCode(),
                City = f.Address.CityPrefix(),
                Country = f.Address.Country()
            });
              
        }
    }

}
