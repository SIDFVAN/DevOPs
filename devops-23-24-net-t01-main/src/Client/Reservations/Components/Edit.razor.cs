using Blanche.Shared.Reservations;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Blanche.Shared.Invoices;
using Blanche.Domain.Reservations;
using System.IO;
using Microsoft.JSInterop;
using System.Net.Mime;
using Blanche.Shared.Formulas;
using Blanche.Shared.Products;
using Blanche.Client.Admin.Products;
using Blanche.Client.Formulas;
using Blazored.SessionStorage;
using System.Text.Json;

namespace Blanche.Client.Reservations.Components
{
    public partial class Edit
    {
        [Parameter]
        public string reservationId { get; set; }

        [Inject] private IInvoiceService _invoiceService { get; set; }
        [Inject] private IReservationService _reservationService { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }
        [Inject] public IFormulaService FormulaService { get; set; } = default!;

        [Inject] public IBeerService BeerService { get; set; } = default!;


        private ReservationDto Reservation;

        private List<FormulaDto.Index> formulas = new();
        private IEnumerable<BeerDto> beers = new List<BeerDto>();

        private EditContext EditContext;

        protected override async Task OnInitializedAsync()
        {
            Reservation = await _reservationService.GetReservationById(Guid.Parse(reservationId));
  
            if (Reservation != null)
            {
                EditContext = new EditContext(Reservation);
            }


            formulas = await FormulaService.GetIndexAsync();

            beers = await BeerService.GetAllAsync(); 
        }

        private async Task CancelEdit()
        {
            _navigationManager.NavigateTo("/admin/reservations");
        }

        private async Task ConfirmReservation()
        {
            

            await _reservationService.UpdateReservationAsync(Reservation);

            if (Reservation.State == ReservationState.OFFER_SENT)
            {
                 
                var quote = await _invoiceService.CreateQuoteForReservationAsync(Reservation);
                  
                await _invoiceService.CreateQuotePdf(quote.Id);

            }

            else if (Reservation.State == ReservationState.INVOICE_SENT)
            {

                var invoice = await _invoiceService.CreateInvoiceForReservationAsync(Reservation);
 
                await _invoiceService.CreateInvoicePdf(invoice.Id);

            }

            else if (Reservation.State == ReservationState.DEPOSIT_PAYED)
            {

                 Console.WriteLine(Reservation.Id);

                await _invoiceService.SendPaymentConfirmation(Reservation.Id);

            }

            _navigationManager.NavigateTo("/admin/reservations");

        }
    }
}
