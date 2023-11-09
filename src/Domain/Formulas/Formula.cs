using Ardalis.GuardClauses;
using Blanche.Domain.Common;

namespace Blanche.Domain.Formulas;

public class Formula : Entity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Days { get; set; }
    public double Price { get; set; }

    public Formula() { }

    public Formula(string name, string description, int days, double price)
    {
        Name = Guard.Against.NullOrEmpty(name, nameof(Name));
        Description = Guard.Against.NullOrEmpty(description, nameof(Description));
        Days = Guard.Against.Zero(days, nameof(Days));
        Price = Guard.Against.Zero(price, nameof(Price));
    }
}