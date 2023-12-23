using Blanche.Shared.Reservations;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blanche.Client.Reservations.Components
{
    public partial class Calendar
    {
        private MudDatePicker picker = default!;
        private readonly DateTime? date = DateTime.Today;
        private IEnumerable<DateTime> alreadyBookedDates = new List<DateTime>();
        private IEnumerable<DateTime> popularDates = new List<DateTime>();
        private IEnumerable<DateTime> bookedByClientDates = new List<DateTime>();

        [Inject] public IReservationService ReservationService { get; set; } = default!;
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public ISessionStorageService SessionStorage { get; set; } = default!;

        [Parameter]
        public Guid CustomerId { get; set; }
        [Parameter]
        public EventCallback<Guid> CustomerIdChanged { get; set; }

        private string ShowPopularDates(DateTime date)
        {
            var alreadyBooked = alreadyBookedDates.Select(d => d.ToShortDateString());
            var popular = popularDates.Select(p => p.ToShortDateString());
            var bookedByClient = bookedByClientDates.Select(b => b.ToShortDateString());

            return bookedByClient.Contains(date.ToShortDateString()) && date > DateTime.Now ? "client-booked" :
            alreadyBooked.Contains(date.ToShortDateString()) && date > DateTime.Now ? "already-booked" :
            popular.Contains(date.ToShortDateString()) && date > DateTime.Now ? "special-day" : string.Empty;
        }

        private bool DisableBookedDates(DateTime date)
        {
            var alreadyBooked = alreadyBookedDates.Select(d => d.ToShortDateString());
            return date < DateTime.Today || alreadyBooked.Contains(date.ToShortDateString());
        }

        protected override async void OnAfterRender(bool firstRender)
        {
            popularDates = await ReservationService.GetPopularDates();
            alreadyBookedDates = await ReservationService.GetAlreadyBookedDates();
            if (CustomerId != null)
            {
                var customerId = CustomerId;
                Console.WriteLine(customerId);
                bookedByClientDates = await ReservationService.GetBookedByClientDates(customerId);
            }
            await picker.GoToDate(DateTime.Today);
            SessionStorage.ClearAsync();
        }

        private void StartBooking()
        {
            NavigationManager.NavigateTo($"booking");
        }
    }
}
