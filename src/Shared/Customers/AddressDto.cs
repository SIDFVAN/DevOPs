using System.ComponentModel.DataAnnotations;

namespace Blanche.Shared.Customers
{
    public class AddressDto
    {
        [Required(ErrorMessage = "Street is required.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "House Number is required.")]
        public string HouseNumber { get; set; }

        [Required(ErrorMessage = "Postal Code is required.")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }


        public class AddressDtoBuilder
        {
            private string? street;
            private string? houseNumber;
            private string? postalCode;
            private string? city;
            private string? country;

            public AddressDtoBuilder WithStreet(string? street)
            {
                this.street = street;
                return this;
            }

            public AddressDtoBuilder WithHouseNumber(string? houseNumber)
            {
                this.houseNumber = houseNumber;
                return this;
            }

            public AddressDtoBuilder WithPostalCode(string? postalCode)
            {
                this.postalCode = postalCode;
                return this;
            }

            public AddressDtoBuilder WithCity(string? city)
            {
                this.city = city;
                return this;
            }

            public AddressDtoBuilder WithCountry(string? country)
            {
                this.country = country;
                return this;
            }

            public AddressDto Build()
            {
                return new AddressDto
                {
                    Street = street,
                    HouseNumber = houseNumber,
                    PostalCode = postalCode,
                    City = city,
                    Country = country
                };
            }
        }
        public static AddressDtoBuilder Builder()
        {
            return new AddressDtoBuilder();
        }
    }
}