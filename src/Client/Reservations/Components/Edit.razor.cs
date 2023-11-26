using Blanche.Shared.Reservations;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Blanche.Shared.Invoice;

namespace Blanche.Client.Reservations.Components
{
    public partial class Edit
    {
        [Parameter]
        public string reservationId { get; set; }

        [Inject] private IInvoiceService _invoiceService { get; set; }
        [Inject] private IReservationService _reservationService { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }


        private ReservationDto Reservation;
        private EditContext EditContext;

        protected override async Task OnInitializedAsync()
        {
            Reservation = await _reservationService.GetReservationById(Guid.Parse(reservationId));

            if (Reservation != null)
            {
                EditContext = new EditContext(Reservation);
            }
        }

        private async Task ConfirmReservation()
        {
            Reservation.IsConfirmed = true;
            await _reservationService.UpdateReservationAsync(Reservation);

            await _invoiceService.CreateQuoteForReservation(Reservation.Id);
            await _invoiceService.CreateInvoicePdf(Reservation.Id);
             
            // Navigate back to the reservation list page
            _navigationManager.NavigateTo("/admin/reservations");
        }
    }
}
