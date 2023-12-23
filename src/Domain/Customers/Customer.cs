using Blanche.Domain.Common;
using Blanche.Domain.Reservations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blanche.Domain.Customers
{
    public class Customer : Entity
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public EmailAddress Email { get; set; } = default!;
        public Address CustomerAddress { get; set; } = default!;

        private readonly List<Reservation> reservations = new();
        public IReadOnlyCollection<Reservation> Reservations => reservations.AsReadOnly();

        public Customer() { }

        public Customer(string firstname, string lastname, string phoneNumber, Address customerAddress, EmailAddress email)
        {
            FirstName = firstname;
            LastName = lastname;
            CustomerAddress = customerAddress;
            Email = email;
        }
    }
} 