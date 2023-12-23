using Blanche.Shared.Products;

namespace Blanche.Client.Reservations
{
	public class ReservationItem
	{
        public ProductDto? Product { get; init; }
        public int? Quantity { get; init; }
        public double? Price { get; init; }
        public double? Total => Price * Quantity;

		public ReservationItem() { }

        public ReservationItem(ProductDto productDto, int quantity, double price)
		{
			Product = productDto;
			Quantity = quantity;
			Price = price;
		}
	}
}
