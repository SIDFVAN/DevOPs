using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using Blanche.Domain.Common;
using Blanche.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Blanche.Domain.Reservations;

public class ReservationItem : ValueObject
{
    [NotMapped]
    public Product Product { get; } = default!;
    public int Quantity { get; }

    private ReservationItem() { }

    public ReservationItem(Product product, int quantity)
    {
        Product = Guard.Against.Null(product, nameof(Product));
        Quantity = Guard.Against.Negative(quantity, nameof(quantity));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Product;
    }
}
