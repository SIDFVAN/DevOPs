using System.ComponentModel.DataAnnotations;

namespace Blanche.Shared.Customers
{
	public class EmailAddressDto
	{
        [Required(ErrorMessage = "Email is verplicht!")]
        [EmailAddress(ErrorMessage = "Geen geldig email adres!")]
        public string? Value { get; set; }
		 
	}
}
