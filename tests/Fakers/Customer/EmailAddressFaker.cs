using Blanche.Domain.Customers;
using Bogus;

namespace tests.Fakers.Customer
{
    public class EmailAddressFaker : Faker<EmailAddress>
    {
        public EmailAddressFaker(string locale = "nl") : base(locale)
        {
            CustomInstantiator(f => new EmailAddress(f.Internet.Email()));
        }
    }
}
