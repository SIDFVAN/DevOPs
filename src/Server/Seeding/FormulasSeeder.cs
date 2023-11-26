using Blanche.Domain.Formulas;

namespace Blanche.Server.Seeding
{
	public static class FormulasSeeder
	{
		public static List<Formula> Seed()
		{
            List<Formula> formulas = new()
            {
                new Formula
                {
                    Id = Guid.NewGuid(),
                    Name = "Only Blanche",
                    Description = "Enkel foodtruck",
                    Price = 350,
                    Days = 1,
                    PricePerDays = new Dictionary<int, double>
                    {
                        { 1, 350.00 },
                        { 2, 450.00 },
                        { 3, 520.00 }
                    },
                    PricePerExtraDay = 50.00,
                    ImageUrl = ""
				},
                new Formula
                {
                    Id = Guid.NewGuid(),
                    Name = "Blanche Deluxe",
                    Description = "Foodtruck en vat(en) bier",
                    Price = 350,
                    Days = 1,
                    PricePerDays = new Dictionary<int, double>
                    {
                        { 1, 350.00 },
                        { 2, 450.00 },
                        { 3, 520.00 }
                    },
                    PricePerExtraDay = 50.00,
                    HasDrinks = true,
                    ImageUrl = ""
                },
                new Formula
                {
                    Id = Guid.NewGuid(),
                    Name = "Blanche Royale",
                    Description = "Foodtruck, vat(en) bier en barbecue",
                    Price = 350,
                    Days = 1,
                    PricePerDays = new Dictionary<int, double>
                    {
                        { 1, 350.00 },
                        { 2, 450.00 },
                        { 3, 520.00 }
                    },
                    PricePerExtraDay = 50.00,
                    HasDrinks = true,
                    HasFood = true,
                    ImageUrl = ""
                }
            };

			return formulas;
		}
	}
}

