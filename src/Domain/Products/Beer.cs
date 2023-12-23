using Blanche.Domain.Common;
using Ardalis.GuardClauses;

namespace Blanche.Domain.Products
{
    public class Beer : Entity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public double Price { get; set; }
        
        public Beer() { }

        public Beer(string name, string description, double price) {
            Name= Guard.Against.NullOrEmpty(name, nameof(Name));
            Description = Guard.Against.NullOrEmpty(description, nameof(Description));
            Price = Guard.Against.NegativeOrZero(price, nameof(Price));
        }
    }
}
