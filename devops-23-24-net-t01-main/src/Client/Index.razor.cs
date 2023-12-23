using Blanche.Shared.Reservations;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;

namespace Blanche.Client
{
    public partial class Index
    {
        private IEnumerable<DateTime> alreadyBookedDates = new List<DateTime>();
        private IEnumerable<DateTime> popularDates = new List<DateTime>();
        private IEnumerable<DateTime> bookedByClientDates = new List<DateTime>();

        [Inject] public IReservationService ReservationService { get; set; } = default!;
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public ISessionStorageService SessionStorage { get; set; } = default!;

        protected override async Task<Task> OnInitializedAsync()
        {
            popularDates = await ReservationService.GetPopularDates();
            alreadyBookedDates = await ReservationService.GetAlreadyBookedDates();
            var result = await SessionStorage.GetItemAsync<ReservationDto>("Reservation");
            if (result != null)
            {
                var customerId = result.Customer.Id;
                bookedByClientDates = await ReservationService.GetBookedByClientDates(customerId);
            }
            return base.OnInitializedAsync();
        }

        private void StartBooking()
        {
            NavigationManager.NavigateTo($"booking");
        }
    }
}
