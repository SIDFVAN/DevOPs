using Blanche.Domain.Invoices;
using tests.Fakers.Common;

namespace tests.Fakers.Invoices
{
	public class InvoiceFaker : EntityFaker<Invoice>
	{
		public InvoiceFaker(string locale = "nl") : base(locale)
		{
			CustomInstantiator(f => new Invoice(
				f.Random.String(),
                f.Date.Recent(0, DateTime.Today),
                f.Date.Soon(2, DateTime.Today),
				f.Random.Bool(),
				f.Random.Bool(),
				f.Random.String()
            ));
		}
	}
}

