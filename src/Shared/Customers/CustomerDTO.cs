using System;
using System.ComponentModel.DataAnnotations;

namespace Blanche.Shared.Customers
{
    public class CustomerDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Gelieve uw voornaam in te vullen.")]
        public string FirstName { get; set; } = default!;

        [Required(ErrorMessage = "Gelieve uw naam in te vullen.")]
        public string LastName { get; set; } = default!;
         

        [Required(ErrorMessage = "Gelieve uw telefoon- of gsmnummer in te vullen.")]
        public string PhoneNumber { get; set; } = default!;

        [Required]
        [ValidateComplexType]
        public EmailAddressDto Email { get; set; } = new EmailAddressDto();

        [Required]
        [ValidateComplexType]
        public AddressDto CustomerAddress { get; set; } = new AddressDto();

        public string VAT {  get; set; }

        public CustomerDto() { }

    }
}

