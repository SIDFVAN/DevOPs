using Blanche.Shared.Common;
using Blanche.Shared.Reservations;

namespace Blanche.Shared.Invoices
{
    public class InvoiceDto : EntityDto
    {
        public string InvoiceNumber { get; set; }
        public DateTime ConfirmationDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public bool IsQuote { get; set; }

        public bool IsPayed { get; set; }

        public string Comments { get; set; }

        public ReservationDto Reservation { get; set; }
    }
}
