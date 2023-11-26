using System.ComponentModel.DataAnnotations;

namespace Blanche.Shared.Customers
{
	public class EmailAddressDto
	{
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Value { get; set; }
		 
	}
}
