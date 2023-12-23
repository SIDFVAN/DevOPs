using System.Net.Http.Json;
using Blanche.Domain.Customers;
using Blanche.Shared.Customers;
using Blanche.Shared.Formulas;

namespace Blanche.Client.Formulas
{
	public class FormulaService : IFormulaService
	{
        private readonly HttpClient client;
        private const string endpoint = "api/formula";

        public FormulaService(HttpClient httpClient)
		{
			client = httpClient;
		}

        public async Task<List<FormulaDto.Index>> GetIndexAsync()
        {
            var response = await client.GetFromJsonAsync<List<FormulaDto.Index>>(endpoint);
            return response!.ToList();
        }

        public async Task<FormulaDto.Detail> GetDetailAsync(Guid formulaId)
        {
            var response = await client.GetFromJsonAsync<FormulaDto.Detail>($"{endpoint}/{formulaId}");
            return response!;
        }

        public async Task<Guid> CreateAsync(FormulaDto.Mutate model)
        {
            var response = await client.PostAsJsonAsync(endpoint, model);
            return await response.Content.ReadFromJsonAsync<Guid>();
        }

        public async Task EditAsync(Guid formulaId, FormulaDto.Mutate model)
        {
            var response = await client.PutAsJsonAsync($"{endpoint}/{formulaId}", model);
        }

        public async Task<Guid> DeleteAsync(Guid formulaId)
        {
            await client.DeleteAsync($"{endpoint}/{formulaId}");
           return  Guid.NewGuid(); // test
        }
    }
}

