using System;
using Blanche.Shared.Formulas;

namespace Blanche.Shared.Reservations
{
	public interface IReservationItemService
	{
        Task<Guid> CreateAsync(ReservationItemDto reservationItemDto);
        Task<Guid> DeleteAsync(Guid reservationItemId);
    }
}

