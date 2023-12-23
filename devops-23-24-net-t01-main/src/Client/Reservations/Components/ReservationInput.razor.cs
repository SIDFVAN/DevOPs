using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using Blanche.Shared.Formulas;
using Blanche.Shared.Products;
using Blanche.Shared.Reservations;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace Blanche.Client.Reservations.Components
{
    public partial class ReservationInput
    {
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public IReservationService ReservationService { get; set; } = default!;
        [Inject] public IFormulaService FormulaService { get; set; } = default!;
        [Inject] public ISessionStorageService SessionStorage { get; set; } = default!;
        [Inject] public IProductService ProductService { get; set; } = default!;
        [Inject] public IBeerService BeerService { get; set; } = default!;

        private readonly ReservationInputForm reservationForm = new();
        private List<FormulaDto.Index> formulas = new();
        private readonly Guid reservationId = Guid.NewGuid();
        public double TotalPrice;

        private IEnumerable<DateTime> alreadyBookedDates = new List<DateTime>();
        private IEnumerable<DateTime> popularDates = new List<DateTime>();
        private IEnumerable<BeerDto> beers = new List<BeerDto>();

        [Parameter]
        public bool Success { get; set; }
        [Parameter]
        public bool Final { get; set; }
        [Parameter]
        public EventCallback<bool> SuccessChanged { get; set; }
        [Parameter]
        public EventCallback<bool> FinalChanged { get; set; }

        public class ReservationInputForm
        {
            [Required(ErrorMessage = "Gelieve één of meerdere datums te selecteren.")]
            public DateRange ReservationDates { get; set; } = new DateRange(DateTime.Now.Date, DateTime.Now.Date);

            [Required(ErrorMessage = "Gelieve een formule te selecteren.")]
            public FormulaDto.Index Formula { get; set; } = default!;

            [Required]
            [Range(1, 100, ErrorMessage = "Het aantal personen mag niet 0 zijn.")]
            public int NumberOfPersons { get; set; }

            [RequiredIf(ErrorMessage = "Gelieve een biertype te selecteren.")]
            public BeerDto SelectedBeer { get; set; } = default!;

            public List<ReservationItemDto> Items { get; set; } = new();

            public string Notes { get; set; } = default!;
        }

        protected override async Task OnInitializedAsync()
        {
            popularDates = await ReservationService.GetPopularDates();
            alreadyBookedDates = await ReservationService.GetAlreadyBookedDates();
            formulas = await FormulaService.GetIndexAsync();
            beers = await BeerService.GetAllAsync();

            var choosenformula = await GetFormulaFromSessionStorage();
            if (choosenformula is not null)
            {
                reservationForm.Formula = choosenformula;
            }

            var storedResult = await SessionStorage.GetItemAsync<ReservationDto>("Reservation");

            if (storedResult is not null)
            {
                reservationForm.ReservationDates = new DateRange(storedResult.StartDate, storedResult.EndDate);
                reservationForm.Formula = storedResult.Formula;
                reservationForm.SelectedBeer = storedResult.TypeOfBeer;
                reservationForm.NumberOfPersons = storedResult.NumberOfPersons;
                reservationForm.Items = storedResult.Items;
                TotalPrice = storedResult.TotalPrice;
                reservationForm.Notes = storedResult.Notes;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var choosenformula = await GetFormulaFromSessionStorage();
            if (choosenformula is not null)
                reservationForm.Formula = choosenformula;

            if (!firstRender)
            {
                var reservation = new ReservationDto
                {
                    Id = reservationId,
                    StartDate = reservationForm.ReservationDates.Start.Value,
                    EndDate = reservationForm.ReservationDates.End.Value,
                    FormulaId = reservationForm.Formula is not null ? reservationForm.Formula.Id : null,
                    Formula = reservationForm.Formula,
                    TypeOfBeer = reservationForm.SelectedBeer,
                    NumberOfPersons = reservationForm.NumberOfPersons,
                    SubTotalPrice = 0,
                    TotalPrice = TotalPrice,
                    Items = reservationForm.Items,
                    Notes = reservationForm.Notes
                    
                };

                if (reservation.TotalPrice is not 0)
                {
                    reservation.TotalPrice = 0;
                    CalculatePrice(reservation);
                }

                await SessionStorage.SetItemAsync("Reservation", reservation);
            }
        }

        private async Task<FormulaDto.Index> GetFormulaFromSessionStorage()
        {
            var result = await SessionStorage.GetItemAsync<FormulaDto.Index>("ChoosenFormula");
            return result;
        }

        private string ShowPopularDates(DateTime date)
        {
            var alreadyBooked = alreadyBookedDates.Select(d => d.ToShortDateString());
            var popular = popularDates.Select(p => p.ToShortDateString());

            return alreadyBooked.Contains(date.ToShortDateString()) && date > DateTime.Now ? "already-booked" :
            popular.Contains(date.ToShortDateString()) && date > DateTime.Now ? "special-day" : string.Empty;
        }

        private bool DisableBookedDates(DateTime date)
        {
            var alreadyBooked = alreadyBookedDates.Select(d => d.ToShortDateString());
            return date < DateTime.Today || alreadyBooked.Contains(date.ToShortDateString());
        }

        private async Task<Task> OnValidSubmit(EditContext context)
        {
            var result = await SessionStorage.GetItemAsync<ReservationDto>("Reservation");
            var reservation = new ReservationDto
            {
                Id = result.Id,
                StartDate = reservationForm.ReservationDates.Start.Value,
                EndDate = reservationForm.ReservationDates.End.Value,
                FormulaId = reservationForm.Formula.Id,
                Formula = reservationForm.Formula,
                TypeOfBeer = reservationForm.SelectedBeer,
                NumberOfPersons = reservationForm.NumberOfPersons,
                Items = reservationForm.Items,
                Notes = reservationForm.Notes
            };

            CalculatePrice(reservation);
            await SessionStorage.SetItemAsync("Reservation", reservation);

            if (!Success)
            {
                Success = true;
                return SuccessChanged.InvokeAsync(Success);
            }
            else
            {
                Final = true;
                return FinalChanged.InvokeAsync(Final);
            }
        }

        private async void CloseChip(ReservationItemDto item)
        {
            item.Product.QuantityInStock += item.Product.QuantityOrdered;
            await ProductService.EditQuantityInStockAsync(item.Product);
            var result = await SessionStorage.GetItemAsync<ReservationDto>("Reservation");
            result.Items?.RemoveAll(i => i.Id == item.Id);
            reservationForm.Items = result.Items;
            await SessionStorage.SetItemAsync("Reservation", result);
            StateHasChanged();
        }

        private async void Cancel()
        {
            foreach (var item in reservationForm.Items)
            {
                item.Product.QuantityInStock += item.Product.QuantityOrdered;
                await ProductService.EditQuantityInStockAsync(item.Product);
            }
            SessionStorage.ClearAsync();
            NavigationManager.NavigateTo($"/");
        }

        private void CalculatePrice(ReservationDto reservation)
        {
            var diffOfDates = Math.Max(reservation.EndDate.Day - reservation.StartDate.Day, 1); 
             
            var prices = reservation.Formula.PricePerDays;

            reservation.TotalPrice += prices[Math.Min(diffOfDates, 3)];

            if (diffOfDates > prices.Keys.Last())
            {
                prices.Values.Last();

                for (int i = 3; i <= diffOfDates; i++)
                {
                    reservation.TotalPrice += reservation.Formula.PricePerExtraDay;
                };
            }

            if (reservation.Formula.HasDrinks && reservation.TypeOfBeer is not null)
            {
                double beerPrice;
                double bbqBurgerPrice;

                beerPrice = reservation.NumberOfPersons * reservation.TypeOfBeer.Price;

                if (reservation.Formula.HasFood)
                {
                    bbqBurgerPrice = reservationForm.NumberOfPersons * 12;
                    reservation.TotalPrice += bbqBurgerPrice;
                }
                reservation.TotalPrice += beerPrice;
            }

            if (reservation.Items.Any())
            {
                double extraProductPrice;
                double totalProductPrice = 0.0;
                foreach (var item in reservation.Items)
                {
                    extraProductPrice = item.Quantity * item.Product.Price;
                    totalProductPrice += extraProductPrice;
                }
                Console.WriteLine(totalProductPrice);

                reservation.TotalPrice += totalProductPrice;
            }

            TotalPrice = reservation.TotalPrice;
        }

        private async void RecalculatePrice()
        {
            var reservation = await SessionStorage.GetItemAsync<ReservationDto>("Reservation");
            if (reservation is not null && reservation.TotalPrice is not 0)
            {
                StateHasChanged();
                reservation.TotalPrice = 0;
                CalculatePrice(reservation);
            }
        }

        private class RequiredIf : RequiredAttribute
        {
            public RequiredIf() { }
            public FormulaDto.Index Formula { get; set; }
            protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            {
                ReservationInputForm model = validationContext.ObjectInstance as ReservationInputForm;
                if (model is not null && model.Formula is not null && model.Formula.HasDrinks is false)
                    return ValidationResult.Success;
                return base.IsValid(value, validationContext);
            }
            public override bool RequiresValidationContext
            {
                get
                {
                    return true;
                }
            }
        }
    }
}
