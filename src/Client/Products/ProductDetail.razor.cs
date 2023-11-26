using Blanche.Shared.Products;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Blanche.Client.Products
{
    public partial class ProductDetail
    {
        [Parameter] public int ProductId { get; set; }
        private ProductDto Product { get; set; } = new();

        //private ShoppingCart ShoppingCart => ShoppingCartService.ShoppingCart;

        protected override async Task OnInitializedAsync()
        {
            Product = await GetProduct(ProductId);
        }

        async Task<ProductDto> GetProduct(int productId)
        {
            return await HttpClient.GetFromJsonAsync<ProductDto>($"api/products/{productId}");
        }
    }
}
