using Blanche.Domain.Formulas;
using Blanche.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Blanche.Server.Seeding
{
	public static class SeedHelper
	{
		public static void SeedData(BlancheDbContext context)
		{
            context.Database.Migrate();

            bool requiresSeeding =
                !context.Products.Any() ||
                !context.Formulas.Any() ||
                !context.Beers.Any();

            if (!requiresSeeding)
            {
                return;
            }

            List<Product> products = ProductsSeeder.Seed();
            if (!context.Products.Any())
            {
                context.AddRange(products);
                    
            }

            List<Formula> formulas = FormulasSeeder.Seed();
            if (!context.Formulas.Any())
            {
                context.AddRange(formulas);
            }

            List<Beer> beers = BeerSeeder.Seed();
            if (!context.Beers.Any())
            {
                context.AddRange(beers);
            }

            context.SaveChanges();
		}
	}
}

