using Blanche.Client.Files;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Blanche.Shared.Formulas;
using Append.Blazor.Sidepanel;

namespace Blanche.Client.Admin.Products.Components.Formula
{
    public partial class Edit
    {
        private MudForm? form;

        private IBrowserFile? image;
        public FormulaDto.Mutate Formula { get; set; } = null!;
        [Parameter] public Guid FormulaId { get; set; }

        [Inject] public IFormulaService FormulaService { get; set; } = default!;
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public IStorageService StorageService { get; set; } = default!;
        [Inject] public ISidepanelService Sidepanel { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await GetFormulaAsync();
        }

        private async Task GetFormulaAsync()
        {
            FormulaDto.Detail detail = await FormulaService.GetDetailAsync(FormulaId);
            if (detail != null)
            {
                Formula = new()
                {
                    Name = detail.Name,
                    Description = detail.Description,
                    Days = detail.Days,
                    ImageUrl = detail.ImageUrl,
                    Price = detail.Price
                };
            }
        }

        public void UploadImage(IBrowserFile file)
        {
            image = file;
            Formula.ImageContentType = image.ContentType;
        }

        private async Task UpdateFormulaAsync()
        {
            await FormulaService.EditAsync(FormulaId, Formula);
            // TODO ÊditAsync method FormulaService must return an UploadUri
            /*if (!string.IsNullOrEmpty(result?.UploadUri.ToString()))
                await StorageService.UploadImageAsync(result.UploadUri, image!);*/

            Close();
            NavigationManager.NavigateTo($"/admin/products");
        }

        public void Close()
        {
            Sidepanel.Close();
        }
    }
}
