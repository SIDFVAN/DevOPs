using System;
namespace Blanche.Shared.Formulas;

public abstract class FormulaResult
{
  public class Index
  {
    public IEnumerable<FormulaDto.Index>? Formulas { get; set; }
    public int TotalAmount { get; set; }
  }

}
