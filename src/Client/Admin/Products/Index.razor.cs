using Append.Blazor.Sidepanel;
using Blanche.Client.Admin.Products.Components;
using Blanche.Client.Files;
using Blanche.Shared.Formulas;
using Blanche.Shared.Products;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blanche.Client.Admin.Products
{
    public partial class Index
    {
        private readonly List<ProductDto> Products = new(){
            new ProductDto {Id = Guid.NewGuid(), Description = "Voor de sfeer en gezelligheid", MinimumUnits=1, Name="Vuurschaal", QuantityInStock = 5, ImageUrl="/images/products/img_vuurschaal.jpg" },
            new ProductDto {Id = Guid.NewGuid(), Description = "Om een lekker stukje vlees of vis te bakken", MinimumUnits=1, Name="Barbecue", QuantityInStock = 5,ImageUrl ="/images/products/img_barbecue.jpg" },
            new ProductDto {Id = Guid.NewGuid(), Description = "Handige en stevige tafels voor al uw feesten", MinimumUnits=1, Name="Tafel", QuantityInStock = 5, ImageUrl="/images/products/img_tafel.jpg" },
            new ProductDto {Id = Guid.NewGuid(), Description = "Voor extra sfeer en gezelligheid 's avonds", MinimumUnits=1, Name="Verlichting", QuantityInStock = 5, ImageUrl="/images/products/img_verlichting.jpg" },
            new ProductDto {Id = Guid.NewGuid(), Description = "Niet genoeg glazen, we hebben er!", MinimumUnits=6, Name="Glazen tripel", QuantityInStock = 36, ImageUrl="/images/products/img_glazen.jpg" },
        };

        private readonly List<BeerDto> Beers = new()
        {
            new BeerDto { Id = Guid.NewGuid(),Name = "Pils", Description = "Lekker biertje", Price = 1.5},
            new BeerDto { Id = Guid.NewGuid(),Name = "Trappist", Description = "Lekker biertje", Price = 2.5},
        };

        private readonly List<FormulaDto.Index> Formulas = new()
        {
            new FormulaDto.Index { Id = Guid.NewGuid(), Name = "Basic", Description = "Just the beervan", ImageUrl = "", Price = 150},
            new FormulaDto.Index { Id = Guid.NewGuid(), Name = "Advanced", Description = "The beervan and beer", ImageUrl = "", Price = 150},
            new FormulaDto.Index { Id = Guid.NewGuid(), Name = "Deluxe", Description = "The beervan, beer and burgers", ImageUrl = "", Price = 150}
        };

        [Inject] public IProductService ProductService { get; set; } = default!;
        [Inject] public IBeerService BeerService { get; set; } = default!;
        [Inject] public IFormulaService FormulaService { get; set; } = default!;
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public IStorageService StorageService { get; set; } = default!;
        [Inject] public ISidepanelService Sidepanel { get; set; } = default!;
        [Inject] public IDialogService DialogService { get; set; } = default!;
        //public IEnumerable<ProductDto>? Products = null!;
        //public IEnumerable<BeerDto>? Beers = null!;
        //public IEnumerable<FormulaDto>? Formulas = null!;

        private bool dense = false;
        private bool hover = true;
        private bool striped = false;
        private bool bordered = false;

        private bool enabled { get; set; } = true;

        private bool isOpen;
        private ProductDto? selectedProduct;

        /*protected override async Task OnInitializedAsync()
        {
            Products = await productService.GetAllAsync();
            Beers = await beerService.GetAllAsync();
            Formulas = await formulaService.GetAsync();
        }*/

        private void ShowCreateBeerForm()
        {
            Sidepanel.Open<Components.Beer.Create>("Beer", "Create");
        }

        private void ShowEditBeerForm(Guid id)
        {
            Sidepanel.Open<Components.Beer.Edit>("Beer", "Edit", (nameof(Components.Beer.Edit.BeerId), id));
        }

        private void ShowCreateFormulaForm()
        {
            Sidepanel.Open<Components.Formula.Create>("Formula", "Create");
        }

        private void ShowEditFormulaForm(Guid id)
        {
            Sidepanel.Open<Components.Formula.Edit>("Formula", "Edit", (nameof(Components.Formula.Edit.FormulaId), id));
        }

        private void ShowCreateProductForm()
        {
            Sidepanel.Open<Components.Product.Create>("Product", "Create");
        }

        private void ShowEditProductForm(Guid id)
        {
            Sidepanel.Open<Components.Product.Edit>("Product", "Edit", (nameof(Components.Product.Edit.ProductId), id));
        }

        public static void AddProduct()
        {

        }

        public async Task DeleteProduct(Guid productId)
        {
            var result = await ConfirmDeleteDialog("Delete product",
                "Do you really want to delete this product? This process cannot be undone!", "Delete", Color.Warning);

            if (!result.Canceled)
            {
                await ProductService.DeleteAsync(productId);
                NavigationManager.NavigateTo("admin/products");
            }
        }

        public async Task DeleteBeer(Guid BeerId)
        {
            var result = await ConfirmDeleteDialog("Delete beer",
                "Do you really want to delete this beer? This process cannot be undone!", "Delete", Color.Warning);

            if (!result.Canceled)
            {
                await BeerService.DeleteAsync(BeerId);
                NavigationManager.NavigateTo("admin/products");
            }
        }

        public async Task DeleteFormula(Guid formulaId)
        {
            var result = await ConfirmDeleteDialog("Delete formula",
                "Do you really want to delete this formula? This process cannot be undone!", "Delete", Color.Warning);

            if (!result.Canceled)
            {
                await FormulaService.DeleteAsync(formulaId);
                NavigationManager.NavigateTo("admin/products");
            }
        }

        private async Task<DialogResult> ConfirmDeleteDialog(string title, string content, string buttonText, Color buttonColor)
        {
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