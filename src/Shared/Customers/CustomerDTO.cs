using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;

namespace Blanche.Shared.Customers
{
    public class CustomerDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required]
        [ValidateComplexType]
        public AddressDto CustomerAddress { get; set; } = new AddressDto();


        public class CustomerBuilder
        {
            private string firstName = string.Empty;
            private string lastName = string.Empty;
            private string phoneNumber = string.Empty;
            private string email = string.Empty;
            private AddressDto customerAddress = new AddressDto();

            public CustomerBuilder WithFirstName(string firstName)
            {
                this.firstName = firstName;
                return this;
            }

            public CustomerBuilder WithLastName(string lastName)
            {
                this.lastName = lastName;
                return this;
            }

            public CustomerBuilder WithPhoneNumber(string phoneNumber)
            {
                this.phoneNumber = phoneNumber;
                return this;
            }

            public CustomerBuilder WithEmail(string email)
            {
                this.email = email;
                return this;
            }

            public CustomerBuilder WithCustomerAddress(AddressDto customerAddress)
            {
                this.customerAddress = customerAddress;
                return this;
            }

            public CustomerDto Build()
            {
                return new CustomerDto
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    Email = email,
                    CustomerAddress = customerAddress
                };
            }
        }
        public static CustomerBuilder Builder()
        {
            return new CustomerBuilder();
        }
    }
}
