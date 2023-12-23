using System.ComponentModel.DataAnnotations;

namespace Blanche.Shared.Customers
{
    public class AddressDto
    {
        [Required(ErrorMessage = "Street is required.")]
        public string Street { get; set; } = default!;

        [Required(ErrorMessage = "House Number is required.")]
        public string Number { get; set; } = default!;

        [Required(ErrorMessage = "Postal Code is required.")]
        public string PostalCode { get; set; } = default!;

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; } = default!;

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; } = default!;

        public AddressDto() { }
       
    }
}