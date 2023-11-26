using Blanche.Domain.Customers.Mappers;
using Blanche.Shared.Customers;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using tests.Fakers.Customer;
using Address = Blanche.Domain.Customers.Address;


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
            // Arrange
            AddressMapper mapper = new AddressMapper();

            //  Act
            AddressDto result = mapper.MapToAddressDto(address);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Street, address.Street);
            Assert.AreEqual(result.Number, address.Number);
            Assert.AreEqual(result.City, address.City);
            Assert.AreEqual(result.PostalCode, address.PostalCode);
            Assert.AreEqual(result.Country, address.Country);
        }

        [Theory]
        [MemberData(nameof(AddressDtoTestData))]
        public void ToAddress_ShouldMapCorrectly(AddressDto addressDto)
        {
            // Arrange
            AddressMapper mapper = new AddressMapper();

            // Act
            Address result = mapper.MapToAddress(addressDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(addressDto.Street, result.Street);
            Assert.AreEqual(addressDto.Number, result.Number);
            Assert.AreEqual(addressDto.City, result.City);
            Assert.AreEqual(addressDto.PostalCode, result.PostalCode);
            Assert.AreEqual(addressDto.Country, result.Country);

        }


    }
}
