using Blanche.Shared.Customers;
using Bogus;
using Bogus.DataSets;
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
            CustomInstantiator(f => new CustomerDto()
            {
                FirstName = f.Person.FirstName,
                LastName = f.Person.LastName,
                PhoneNumber = f.Phone.PhoneNumber(),
                CustomerAddress = new AddressDtoFaker(locale),
                Email = new EmailAddressDto() { Value = f.Person.Email}
            });
            
        }
    }
}
