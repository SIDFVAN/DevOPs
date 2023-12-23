

using Blanche.Shared.Reservations;

namespace Blanche.Shared.Invoices
{
    public interface IInvoiceService
    {
        Task<InvoiceDto> CreateQuoteForReservationAsync(ReservationDto reservationDto);
        Task<InvoiceDto> CreateInvoiceForReservationAsync(ReservationDto reservationDto);

        Task CreateInvoicePdf(Guid invoiceId);
        Task CreateQuotePdf(Guid quoteId);

        Task<InvoiceDto> GetInvoiceById(Guid invoiceId);

        Task SendPaymentConfirmation(Guid reservationId);

        Task<InvoiceDto> GetInvoiceByReservationId(Guid reservationId);
    }
}
