using Blanche.Shared.Customers;
using Blanche.Domain.Customers; 

namespace Mappers.Customer
{
    public class CustomerMapperManual

    {
        public static CustomerDto MapToDto(Blanche.Domain.Customers.Customer customer)
        {
            return CustomerDto.Builder()
              .WithFirstName(customer.FirstName)
              .WithLastName(customer.LastName)
              .WithPhoneNumber(customer.PhoneNumber)
              .WithEmail(customer.Email.Value)
              .WithCustomerAddress(AddressMapperManual.MapToDto(customer.CustomerAddress))
              .Build();

        }

        public static Blanche.Domain.Customers.Customer MapToEntity(CustomerDto customerDto)
        {
            return Blanche.Domain.Customers.Customer.Builder()
                .WithFirstName(customerDto.FirstName)
                .WithLastName(customerDto.LastName)
                .WithPhoneNumber(customerDto.PhoneNumber)
                .WithEmail(new EmailAddress(customerDto.Email))
                .WithAddress(AddressMapperManual.MapToEntity(customerDto.CustomerAddress))
                .Build();
        }
    }
}
