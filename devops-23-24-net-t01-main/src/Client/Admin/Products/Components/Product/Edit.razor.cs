using Append.Blazor.Sidepanel;
using Blanche.Client.Files;
using Blanche.Domain.Products;
using Blanche.Shared.Products;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace Blanche.Client.Admin.Products.Components.Product
{
    public partial class Edit
    {


        private IBrowserFile? image; 
        public ProductDto Product { get; set; } = new();
        [Parameter] public Guid ProductId { get; set; } 
        [Parameter] public EventCallback OnProductEdited { get; set; }

        [Inject] public IProductService ProductService { get; set; } = default!;
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public IStorageService StorageService { get; set; } = default!;
        [Inject] public IDialogService DialogService {  get; set; } = default!;
        [Inject] public ISidepanelService Sidepanel { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await GetProductAsync();
        }

        private async Task GetProductAsync()
        {
            Product = await ProductService.GetByIdAsync(ProductId);
        }

        /*private void UploadImages(IBrowserFile file)
        {
            files.Add(file);
            //TODO upload the files to the server
        }*/

        public void UploadImage(IBrowserFile file)
        {
            image = file;
            Product.ImageContentType = image.ContentType;
        }

        private async Task DeleteImage(string url)
        {
            var dialogOptions = new DialogOptions
            {
                CloseOnEscapeKey = true,
                CloseButton = true,
                DisableBackdropClick = true,
                Position = DialogPosition.Center,
                MaxWidth = MaxWidth.ExtraSmall
            };

            var parameters = new DialogParameters<Dialog>();
            parameters.Add(x => x.ContentText, "Do you really want to delete the image? This process cannot be undone!");
            parameters.Add(x => x.ButtonText, "Delete");
            parameters.Add(x => x.Color, Color.Error);

            var dialog = await DialogService.ShowAsync<Dialog>("Delete Image", parameters, dialogOptions);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                await StorageService.DeleteImageAsync(url);
                Product.ImageUrl = string.Empty;
            }
        }

        private async Task UpdateProductAsync()
        {
            ProductResult.Saved? result = await ProductService.EditAsync(Product);
            if (!string.IsNullOrEmpty(result?.UploadUri?.ToString()))
                await StorageService.UploadImageAsync(result.UploadUri, image!);
            Close();
            //await ProductAddedOrUpdated.InvokeAsync(Product);
            await OnProductEdited.InvokeAsync();
        }

        private void Close()
        {
            Sidepanel.Close();
        }
    }
}
