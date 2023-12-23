using Blanche.Domain.Common;
using Blanche.Domain.Reservations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Blanche.Domain.Invoices
{
    public class Invoice : Entity
    {
        public string InvoiceNumber { get; set; } = default!;
        public DateTime ConfirmationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsQuote { get; set; }
        public bool IsPayed { get; set; }

        public string Comments { get; set; }

        [NotMapped]
        [JsonIgnore]
        public Reservation Reservation { get; set; }

        public Guid ReservationId { get; set; }

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
