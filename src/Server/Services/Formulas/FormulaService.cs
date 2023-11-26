using Blanche.Domain.Formulas;
using Blanche.Domain.Exceptions;
using Blanche.Shared.Formulas;
//using Blanche.Persistence; Deze map hebben wij precies niet staan. staat ergens anders zie hieronder
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
                Price = x.Price,
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
                .Where(x => x.Id == formulaId)
                .Select(x => new FormulaDto.Detail
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Name = x.Name,
                Description = x.Description,
                Days = x.Days,
                Price = x.Price,
                ImageUrl = x.ImageUrl,
                //HasDrinks = x.HasDrinks,
                //HasFood = x.HasFood
                }).SingleOrDefaultAsync(x => x.Id == formulaId);


            if (formula is null)
            {
                throw new EntityNotFoundException(nameof(Formula), formulaId);

            }
            return formula;
        }

        public async Task<Guid> CreateAsync(FormulaDto.Mutate model)
        {
            bool alreadyExists = await dbContext.Formulas.AnyAsync(x => x.Name == model.Name);
            if (alreadyExists)
            {
                throw new EntityAlreadyExistsException(nameof(Formula), nameof(model.Name), model.Name);
            }

            Formula formula = new Formula(
                model.Name!,
                model.Description!,
                model.Days ?? 0,
                model.Price ?? 0,
                model.ImageUrl!
                );

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
            existingFormula.Days = model.Days ?? 0;
            existingFormula.Price = model.Price ?? 0;
            existingFormula.ImageUrl = model.ImageUrl;

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


