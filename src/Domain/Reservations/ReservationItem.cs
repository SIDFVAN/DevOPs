using Ardalis.GuardClauses;
using Blanche.Domain.Common;
using Blanche.Domain.Products;

namespace Blanche.Domain.Reservations;

public class ReservationItem : ValueObject
{
    public Product Product { get; } = default!;
    public int Quantity { get; }
    public decimal Price { get; init; }
    public decimal Total => Price * Quantity;

    private ReservationItem() { }

    public ReservationItem(Product product, int quantity, decimal price)
    {
        Product = Guard.Against.Null(product, nameof(Product));
        Quantity = Guard.Against.Negative(quantity, nameof(quantity));
        Price = Guard.Against.Negative(price, nameof(Price));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Product;
    }
}
