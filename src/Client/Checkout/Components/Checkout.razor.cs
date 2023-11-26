

using Blanche.Client.Customer.Components; 
using Blanche.Shared.Formulas;
using Blanche.Shared.Products;
using Blanche.Shared.Reservations;
using Microsoft.AspNetCore.Components; 
using Microsoft.JSInterop;
using System.Text.Json;

namespace Blanche.Client.Checkout.Components
{
    public partial class Checkout
    {
        [Inject] private HttpClient HttpClient { get; set; } = default!;
        [Inject] private IJSRuntime js { get; set; } = default!;

        private ReservationDto? reservationDto { get; set; } = new();    
      
        private CustomerDetail customerDetail;

        private bool requestOk;
        private string? ErrorMessage;

        protected override async Task OnInitializedAsync()
        {
                        
            var productsJson = await js.InvokeAsync<string>("localStorage.getItem", "chosen_products");
            var formulaJson = await js.InvokeAsync<string>("localStorage.getItem", "chosen_formula");
             

            if (!string.IsNullOrEmpty(productsJson))
            {
                reservationDto.Items = JsonSerializer.Deserialize<List<ProductDto>>(productsJson);
            }
            else
            { 
                reservationDto.Items = new List<ProductDto>();
            }

            //if (!string.IsNullOrEmpty(formulaJson))
            //{
            //    reservationDto.Formula = JsonSerializer.Deserialize<FormulaDto.Index>(formulaJson);
            //}
            //else
            //{
            //    //ErrorMessage = "Geen formule geselecteerd";
            //}

            //// test code
            //reservationDto.Formula = new FormulaDto.Index { Name = "test", Description = "test", Price = 20.5};
            //reservationDto.Items.Add(new ProductDto() { Price = 2.5, Name = "item_1", Description = "test_1 item_1", Quantity = 10 } );
            //reservationDto.NumberOfPersons = 5;
            //// end test code
             
        }

        public async Task SetReservationAsync()
        {

            //if (customerDetail.CheckCustomerDetails())
            //{
            //    reservationDto.StartDate = DateTime.Now;
            //    reservationDto.EndDate = DateTime.Now;

            //    var response = await HttpClient.PostAsJsonAsync($"api/reservation", reservationDto);

            //    if (response.IsSuccessStatusCode)
            //    {
            //        requestOk = true;
            //        // Deserialize the response into a ReservationDto
            //        reservationDto = await response.Content.ReadFromJsonAsync<ReservationDto>();
            //        await js.InvokeVoidAsync("localStorage.removeItem", "chosen_products");
            //        await js.InvokeVoidAsync("localStorage.removeItem", "chosen_formula");
            //    }
            //    else
            //    {
            //        ErrorMessage = $"Failed to create a reservation. Status code: {response.StatusCode}";
            //    }
            //}


        }

    }
}
