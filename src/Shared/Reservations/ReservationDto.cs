using Blanche.Shared.Common;
using Blanche.Shared.Customers;
using Blanche.Shared.Formulas;

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
        public FormulaDto? Formula { get; set; } = new();
        public List<ReservationItemDto>? Items { get; set; } = new();

        public static ReservationDtoBuilder Builder()
        {
            return new ReservationDtoBuilder();
        }
        public class ReservationDtoBuilder
        {
            private Guid Id;
            private DateTime startDate;
            private DateTime endDate;
            private double totalPrice;
            private double subTotalPrice;
            private int NumberOfPersons;
            private bool isConfirmed;
            private CustomerDto customer = new CustomerDto();
            private FormulaDto formula = new FormulaDto();
            private List<ReservationItemDto> items = new List<ReservationItemDto>();

            public ReservationDtoBuilder WithId(Guid id)
            {
                this.Id = id;
                return this;
            }

            public ReservationDtoBuilder WithStartDate(DateTime startDate)
            {
                this.startDate = startDate;
                return this;
            }

            public ReservationDtoBuilder WithEndDate(DateTime endDate)
            {
                this.endDate = endDate;
                return this;
            }

            public ReservationDtoBuilder WithNumberOfPersons(int number)
            {
                this.NumberOfPersons = number;
                return this;
            }

            public ReservationDtoBuilder WithTotalPrice(double totalPrice)
            {
                this.totalPrice = totalPrice;
                return this;
            }

            public ReservationDtoBuilder WithSubTotalPrice(double subTotalPrice)
            {
                this.subTotalPrice = subTotalPrice;
                return this;
            }

            public ReservationDtoBuilder WithIsConfirmed(bool isConfirmed)
            {
                this.isConfirmed = isConfirmed;
                return this;
            }

            public ReservationDtoBuilder WithCustomer(CustomerDto customer)
            {
                this.customer = customer;
                return this;
            }

            public ReservationDtoBuilder WithFormula(FormulaDto formula)
            {
                this.formula = formula;
                return this;
            }

            public ReservationDtoBuilder WithItems(List<ReservationItemDto> items)
            {
                this.items = items;
                return this;
            }

            public ReservationDto Build()
            {
                return new ReservationDto
                {
                    Id = Id,
                    StartDate = startDate,
                    EndDate = endDate,
                    TotalPrice = totalPrice,
                    SubTotalPrice = subTotalPrice,
                    IsConfirmed = isConfirmed,
                    Customer = customer,
                    NumberOfPersons = NumberOfPersons,
                    Formula = formula,
                    Items = items
                };
            }
        }

    }
}