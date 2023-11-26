using Blanche.Shared.Invoice;
using Blanche.Shared.Reservations;
using System.Net.Http.Json;

namespace Blanche.Server.Services.Invoice
{
    public class InvoiceService : IInvoiceService
    { 

         
        public async Task<Stream> CreateInvoicePdf(Guid reservationId)
        {
            throw new NotImplementedException();
        }

        public async Task<InvoiceDto> CreateQuoteForReservation(Guid reservationId)
        {
            throw new NotImplementedException();
        }


    }
}
