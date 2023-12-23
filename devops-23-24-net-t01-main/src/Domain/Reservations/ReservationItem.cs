using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using Blanche.Domain.Common;
using Blanche.Domain.Products;

namespace Blanche.Domain.Reservations;

public class ReservationItem : Entity
{
    public Guid ReservationId { get; set; }
    public Guid? ProductId { get; set; }
    public virtual Product? Product { get; set; }
    public int Quantity { get; set; } = default!;
    public double Price { get; set; } = default!;

    public ReservationItem() { }

    public ReservationItem(Product product, int quantity, double price)
    {
        Product = product;
        Quantity = Guard.Against.NegativeOrZero(quantity, nameof(quantity));
        Price = Guard.Against.NegativeOrZero(price, nameof(price));
    }
}
