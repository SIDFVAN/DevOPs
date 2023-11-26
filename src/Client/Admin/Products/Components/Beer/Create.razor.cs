using Append.Blazor.Sidepanel;
using Blanche.Shared.Products;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace Blanche.Client.Admin.Products.Components.Beer
{
    public partial class Create
    {
        public MudForm? Form { get; set; }
        public BeerDto Beer { get; set; } = new BeerDto();
        [Inject] public IBeerService BeerService { get; set; } = default!;
        [Inject] public ISidepanelService Sidepanel { get; set; } = default!;

        public async Task CreateBeerAsync()
        {
            await Form!.Validate();
            if (Form.IsValid) { 
                await BeerService.CreateAsync(Beer);
                Close();
            }
        }

        public void Close()
        {
            Sidepanel.Close();
        }
    }
}
