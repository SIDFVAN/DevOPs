using Blanche.Domain.Formulas;
using Blanche.Domain.Exceptions;
using Blanche.Shared.Formulas;
using Microsoft.EntityFrameworkCore;

namespace Blanche.Server.Services.Formulas
{
    public class FormulaService : IFormulaService
    {

        private readonly BlancheDbContext dbContext;

        public FormulaService(BlancheDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<FormulaDto.Index>> GetIndexAsync()
        {
            var formulas = await dbContext.Formulas
                .Select(x => new FormulaDto.Index
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price ?? 0.0,
                ImageUrl = x.ImageUrl,
                HasDrinks = x.HasDrinks,
                HasFood = x.HasFood,
                PricePerDays = x.PricePerDays,
                PricePerExtraDay = x.PricePerExtraDay
            }).ToListAsync();

            return formulas;
        }

        public async Task<FormulaDto.Detail> GetDetailAsync(Guid formulaId)
        {
            var formula = await dbContext.Formulas
                //.Where(x => x.Id == formulaId)
                .Select(x => new FormulaDto.Detail
                {
                    Id = x.Id,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    Name = x.Name,
                    Description = x.Description, 
                    Price = x.Price,
                    ImageUrl = x.ImageUrl,
                    HasDrinks = x.HasDrinks,
                    HasFood = x.HasFood,
                    PricePerDays = x.PricePerDays,
                    PricePerExtraDay = x.PricePerExtraDay
                }).SingleOrDefaultAsync(x => x.Id == formulaId);

            return formula is null ? throw new EntityNotFoundException(nameof(Formula), formulaId) : formula;
        }

        public async Task<Guid> CreateAsync(FormulaDto.Mutate model)
        {
            bool alreadyExists = await dbContext.Formulas.AnyAsync(x => x.Name == model.Name);
            if (alreadyExists)
            {
                throw new EntityAlreadyExistsException(nameof(Formula), nameof(model.Name), model.Name);
            }

            Formula formula = new(
                model.Name!,
                model.Description!, 
                model.Price ?? 0,
                model.ImageUrl!
                )
            {
                HasDrinks = model.HasDrinks!,
                HasFood = model.HasFood,
                PricePerDays = model.PricePerDays,
                PricePerExtraDay = model.PricePerExtraDay 
            };

            dbContext.Formulas.Add(formula);
            await dbContext.SaveChangesAsync();

            return formula.Id;

        }

        public async Task EditAsync(Guid formulaId, FormulaDto.Mutate model)
        {
            Formula? existingFormula = await dbContext.Formulas.SingleOrDefaultAsync(x => x.Id == formulaId);


            if (existingFormula is null)
            {
                throw new EntityNotFoundException(nameof(Formula), formulaId);
            }

            existingFormula.Name = model.Name!;
            existingFormula.Description = model.Description!;  
            existingFormula.ImageUrl = model.ImageUrl;
            existingFormula.HasDrinks = model.HasDrinks;
            existingFormula.HasFood = model.HasFood;
            existingFormula.PricePerDays = model.PricePerDays;
            existingFormula.PricePerExtraDay = model.PricePerExtraDay;

            await dbContext.SaveChangesAsync();
        }


        public async Task<Guid> DeleteAsync(Guid formulaId)
        {
            Formula? formula = await dbContext.Formulas.SingleOrDefaultAsync(x => x.Id == formulaId);

            if(formula is null)
            {
                throw new EntityNotFoundException(nameof(Formula), formulaId);
            }

            dbContext.Formulas.Remove(formula);
            await dbContext.SaveChangesAsync();

            return formulaId;
        }
    }
}


