using Shouldly;
using Blanche.Domain.Customers;
using Blanche.Domain.Formulas;
using Blanche.Domain.Reservations;
using tests.Fakers.Formulas; 
using Xunit.Abstractions; 
using Blanche.Domain.Products;
using tests.Fakers.Products;
using tests.Fakers.Reservations;

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
            ReservationState state = ReservationState.OFFER_SENT;
            int numberOfPersons = 45;
            Customer customer = new CustomerFaker(); 
            //string formula = "Only Blanche";
            Formula formula = new FormulaFaker();
            Beer beer = new BeerFaker();
            List<ReservationItem> items = new ();

            Reservation reservation = new (startDate, endDate, totalPrice, state, numberOfPersons, customer, formula, beer, items);

            reservation.StartDate.ShouldBe(startDate);
            reservation.EndDate.ShouldBe(endDate);
            reservation.TotalPrice.ShouldBe(totalPrice);
            reservation.State.ShouldBe(state);
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
            ReservationState state = ReservationState.OFFER_SENT;
            int numberOfPersons = 45;
            Customer customer = new CustomerFaker(); 
            Formula formula = new FormulaFaker();
            Beer beer = new BeerFaker();
            List<ReservationItem> items = new ()
            {
                new ReservationItemFaker(),
                new ReservationItemFaker()
            };

            Reservation reservation = new(startDate, endDate, totalPrice, state, numberOfPersons, customer, formula, beer, items);
         
            reservation.StartDate.ShouldBe(startDate);
            reservation.EndDate.ShouldBe(endDate);
            reservation.TotalPrice.ShouldBe(totalPrice);
            reservation.State.ShouldBe(state);
            reservation.Customer.ShouldBe(customer); 
            reservation.Formula.ShouldNotBeNull();
            reservation.Formula.ShouldBe(formula);
            reservation.TypeOfBeer.ShouldNotBeNull();
            reservation.TypeOfBeer.ShouldBe(beer);
            //reservation.Items.ShouldBe(items);
        }

        [Fact]
        public void Be_created_without_extra_products()
        {
            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today.AddDays(1);
            double totalPrice = 250.00;
            ReservationState state = ReservationState.OFFER_SENT;
            int numberOfPersons = 45;
            Customer customer = new CustomerFaker(); 
            //string formula = "Only Blanche";
            Formula formula = new FormulaFaker();
            Beer beer = new BeerFaker();
            List<ReservationItem> items = new();

            Reservation reservation = new (startDate, endDate, totalPrice, state, numberOfPersons, customer, formula, beer, items);

            reservation.StartDate.ShouldBe(startDate);
            reservation.EndDate.ShouldBe(endDate);
            reservation.TotalPrice.ShouldBe(totalPrice);
            reservation.State.ShouldBe(state);
            reservation.NumberOfPersons.ShouldBe(numberOfPersons);
            reservation.Customer.ShouldBe(customer); 
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
            ReservationState state = ReservationState.OFFER_SENT;
            int numberOfPersons = 45;
            Customer customer = new CustomerFaker(); 
            //string formula = "Only Blanche";
            Formula formula = new FormulaFaker();
            Beer beer = new BeerFaker();
            List<ReservationItem> items = new();

            Action act = () =>
            {
                Reservation reservation = new (startDate, endDate, totalPrice, state, numberOfPersons, customer, formula, beer, items);
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
            ReservationState state = ReservationState.OFFER_SENT;
            int numberOfPersons = 45;
            Customer customer = new CustomerFaker();
            //string formula = "Only Blanche";
            Formula formula = new FormulaFaker();
            Beer beer = new BeerFaker();
            List<ReservationItem> items = new();

            Action act = () =>
            {
                Reservation reservation = new (startDate, endDate, totalPrice, state, numberOfPersons, customer, formula, beer, items);
            };

            act.ShouldThrow<Exception>();
        }
    }
}

