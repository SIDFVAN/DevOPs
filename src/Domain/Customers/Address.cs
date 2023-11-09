using Ardalis.GuardClauses;
using Blanche.Domain.Common; 
using System.IO;

namespace Blanche.Domain.Customers
{
    public class Address : ValueObject
	{
        public string Street { get; private set; } = string.Empty;
        public string Number { get; private set; } = default!;
        public string PostalCode { get; private set; } = string.Empty;
        public string City { get; private set; } = string.Empty;
        public string Country { get; private set; } = string.Empty;

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Street.ToLower();
            yield return Number;
            yield return PostalCode.ToLower();
            yield return City.ToLower();
            yield return Country.ToLower();
        }

        public override string ToString()
        {
            return $"{Street} {Number},{PostalCode}-{City} {Country}";
        }

        public class AddressBuilder
        {
            private string Street { get; set; }
            private string Number { get; set; }
            private string PostalCode { get; set; }
            private string City { get; set; }
            private string Country { get; set; }

            public AddressBuilder WithStreet(string street)
            {
                Street = street;
                return this;
            }

            public AddressBuilder WithNumber(string number)
            {
                Number = number;
                return this;
            }

            public AddressBuilder WithPostalCode(string postalCode)
            {
                PostalCode = postalCode;
                return this;
            }

            public AddressBuilder WithCity(string city)
            {
                City = city;
                return this;
            }

            public AddressBuilder WithCountry(string country)
            {
                Country = country;
                return this;
            }

            public Address Build()
            {
                return new Address() {
                    Street = Street,
                    Number = Number,
                    PostalCode = PostalCode,
                    City = City,
                    Country = Country
                };

            }
        }

        public static AddressBuilder Builder()
        {
            return new AddressBuilder();
        }
    }
}

