using Blanche.Domain.Reservations;
using Blanche.Shared.Reservations;
using Riok.Mapperly.Abstractions;

namespace Blanche.Mappers.Reservations
{
    [Mapper]
    public static partial class ReservationMapper
    {
        public static partial ReservationDto ReservationToReservationDto(Reservation reservation);

        public static partial Reservation ReservationDtoToReservation(ReservationDto reservationDto);
    }
}