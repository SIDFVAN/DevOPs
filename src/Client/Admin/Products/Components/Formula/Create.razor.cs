using Append.Blazor.Sidepanel;
using Blanche.Domain.Products;
using Blanche.Shared.Formulas;
using Blanche.Shared.Products;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace Blanche.Client.Admin.Products.Components.Formula
{
    public partial class Create
    {
        private MudForm? form;
        private IBrowserFile? image;
        public FormulaDto.Mutate Formula { get; set; } = new FormulaDto.Mutate();
        [Inject] public IFormulaService FormulaService { get; set; } = default!;
        [Inject] public ISidepanelService Sidepanel { get; set; } = default!;
        
        public async Task CreateFormulaAsync()
        {
            await FormulaService.CreateAsync(Formula);
            Close();
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
    }
}
