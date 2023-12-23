using Blanche.Domain.Exceptions;
using Blanche.Domain.Files;
using Blanche.Domain.Products;
using Blanche.Shared.Products;
using Microsoft.EntityFrameworkCore;

namespace Blanche.Server.Services.Products
{
    public class BeerService: IBeerService
    {
        private readonly BlancheDbContext dbContext;

        public BeerService(BlancheDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Guid> CreateAsync(BeerDto beerDto)
        {
            if (await dbContext.Beers.AnyAsync(x => x.Name == beerDto.Name))
                throw new EntityAlreadyExistsException(nameof(Beer), nameof(Beer.Name), beerDto.Name);
            
            Beer beer = new()
            {
                Name = beerDto.Name!,
                Description = beerDto.Description!,
                Price = beerDto.Price,
            };

            dbContext.Beers.Add(beer);
            await dbContext.SaveChangesAsync();

            return beer.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            Beer? beer = await dbContext.Beers.SingleOrDefaultAsync(x => x.Id == id);

            if (beer is null)
                throw new EntityNotFoundException(nameof(Beer), id);

            dbContext.Beers.Remove(beer);

            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BeerDto>> GetAllAsync()
        {
            List<BeerDto> beers = await dbContext.Beers.Select(x => new BeerDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
            }).ToListAsync();
            return beers;
        }

        public async Task<BeerDto> GetByIdAsync(Guid id)
        {
            var beer = await dbContext.Beers
                .Select(x => new BeerDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price
                }).SingleOrDefaultAsync(x => x.Id == id);

            return beer is null ? throw new EntityNotFoundException(nameof(Beer), id) : beer;
        }

        public async Task UpdateAsync(BeerDto beerDto)
        {
            Beer? existingBeer = await dbContext.Beers.SingleOrDefaultAsync(x => x.Id == beerDto.Id);

            if (existingBeer is null)
                throw new EntityNotFoundException(nameof(Beer), beerDto.Id);

            existingBeer.Name = beerDto.Name;
            existingBeer.Description = beerDto.Description;
            existingBeer.Price = beerDto.Price;

            await dbContext.SaveChangesAsync();
        }
    }
}
