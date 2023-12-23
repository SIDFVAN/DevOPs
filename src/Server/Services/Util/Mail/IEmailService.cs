
using Blanche.Shared.Invoices;
using Blanche.Shared.Reservations;

namespace Blanche.Server.Services.Util.Mail
{
    public interface IEmailService
    {
        Task SendReservationConfirmationMailAsync(ReservationDto reservationDto);

        Task SendInvoiceMailWithPdf(InvoiceDto invoice, byte[] pdf, string htmlBody, string pdfFileName);

        Task SendReservationPrePayedMailAsync(InvoiceDto invoice);
    }
}


