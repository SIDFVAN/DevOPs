using Blanche.Domain.Customers.Mappers;
using Blanche.Domain.Customers; 
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using tests.Fakers.Customer;
using Blanche.Shared.Customers;
using Mappers.Customer;

namespace tests.Mappers
{

    public class CustomerMapperTests
    {

        public static IEnumerable<object[]> CustomerTestData()
        {

            yield return new object[] {
                new CustomerFaker().Generate()};

        }

        public static IEnumerable<object[]> CustomerDtoTestData()
        {

            yield return new object[] {
                new CustomerDtoFaker().Generate()};

        }

        [Theory]
        [MemberData(nameof(CustomerTestData))]
        public void ToCustomerDto_ShouldMapCorrectly(Customer customer)
        {
            
            // Arrange and Act
            CustomerDto result = CustomerMapperManual.MapToDto(customer);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.FirstName, customer.FirstName);
            Assert.AreEqual(result.LastName, customer.LastName);
            Assert.AreEqual(result.PhoneNumber, customer.PhoneNumber);
            Assert.AreEqual(result.Email, customer.Email.Value);
        }

        [Theory]
        [MemberData(nameof(CustomerDtoTestData))]
        public void ToCustomer_ShouldMapCorrectly(CustomerDto customerDto)
        { 
            // Arrange and Act
            Customer result = CustomerMapperManual.MapToEntity(customerDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.FirstName, customerDto.FirstName); 
            Assert.AreEqual(result.LastName, customerDto.LastName);
            Assert.AreEqual(result.PhoneNumber, customerDto.PhoneNumber);
            Assert.AreEqual(result.Email.Value.ToString(), customerDto.Email.ToString());
        }
 
         
    }
}
