using Blanche.Shared.Reservations;
using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http.Json;

namespace Blanche.Client.Admin.Reservations
{
     
    public partial class Index
    {
        [Inject] private NavigationManager _navManager { get; set; }
        [Inject] private IReservationService _reservationService { get; set; }
         

        private bool dense = false;
        private bool hover = true;
        private bool striped = false;
        private bool bordered = false;

        private string searchString = "";

        private ReservationDto selectedReservation = null;

        private HashSet<ReservationDto> selectedItems = new HashSet<ReservationDto>();
        private IEnumerable<ReservationDto> Reservations = new List<ReservationDto>();

        protected override async Task OnInitializedAsync()
        {
            Reservations = await _reservationService.GetOpenReservations();
        }

        private void HandleOnRowClick(Guid guid)
        {
            _navManager.NavigateTo($"/reservation/{guid.ToString()}/edit");
        }

        // private void HandleOnRowClick(TableRowClickEventArgs<ReservationDto> args)
        // {
        //     navManager.NavigateTo($"/reservation/{args.Item.Id}");
        // }

        private bool FilterFunction(ReservationDto reservation) => FilterFunc(reservation, searchString);

        private bool FilterFunc(ReservationDto reservation, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;

            if (reservation.Customer.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (reservation.Customer.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (reservation.Customer.Email.Value.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;


            return false;
        }
    }
}
