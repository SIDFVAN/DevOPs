using Blanche.Domain.Reservations;
using Blanche.Shared.Common;
using Blanche.Shared.Customers;
using Blanche.Shared.Formulas;
using Blanche.Shared.Invoices;
using Blanche.Shared.Products;

namespace Blanche.Shared.Reservations
{
    public class ReservationDto : EntityDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalPrice { get; set; }
        public double SubTotalPrice { get; set; }
        public ReservationState State { get; set; } = ReservationState.NEW; // default 
        public int NumberOfPersons { get; set; }
        public Guid? CustomerId { get; set; }
        public CustomerDto? Customer { get; set; } = new();
        public Guid? FormulaId { get; set; }
        public FormulaDto.Index Formula { get; set; } = new();
        public List<ReservationItemDto?> Items { get; set; } = default!;
        public List<InvoiceDto>? Invoices { get; set; } = new();
        public Guid? TypeOfBeerId { get; set; }
        public BeerDto? TypeOfBeer { get; set; }
        public string Notes { get; set; }

        public ReservationDto() { }
    }
}