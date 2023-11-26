using Blanche.Shared.Products;
using System.Net.Http.Json;

namespace Blanche.Client.Products
{
    public class ProductService : IProductService
    {
        private readonly HttpClient client;
        private const string endPoint = "api/product";

        public ProductService(HttpClient httpClient)
        {
            client = httpClient;
        }

        public async Task<IEnumerable<ProductDto>?> GetAllAsync()
        {
            var response = await client.GetFromJsonAsync<IEnumerable<ProductDto>?>(endPoint);
            return response!.ToList();
        }

        public async Task<ProductDto> GetByIdAsync(Guid productId)
        {
            var response = await client.GetFromJsonAsync<ProductDto>($"{endPoint}/{productId}");
            return response!;
        }

        public async Task DeleteAsync(Guid productId)
        {
            await client.DeleteAsync($"{endPoint}/{productId}");
        }

        public async Task<ProductResult.Saved?> CreateAsync(ProductDto productDto)
        {
            var response = await client.PostAsJsonAsync($"{endPoint}", productDto);
            return await response.Content.ReadFromJsonAsync<ProductResult.Saved?>();
        }

        public async Task<ProductResult.Saved?> EditAsync(ProductDto productDto)
        {
            var response = await client.PutAsJsonAsync($"{endPoint}", productDto);
            return await response.Content.ReadFromJsonAsync<ProductResult.Saved?>();
        }

        public async Task<ProductDto?> EditQuantityInStockAsync(ProductDto productDto)
        {
            var response = await client.PutAsJsonAsync(endPoint, productDto);
            return await response.Content.ReadFromJsonAsync<ProductDto?>();
        }
    }
}
