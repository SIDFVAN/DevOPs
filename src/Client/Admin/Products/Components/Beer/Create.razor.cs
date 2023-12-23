using Append.Blazor.Sidepanel;
using Blanche.Shared.Products;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace Blanche.Client.Admin.Products.Components.Beer
{
    public partial class Create
    {
        public BeerDto Beer { get; set; } = new BeerDto();
        [Parameter] public EventCallback OnBeerCreated { get; set; }
        [Inject] public IBeerService BeerService { get; set; } = default!;
        [Inject] public ISidepanelService Sidepanel { get; set; } = default!;
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;

        public async Task CreateBeerAsync()
        {
            await BeerService.CreateAsync(Beer);
            Close();
             await OnBeerCreated.InvokeAsync();

        }

        public void Close()
        {
            Sidepanel.Close();
        }
    }
}
