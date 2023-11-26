using Blanche.Shared.Invoice;
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
        public async Task<Stream> CreateInvoicePdf(Guid reservationId)
        {
            var response = await client.GetByteArrayAsync(endpoint + "/quotation/" + reservationId + "/send");
            return new MemoryStream(response);
        }

        public Task<InvoiceDto> CreateQuoteForReservation(Guid reservationId)
        {
            throw new NotImplementedException();
        }
    }
}
