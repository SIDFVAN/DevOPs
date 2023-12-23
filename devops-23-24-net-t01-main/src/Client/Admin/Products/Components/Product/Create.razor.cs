using Append.Blazor.Sidepanel;
using Blanche.Client.Files;
using Blanche.Domain.Products;
using Blanche.Shared.Products;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace Blanche.Client.Admin.Products.Components.Product
{
    public partial class Create
    {
        private MudForm? form;
        private IBrowserFile? image;
        public ProductDto Product { get; set; } = new ProductDto(); 
        [Parameter] public EventCallback OnProductCreated { get; set; }
        [Inject] public IProductService ProductService { get; set; } = default!;
        [Inject] public ISidepanelService Sidepanel { get; set; } = default!;
        [Inject] public IStorageService StorageService { get; set; } = default!;
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;

        public void UploadImage(IBrowserFile file)
        {
            image = file;
            Product.ImageContentType = image.ContentType;
        }

        public async Task CreateProductAsync()
        {
            ProductResult.Saved? result = await ProductService.CreateAsync(Product);
            Close();
            //await ProductAddedOrUpdated.InvokeAsync(Product);
            await OnProductCreated.InvokeAsync();
        }

        public void Close()
        {
            Sidepanel.Close();
        }
    }
}
