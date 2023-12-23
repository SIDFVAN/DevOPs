using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Blanche.Shared.Reservations;
using Blazored.SessionStorage;
using System.ComponentModel.DataAnnotations;
using Blanche.Shared.Customers;
using Newtonsoft.Json;
using System.Text.Json;

namespace Blanche.Client.Customer.Components
{
    public partial class CustomerDetail
    {
        [Inject] public IReservationService ReservationService { get; set; } = default!;
        [Inject] public ISessionStorageService SessionStorage { get; set; } = default!;

        private CustomerDto customerDetailsForm = new(); 

        [Parameter]
        public Guid CustomerId { get; set; }
        [Parameter]
        public bool Sent { get; set; }
        [Parameter]
        public EventCallback<Guid> CustomerIdChanged { get; set; }
        [Parameter]
        public EventCallback<bool> SentChanged { get; set; }

        

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                 
                var sessionData = await SessionStorage.GetItemAsync<ReservationDto>("Reservation");
                sessionData.Customer = customerDetailsForm;
                await SessionStorage.SetItemAsync("Reservation", sessionData);
                var result = await SessionStorage.GetItemAsync<ReservationDto>("Reservation");
                  
            }
        }

        protected override async Task OnInitializedAsync()
        {
            var storedResult = await SessionStorage.GetItemAsync<ReservationDto>("Reservation");

            if (storedResult != null)
            {
                customerDetailsForm = storedResult.Customer;
 
            }
        }

        private async Task<Task> OnValidSubmit(EditContext context)
        {
            Sent = true;
            await SentChanged.InvokeAsync(Sent);
            var reservation = await SessionStorage.GetItemAsync<ReservationDto>("Reservation");
            CustomerId = await ReservationService.CreateReservationAsync(reservation);
            await SessionStorage.RemoveItemAsync("Reservation");
            return CustomerIdChanged.InvokeAsync(CustomerId);
        }
    }
}
