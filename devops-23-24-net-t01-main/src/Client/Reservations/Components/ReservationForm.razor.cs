using Blanche.Shared.Reservations;
using Microsoft.AspNetCore.Components;
using Blazored.SessionStorage;

namespace Blanche.Client.Reservations.Components
{
    public partial class ReservationForm
    {
        [Inject] public IReservationService ReservationService { get; set; } = default!;
        [Inject] public ISessionStorageService SessionStorage { get; set; } = default!;
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;

        private bool success;
        protected bool final;
        protected bool sent;
        protected Guid customerId;

        private void ClearForm()
        {
            NavigationManager.NavigateTo($"/availability/{customerId}");
        }
    }
}
