using Blanche.Domain.Products;

namespace Blanche.Server.Seeding
{
    public static class BeerSeeder
    {
        public static List<Beer> Seed()
        {
            List<Beer> beers = new()
            {
                new Beer
                {
                    Id = Guid.NewGuid(),
                    Name = "Pils",
                    Description = "Een licht biertje om de dorst te lessen.",
                    Price = 1.5
                },
                new Beer
                {
                    Id = Guid.NewGuid(),
                    Name = "Tripel",
                    Description = "Een zwaarder biertje met een volle smaak.",
                    Price = 3
                },
            };

            return beers;
        }
    }
}

