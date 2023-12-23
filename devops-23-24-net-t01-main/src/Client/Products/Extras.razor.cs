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
                var reservationItem = new ReservationItemDto
                {
                    Id = Guid.NewGuid(),
                    ProductId = item.Id,
                    Product = item,
                    Price = item.Price,
                    Quantity = item.QuantityOrdered
                };
                item.QuantityInStock -= item.QuantityOrdered;
                result.Items?.Add(reservationItem);
                await SessionStorage.SetItemAsync("Reservation", result);
                item.IsAdded = true;
                StateHasChanged();
            }
        }

        public async void DeleteProductFormReservation(ProductDto item)
        {
            item.QuantityInStock += item.QuantityOrdered;
            var result = await SessionStorage.GetItemAsync<ReservationDto>("Reservation");
            result.Items?.RemoveAll(i => i.Product.Name == item.Name);
            item.QuantityOrdered = 0;
            await SessionStorage.SetItemAsync("Reservation", result);
            item.IsAdded = false;
            StateHasChanged();
        }
    }
}
