namespace Blanche.Shared.Exceptions
{
	public class EntityNotFoundException : Exception
	{
		public EntityNotFoundException()
            : base("De boeking kan niet geveonden worden.")
        {
		}
	}
}

