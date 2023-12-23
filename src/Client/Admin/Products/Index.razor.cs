using Append.Blazor.Sidepanel;
using Blanche.Client.Admin.Products.Components;
using Blanche.Client.Files;
using Blanche.Domain.Formulas;
using Blanche.Domain.Products;
using Blanche.Shared.Formulas;
using Blanche.Shared.Products;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System.Diagnostics.Metrics;
using static MudBlazor.CategoryTypes;
using Dialog = Blanche.Client.Admin.Products.Components.Dialog;

namespace Blanche.Client.Admin.Products
{
    public partial class Index
    {
        [Inject] public IProductService ProductService { get; set; } = default!;
        [Inject] public IBeerService BeerService { get; set; } = default!;
        [Inject] public IFormulaService FormulaService { get; set; } = default!;
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public IStorageService StorageService { get; set; } = default!;
        [Inject] public ISidepanelService Sidepanel { get; set; } = default!;
        [Inject] public IDialogService DialogService { get; set; } = default!;

        public List<ProductDto>? Products = null!;
        public List<BeerDto>? Beers = null!;
        public List<FormulaDto.Index>? Formulas = null!;
        
        private bool dense = false;
        private bool hover = true;
        private bool striped = false;
        private bool bordered = false;

        private bool enabled { get; set; } = true;

        private bool isOpen;
        private ProductDto? selectedProduct;

        protected override async Task OnInitializedAsync()
        {
            await GetBeersAsync();
            await GetFormulasAsync();
            await GetProductsAsync();
        }

        private async Task GetBeersAsync()
        {
            Beers = (await BeerService.GetAllAsync()).ToList();
        }

        private async Task GetFormulasAsync() 
        {
            Formulas = (await FormulaService.GetIndexAsync()).ToList();
        }

        private async Task GetProductsAsync()
        {
            Products = (await ProductService.GetAllAsync()).ToList();
        }
         
        private void ShowCreateBeerForm()
        {
            
            var callback = EventCallback.Factory.Create(this, GetBeersAsync);
            var parameters = new Dictionary<string, object>
            {
              { nameof(Components.Beer.Create.OnBeerCreated), callback }
            };
            Sidepanel.Open<Components.Beer.Create>("Beer", "create", parameters);

        }

        private void ShowEditBeerForm(Guid id)
        {
             
            var callback = EventCallback.Factory.Create(this, GetBeersAsync);
            var parameters = new Dictionary<string, object>
            {
              { nameof(Components.Beer.Edit.BeerId), id },
              { nameof(Components.Beer.Edit.OnBeerEdited), callback }
            }; 

            Sidepanel.Open<Components.Beer.Edit>("Beer", "Edit", parameters);
        }

        private void ShowCreateFormulaForm()
        {
            var callback = EventCallback.Factory.Create(this, GetFormulasAsync);
            Sidepanel.Open<Components.Formula.Create>("Formula", "Create", ("OnFormulaCreated", callback));
        }

        private void ShowEditFormulaForm(Guid id)
        {
            var callback = EventCallback.Factory.Create(this, GetFormulasAsync);
            var parameters = new Dictionary<string, object>
            {
              { nameof(Components.Formula.Edit.FormulaId), id },
              {  "OnFormulaEdit", callback }
            };
            Sidepanel.Open<Components.Formula.Edit>("Formula", "Edit", parameters);
        }

        private void ShowCreateProductForm()
        {
             var callback = EventCallback.Factory.Create(this, GetProductsAsync);
            var parameters = new Dictionary<string, object>
            {
              { nameof(Components.Product.Create.OnProductCreated), callback }
            };
             Sidepanel.Open<Components.Product.Create>("Product", "Create", parameters);
        }

        private void ShowEditProductForm(Guid id)
        {
             var callback = EventCallback.Factory.Create(this, GetProductsAsync);
            var parameters = new Dictionary<string, object>
            {
              { nameof(Components.Product.Edit.ProductId), id },
              { nameof(Components.Product.Edit.OnProductEdited), callback }
            };
            Sidepanel.Open<Components.Product.Edit>("Product", "Edit", parameters);
        }

        public async Task DeleteProduct(Guid productId)
        {
            var callback = EventCallback.Factory.Create(this, GetProductsAsync);
            var result = await ConfirmDeleteDialog("Delete product",
                "Do you really want to delete this product? This process cannot be undone!", "Delete", Color.Warning);

            if (!result.Canceled)
            {
                await ProductService.DeleteAsync(productId);
               
                await callback.InvokeAsync();
            }
        }

        public async Task DeleteBeer(Guid BeerId)
        {
            var callback = EventCallback.Factory.Create(this, GetBeersAsync);
            var result = await ConfirmDeleteDialog("Delete beer",
                "Do you really want to delete this beer? This process cannot be undone!", "Delete", Color.Warning);

            if (!result.Canceled)
            {

                await BeerService.DeleteAsync(BeerId);
                await callback.InvokeAsync();
            }
        }

        public async Task DeleteFormula(Guid formulaId)
        {
            var callback = EventCallback.Factory.Create(this, GetFormulasAsync);
            var result = await ConfirmDeleteDialog("Delete formula",
                "Do you really want to delete this formula? This process cannot be undone!", "Delete", Color.Warning);

            if (!result.Canceled)
            {
                await FormulaService.DeleteAsync(formulaId);
                await callback.InvokeAsync();
            }
        }

        private async Task<DialogResult> ConfirmDeleteDialog(string title, string content, string buttonText, Color buttonColor)
        {
            StateHasChanged();
            var dialogOptions = new DialogOptions
            {
                CloseOnEscapeKey = true,
                CloseButton = true,
                DisableBackdropClick = true,
                Position = DialogPosition.Center,
                MaxWidth = MaxWidth.ExtraSmall
            };

            var parameters = new DialogParameters<Dialog>
            {
                { x => x.ContentText, content },
                { x => x.ButtonText, buttonText },
                { x => x.Color, buttonColor }
            };

            var dialog = await DialogService.ShowAsync<Dialog>(title, parameters, dialogOptions);
            return await dialog.Result;
        }
    }
}