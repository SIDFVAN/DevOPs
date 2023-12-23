using Blanche.Domain.Formulas;
using tests.Fakers.Common;

namespace tests.Fakers.Formulas
{
	public class FormulaFaker : EntityFaker<Formula>
	{
		public FormulaFaker(string locale = "nl") : base(locale)
        {
			CustomInstantiator(f => new Formula(
				f.Commerce.ProductName(),
				f.Lorem.Paragraph(),
 				f.Random.Double(0,250),
				f.Lorem.Lines()
			));
		}
	}
}

