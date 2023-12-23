using Append.Blazor.Sidepanel; 
using Blanche.Shared.Formulas; 
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace Blanche.Client.Admin.Products.Components.Formula
{
    public partial class Create
    {
         private IBrowserFile? image;
        public FormulaDto.Mutate Formula { get; set; } = new FormulaDto.Mutate();
        [Parameter] public EventCallback OnFormulaCreated { get; set; }
 
        [Inject] public IFormulaService FormulaService { get; set; } = default!;
        [Inject] public ISidepanelService Sidepanel { get; set; } = default!;
        [Parameter] public EventCallback OnDataUpdated { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;

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

        public async Task CreateFormulaAsync()
        {
            foreach (var price in ListPricePerdays)
            {
                Formula.PricePerDays.Add(price.NumberOfDays, price.Price);
            }

            await FormulaService.CreateAsync(Formula);
            Close();
            await OnFormulaCreated.InvokeAsync();
        }

        public void UploadImage(IBrowserFile file)
        {
            image = file;
            Formula.ImageContentType = image.ContentType;
        }

        public void Close()
        {
            Sidepanel.Close();
        }

        private void AddNewKeyValuePair()
        {
            int newKey = GetNewKey();  
            Formula.PricePerDays.Add(newKey, 0.0);
        }

        private int GetNewKey()
        {

            if (Formula.PricePerDays.Any())
            {
                return Formula.PricePerDays.Keys.Max() + 1;
            }
            else
            {
                return 1;  
            }
        }

        private void RemoveKeyValuePair(int day)
        {
            if (Formula.PricePerDays.Any() && Formula.PricePerDays.Keys.Max() > 1)
                Formula.PricePerDays.Remove(day);
        }
    }
}
