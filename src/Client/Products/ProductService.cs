using Blanche.Shared.Products;
using System.Net;
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
        public async Task<int> CreateAsync(ProductDto productDTO)
        {
            var response = await client.PostAsJsonAsync(endPoint, productDTO);
            return await response.Content.ReadFromJsonAsync<int>();
        }

        public async Task DeleteAsync(int productId)
        {
            await client.DeleteAsync($"{endPoint}/{productId}");
        }

        public Task EditAsync(ProductDto productDTO)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> GetById(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
