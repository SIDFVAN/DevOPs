using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Blanche.Shared.Products
{
	public class ProductDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string ImageUrl { get; set; } = string.Empty;
        public double Price { get; set; }
		public int Quantity {  get; set; }
		public int MinimumUnits { get; set; }
		public bool IsAdded { get; set; } = false;

        [JsonPropertyName("DetailUrlImages")]
        public List<string> DetailUrlImages = new();
	

        public class ProductDtoBuilder
        {
            private readonly ProductDto product = new();

            public ProductDtoBuilder WithId(Guid id)
            {
                product.Id = id;
                return this;
            }

            public ProductDtoBuilder WithName(string name)
            {
                product.Name = name;
                return this;
            }

            public ProductDtoBuilder WithDescription(string description)
            {
                product.Description = description;
                return this;
            }

            public ProductDtoBuilder WithUrl(string url)
            {
                product.ImageUrl = url;
                return this;
            }

            public ProductDtoBuilder WithNumber(int number)
            {
                product.Quantity = number;
                return this;
            }

            public ProductDtoBuilder WithUnitsPerQuantity(int unitsPerQuantity)
            {
                product.MinimumUnits = unitsPerQuantity;
                return this;
            }

            public ProductDto Build()
            {
                return product;
            }
        }

        public static ProductDtoBuilder Builder()
        {
            return new ProductDtoBuilder();
        }
    }
}
