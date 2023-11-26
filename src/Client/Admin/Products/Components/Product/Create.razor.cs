using Append.Blazor.Sidepanel;
using Blanche.Client.Files;
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
        [Inject] public IProductService ProductService { get; set; } = default!;
        [Inject] public ISidepanelService Sidepanel { get; set; } = default!;
        [Inject] public IStorageService StorageService { get; set; } = default!;

        public void UploadImage(IBrowserFile file)
        {
            image = file;
            Product.ImageContentType = image.ContentType;
        }

        public async Task CreateProductAsync()
        {
            ProductResult.Saved? result = await ProductService.CreateAsync(Product);
            if (!string.IsNullOrEmpty(result?.UploadUri.ToString()))
                await StorageService.UploadImageAsync(result.UploadUri, image!);
            Close();
        }

        public void Close()
        {
            Sidepanel.Close();
        }
    }
}
