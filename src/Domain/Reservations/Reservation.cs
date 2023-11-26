using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using Blanche.Domain.Common;
using Blanche.Domain.Customers;
using Blanche.Domain.Formulas;
using Blanche.Domain.Invoices;
using Blanche.Domain.Products;

namespace Blanche.Domain.Reservations;

public class Reservation : Entity
{
    private readonly List<ReservationLine> lines = new();
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double TotalPrice { get; set; }
    public bool IsConfirmed { get; set; }
    //public ReservationState State { get; set; }
    public int NumberOfPersons { get; set; }
    public Customer Customer { get; set; } = default!;
    public Invoice Invoice { get; set; } = default!;
    public Formula Formula { get; set; } = default!;
    //public string TypeOfBeer { get; set; } = default!;
    public Beer? TypeOfBeer { get; set; }

    [NotMapped]
    public IEnumerable<Product> Items { get; set; } = new List<Product>();

    public IReadOnlyCollection<ReservationLine> Lines => lines.AsReadOnly();
    
    public Reservation() { }

    public Reservation(DateTime startDate, DateTime endDate, double totalPrice, bool isConfirmed, int numberOfPersons, Customer customer, Formula formula, Invoice invoice, Beer beer, IEnumerable<Product> items)
    {
        StartDate = Guard.Against.OutOfRange(startDate, nameof(StartDate), DateTime.Today, DateTime.Today.AddDays(365));
        EndDate = Guard.Against.Null(endDate, nameof(endDate));
        TotalPrice = Guard.Against.NegativeOrZero(totalPrice, nameof(TotalPrice));
        IsConfirmed = Guard.Against.Null(isConfirmed, nameof(IsConfirmed));
        NumberOfPersons = Guard.Against.NegativeOrZero(numberOfPersons, nameof(NumberOfPersons));
        Customer = Guard.Against.Null(customer, nameof(Customer));
        Invoice = Guard.Against.Null(invoice, nameof(Invoice));
        Formula = Guard.Against.Null(formula, nameof(Formula));
        TypeOfBeer = beer;
        Items = items;

        foreach (Product item in items)
        {
            lines.Add(new ReservationLine(this, item));
        }
    }
      
}

