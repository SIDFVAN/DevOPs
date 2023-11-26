using Shouldly;
using Blanche.Domain.Customers;
using Blanche.Domain.Formulas;
using Blanche.Domain.Reservations;
using tests.Fakers.Formulas;
using tests.Fakers.Reservations;
using Xunit.Abstractions;
using tests.Fakers.Invoices;
using Blanche.Domain.Invoices;
using Blanche.Domain.Products;
using tests.Fakers.Products;
using Xunit.Sdk;

namespace tests.Reservations
{
	public class Reservation_Should
	{
        private readonly ITestOutputHelper output;

        public Reservation_Should(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Be_created_with_one_formula()
		{
            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today.AddDays(1);
            double totalPrice = 250.00;
            bool isConfirmed = false;
            int numberOfPersons = 45;
            Customer customer = new CustomerFaker();
            Invoice invoice = new InvoiceFaker();
            //string formula = "Only Blanche";
            Formula formula = new FormulaFaker();
            Beer beer = new BeerFaker();
            List<Product> items = new ();

            Reservation reservation = new (startDate, endDate, totalPrice, isConfirmed, numberOfPersons, customer, formula, invoice, beer, items);

            reservation.StartDate.ShouldBe(startDate);
            reservation.EndDate.ShouldBe(endDate);
            reservation.TotalPrice.ShouldBe(totalPrice);
            reservation.IsConfirmed.ShouldBe(isConfirmed);
            reservation.Customer.ShouldBe(customer);
            reservation.Formula.ShouldNotBeNull();
            reservation.Formula.ShouldBe(formula);
            reservation.TypeOfBeer.ShouldNotBeNull();
            reservation.TypeOfBeer.ShouldBe(beer);
            reservation.Items.ShouldBe(items);
		}

        [Fact]
        public void Be_created_with_extra_products()
        {
            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today.AddDays(1);
            double totalPrice = 250.00;
            bool isConfirmed = false;
            int numberOfPersons = 45;
            Customer customer = new CustomerFaker();
            Invoice invoice = new InvoiceFaker();
            //string formula = "Only Blanche";
            Formula formula = new FormulaFaker();
            Beer beer = new BeerFaker();
            List<Product> items = new ()
            {
                new ProductFaker(),
                new ProductFaker()
            };

            Reservation reservation = new(startDate, endDate, totalPrice, isConfirmed, numberOfPersons, customer, formula, invoice, beer, items);

            reservation.StartDate.ShouldBe(startDate);
            reservation.EndDate.ShouldBe(endDate);
            reservation.TotalPrice.ShouldBe(totalPrice);
            reservation.IsConfirmed.ShouldBe(isConfirmed);
            reservation.Customer.ShouldBe(customer);
            reservation.Invoice.ShouldNotBeNull();
            reservation.Formula.ShouldNotBeNull();
            reservation.Formula.ShouldBe(formula);
            reservation.TypeOfBeer.ShouldNotBeNull();
            reservation.TypeOfBeer.ShouldBe(beer);
            reservation.Items.ShouldBe(items);
        }

        [Fact]
        public void Be_created_without_extra_products()
        {
            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today.AddDays(1);
            double totalPrice = 250.00;
            bool isConfirmed = false;
            int numberOfPersons = 45;
            Customer customer = new CustomerFaker();
            Invoice invoice = new InvoiceFaker();
            //string formula = "Only Blanche";
            Formula formula = new FormulaFaker();
            Beer beer = new BeerFaker();
            List<Product> items = new();

            Reservation reservation = new (startDate, endDate, totalPrice, isConfirmed, numberOfPersons, customer, formula, invoice, beer, items);

            reservation.StartDate.ShouldBe(startDate);
            reservation.EndDate.ShouldBe(endDate);
            reservation.TotalPrice.ShouldBe(totalPrice);
            reservation.IsConfirmed.ShouldBe(isConfirmed);
            reservation.NumberOfPersons.ShouldBe(numberOfPersons);
            reservation.Customer.ShouldBe(customer);
            reservation.Invoice.ShouldNotBeNull();
            reservation.Formula.ShouldNotBeNull();
            reservation.Formula.ShouldBe(formula);
            reservation.TypeOfBeer.ShouldNotBeNull();
            reservation.TypeOfBeer.ShouldBe(beer);
            reservation.Items.ShouldBeEmpty();
            reservation.Items.ShouldNotBeNull();
        }

        [Fact]
        public void Throw_exception_when_startDate_in_the_past()
        {
            DateTime startDate = DateTime.Today.AddDays(-1);
            DateTime endDate = DateTime.Today.AddDays(1);
            double totalPrice = 250.00;
            bool isConfirmed = false;
            int numberOfPersons = 45;
            Customer customer = new CustomerFaker();
            Invoice invoice = new InvoiceFaker();
            //string formula = "Only Blanche";
            Formula formula = new FormulaFaker();
            Beer beer = new BeerFaker();
            List<Product> items = new();

            Action act = () =>
            {
                Reservation reservation = new (startDate, endDate, totalPrice, isConfirmed, numberOfPersons, customer, formula, invoice, beer, items);
            };

            output.WriteLine(startDate.ToShortDateString());
            act.ShouldThrow<Exception>();
        }

        [Fact]
        public void Throw_exception_when_total_price_zero_or_negative()
        {
            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today.AddDays(1);
            double totalPrice = -250.00;
            bool isConfirmed = false;
            int numberOfPersons = 45;
            Customer customer = new CustomerFaker();
            Invoice invoice = new InvoiceFaker();
            //string formula = "Only Blanche";
            Formula formula = new FormulaFaker();
            Beer beer = new BeerFaker();
            List<Product> items = new();

            Action act = () =>
            {
                Reservation reservation = new (startDate, endDate, totalPrice, isConfirmed, numberOfPersons, customer, formula, invoice, beer, items);
            };

            act.ShouldThrow<Exception>();
        }
    }
}

