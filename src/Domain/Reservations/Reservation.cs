using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using Blanche.Domain.Common;
using Blanche.Domain.Customers;
using Blanche.Domain.Formulas;


namespace Blanche.Domain.Reservations;

public class Reservation : Entity
{
    private readonly List<ReservationLine> lines = new();
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double TotalPrice { get; set; }
    public bool IsConfirmed { get; set; }
    public int NumberOfPersons { get; set; }
    public Customer Customer { get; set; } = default!;
    public Formula Formula { get; set; } = default!;
    [NotMapped]
    public IEnumerable<ReservationItem> Items { get; set; } = new List<ReservationItem>();

    public IReadOnlyCollection<ReservationLine> Lines => lines.AsReadOnly();

    public Reservation() { }

    public Reservation(DateTime startDate, DateTime endDate, double totalPrice, bool isConfirmed, int numberOfPersons, Customer customer, Formula formula, IEnumerable<ReservationItem> items)
    {
        StartDate = Guard.Against.OutOfRange(startDate, nameof(StartDate), DateTime.Today, DateTime.Today.AddDays(365));
        EndDate = Guard.Against.Null(endDate, nameof(endDate));
        TotalPrice = Guard.Against.NegativeOrZero(totalPrice, nameof(TotalPrice));
        IsConfirmed = Guard.Against.Null(isConfirmed, nameof(IsConfirmed));
        NumberOfPersons = Guard.Against.NegativeOrZero(numberOfPersons, nameof(NumberOfPersons));
        Customer = Guard.Against.Null(customer, nameof(Customer));
        Formula = Guard.Against.Null(formula, nameof(Formula));
        Items = Guard.Against.Null(items, nameof(Items));

        foreach (ReservationItem item in items)
        {
            lines.Add(new ReservationLine(this, item));
        }
    }

    public static ReservationBuilder Builder()
    {
        return new ReservationBuilder();
    }

    public class ReservationBuilder
    {
        private Guid _id;
        private DateTime _startDate;
        private DateTime _endDate;
        private double _totalPrice;
        private bool _isConfirmed;
        private Customer _customer = default!;
        private Formula _formula = default!;
        private int _numPersons;
        private IEnumerable<ReservationItem> _items = new List<ReservationItem>();

        public ReservationBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }
        public ReservationBuilder WithStartDate(DateTime startDate)
        {
            _startDate = Guard.Against.OutOfRange(startDate, nameof(startDate), DateTime.Today, DateTime.Today.AddDays(365));
            return this;
        }

        public ReservationBuilder WithEndDate(DateTime endDate)
        {
            _endDate = Guard.Against.Null(endDate, nameof(endDate));
            return this;
        }

        public ReservationBuilder WithTotalPrice(double totalPrice)
        {
            _totalPrice = Guard.Against.NegativeOrZero(totalPrice, nameof(totalPrice));
            return this;
        }

        public ReservationBuilder WithNumberOfPersons(int number)
        {
            _numPersons = Guard.Against.NegativeOrZero(number, nameof(number));
            return this;
        }
        

        public ReservationBuilder WithIsConfirmed(bool isConfirmed)
        {
            _isConfirmed = Guard.Against.Null(isConfirmed, nameof(isConfirmed));
            return this;
        }

        public ReservationBuilder WithCustomer(Customer customer)
        {
            _customer = Guard.Against.Null(customer, nameof(customer));
            return this;
        }

        public ReservationBuilder WithFormula(Formula formula)
        {
            _formula = Guard.Against.Null(formula, nameof(formula));
            return this;
        }

        public ReservationBuilder WithItems(IEnumerable<ReservationItem> items)
        {
            _items = Guard.Against.Null(items, nameof(items));
            return this;
        }

        public Reservation Build()
        {
            var reservation = new Reservation(_startDate, _endDate, _totalPrice, _isConfirmed, _numPersons, _customer, _formula, _items);
            return reservation;
        }
    }

}

