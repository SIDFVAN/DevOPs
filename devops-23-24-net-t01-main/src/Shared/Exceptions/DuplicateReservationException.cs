namespace Blanche.Shared.Exceptions
{
	public class DuplicateReservationException : Exception
	{
		public DuplicateReservationException()
			: base("Er bestaat al een boeking voor deze periode.")
		{
		}
	}
}