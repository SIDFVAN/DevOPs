using Blanche.Domain.Customers.Mappers;
using Blanche.Shared.Customers;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using tests.Fakers.Customer;
using Address = Blanche.Domain.Customers.Address;
using Mappers.Customer;

namespace tests.Mappers
{
    public class AddressMapperTest
    {
        public static IEnumerable<object[]> AddressTestData()
        {

            yield return new object[] {
                new AddressFaker().Generate()};

        }

        public static IEnumerable<object[]> AddressDtoTestData()
        {

            yield return new object[] {
                new AddressDtoFaker().Generate()};

        }

        [Theory]
        [MemberData(nameof(AddressTestData))]
        public void ToAddressDto_ShouldMapCorrectly(Address address)
        {
            //  Act
            AddressDto result = AddressMapperManual.MapToDto(address);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Street, address.Street);
            Assert.AreEqual(result.HouseNumber, address.Number);
            Assert.AreEqual(result.City, address.City);
            Assert.AreEqual(result.PostalCode, address.PostalCode);
            Assert.AreEqual(result.Country, address.Country);
        }

        [Theory]
        [MemberData(nameof(AddressDtoTestData))]
        public void ToAddress_ShouldMapCorrectly(AddressDto addressDto)
        { 
            // Act
            Address result = AddressMapperManual.MapToEntity(addressDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Street, addressDto.Street);
            Assert.AreEqual(result.Number, addressDto.HouseNumber);
            Assert.AreEqual(result.City, addressDto.City);
            Assert.AreEqual(result.PostalCode, addressDto.PostalCode);
            Assert.AreEqual(result.Country, addressDto.Country);
        }


    }
}
