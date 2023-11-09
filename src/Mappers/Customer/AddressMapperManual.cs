using Blanche.Domain.Customers;
using Blanche.Shared.Customers;


namespace Mappers.Customer
{
    public class AddressMapperManual
    {
        public static AddressDto MapToDto(Address address)
        {
            return AddressDto.Builder()
              .WithStreet(address.Street)
              .WithHouseNumber(address.Number)
              .WithPostalCode(address.PostalCode)
              .WithCity(address.City)
              .WithCountry(address.Country)
              .Build();
        }

        public static Address MapToEntity(AddressDto addressDto)
        {
            return Address.Builder()
              .WithStreet(addressDto.Street)
              .WithNumber(addressDto.HouseNumber)
              .WithPostalCode(addressDto.PostalCode)
              .WithCity(addressDto.City)
              .WithCountry(addressDto.Country)
              .Build();
        }
    }
}
