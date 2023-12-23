using Blanche.Shared.Products;

namespace Blanche.Shared.Reservations
{
    public class OrderlineDTO
    {
        public int Quantity { get; set; }
        public ProductDto Product { get; set; } = new ProductDto();

        public OrderlineDTO(int quantity, ProductDto product)
        {
            Quantity = quantity;
            this.Product = product;
        }

        public double GetPrice()
        {
            return Quantity * Product.MinimumUnits;
        }
    }
}