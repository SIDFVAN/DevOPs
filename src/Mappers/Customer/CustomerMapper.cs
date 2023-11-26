using Blanche.Shared.Customers;
using Riok.Mapperly.Abstractions;


namespace Blanche.Domain.Customers.Mappers
{
    [Mapper]
    public partial class CustomerMapper
    {
        public partial CustomerDto ToCustomerDto(Customer customer);

        public partial Customer ToCustomer(CustomerDto customerDto);
    }
}
