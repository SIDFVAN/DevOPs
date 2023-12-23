using Blanche.Domain.Reservations;

namespace Blanche.Shared.Reservations
{
    public interface IReservationService
    {
        // Reservation data
        Task<ReservationDto> GetReservationById(Guid reservationId);
        Task<Guid> CreateReservationAsync(ReservationDto reservationDto);
        Task<ReservationDto?> UpdateReservationAsync(ReservationDto reservationDto);
        Task<List<ReservationDto>> GetReservationsByCustomerId(Guid customerId);
        Task<List<ReservationDto>> GetReservationsByState(ReservationState state);

        // Calendar dates
        Task<PopularDateDto?> AddPopularDate(PopularDateDto popularDateDto);
        Task<IEnumerable<DateTime>> GetPopularDates();
        Task<IEnumerable<DateTime>> GetAlreadyBookedDates();
        Task<IEnumerable<DateTime>> GetBookedByClientDates(Guid id);
    }
}
