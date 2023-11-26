using System;
using Ardalis.GuardClauses;
using Blanche.Domain.Common;

namespace Blanche.Domain.Formulas;

public class Formula : Entity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Days { get; set; }
    public double Price { get; set; }
    public string? ImageUrl { get; set; }
    public bool HasDrinks { get; set; }
    public bool HasFood { get; set; }
    public Dictionary<int, double> PricePerDays { get; set; }
    public double PricePerExtraDay { get; set; }

    public Formula() { }

    public Formula(string name, string description, int days, double price, string imageUrl)
    {
        Name = Guard.Against.NullOrEmpty(name, nameof(Name));
        Description = Guard.Against.NullOrEmpty(description, nameof(Description));
        Days = Guard.Against.Zero(days, nameof(Days));
        Price = Guard.Against.Zero(price, nameof(Price));
        ImageUrl = Guard.Against.Null(imageUrl, nameof (ImageUrl));
    }
}