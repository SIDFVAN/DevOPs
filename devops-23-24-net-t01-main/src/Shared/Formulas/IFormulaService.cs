using Blanche.Shared.Formulas;

namespace Blanche.Shared.Formulas
{
    public interface IFormulaService
    {
        Task<List<FormulaDto.Index>> GetIndexAsync();
        Task<FormulaDto.Detail> GetDetailAsync(Guid formulaId); 
        Task<Guid> CreateAsync(FormulaDto.Mutate model);
        Task EditAsync(Guid formulaId, FormulaDto.Mutate model);
        Task<Guid> DeleteAsync(Guid formulaId);

    }
}

