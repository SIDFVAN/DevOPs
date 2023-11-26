using Blanche.Domain.Formulas;
using Blanche.Domain.Reservations;
using Blanche.Shared.Formulas;
using Blanche.Shared.Reservations;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blanche.Mappers.Formulas
{
    [Mapper]
    public static partial class FormulaMapper
    {
        public static partial FormulaDto.Index FormulaToFormulaDto(Formula formula);

        public static partial Formula FormulaDtoToFormula(FormulaDto.Mutate formulaDto);
    }
}
