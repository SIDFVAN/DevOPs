

namespace Blanche.Shared.Invoice
{
    public interface IInvoiceService
    {
        Task<InvoiceDto> CreateQuoteForReservation(Guid reservationId);
        
        Task<Stream> CreateInvoicePdf(Guid reservationId);
    }
}
