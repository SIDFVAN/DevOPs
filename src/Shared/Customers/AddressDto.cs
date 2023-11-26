using System.ComponentModel.DataAnnotations;

namespace Blanche.Shared.Customers
{
    public class AddressDto
    {
        [Required(ErrorMessage = "Street is required.")]
        public string Street { get; set; } = "test";

        [Required(ErrorMessage = "House Number is required.")]
        public string Number { get; set; } = "test";

        [Required(ErrorMessage = "Postal Code is required.")]
        public string PostalCode { get; set; } = "test";

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; } = "test";

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; } = "test";

        public AddressDto() { }
       
    }
}