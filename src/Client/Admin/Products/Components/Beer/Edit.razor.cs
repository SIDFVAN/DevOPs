using Append.Blazor.Sidepanel;
using Blanche.Shared.Products;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blanche.Client.Admin.Products.Components.Beer
{
    public partial class Edit
    {
        [Parameter] public Guid BeerId { get; set; }
        [Parameter] public EventCallback OnBeerEdited { get; set; }
        public BeerDto Beer { get; set; } = new();
        [Inject] public IBeerService BeerService { get; set; } = default!;
        [Inject] public ISidepanelService Sidepanel { get; set; } = default!;

        [Inject] public NavigationManager NavigationManager { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            Console.WriteLine(BeerId);
            Beer = await BeerService.GetByIdAsync(BeerId);
        }

        public async Task UpdateBeerAsync()
        {
            Console.WriteLine("TEST1213212");
            await BeerService.UpdateAsync(Beer);
            Close();
            Console.WriteLine("TEST1213212");
            await OnBeerEdited.InvokeAsync();
        }

        public void Close() {
            Sidepanel.Close();
        }
    }
}
