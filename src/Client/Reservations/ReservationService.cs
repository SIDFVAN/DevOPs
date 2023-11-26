using System.Net.Http.Json;
using Blanche.Domain.Reservations;
using System.Text.Json;
using Blanche.Shared.Reservations;

namespace Blanche.Client.Reservations
{
    public class ReservationService : IReservationService
    {
        private readonly HttpClient client;
        private const string endpoint = "api/Reservation";

        public ReservationService(HttpClient httpClient)
        {
            client = httpClient;
        }
       
        public async Task<Guid> CreateReservationAsync(ReservationDto reservationDto)
        {
            var response = await client.PostAsJsonAsync(endpoint, reservationDto);
            return await response.Content.ReadFromJsonAsync<Guid>();
        }

        public async Task<ReservationDto?> UpdateReservationAsync(ReservationDto reservationDto)
        {
            var response = await client.PutAsJsonAsync(endpoint, reservationDto);
            return await response.Content.ReadFromJsonAsync<ReservationDto>();
        }

        public async Task<List<ReservationDto>> GetReservationsByCustomerId(Guid customerId)
        {
            var response = await client.GetFromJsonAsync<List<ReservationDto>>($"{endpoint}/{customerId}");
            return response!;
        }

        public async Task<PopularDateDto?> AddPopularDate(PopularDateDto popularDateDto)
        {
            var response = await client.PostAsJsonAsync($"{endpoint}/popularDate", popularDateDto);
            return await response.Content.ReadFromJsonAsync<PopularDateDto>();
        }

        public async Task<IEnumerable<DateTime>> GetPopularDates()
        {
            var response = await client.GetFromJsonAsync<List<DateTime>>($"{endpoint}/popularDate");
            var popularDates = response!.Select(p => p.Date);
            return popularDates.ToList();
        }

        public async Task<IEnumerable<DateTime>> GetAlreadyBookedDates()
        {
            var response = await client.GetFromJsonAsync<List<DateTime>>($"{endpoint}/alreadyBookedDates");
            return response!;
        }
        public async Task<IEnumerable<DateTime>> GetBookedByClientDates(Guid customerId)
        {
            var response = await client.GetFromJsonAsync<List<DateTime>>($"{endpoint}/bookedByClient/{customerId}");
            return response!;
        }

        public async Task<List<ReservationDto>> GetOpenReservations()
        {
            var response = await client.GetFromJsonAsync<List<ReservationDto>>($"{endpoint}?open=true");
            return response!;
        }


        public async Task<ReservationDto> GetReservationById(Guid reservationId)
        {
            var response = await client.GetFromJsonAsync<ReservationDto>($"{endpoint}/{reservationId}");
            return response!;
        }
    }
}
