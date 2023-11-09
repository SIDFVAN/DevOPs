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

        [NotMapped]
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

        public class CustomerBuilder
        {
            private string? FirstName { get; set; }
            private string? LastName { get; set; }
            private string? PhoneNumber { get; set; }
            private EmailAddress? Email { get; set; }
            private Address? Address { get; set; }

            public CustomerBuilder WithFirstName(string firstName)
            {
                FirstName = firstName;
                return this;
            }

            public CustomerBuilder WithLastName(string lastName)
            {
                LastName = lastName;
                return this;
            }

            public CustomerBuilder WithPhoneNumber(string phone)
            {
                PhoneNumber = phone;
                return this;
            }

            public CustomerBuilder WithEmail(EmailAddress emailAddress)
            {
                Email = emailAddress;
                return this;
            }

            public CustomerBuilder WithAddress(Address address)
            {
                Address = address;
                return this;
            }

            public Customer Build()
            {
                return new Customer
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    PhoneNumber = PhoneNumber,
                    Email = Email,
                    CustomerAddress = Address
                };
            }
        }

        public static CustomerBuilder Builder()
        {
            return new CustomerBuilder();
        }

    }
} 