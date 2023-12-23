using Blanche.Domain.Products;

namespace Blanche.Server.Seeding
{
    public static class ProductsSeeder
    {
        public static List<Product> Seed()
        {
            List<Product> products = new()
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Description = "Inclusief 5 GN bakken ¼ + 5 deksels, diepte 150 mm",
                    MinimumUnits = 1,
                    Name = "Saladette (34cm (l) x 120cm (b) x 28cm (h)",
                    QuantityInStock = 2,
                    Price = 65,
                    ImageUrl = "/images/products/Saladette.jpg"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Description = "",
                    MinimumUnits = 1,
                    Name = "GN bakken¼ + 5 deksels, diepte 150 mm",
                    QuantityInStock = 20,
                    Price = 3,
                    ImageUrl = "/images/products/GNBakken1_4.jpg"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Description = "",
                    MinimumUnits = 1,
                    Name = "GN bakken¼ + 5 deksels, diepte 150 mm",
                    QuantityInStock = 10,
                    Price = 3,
                    ImageUrl = "/images/products/GNBakken1_6.jpg"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Description = "3x glazen schuifdeur - zwart (50cm (l) x 135cm (b) x 87cm (h)",
                    MinimumUnits = 1,
                    Name = "Barkoeler 320 liter",
                    QuantityInStock = 1,
                    Price = 65,
                    ImageUrl = "/images/products/Barkoeler.jpg"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Description = "",
                    MinimumUnits = 1,
                    Name = "Cocktail glas gouden rand 330ml",
                    QuantityInStock = 100,
                    Price = 0.5,
                    ImageUrl = "/images/products/CocktailGlasGoud.jpg"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Description = "",
                    MinimumUnits = 1,
                    Name = "Cocktail glas type rand 330ml",
                    QuantityInStock = 100,
                    Price = 0.2,
                    ImageUrl = "/images/products/CocktailGlasType.jpg"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Description = "",
                    MinimumUnits = 1,
                    Name = "Cocktail glas gouden rand 330ml",
                    QuantityInStock = 100,
                    Price = 0.15,
                    ImageUrl = "/images/products/CocktailGlazen.jpg"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Description = "Guirlandes 10m",
                    MinimumUnits = 1,
                    Name = "Lichtslinger",
                    QuantityInStock = 4,
                    Price = 15,
                    ImageUrl = "/images/products/Lichtslinger.jpg"
                },
            };

            return products;
        }
    }
}

