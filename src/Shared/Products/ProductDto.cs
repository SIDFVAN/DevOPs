using Blanche.Shared.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Blanche.Shared.Products
{
	public class ProductDto : EntityDto
	{
		public string Name { get; set; } = default!;
		public string Description { get; set; } = default!;
		public string ImageUrl { get; set; } = default!;
        public double Price { get; set; }
		public int QuantityInStock {  get; set; }
		public int MinimumUnits { get; set; }
        [NotMapped]
        public int QuantityOrdered { get; set; }
        [NotMapped]
        public bool IsAdded { get; set; } = false;
        [NotMapped]
        public string ImageContentType { get; set; } = string.Empty;

        [NotMapped]
        [JsonPropertyName("DetailUrlImages")]
        public List<string> DetailUrlImages = new();
	 
        public ProductDto() { }
    }
}
