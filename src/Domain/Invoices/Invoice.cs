using Blanche.Domain.Common;
 
namespace Blanche.Domain.Invoices
{
    public class Invoice : Entity
    {
        public string InvoiceNumber { get; set; } = default!;
        public DateTime ConfirmationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsQuote { get; set; }
        public bool IsPayed { get; set; }
        public string Comments { get; set; } = default!;

        public Invoice() { }

        public Invoice(string invoiceNumber, DateTime confirmationDate, DateTime expirationDate, bool isQuote, bool isPayed, string comments)
        {
            InvoiceNumber = invoiceNumber;
            ConfirmationDate = confirmationDate;
            ExpirationDate = expirationDate;
            IsQuote = isQuote;
            IsPayed = isPayed;
            Comments = comments;
        }
    }
}
