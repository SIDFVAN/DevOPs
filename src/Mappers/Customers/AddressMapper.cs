using Blanche.Shared.Customers;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Blanche.Domain.Customers.Mappers
{
    [Mapper]
    public partial class AddressMapper
    {
        public partial Address MapToAddress(AddressDto addressDto);

        public partial AddressDto MapToAddressDto(Address address);
        
    }
}
