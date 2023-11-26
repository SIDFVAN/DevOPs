using System;
using System.ComponentModel.DataAnnotations;

namespace Blanche.Shared.Customers
{
    public class CustomerDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        public string? PhoneNumber { get; set; }

        [Required]
        [ValidateComplexType]
        public EmailAddressDto Email { get; set; } = new EmailAddressDto();

        [Required]
        [ValidateComplexType]
        public AddressDto CustomerAddress { get; set; } = new AddressDto();

        public CustomerDto() { }

    }
}

