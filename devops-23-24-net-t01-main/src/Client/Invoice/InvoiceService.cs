using Blanche.Shared.Invoices;
using Blanche.Shared.Reservations;
using System.Net.Http.Json;

namespace Blanche.Client.Invoice
{
    public class InvoiceService : IInvoiceService
    {
        private readonly HttpClient client;
        private const string endpoint = "api/invoice";

        public InvoiceService(HttpClient httpClient)
        {
            client = httpClient;
        }
        public async Task CreateQuotePdf(Guid quoteId)
        {
            await client.GetAsync($"{endpoint}/quote/{quoteId}/send");
        }

        public async Task CreateInvoicePdf(Guid invoiceId)
        {
            await client.GetAsync($"{endpoint}/{invoiceId}/send");
        }

        public async Task<InvoiceDto> CreateQuoteForReservationAsync(ReservationDto reservationDto)
        {
            var response = await client.PostAsJsonAsync(endpoint + "/quote", reservationDto);
            Console.WriteLine(response.ToString());
            return await response.Content.ReadFromJsonAsync<InvoiceDto>();
        }

        public async Task<InvoiceDto> CreateInvoiceForReservationAsync(ReservationDto reservationDto)
        {
            var response = await client.PostAsJsonAsync(endpoint, reservationDto);
            Console.WriteLine(response.ToString());
            return await response.Content.ReadFromJsonAsync<InvoiceDto>();
        }

        public Task<InvoiceDto> GetInvoiceById(Guid invoiceId)
        {
            throw new NotImplementedException();
        }

        public async Task SendPaymentConfirmation(Guid reservationId)
        {
            await client.PostAsJsonAsync<InvoiceDto>($"{endpoint}/{reservationId}/prepayed", new InvoiceDto());
        }

        public Task<InvoiceDto> GetInvoiceByReservationId(Guid reservationId)
        {
            throw new NotImplementedException();
        }
    }
}
