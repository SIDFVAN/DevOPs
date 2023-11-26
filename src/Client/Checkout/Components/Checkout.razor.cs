

using Blanche.Client.Customer.Components; 
using Blanche.Shared.Formulas;
using Blanche.Shared.Products;
using Blanche.Shared.Reservations;
using Microsoft.AspNetCore.Components; 
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Text.Json;

namespace Blanche.Client.Checkout.Components
{
    public partial class Checkout
    {
        [Inject] private HttpClient HttpClient { get; set; } = default!;
        [Inject] private IJSRuntime js { get; set; } = default!;

        private ReservationDto? reservationDto = new ReservationDto();
      
        private CustomerDetail customerDetail;

        private bool requestOk;
        private string? ErrorMessage;

        protected override async Task OnInitializedAsync()
        {
             
            var productsJson = await js.InvokeAsync<string>("localStorage.getItem", "chosen_products");
            var formulaJson = await js.InvokeAsync<string>("localStorage.getItem", "chosen_formula");
             

            if (!string.IsNullOrEmpty(productsJson))
            {
                reservationDto.Items = JsonSerializer.Deserialize<List<ReservationItemDto>>(productsJson);
            }
            else
            { 
                reservationDto.Items = new List<ReservationItemDto>();
            }

            if (!string.IsNullOrEmpty(formulaJson))
            {
                reservationDto.Formula = JsonSerializer.Deserialize<FormulaDto>(formulaJson);
            }
            else
            {
                //ErrorMessage = "Geen formule geselecteerd";
            }
                         
            // test code
            reservationDto.Formula = new FormulaDto("test", "test", 20);
            reservationDto.Items.Add(new ReservationItemDto() { Quantity = 10, Product = new ProductDto() { Price = 2.5, Name = "item_1", Description = "test_1 item_1" } });
            reservationDto.NumberOfPersons = 5;
            // end test code
             
        }

        public async Task SetReservationAsync()
        {

            if (customerDetail.CheckCustomerDetails())
            {
                reservationDto.StartDate = DateTime.Now;
                reservationDto.EndDate = DateTime.Now;

                var response = await HttpClient.PostAsJsonAsync($"api/reservation", reservationDto);

                if (response.IsSuccessStatusCode)
                {
                    requestOk = true;
                    // Deserialize the response into a ReservationDto
                    reservationDto = await response.Content.ReadFromJsonAsync<ReservationDto>();
                    await js.InvokeVoidAsync("localStorage.removeItem", "chosen_products");
                    await js.InvokeVoidAsync("localStorage.removeItem", "chosen_formula");
                }
                else
                {
                    ErrorMessage = $"Failed to create a reservation. Status code: {response.StatusCode}";
                }
            }


        }

    }
}
