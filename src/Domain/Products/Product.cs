using Ardalis.GuardClauses;
using Blanche.Domain.Common;

namespace Blanche.Domain.Products
{
    public class Product : Entity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public double Price { get; set; } = default!;
        public string ImageUrl { get; set; } = string.Empty;
        public int Quantity { get; set; } = default!;
        public int MinimumUnits { get; set; } = default!;

        public Product(string name, string description, int quantity, double price)
        {
            Name = Guard.Against.NullOrEmpty(name, nameof(Name));
            Description = Guard.Against.NullOrEmpty(description, nameof(Description));
            Quantity = Guard.Against.Zero(quantity, nameof(Quantity));
            Price = Guard.Against.Zero(price, nameof(Price));
        }
    }
}