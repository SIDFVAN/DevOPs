using Blanche.Shared.Products;

namespace Blanche.Shared.Reservations
{
	public class ReservationItemDto
	{
        public ProductDto? Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; init; }
        public decimal Total => Price * Quantity;
    }
}