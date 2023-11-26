using Blanche.Shared.Products;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Blanche.Shared.Reservations;

namespace Blanche.Client.Products
{
    public partial  class Extras
    {
        private IEnumerable<ProductDto> ProductList = new List<ProductDto>();
        private bool isOpen;

        [Inject] public IProductService ProductService { get; set; } = default!;
        [Inject] public ISessionStorageService SessionStorage { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            var response = await ProductService.GetAllAsync();
            ProductList = response!;
        }

        private async void AddProductToReservation(ProductDto item)
        {
            var result = await SessionStorage.GetItemAsync<ReservationDto>("Reservation");
            if (result != null && item.QuantityOrdered != 0)
            {
                item.QuantityInStock -= item.QuantityOrdered;
                result.Items?.Add(item);
                await SessionStorage.SetItemAsync("Reservation", result);
                item.IsAdded = true;
                //await ProductService.EditQuantityInStockAsync(item);
                StateHasChanged();
            }
        }

        public async void DeleteProductFormReservation(ProductDto item)
        {
            item.QuantityInStock += item.QuantityOrdered;
            //await ProductService.EditQuantityInStockAsync(item);
            var result = await SessionStorage.GetItemAsync<ReservationDto>("Reservation");
            result.Items?.RemoveAll(i => i.Name == item.Name);
            item.QuantityOrdered = 0;
            await SessionStorage.SetItemAsync("Reservation", result);
            item.IsAdded = false;
            StateHasChanged();
        }
    }
}
