using Blanche.Domain.Common;

namespace Blanche.Domain.Reservations
{
	public class PopularDate : Entity
	{
		public string Description { get; set; } = default!;
        public DateTime Date { get; set; }

		public PopularDate() { }
	}
}