using Blanche.Shared.Reservations;
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
        private readonly Guid customerId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa9");

        [Inject] public IReservationService ReservationService { get; set; } = default!;

        private string ShowPopularDates(DateTime date)
        {
            var alreadyBooked = alreadyBookedDates.Select(d => d.ToShortDateString());
            var popular = popularDates.Select(p => p.ToShortDateString());
            var bookedByClient = bookedByClientDates.Select(b => b.ToShortDateString());

            return popular.Contains(date.ToShortDateString()) ? "special-day" :
            bookedByClient.Contains(date.ToShortDateString()) ? "client-booked" :
            alreadyBooked.Contains(date.ToShortDateString()) ? "already-booked" : string.Empty;
        }

        protected override async void OnAfterRender(bool firstRender)
        {
            popularDates = await ReservationService.GetPopularDates();
            alreadyBookedDates = await ReservationService.GetAlreadyBookedDates();
            bookedByClientDates = await ReservationService.GetBookedByClientDates(customerId);
            await picker.GoToDate(DateTime.Today);
        }

        private void CurrentDate()
        {
            picker.GoToDate(DateTime.Today);
        }
    }
}
