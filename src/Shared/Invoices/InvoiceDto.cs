using Blanche.Shared.Common;

namespace Blanche.Shared.Invoice
{
    public class InvoiceDto : EntityDto
    {
        public string InvoiceNumber { get; set; } = "test";
        public DateTime ConfirmationDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public bool IsQuote { get; set; }

        public bool IsPayed { get; set; }

        public string Comments { get; set; } = "test";
    }
}
