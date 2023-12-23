using tests.Fakers.Common;
using Blanche.Domain.Customers;
using tests.Fakers.Customer; 

public class CustomerFaker : EntityFaker<Customer>
{
    public CustomerFaker(string locale = "nl") : base(locale)
    {
        CustomInstantiator(f => new Customer(
            f.Person.FirstName,
            f.Person.LastName,
            f.Person.Phone,
            new AddressFaker(locale),
            new EmailAddressFaker(locale)
        ));  
    }
}
