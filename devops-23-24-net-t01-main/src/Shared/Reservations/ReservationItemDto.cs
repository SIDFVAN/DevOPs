using Blanche.Shared.Common;
using Blanche.Shared.Products;

namespace Blanche.Shared.Reservations
{
	public class ReservationItemDto : EntityDto
	{
        public Guid ReservationId { get; set; }
        public Guid? ProductId { get; set; }
        public virtual ProductDto? Product { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public ReservationItemDto() { }
    }
}