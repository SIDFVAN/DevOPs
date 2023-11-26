using Blanche.Shared.Products;
using System.Net.Http.Json;

namespace Blanche.Client.Admin.Products
{
    public class BeerService : IBeerService
    {
        private const string Endpoint = "api/beer";
        private readonly HttpClient httpClient;
        public BeerService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<BeerDto> GetByIdAsync(Guid id)
        {
            var response = await httpClient.GetFromJsonAsync<BeerDto>($"{Endpoint}/{id}");
            return response!;
        }

        public async Task<IEnumerable<BeerDto>> GetAllAsync()
        {
            var response = await httpClient.GetFromJsonAsync<IEnumerable<BeerDto>>(Endpoint);
            return response!;
        }

        public async Task<Guid> CreateAsync(BeerDto beerDto)
        {
            var response = await httpClient.PostAsJsonAsync($"{Endpoint}", beerDto);
            return await response.Content.ReadFromJsonAsync<Guid>();
        }

        public async Task UpdateAsync(BeerDto beerDto)
        {
            await httpClient.PutAsJsonAsync($"{Endpoint}", beerDto);
        }

        public async Task DeleteAsync(Guid id)
        {
            await httpClient.DeleteAsync($"{Endpoint}/{id}");
        }
    }
}
