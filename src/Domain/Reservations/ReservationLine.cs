using Ardalis.GuardClauses;
using Blanche.Domain.Common;
using Blanche.Domain.Products;

namespace Blanche.Domain.Reservations
{
    public class ReservationLine : Entity
    {
        public Reservation Reservation { get; } = default!;
        public Product Product { get; } = default!;
        public int Quantity { get; } = default!;
        public double Price { get; } = default!;

        private ReservationLine() { }

        public ReservationLine(Reservation reservation, ReservationItem item)
        {
            Reservation = Guard.Against.Null(reservation, nameof(Reservation));
            Guard.Against.Null(item, nameof(ReservationItem));
            Quantity = item.Quantity;
            Product = item.Product;
            Price = Product.Price;
        }
    }
}
