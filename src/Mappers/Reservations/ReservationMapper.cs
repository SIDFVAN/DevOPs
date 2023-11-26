using Blanche.Domain.Customers;
using Blanche.Domain.Reservations;
using Blanche.Shared.Customers;
using Blanche.Shared.Reservations;
using Riok.Mapperly.Abstractions;

namespace Blanche.Mappers.Reservations
{
    [Mapper]
    public static partial class ReservationMapper
    {
        public static partial ReservationDto ReservationToReservationDto(Reservation reservation);

        //[MapProperty("FormulaName", "Formula.Name")]
        //public static partial Reservation ReservationDtoToReservation(ReservationDto reservationDto);

        public static partial Reservation ReservationDtoToReservation(ReservationDto reservationDto);
    }
}