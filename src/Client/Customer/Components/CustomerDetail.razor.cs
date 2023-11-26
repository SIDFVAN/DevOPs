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

        private CustomerDetailsForm customerDetailsForm = new();
        private Guid customerId = Guid.NewGuid();

        [Parameter]
        public Guid CustomerId { get; set; }
        [Parameter]
        public bool Sent { get; set; }
        [Parameter]
        public EventCallback<Guid> CustomerIdChanged { get; set; }
        [Parameter]
        public EventCallback<bool> SentChanged { get; set; }

        public class CustomerDetailsForm
        {
            [Required(ErrorMessage = "Gelieve uw voornaam in te vullen.")]
            public string FirstName { get; set; } = default!;

            [Required(ErrorMessage = "Gelieve uw naam in te vullen.")]
            public string Name { get; set; } = default!;

            [Required(ErrorMessage = "Gelieve uw email in te vullen.")]
            [EmailAddress]
            public string Email { get; set; } = default!;

            [Required(ErrorMessage = "Gelieve uw telefoon- of gsmnummer in te vullen.")]
            public string PhoneNumber { get; set; } = default!;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                var customer = new CustomerDto
                {
                    Id = customerId,
                    FirstName = customerDetailsForm.FirstName,
                    LastName = customerDetailsForm.Name,
                    Email = new EmailAddressDto { Value = customerDetailsForm.Email },
                    PhoneNumber = customerDetailsForm.PhoneNumber
                };
                var sessionData = await SessionStorage.GetItemAsync<ReservationDto>("Reservation");
                sessionData.Customer = customer;
                await SessionStorage.SetItemAsync("Reservation", sessionData);
                var result = await SessionStorage.GetItemAsync<ReservationDto>("Reservation");
                customerDetailsForm.FirstName = result.Customer.FirstName;
                customerDetailsForm.Name = result.Customer.LastName;
                customerDetailsForm.Email = result.Customer.Email.Value;
                customerDetailsForm.PhoneNumber = result.Customer.PhoneNumber;
            }
        }

        protected override async Task OnInitializedAsync()
        {
            var storedResult = await SessionStorage.GetItemAsync<ReservationDto>("Reservation");

            if (storedResult != null)
            {
                customerDetailsForm.FirstName = storedResult.Customer.FirstName;
                customerDetailsForm.Name = storedResult.Customer.LastName;
                customerDetailsForm.Email = storedResult.Customer.Email.Value;
                customerDetailsForm.PhoneNumber = storedResult.Customer.PhoneNumber;
            }
        }

        private async Task<Task> OnValidSubmit(EditContext context)
        {
            var reservation = await SessionStorage.GetItemAsync<ReservationDto>("Reservation");
            CustomerId = await ReservationService.CreateReservationAsync(reservation);
            await CustomerIdChanged.InvokeAsync(CustomerId);
            Sent = true;
            return SentChanged.InvokeAsync(Sent);
        }
    }
}
