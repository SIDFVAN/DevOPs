using Ardalis.GuardClauses;
using Blanche.Domain.Common; 
using System.IO;

namespace Blanche.Domain.Customers
{
    public class Address : ValueObject
	{
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = default!;
        public string PostalCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        
        public Address() { }    

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
         
    }
}

