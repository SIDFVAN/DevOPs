using Blanche.Shared.Common;
using Blanche.Shared.Customers;
using Blanche.Shared.Formulas;
using Blanche.Shared.Invoice;
using Blanche.Shared.Products;

namespace Blanche.Shared.Reservations
{
    public class ReservationDto : EntityDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalPrice { get; set; }
        public double SubTotalPrice { get; set; }
        public bool IsConfirmed { get; set; }
        public int NumberOfPersons { get; set; }
        public CustomerDto? Customer { get; set; } = new();
        public FormulaDto.Index Formula { get; set; } = new();
        public List<ProductDto>? Items { get; set; } = new();
        public InvoiceDto? Invoice { get; set; } = new();
        public BeerDto? TypeOfBeer { get; set; }

        public ReservationDto() { }
  
    }
}