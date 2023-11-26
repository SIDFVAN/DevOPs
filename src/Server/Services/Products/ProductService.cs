using System.Linq;
using System.Reflection;
using Blanche.Server.Services.Files;
using Blanche.Domain.Exceptions;
using Blanche.Domain.Files;
using Blanche.Domain.Formulas;
using Blanche.Domain.Products;
using Blanche.Shared.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Policy;
using Blanche.Mappers.Products;
using Blanche.Server.Persistence;

namespace Blanche.Server.Services.Products;

public class ProductService : IProductService
{
    private readonly BlancheDbContext dbContext;
    protected readonly IUnitOfWork _unitOfWork;
    private readonly IStorageService storageService;

    public ProductService(IUnitOfWork unitOfWork, BlancheDbContext dbContext, IStorageService storageService)
    {
        this.dbContext = dbContext;
        _unitOfWork = unitOfWork;
        this.storageService = storageService;
    }

    public async Task<ProductResult.Saved?> CreateAsync(ProductDto productDto)
    {
        if (await dbContext.Products.AnyAsync(x => x.Name == productDto.Name))
            throw new EntityAlreadyExistsException(nameof(Product), nameof(Product.Name), productDto.Name);

        ImageFile image = new(storageService.BasePath, productDto.ImageContentType!);

        Product product = new()
        {
            Name = productDto.Name!,
            Description = productDto.Description!,
            Price = productDto.Price,
            QuantityInStock = productDto.QuantityInStock,
            MinimumUnits = productDto.MinimumUnits,
            ImageUrl = image.FileUri.ToString()
        };
        
        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();

        Uri uploadSas = storageService.GenerateImageUploadSas(image);

        ProductResult.Saved result = new()
        {
            ProductId = product.Id,
            UploadUri = uploadSas.ToString()
        };

        return result;
    }

    public async Task DeleteAsync(Guid productId)
    {
        Product? product = await dbContext.Products.SingleOrDefaultAsync(x => x.Id == productId);

        if (product is null)
            throw new EntityNotFoundException(nameof(Product), productId);

        dbContext.Products.Remove(product);

        await dbContext.SaveChangesAsync();
    }

    public async Task<ProductResult.Saved?> EditAsync(ProductDto productDto)
    {
        Product? existingProduct = await dbContext.Products.SingleOrDefaultAsync(x => x.Id == productDto.Id);

        if (existingProduct is null)
            throw new EntityNotFoundException(nameof(Product), productDto.Id);

        ImageFile image;
        string url = default!;

        if (!productDto.ImageContentType.IsNullOrEmpty()) 
        { 
            image = new(storageService.BasePath, productDto.ImageContentType!);
            existingProduct.ImageUrl = image.FileUri.ToString();
            Uri uploadSas = storageService.GenerateImageUploadSas(image);
            url = uploadSas.ToString();
        }

        existingProduct.Name = productDto.Name;
        existingProduct.Description = productDto.Description;
        existingProduct.Price = productDto.Price;
        existingProduct.QuantityInStock = productDto.QuantityInStock;
        existingProduct.MinimumUnits = productDto.MinimumUnits;

        await dbContext.SaveChangesAsync();

        ProductResult.Saved result = new()
        {
            ProductId = productDto.Id,
            UploadUri = url
        };

        return result;
    }

    public async Task<ProductDto> EditQuantityInStockAsync(ProductDto productDto)
    {
        var product = ProductMapper.ToProduct(productDto);

        var productToUpdate = await _unitOfWork.Products.GetAsync(p => p.Id == productDto.Id);

        productToUpdate.QuantityInStock = product.QuantityInStock;

        _unitOfWork.Products.Update(productToUpdate);
        await _unitOfWork.CommitAsync();

        return ProductMapper.ToProductDto(productToUpdate);
    }

    public async Task<IEnumerable<ProductDto>?> GetAllAsync()
    {
        List<ProductDto> products = await dbContext.Products.Select(x => new ProductDto
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            ImageUrl = x.ImageUrl,
            QuantityInStock = x.QuantityInStock,
            MinimumUnits = x.MinimumUnits,
            Price = x.Price
        }).ToListAsync();
        return products;
    }

    public async Task<ProductDto> GetByIdAsync(Guid productId)
    {
        var query = dbContext.Products.AsQueryable();

        var product = await dbContext.Products
                .Where(x => x.Id == productId)
                .Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    QuantityInStock = x.QuantityInStock,
                    MinimumUnits = x.MinimumUnits
                }).SingleOrDefaultAsync(x => x.Id == productId);

        return product is null ? throw new EntityNotFoundException(nameof(Product), productId) : product;
    }
}
