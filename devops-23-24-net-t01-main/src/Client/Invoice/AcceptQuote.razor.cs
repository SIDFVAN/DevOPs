using Blanche.Domain.Reservations;
using Blanche.Shared.Invoices;
using Blanche.Shared.Reservations;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;

namespace Blanche.Client.Invoice
{


    public partial class AcceptQuote
    {


        [Parameter]
        public string Id { get; set; }

        private bool acceptanceConfirmed = false;


        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] private IReservationService _reservationService { get; set; }
        [Inject] private IInvoiceService _invoiceService { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }

        private ReservationDto reservation = new();

        protected override async Task OnInitializedAsync()
        {

            try
            {
                reservation = await _reservationService.GetReservationById(Guid.Parse(Id));
                if(reservation.State != ReservationState.OFFER_SENT)
                {
                    acceptanceConfirmed = true;
                    throw new Exception("Reservatie is al bevestigd!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Snackbar.Add($"Error: {ex.Message}", Severity.Warning);
                
            }
             

        }

        private async Task HandleAcceptQuote()
        {
            if (reservation != null)
            {
                reservation.State = ReservationState.OFFER_CONFIRMED;

                await _reservationService.UpdateReservationAsync(reservation);

                var invoice = await _invoiceService.CreateInvoiceForReservationAsync(reservation);
                 

                await _invoiceService.CreateInvoicePdf(invoice.Id);

                Snackbar.Add("Offerte is bevestigd", Severity.Success);
                acceptanceConfirmed = true;
            }
        }
    }
}
