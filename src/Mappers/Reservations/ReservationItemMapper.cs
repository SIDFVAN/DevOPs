using Blanche.Domain.Reservations;
using Blanche.Shared.Reservations;
using Riok.Mapperly.Abstractions;

namespace Blanche.Mappers.Reservations
{
    [Mapper]
    public static partial class ReservationItemMapper
	{
        public static partial ReservationItemDto ReservationItemToReservationItemDto(ReservationItem reservationItem);

        public static partial ReservationItem ReservationItemDtoToReservationItem(ReservationItemDto reservationItemDto);
    }
}