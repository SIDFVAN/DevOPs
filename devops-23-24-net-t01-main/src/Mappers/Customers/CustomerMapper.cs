using Blanche.Domain.Customers;
using Blanche.Shared.Customers;
using Riok.Mapperly.Abstractions;


namespace Blanche.Mappers.Customers
{
    [Mapper]
    public static partial class CustomerMapper
    {
        public static partial CustomerDto ToCustomerDto(Customer customer);

        public static partial Customer ToCustomer(CustomerDto customerDto);
    }
}
