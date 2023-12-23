using Blanche.Client.Files;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Blanche.Shared.Formulas;
using Append.Blazor.Sidepanel;
using Blanche.Domain.Reservations;
using Blanche.Shared.Products;
using System.ComponentModel.DataAnnotations;

namespace Blanche.Client.Admin.Products.Components.Formula
{
    public partial class Edit
    {
        private MudForm? form;
        private IBrowserFile? image;
        public FormulaDto.Mutate Formula { get; set; } = new();
        [Parameter] public Guid FormulaId { get; set; }
        [Parameter] public EventCallback OnFormulaEdit { get; set; }

        [Inject] public IFormulaService FormulaService { get; set; } = default!;
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public IStorageService StorageService { get; set; } = default!;
        [Inject] public ISidepanelService Sidepanel { get; set; } = default!;

        public class PricePerday
        {
            [Required]
            [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "The number of day must be bigger then {0}")]
            public int NumberOfDays { get; set; }

            [Required]
            [Range(minimum: 0, maximum: double.MaxValue, ErrorMessage = "The price must be bigger then {0}")]
            public double Price { get; set; }
        }

        private PricePerday NewPricePerDay = new();
        private List<PricePerday> ListPricePerdays = new();

        public void AddPricePerDay()
        {
            ListPricePerdays.Add(NewPricePerDay);
            ListPricePerdays.Sort((x, y) => x.NumberOfDays.CompareTo(y.NumberOfDays));
            NewPricePerDay = new();
        }

        private void EditPricePerDay(PricePerday pricePerday)
        {
            ListPricePerdays.Remove(pricePerday);
            NewPricePerDay = pricePerday;
        }

        private void DeletePricePerDay(PricePerday pricePerday)
        {
            ListPricePerdays.Remove(pricePerday);
        }

        protected override async Task OnInitializedAsync()
        {

            FormulaDto.Detail detail = await FormulaService.GetDetailAsync(FormulaId);
            if (detail != null)
            {
                Formula = new()
                {
                    Name = detail.Name,
                    Description = detail.Description,
                    ImageUrl = detail.ImageUrl,
                    Price = detail.Price,
                    HasDrinks = detail.HasDrinks,
                    HasFood = detail.HasFood,
                    PricePerExtraDay = detail.PricePerExtraDay,
                    PricePerDays = detail.PricePerDays,
                };
                 

                foreach (KeyValuePair<int, double> entry in detail.PricePerDays)
                {
                    ListPricePerdays.Add(new() { NumberOfDays = entry.Key, Price = entry.Value });
                }

            }
        }

         
        public void UploadImage(IBrowserFile file)
        {
            image = file;
            Formula.ImageContentType = image.ContentType;
        }

        private async Task UpdateFormulaAsync()
        {
            Formula.PricePerDays = new();
            foreach (var item in ListPricePerdays)
            {
                Formula.PricePerDays.Add(item.NumberOfDays, item.Price);
            }
            await FormulaService.EditAsync(FormulaId, Formula);
            var FormulaUpdate = await FormulaService.GetDetailAsync(FormulaId);


            Close();
            await OnFormulaEdit.InvokeAsync();
        }

        public void Close()
        {
            Sidepanel.Close();
        }

        private void RemoveKeyValuePair(int day)
        {
            if (Formula.PricePerDays.Any() && Formula.PricePerDays.Keys.Max() > 1)
                Formula.PricePerDays.Remove(day);
        }
    }
}
