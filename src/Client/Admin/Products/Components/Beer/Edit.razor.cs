using Append.Blazor.Sidepanel;
using Blanche.Shared.Products;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blanche.Client.Admin.Products.Components.Beer
{
    public partial class Edit
    {
        private MudForm? form;
        [Parameter] public Guid BeerId { get; set; }
        public BeerDto Beer { get; set; } = default!;
        [Inject] public IBeerService BeerService { get; set; } = default!;
        [Inject] public ISidepanelService Sidepanel { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            Beer = await BeerService.GetByIdAsync(BeerId);
        }

        public async Task UpdateBeerAsync()
        {
            await BeerService.UpdateAsync(Beer);
            Close();
        }

        public void Close() {
            Sidepanel.Close();
        }
    }
}
