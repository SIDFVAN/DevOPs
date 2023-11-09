using Blanche.Shared.Customers;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tests.Fakers.Common;

namespace tests.Fakers.Customer
{
    public class CustomerDtoFaker : Faker<CustomerDto>
    {
        public CustomerDtoFaker(string locale = "nl") : base(locale)
        {
            CustomInstantiator(f => CustomerDto.Builder()
            .WithFirstName(f.Person.FirstName)
            .WithLastName(f.Person.LastName)
            .WithPhoneNumber(f.Phone.PhoneNumber())
            .WithCustomerAddress(new AddressDtoFaker(locale))
            .WithEmail(f.Person.Email)
            .Build());

        }
    }
}
