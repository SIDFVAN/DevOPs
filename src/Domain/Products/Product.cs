using Ardalis.GuardClauses;
using Blanche.Domain.Common;

namespace Blanche.Domain.Products
{
    public class Product : Entity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        //public string Category { get; set; } = default!;
        public double Price { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public int QuantityInStock { get; set; } = default!;
        public int MinimumUnits { get; set; } = default!;

        public Product() { }

        public Product(string name, string description, int quantity, double price, string imageUrl, int minimumUnits)
        {
            Name = Guard.Against.NullOrWhiteSpace(name, nameof(Name));
            Description = Guard.Against.NullOrWhiteSpace(description, nameof(Description));
            QuantityInStock = Guard.Against.Negative(quantity, nameof(QuantityInStock));
            Price = Guard.Against.NegativeOrZero(price, nameof(Price));
            ImageUrl = Guard.Against.NullOrWhiteSpace(imageUrl, nameof(ImageUrl));
            MinimumUnits = Guard.Against.NegativeOrZero(minimumUnits, nameof(MinimumUnits));
        }
    }
}