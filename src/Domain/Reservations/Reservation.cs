using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Ardalis.GuardClauses;
using Blanche.Domain.Common;
using Blanche.Domain.Customers;
using Blanche.Domain.Formulas;
using Blanche.Domain.Invoices;
using Blanche.Domain.Products;
using Blanche.Shared.Reservations;

namespace Blanche.Domain.Reservations;

public class Reservation : Entity
{
    private readonly List<ReservationItem> items = new();

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double TotalPrice { get; set; }
    public ReservationState State { get; set; }
    public int NumberOfPersons { get; set; }
    public Guid? CustomerId { get; set; }
    public virtual Customer Customer { get; set; } = default!;  
    public List<Invoice>? Invoices { get; set; } = default!;
    public Guid? FormulaId { get; set; }
    public virtual Formula Formula { get; set; } = default!;
    public Guid? TypeOfBeerId { get; set; }
    public virtual Beer? TypeOfBeer { get; set; }
    public string? Notes { get; set; }
    public List<ReservationItem?> Items => items;
    public Reservation() { }

    public Reservation(DateTime startDate, DateTime endDate, double totalPrice, ReservationState state, int numberOfPersons, Customer customer, Formula formula, Beer beer, IEnumerable<ReservationItem> extras)
    {
        StartDate = Guard.Against.OutOfRange(startDate, nameof(StartDate), DateTime.Today, DateTime.Today.AddDays(365));
        EndDate = Guard.Against.Null(endDate, nameof(endDate));
        TotalPrice = Guard.Against.NegativeOrZero(totalPrice, nameof(TotalPrice));
        State = Guard.Against.Null(state, nameof(state));
        NumberOfPersons = Guard.Against.NegativeOrZero(numberOfPersons, nameof(NumberOfPersons));
        Customer = Guard.Against.Null(customer, nameof(Customer)); 
        Formula = Guard.Against.Null(formula, nameof(Formula));
        TypeOfBeer = beer;

        foreach (var extra in extras)
        {
            items.Add(new ReservationItem(extra.Product, extra.Quantity, extra.Price));
        }
    }

    public void AddItemsToReservation(List<ReservationItem> reservationItems)
    {
        if (!items.Any())
        {
            foreach (var item in reservationItems)
            {
                items.Add(item);
            }
        }
    }
}

