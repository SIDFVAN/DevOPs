using Blanche.Shared.Reservations;
using Microsoft.AspNetCore.Components;

namespace Blanche.Client
{
    public partial class Index
    {
        private IEnumerable<DateTime> alreadyBookedDates = new List<DateTime>();
        private IEnumerable<DateTime> popularDates = new List<DateTime>();
        private IEnumerable<DateTime> bookedByClientDates = new List<DateTime>();
        private Guid customerId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa9");

        [Inject] public IReservationService ReservationService { get; set; } = default!;

        protected override async Task<Task> OnInitializedAsync()
        {
            popularDates = await ReservationService.GetPopularDates();
            alreadyBookedDates = await ReservationService.GetAlreadyBookedDates();
            bookedByClientDates = await ReservationService.GetBookedByClientDates(customerId);
            return base.OnInitializedAsync();
        }
    }
}
